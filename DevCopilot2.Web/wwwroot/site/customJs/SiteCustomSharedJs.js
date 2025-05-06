$(document).ready(async function (e) {
    await ShowAdminDropDown();
    function highlightThumb() {
        var sliderId = this.$el.attr("id");
        var thumbs = $('.swiper-thumbs[data-swiper="#' + sliderId + '"]');
        // if thumbs for this slider exist
        if (thumbs.length > 0) {
            thumbs.find(".swiper-thumb-item.active").removeClass("active");
            thumbs
                .find(".swiper-thumb-item")
                .eq(this.realIndex)
                .addClass("active");
        }
    }

    $(document).on('click', '.quick-view-btn', async function (e) {
        var value = $(this).attr("data-value");
        open_waiting('#quickview-modal-container');
        try {
            const response = await $.ajax({
                type: 'GET',
                url: `/proudcts/detail/partial/${value}`
            });
            if (response.status === 'danger') {
                ShowErrorMessage("مشکلی در نمایش محصول پیش آمد!.");
                close_waiting('#quickview-modal-container');
                return;
            }
            close_waiting('#quickview-modal-container');
            
            $('#quickview-modal-container').html(response);
            quickViewSlider = new Swiper("#quickViewSlider", {
                mode: "horizontal",
                loop: true,
                on: {
                    slideChangeTransitionStart: highlightThumb,
                },
            });
            let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
            await UpdateBasketProductsBtns(basketProducts);
            let favoriteProducts = JSON.parse(localStorage.getItem('favoriteProducts')) || [];
            await UpdateFavoriteProductsBtns(favoriteProducts);
            $(document).on("click", ".swiper-thumb-item", function (e) {
                e.preventDefault();
                var swiperId = $(this).parents(".swiper-thumbs").data("swiper");
                $(swiperId)[0].swiper.slideToLoop($(this).index());
            });
            $('.selectpicker').selectpicker('refresh');
            AddSelectDefaultData();
        } catch (error) {
            console.error('Error getting product information:', error);
            close_waiting('#quickview-modal-container');
            return null;
        }
    });
    //start basket and favorites
    let favoriteProducts = JSON.parse(localStorage.getItem('favoriteProducts')) || [];
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    await UpdateBasket(basketProducts);
    await UpdateFavoriteProductsBtns(favoriteProducts);
    await UpdateBasketProductsBtns(basketProducts);
    $(document).on('submit', '#remove-out-of-stock-order-details-form', async function (e) {
        e.preventDefault();
        await GetFormResult("remove-out-of-stock-order-details-form");
    });
    await RemoveOutOfStockOrderDetails();

    $(document).on('click', '.update-basket-btn', async function (e) {
        await UpdateBasket(basketProducts);
        await UpdateBasketProductsBtns(basketProducts);
        ShowSuccessMessage('سبد خرید شما بروز است');
    });

    $(document).on('click', '.favorite-btn', function () {
        const $btn = $(this);
        const value = $btn.data('value');

        if (!$btn.hasClass('checked')) {
            // Add to favorites
            favoriteProducts.push(value);
        } else {
            // Remove from favorites
            favoriteProducts = favoriteProducts.filter(item => item !== value);
        }
        $('.favorite-btn[data-value="' + value + '"]').toggleClass('checked');
        // Save updated favoriteProducts array to local storage
        localStorage.setItem('favoriteProducts', JSON.stringify(favoriteProducts));
        UpdateFavoriteProductsBtns(favoriteProducts);
    });

    $(document).on('click', '.add-to-cart-btn', async function () {
        var productId = $(this).data('product-id');
        open_waiting((`.single-product-item[data-value=${productId}]`));
        const $btn = $(this);
        await AddToCartBtnClicked($btn);
        close_waiting((`.single-product-item[data-value=${productId}]`));
    });

    $(document).on('change', '.size-select', function (e) {
        HandleSizeSelectedChange($(this));
    });

    ShowActivePhoneNumberOtp();

    $(document).on('submit', '#register-login-form', async function (e) {
        e.preventDefault();
        $("#submit-phone-number-btn").addClass('disabled');
        var phoneNumber = $("#register-login-phonenumber-input").val();
        if (phoneNumber == null || phoneNumber.length < 11) {
            ShowErrorMessage('شماره تلفن وارد شده معتبر نمی باشد');
            $("#submit-phone-number-btn").removeClass('disabled');
            return;
        }
        var expireDate = GetPhoneNumberOtpExpireDate(phoneNumber);
        var currentDateTime = new Date();
        if (expireDate == null || expireDate < currentDateTime) {

            var result = await GetFormResult('register-login-form');
            if (result === null) {
                ShowErrorMessage('مشکلی در انجام عملیات پیش آمد');
                $("#submit-phone-number-btn").removeClass('disabled');
                return;
            }
            if (result.status === "Danger") {
                ShowErrorMessage(result.message);
                $("#submit-phone-number-btn").removeClass('disabled');
                return;
            }
            ShowSuccessMessage('کد ورود برای شما ارسال شد');
            HandleOtpCodeSent(phoneNumber);
            $("#submit-phone-number-btn").removeClass('disabled');
        }
        else {
            ShowInfoMessage('کد تایید از قبل برای شما ارسال شده است');
            $("#submit-phone-number-btn").removeClass('disabled');
        }
        $('.phonenumber-input').val(phoneNumber);
        $('#register-login-form').addClass('d-none');
        $('#login-form').removeClass('d-none');
        $("#submit-phone-number-btn").removeClass('disabled');
    });

    var previousPhoneNumberOtpCode = '';
    $(document).on('keyup', '#phonenumber-otp-code-input', function (e) {
        var value = $(this).val();
        var length = value.length;
        if (length === 4) {
            $('#login-form').submit();
        }
    });

    $(document).on('keyup', '#register-login-phonenumber-input', function (e) {
        var value = $(this).val();
        var length = value.length;
        if (length === 11) {
            $('#register-login-form').submit();
        }
    });

    $(document).on('submit', '#login-form', async function (e) {
        e.preventDefault();
        var otpCode = $('#phonenumber-otp-code-input').val();
        if (otpCode === previousPhoneNumberOtpCode) {
            ShowErrorMessage('کد وارد شده اشتباه است ');
            return;
        }
        previousPhoneNumberOtpCode = otpCode;
        var response = await GetFormResult('login-form');
        ShowMessageByResponse(response);
        if (response.status.toLowerCase() === 'success') {
            var redirectUrl = $('#redirect-url-input').val();
            var phoneNumber = $("#register-login-phonenumber-input").val();
            RemovePhoneNumberFromList(phoneNumber);
            await AddLocalBasketItemsToServer(basketProducts);
            window.location.href = redirectUrl;
            return;
        }
    });

    $(document).on('input', '.phonenumber-input', function () {
        if ($(this).val().length > 11) {
            $(this).val($(this).val().substring(0, 11));
        }
    });

    $(document).on('input', '#phonenumber-otp-code-input', function () {
        if ($(this).val().length > 4) {
            $(this).val($(this).val().substring(0, 4));
        }
    });

    $(document).on('click', '.authorize-required', function (e) {
        if (!IsUserAuthenticated()) {
            e.preventDefault();
            var href = $(this).attr('href');
            $('#redirect-url-input').val(href);
            $('.modal').modal('hide');
            $('#loginModal').modal('show');
            ShowInfoMessage('برای دیدن این صفحه لطفا به سایت ورود کنید');
        }
    });

    $(document).on('click', '#edit-phonenumber-btn', function (e) {
        $('#phonenumber-otp-code-input').val('');
        $('#register-login-form').removeClass('d-none');
        $('#login-form').addClass('d-none');
    });

    $(document).on('click', '#resend-code-btn', function (e) {
        if (!$(this).hasClass('disabled')) {
            $(this).addClass('disabled');
            $('#resend-otp-code-form').submit();
        }
    });

    $(document).on('submit', '#resend-otp-code-form', async function (e) {
        e.preventDefault();
        $("#resend-code-btn").addClass('disabled');
        var response = await GetFormResult('resend-otp-code-form');
        ShowMessageByResponse(response);
        if (response.status.toLowerCase() === "success") {
            var phoneNumber = $('#register-login-phonenumber-input').val();
            HandleOtpCodeSent(phoneNumber);
        }
        $("#resend-code-btn").removeClass('disabled');
    });

});
$(window).on('load', function () {
    $('.defer-stylesheet').each(function () {
        var src = $(this).data('src');
        AppendStyleSheet(src);
    });
});
async function RemoveOutOfStockOrderDetails() {
    if (IsUserAuthenticated())
        if (+$("#remove-out-of-stock-order-details").val() === +1)
            $('#remove-out-of-stock-order-details-form').submit();
}

async function ShowAdminDropDown() {
    var response = await Get("/User/IsAdmin");
    if (response.data === true) {
        var dropDownHtml =
            `<div class="dropdown ps-3 ms-0">
                            <a class="topbar-link"
                               href="/admin"
                               title="پنل ادمین">
                               پنل ادمین
                            </a>
            </div>`;
        $(dropDownHtml).insertBefore("#logout-nav-item")
    }
}

function ShowActivePhoneNumberOtp() {
    var firstValidItem = GetFirstValidPhoneNumberOtpCode();
    if (firstValidItem != null) {
        $('.phonenumber-input').val(firstValidItem.phoneNumber);
        $('#register-login-form').addClass('d-none');
        $('#login-form').removeClass('d-none');
        StartResendOtpTimer(new Date(firstValidItem.sentDateTime));
    }
}
function HandleOtpCodeSent(phoneNumber) {
    UpsertOtpCode(phoneNumber);
}

function GetPhoneNumberOtpExpireDate(phoneNumber) {
    var list = getList('otpCodeList');
    var item = list.filter(a => a.phoneNumber == phoneNumber)[0];
    if (!item) return null;
    var sentDateTime = new Date(item.sentDateTime);
    return sentDateTime;
}

function GetFirstValidPhoneNumberOtpCode() {
    var list = getList('otpCodeList');
    var currentDateTime = new Date();
    var result = null
    list.forEach(function (item) {
        var expireDateTime = new Date(item.sentDateTime);
        if (expireDateTime > currentDateTime) {
            result = item;
            return item;
        }
    });
    return result;
}

function UpsertOtpCode(phoneNumber) {
    var list = getList('otpCodeList');
    var currentDateTime = new Date().toISOString();
    currentDateTime = new Date(currentDateTime);
    currentDateTime.setMinutes(currentDateTime.getMinutes() + 2);

    var found = false;

    list.forEach(function (item) {
        if (item.phoneNumber === phoneNumber) {
            item.sentDateTime = currentDateTime;
            found = true;
        }
    });

    if (!found) {
        list.push({ phoneNumber: phoneNumber, sentDateTime: currentDateTime });
    }

    StartResendOtpTimer(new Date(currentDateTime));
    saveList(list, 'otpCodeList');
}
var timerInterval; // Declare the variable outside the function

function StartResendOtpTimer(stopDateTime) {
    if (timerInterval) {
        clearInterval(timerInterval); // Clear any existing interval
    }

    timerInterval = setInterval(function () {
        var currentDateTime = new Date();
        var timeRemaining = stopDateTime.getTime() - currentDateTime.getTime();
        if (timeRemaining <= 0) {
            clearInterval(timerInterval);
            $('#resend-code-btn span').text('ارسال مجدد کد');
            $('#resend-code-btn').removeAttr('disabled');
            $('#resend-code-btn').removeClass('disabled');
        } else {
            var minutes = Math.floor(timeRemaining / 60000);
            var seconds = Math.floor((timeRemaining % 60000) / 1000);
            $('#resend-code-btn').attr('disabled', '');
            $('#resend-code-btn').addClass('disabled');
            var remainingTime = (minutes < 10 ? '0' : '') + minutes + ':' + (seconds < 10 ? '0' : '') + seconds;
            $('#resend-code-btn span').text(
                `تا ارسال مجدد کد ${remainingTime}`
            );
        }
    }, 1000);
}


function RemovePhoneNumberFromList(phoneNumber) {
    var list = getList('otpCodeList');
    list = list.filter(a => a.phoneNumber != phoneNumber);
    saveList(list, 'otpCodeList');
}

//start cart section

async function AddToCartBtnClicked($btn) {
    basketProducts = await UpsertToBasket($btn);
    localStorage.setItem('basketProducts', JSON.stringify(basketProducts));
    await UpdateBasketProductsBtns(basketProducts);
    await UpdateBasket(basketProducts);
}
function HandleSizeSelectedChange(element) {
    var productId = $(element).attr('data-product-id');
    var value = $(element).val();
    $('.size-select[data-product-id="' + productId + '"]').val(value);
    $('.add-to-cart-btn[data-product-id="' + productId + '"]:not(.minimal)').addClass('d-real-none');
    $('.add-to-cart-btn[data-value="' + value + '"]').removeClass('d-real-none');

    var selectedValue = $(element).val();
    var matchingOption = $(element).find("option").filter(function () {
        return $(this).val() === selectedValue;
    });
    ShowSingleSizePrices(matchingOption);
}

function ShowSingleSizePrices(element) {
    var price = $(element).data('price');
    if (!price) return;
    var discount = $(element).data('discount');
    var quantity = $(element).data('quantity-remaining');
    var finalPrice = price - (discount > 0 ? discount : 0);
    var html = quantity <= 0 ? `` :
        `<li class="list-inline-item h4 fw-light mb-0">
          ${$.ToAmount(finalPrice)}
         </li>
         ${(discount > 0 ?
            `
             <li class="list-inline-item text-muted fw-light">
                                    <del>${$.ToAmount(price)}</del>
             </li>` :
            ``)}`;

    $(`.size-price-section-container`)
        .html(html);
}

function SetSizeInBasketAsDefaultSize(productId) {
    var sizes = [];
    $(`.size-select[data-product-id='${productId}'] option`).each(function () {
        sizes.push($(this).val());
    });
    if (sizes.length === 0) return;
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];

    var firstSizeInBasket = $.grep(sizes, function (sizeId) {
        return $.grep(basketProducts, function (product) {
            return sizeId === product.id.toString();
        }).length > 0;
    })[0];
    if (firstSizeInBasket !== undefined && firstSizeInBasket !== null && firstSizeInBasket > +0) {
        $(`.size-select[data-product-id='${productId}']`).val(firstSizeInBasket).change();
        $(`.size-select[data-product-id='${productId}'] option[value="${firstSizeInBasket}"]`).prop('selected', true);
    }
}

// Event delegation for dynamically loaded content
$(document).on('change', '.size-select', function () {
    HandleSizeSelectedChange($(this));
});
//start server functions
var removeSuccessMessagesCount = 0;
async function UpsertToBasket($btn) {
    basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    const value = $btn.data('value');
    var isMinimal = $btn.hasClass('minimal');
    var productId = $btn.data('product-id');
    if (!$btn.hasClass('checked')) {
        var maxCount = $btn.data('quantity-remaining');
        var preferedCount = $('#count').val();
        if (preferedCount == undefined || preferedCount <= 0)
            preferedCount = 1;
        var currentItemInBasket = basketProducts.filter(item => item.id == value);
        var currentItemCount = 0;
        if (currentItemInBasket.length > 0)
            currentItemCount = currentItemInBasket.count;
        var totalCount = +currentItemCount + +preferedCount;
        var productId = $btn.data('product-id')
        if (+totalCount > +maxCount) {
            ShowErrorMessage('تعداد درخواستی شما از تعداد موجود در انبار بیشتر است');
            return basketProducts;
        }
        if (IsUserAuthenticated()) {
            await AddToServerBasket(value, totalCount);
        }
        else {
            ShowSuccessMessage("محصول مورد نظر به سبد خرید اضافه شد");
            $('#sidebarCart').modal('show');
        }
        basketProducts.push({
            id: value,
            count: totalCount,
            productId: productId,
            createDate: new Date().toLocaleString(),
        });
    }
    else {
        if (!isMinimal) {
            basketProducts = await RemoveSingleProductFromBasket(value);
        }
        else {
            var products = basketProducts.filter(a => a.productId == productId);
            removeSuccessMessagesCount = 0;
            products.forEach(async function (item) {
                basketProducts = await RemoveSingleProductFromBasket(item.id);
            });
            removeSuccessMessagesCount = 0;
        }
    }
    return basketProducts;
}

async function RemoveSingleProductFromBasket(value) {
    var success = true;
    if (IsUserAuthenticated()) {
        success = await RemoveFromServerBasket(value);
    }
    basketProducts = basketProducts.filter(item => item.id !== value);
    if (success) {
        if (removeSuccessMessagesCount === 0) {
            ShowSuccessMessage("محصول مورد نظر از سبد خرید حذف شد");
            removeSuccessMessagesCount++;
        }
    }
    return basketProducts;
}

async function AddToServerBasket(id, count) {
    try {
        const result = await $.ajax({
            url: `/order/add-to-basket-json/${id}/${count}`,
            method: 'GET'
        });
        ShowMessageByResponse(result);
        if (result.status === "Success") {
            await UpdateBasketProductsBtns();
            await UpdateBasket();
            $('#sidebarCart').modal('show');
            return await true;
        }
        return await false;
    } catch (error) {
        console.error('Error adding product to basket:', error);
        return await false;
    }
}

async function RemoveFromServerBasket(id) {
    try {
        const result = await $.ajax({
            url: `/order/remove-product-from-basket-json/${id}`,
            method: 'GET'
        });
        if (result.status === "Success") {
            await UpdateBasketProductsBtns();
            await UpdateBasket();
            return await true;
        }
        return await false;
    } catch (error) {
        console.error('Error while removing product from basket:', error);
        return await false;
    }
}

async function LoadFromServerBasket() {
    try {
        const result = await $.ajax({
            url: `/order/basket-json`,
            method: 'GET'
        });
        if (result.status === "Success") {

            var newBasketProductsArray = [];
            result.data.forEach(function (product) {
                newBasketProductsArray.push({
                    id: product.id,
                    count: product.count,
                    productId: product.productId,
                    createDate: product.createDate,
                });
            });
            localStorage.setItem('basketProducts', JSON.stringify(newBasketProductsArray));

            await ShowBasket(result.data);
            return await true;
        }
        return await false;
    } catch (error) {
        console.error('Error adding product to basket:', error);
        return await false;
    }
}

async function DecrementFromServerBasket(id) {
    try {
        const result = await $.ajax({
            url: `/order/decrement-basket-json/${id}`,
            method: 'GET'
        });
        if (result.status === "Success") {
            await UpdateBasketProductsBtns();
            await UpdateBasket();
            return await true;
        }
        return await false;
    } catch (error) {
        console.error('Error decrementing product from basket:', error);
        return await false;
    }
}

async function ChangeCountFromServerBasket(id, count) {
    try {
        const result = await $.ajax({
            url: `/order/change-basket-count-json/${id}/${count}`,
            method: 'GET'
        });
        ShowMessageByResponse(result);
        if (result.status === "Success") {
            await UpdateBasketProductsBtns();
            await UpdateBasket();
            return await true;
        }
        return await false;
    } catch (error) {
        console.error('Error changing product count from basket:', error);
        return await false;
    }
}


//end server functions
function UpdateFavoriteProductsBtns(favoriteProducts) {
    favoriteProducts.forEach(value => {
        $('.favorite-btn[data-value="' + value + '"]').addClass('checked');
    });
    $('.favorite-products-count').text(favoriteProducts.length);
    $('.favorite-btn').each(function () {
        const $btn = $(this);
        var isCustomIcon = $btn.hasClass('has-custom-icon');
        if (isCustomIcon) {
            if ($btn.hasClass('checked')) {
                $btn.html(`<svg class="svg-icon svg-icon-heavy">
                          <use xlink:href="#heart-fill"> </use>
                        </svg>`);
            } else {
                $btn.html(`<svg class="svg-icon svg-icon-heavy">
                          <use xlink:href="#heart-1"> </use>
                        </svg>`);
            }
        }
        else {
            if ($btn.hasClass('checked')) {
                $btn.html(`<i class="far fa-heart me-2"></i>حذف از مورد علاقه ها`);
            } else {
                $btn.html(`<i class="far fa-heart me-2"></i>افزودن به موردعلاقه ها`);
            }
        }
    });
}

async function UpdateBasketProductsBtns(basketProducts) {
    if (IsUserAuthenticated()) {
        try {
            const result = await $.ajax({
                url: `/order/basket-json`,
                method: 'GET'
            });
            if (result.status === "Success") {
                basketProducts = result.data;
            }
        } catch (error) {
            console.error('Error adding product to basket:', error);
        }
    }
    $('.add-to-cart-btn').removeClass('checked');
    if (!basketProducts)
        basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    basketProducts.forEach(item => {
        $('.add-to-cart-btn[data-value="' + item.id + '"]').addClass('checked');
        $('.add-to-cart-btn.minimal[data-product-id="' + item.productId + '"]').addClass('checked');
    });
    $('.add-to-cart-btn').each(function () {
        const $btn = $(this);
        const isMinimal = $(this).hasClass('minimal');
        const quantity = $(this).data('quantity-remaining');
        if (quantity <= 0) {
            $btn.text('ناموجود');
            $btn.attr('disabled', 'true');
            $btn.addClass('default-cursor');
        }
        else {
            if (isMinimal) {
                if ($btn.hasClass('checked')) {
                    $btn.removeClass('text-dark');
                    $btn.addClass('text-danger');
                    if (!$btn.hasClass('has-icon'))
                        $btn.text('حذف');
                } else {
                    $btn.addClass('text-dark');
                    $btn.removeClass('text-danger');
                    if (!$btn.hasClass('has-icon'))
                        $btn.text('خرید');
                }
            }
            else {
                if ($btn.hasClass('checked')) {
                    $btn.removeClass('btn-dark');
                    $btn.addClass('btn-danger');
                    if (!$btn.hasClass('has-icon'))
                        $btn.html(`
                    <i class="fa fa-shopping-cart me-2"></i>
                    حذف از سبد خرید`);
                } else {
                    $btn.addClass('btn-dark');
                    $btn.removeClass('btn-danger');
                    if (!$btn.hasClass('has-icon'))
                        $btn.html(`
                    <i class="fa fa-shopping-cart me-2"></i>
                    افزودن به سبد خرید`);
                }
            }
        }
    });
}

async function UpdateBasket(basketProducts) {
    if (IsUserAuthenticated()) {
        await LoadFromServerBasket();
        return;
    }
    await ShowBasket(basketProducts);
}

async function AddLocalBasketItemsToServer(basketProducts) {
    if (!basketProducts) return;
    for (var product of basketProducts) {
        await AddToServerBasket(product.id, product.count);
    }
}

async function ShowBasket(basketProducts) {
    $('#basket-items-content-container').empty();
    $('#cart-page-items-container').empty();
    if (basketProducts.length == 0 || basketProducts == null) {
        $('.empty-basket-container').removeClass('d-none');
        $('.basket-items-content-container').addClass('d-none');
        $('.basket-buttons-container').addClass('d-none');
        $('.basket-items-count-span').html('0');
        return;
    }
    $('.empty-basket-container').addClass('d-none');
    $('.basket-items-content-container').removeClass('d-none');
    $('.basket-items-count-span').html('0');
    let totalPrice = 0;
    let totalDiscount = 0;
    let productsCount = 0;
    basketProducts.sort(function (a, b) {
        return new Date(a.createDate) - new Date(b.createDate);
    });
    for (const item of basketProducts) {
        try {
            const result = await $.ajax({
                url: `/ProductSelectedOptionValue/Detail/${item.id}`,
                method: 'GET'
            });
            var product = result.data;
            if (product != null) {
                if (product.quantityInStock > 0) {
                    totalPrice += (+product.price * item.count);
                    if (+product.discountPrice > +0) {
                        totalDiscount += (+product.discountPrice * item.count);
                    }
                    +productsCount++;
                }
                product.productImageName = $.ConvertImageNameToWebP(product.productImageName);
                $('#cart-page-items-container').append(GetBasketPageItemHtml(product, item.count));
                var itemHtml = GetBasketItemHtml(product, item.count)
                $('#basket-items-content-container').append(itemHtml);
            }
        } catch (error) {
            console.error('Error fetching product:', error);
        }
    }
    $('.basket-items-count-span').html(productsCount);
    $('#basket-total-price-container').html(`${(totalDiscount > 0 ? `
                       <h5 class="mb-4">
                            مبلغ کل:
                            <s class="me-2 text-gray-500">
                            ${totalPrice.toLocaleString()}
                            </s>
                        </h5>` : ``)}
        <h5 class="mb-4">
                        قابل پرداخت:
                           <span class="float-right">
                        ${(totalPrice - totalDiscount).toLocaleString()}
                           </span>
                        </h5>`);
    $('.basket-total-price').text(totalPrice.toLocaleString());
    $('.basket-final-price').text((totalPrice - totalDiscount).toLocaleString());
}

function GetBasketItemHtml(product, count) {
    var url = GetProductUrl(product);
    var baseImagePath = $('#product-base-img-path').val();
    product.productTitle = `${product.productTitle} ${product.productOptionTitle} ${product.productOptionValueTitle}`;
    return `
    <div class="navbar-cart-product">
                        <div class="d-flex align-items-center">
                            <a href="${url}" title="${product.productTitle}">
                                <img class="img-fluid navbar-cart-product-image"
                                     src="${baseImagePath + product.productImageName}"
                                     alt="${product.productTitle}" title="${product.productTitle}" />
                            </a>
                            <div class="w-100">
                                <a class="navbar-cart-product-remove" 
                                   onclick="RemoveSingleProductInBasket(${product.id})">
                                    <svg class="svg-icon sidebar-cart-icon">
                                        <use xlink:href="#close-1"> </use>
                                    </svg>
                                </a>
                                ${(product.quantityInStock > 0 ?
            GetBasketItemPriceSectionHtml(product, count) :
            GetBasketItemNoQuantitySectionHtml(product))}
                            </div>
                        </div>
                    </div>`;
}

function GetBasketItemPriceSectionHtml(product, count) {
    var url = GetProductUrl(product);
    var currentPrice = product.price - (product.discountPrice > 0 ? product.discountPrice : 0);
    return `
                   <div class="ps-3">
                        <a class="navbar-cart-product-link text-dark link-animated"
                           href="${url}" title="${product.productTitle}"
                           >${product.productTitle}</a>
                        <small class="d-block text-muted">تعداد: ${count} </small>
                        ${(product.discountPrice > 0 ?
            `<s class="me-2 text-gray-500">${product.price.toLocaleString()}</s>` : ``)}
                        <strong class="d-block text-sm">${currentPrice.toLocaleString()}</strong>
                    </div>`;
}

function GetBasketItemNoQuantitySectionHtml(product) {
    var url = GetProductUrl(product);
    return `
                   <div class="ps-3">
                        <a class="navbar-cart-product-link text-dark link-animated"
                           href="${url}" title="${product.productTitle}"
                           >${product.productTitle}</a>
                        <small class="d-block text-danger">ناموجود</small>
                    </div>`;
}

function GetBasketPageInStockItemHtml(product, count) {
    var url = GetProductUrl(product);
    var baseImagePath = $('#product-base-img-path').val();
    var finalPrice = product.price;
    if (product.discountPrice > 0)
        finalPrice -= +product.discountPrice;
    return `
    <div class="cart-item">
        <div class="row d-flex align-items-center text-start text-md-center">
            <div class="col-12 col-md-5">
                <a class="cart-remove close mt-3 d-md-none" 
                href="#" onclick="RemoveSingleProductInBasket(${product.id})">
                <i class="fa fa-times"></i>
                </a>
                <div class="d-flex align-items-center">
                    <a href="${url}" title="${product.productTitle}">
                    <img class="cart-item-img"
                    src="${(baseImagePath + product.productImageName)}" 
                    alt="${product.productTitle}">
                    </a>
                    <div class="cart-title text-start">
                        <a class="text-dark link-animated" 
                        href="${url}" title="${product.productTitle}">
                        <strong>${product.productTitle}</strong>
                        </a>
                        <br>
                        <span class="text-muted text-sm">
                        ${product.productOptionTitle}: 
                        ${product.productOptionValueTitle}</span>
                       </div>
                </div>
            </div>
            <div class="col-12 col-md-7 mt-4 mt-md-0">
                <div class="row align-items-center">
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-6 d-md-none text-muted">قیمت هر عدد</div>
                            <div class="col-6 col-md-12 text-end
                            text-md-center">${finalPrice.toLocaleString()}
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row align-items-center">
                            <div class="d-md-none col-7 
                            col-sm-9 text-muted">تعداد</div>
                            <div class="col-5 col-sm-3 col-md-12">
                                <div class="d-flex align-items-center">
                                    <button class="btn btn-items 
                                    btn-items-decrease" 
                                    onclick="DecrementSingleProductInBasket(${product.id})"
                                    >-</button>
                                    <input class="form-control text-center border-0 
                                    border-md input-items" type="text" value="${count}"
                                    onchange="ChangeSingleProductCountInBasket(this)"
                                    data-value="${product.id}"
                                    data-max-value="${product.quantityInStock}">
                                    <button class="btn btn-items btn-items-increase"
                                    onclick="IncrementSingleProductInBasket(${product.id},${+count + +1},${product.quantityInStock})"
                                    >+</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-6 d-md-none text-muted">قیمت کل</div>
                            <div class="col-6 col-md-12 text-end text-md-center">
                            ${(finalPrice * count).toLocaleString()}
                            </div>
                        </div>
                    </div>
                    <div class="col-2 d-none d-md-block text-center">
                        <a class="cart-remove text-muted" href="#"
                        onclick="RemoveSingleProductInBasket(${product.id})">
                            <svg class="svg-icon w-2rem h-2rem svg-icon-light">
                                <use xlink:href="#close-1"> </use>
                            </svg>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>`;
}

function GetBasketPageOutOfStockItemHtml(product) {
    var url = GetProductUrl(product);
    var baseImagePath = $('#product-base-img-path').val();
    return `
    <div class="cart-item">
        <div class="row d-flex align-items-center text-start text-md-center">
            <div class="col-12 col-md-5">
                <a class="cart-remove close mt-3 d-md-none"
                href="#" onclick="RemoveSingleProductInBasket(${product.id})">
                <i class="fa fa-times"></i>
                </a>
                <div class="d-flex align-items-center">
                    <a href="${url}" title="${product.productTitle}">
                    <img class="cart-item-img"
                    src="${(baseImagePath + product.productImageName)}" 
                    alt="${product.productTitle}">
                    </a>
                    <div class="cart-title text-start">
                        <a class="text-dark link-animated" 
                        href="${url}" title="${product.productTitle}">
                        <strong>${product.productTitle}</strong>
                        </a>
                        <br>
                        <span class="text-muted text-sm">
                        ${product.productOptionTitle}: 
                        ${product.productOptionValueTitle}</span>
                       </div>
                </div>
            </div>
            <div class="col-12 col-md-7 mt-4 mt-md-0">
                <div class="row align-items-center">
                    <div class="col-md-10 text-end
                            text-md-center text-danger">
                        ناموجود
                    </div>


                    <div class="col-2 d-none d-md-block text-center">
                        <a class="cart-remove text-muted" href="#" onclick="RemoveSingleProductInBasket(${product.id})">
                            <svg class="svg-icon w-2rem h-2rem svg-icon-light">
                                <use xlink:href="#close-1"> </use>
                            </svg>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>`;
}

function GetBasketPageItemHtml(product, count) {
    return product.quantityInStock > 0 ? GetBasketPageInStockItemHtml(product, count) :
        GetBasketPageOutOfStockItemHtml(product);
}

function GetProductUrl(product) {
    return `/products/detail/${product.productId}/${ToUrl(product.productTitle)}`;
}

function GetProductListDtoUrl(product) {
    return `/products/detail/${product.id}/${ToUrl(product.title)}`;
}

async function RemoveSingleProductInBasket(id) {
    if (IsUserAuthenticated()) {
        await RemoveFromServerBasket(id);
    }
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];

    // Remove the product if count is 0
    basketProducts = basketProducts.filter(item => item.id != id);

    // Save updated basketProducts to localStorage
    localStorage.setItem('basketProducts', JSON.stringify(basketProducts));

    await UpdateBasketProductsBtns(basketProducts);
    await UpdateBasket(basketProducts);
}

async function IncrementSingleProductInBasket(id, count, quantityInStock) {
    if (+count > +quantityInStock) {
        ShowErrorMessage("تعداد درخواستی از تعداد موجود بیشتر است");
        return;
    }
    if (IsUserAuthenticated()) {
        await AddToServerBasket(id, count);
    }
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    const existingProduct = basketProducts.find(item => item.id === id);

    if (existingProduct) {
        // Product already exists, increase the count
        existingProduct.count++;
    } else {
        // Product does not exist, add it with count 1
        basketProducts.push({ id: id, count: 1 });
    }

    // Remove the product if count is 0
    basketProducts = basketProducts.filter(item => item.count > 0);

    // Save updated basketProducts to localStorage
    localStorage.setItem('basketProducts', JSON.stringify(basketProducts));

    // Update UI (you can implement your own function here)
    await UpdateBasketProductsBtns(basketProducts);
    await UpdateBasket(basketProducts);
}

async function DecrementSingleProductInBasket(id) {
    if (IsUserAuthenticated()) {
        await DecrementFromServerBasket(id);
    }
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    const existingProduct = basketProducts.find(item => item.id === id);

    if (existingProduct) {
        // Product already exists, increase the count
        existingProduct.count--;
    }

    // Remove the product if count is 0
    basketProducts = basketProducts.filter(item => item.count > 0);

    // Save updated basketProducts to localStorage
    localStorage.setItem('basketProducts', JSON.stringify(basketProducts));

    // Update UI (you can implement your own function here)
    await UpdateBasketProductsBtns(basketProducts);
    await UpdateBasket(basketProducts);
}

async function ChangeSingleProductCountInBasket(element) {
    var id = $(element).data('value');
    var count = $(element).val();
    var maxCount = $(element).data('max-value');
    if (+count > +maxCount) {
        ShowErrorMessage("تعداد درخواستی از موجودی انبار بیشتر است");
        return;
    }
    if (IsUserAuthenticated()) {
        await ChangeCountFromServerBasket(id, count);
    }
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    const existingProduct = basketProducts.find(item => item.id === id);

    if (existingProduct) {
        // Product already exists, increase the count
        existingProduct.count = +count;
    }

    // Remove the product if count is 0
    basketProducts = basketProducts.filter(item => item.count > 0);

    // Save updated basketProducts to localStorage
    localStorage.setItem('basketProducts', JSON.stringify(basketProducts));

    // Update UI (you can implement your own function here)
    await UpdateBasketProductsBtns(basketProducts);
    await UpdateBasket(basketProducts);
}
function AppendStyleSheet(url) {
    var link = document.createElement('link');
    link.rel = 'stylesheet';
    link.href = url;
    document.head.appendChild(link);
  }