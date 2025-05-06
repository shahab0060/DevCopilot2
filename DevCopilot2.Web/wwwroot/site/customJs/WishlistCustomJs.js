$(document).ready(async function (e) {
    let favoriteProducts = JSON.parse(localStorage.getItem('favoriteProducts')) || [];
    (async () => {
        for (const productId of favoriteProducts) {
            const response = await Get(`/products/detail-json/${productId}`);
            if (response && response.data) {
                const html = GetSingleProductHtml(response.data);
                $('.favorite-products-container').append(html);
            }
        }
    })();

});

function GetSingleProductHtml(product) {
    var baseImagePath = $('#product-base-img-path').val();
    product.imageName = $.ConvertImageNameToWebP(product.imageName);
    var baseProduct = product;
    var finalPrice = baseProduct.price - (baseProduct.discountPrice > 0 ?
        baseProduct.discountPrice : 0);
    var isRemaining = baseProduct.firstActiveSelectedOptionValue.quantityInStock > 0;
    return ` <div class="cart-item">
                <div class="row d-flex align-items-center text-start text-md-center">
                    <div class="col-12 col-md-5">
                        <div class="d-flex align-items-center">
                            <a href="${GetProductListDtoUrl(baseProduct)}" title="${baseProduct.title}">
                                <img class="cart-item-img"
                                     src="${baseImagePath + baseProduct.imageName}"
                                     alt="${baseProduct.title}" title="${baseProduct.title}">
                            </a>
                            <div class="cart-title text-start">
                                <a class="text-dark" href="${GetProductListDtoUrl(baseProduct)}"
                                title="${baseProduct.title}">
                                    <strong>${baseProduct.title}</strong>
                                </a>
                                <br>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-7 mt-4 mt-md-0">
                        <div class="row align-items-center">
                            <div class="col-md-2">
                                ${isRemaining ?
        `<div class="row">
    <div class="col-6 d-md-none text-muted">قیمت هر عدد</div>
    <div class="col-6 col-md-12 text-end text-md-center">
    ${finalPrice.toLocaleString()}</div>
</div>` : ``}
                            </div>
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-6 d-md-none text-muted">وضعیت</div>
                                    <div class="col-6 col-md-12 text-end text-md-start">
                                        ${(isRemaining
            ? `<div class="badge p-lg-2 bg-primary">موجود</div>`
        : `<div class="badge p-lg-2 bg-danger">نا موجود</div>`)}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>`;
}