$(document).ready(function (e) {
    $('.condition-select').each(function () {
        AddConditionsSelectOptions(this);
    });

    ShowUseFullLinkInputs();

    $('#Type').change(function (e) {
        ShowUseFullLinkInputs();
    });

    function AddConditionsSelectOptions(conditionElement) {

        $(conditionElement).change(function () {
            LoadOptions(conditionElement);
        });
        LoadOptions(conditionElement);
    }

    function LoadOptions(conditionElement) {
        var dataId = $(conditionElement).attr('data-condition');
        var addDefaultOption = $(conditionElement).attr('data-add-default');
        var value = $(conditionElement).val();
        var matchOptions = $(`.select-hidden-option[data-select-id='${dataId}'][data-filter='${value}']`);
        var selectTag = $(`[data-id="${dataId}"]`);
        var value = selectTag.data("value");
        selectTag.empty();

        if (addDefaultOption)
            selectTag.prepend(`<option value="0">لطفا انتخاب کنید</option>`);
        if (!(matchOptions.length > +0)) return;
        $(matchOptions).each(function (index, item) {
            var value = $(item).attr('data-value');
            var title = $(item).attr('data-title');
            var html = `<option value="${value}">${title}</option>`;
            selectTag.append(html);
        });

        if (value > 0) {
            selectTag.val(value).trigger('change');
        }
    }

    //end conditions select optinos

    $('#usefullLinkForm').submit(function (e) {
        var valid = ValidateUpsertUseFullLinks();
        if (valid === false)
            e.preventDefault();
    });

    function ValidateUpsertUseFullLinks() {
        var displayPosition = $('#DisplayPosition').val();
        var address = $('#LinkAddress').val();
        var type = $('#Type').val();
        var image = $('#Image').val();
        if (type === "NoSubItems")
            if (address === null || address.trim() === "") {
                ShowErrorMessage('در حالت بدون زیر مجموعه وارد کردن لینک اجباری است');
                return false;
            }
        if (displayPosition === 'Footer') {
            if (type === 'RowsAndImage' || type === 'RowsAndSubRows') {
                ShowErrorMessage('در حالت فوتر فقط نوع متنی یا بدون زیر مجموعه مجاز است.');
                return false;
            }

        }
        else {
            if (type == "RowsAndImage")
                if (image === null) {
                    ShowErrorMessage("لطفا عکس را انتخاب کنید");
                    return false;
                }
        }
    }

    function ShowUseFullLinkInputs() {
        $('.usefull-link').removeClass('d-none');
        var displayPosition = $('#DisplayPosition');
        var type = $('#Type').val();
        var addressInput = $("#LinkAddress");
        var addressContainer = $(addressInput).parent().parent();
        var imageInput = $("#Image");
        var imageParent = $(imageInput).parent();
        var imageDataValue = $(imageInput).attr('data-value');
        var img = $(`img.${imageDataValue}`);
        var imgMainParent = $(img).parent().parent();
        var badgeInput = $('#BadgeTitle').parent();
        var hasParent = +$('#ParentId').val() > +0 || +$('#RowId').val() > +0;
        if (displayPosition === "Footer") {
            imageInput.val(null);
            imageParent.addClass('d-none');
            imgMainParent.addClass('d-none');
        }
        switch (type) {
            case "Text": {
                addressInput.val(null);
                addressContainer.addClass('d-none');
                imageInput.val(null);
                imageParent.addClass('d-none');
                imgMainParent.addClass('d-none');
                badgeInput.addClass('d-none');
                break;
            }
            case "RowsAndImage": {
                addressInput.val(null);
                addressContainer.addClass('d-none');
                badgeInput.addClass('d-none');
                break;
            }
            case "RowsAndSubRows": {
                addressInput.val(null);
                addressContainer.addClass('d-none');
                imageInput.val(null);
                imageParent.addClass('d-none');
                imgMainParent.addClass('d-none');
                badgeInput.addClass('d-none');
                break;
            }
            case "NoSubItems": {
                imageInput.val(null);
                imageParent.addClass('d-none');
                imgMainParent.addClass('d-none');
                break;
            }
        }
        if (hasParent)
            addressContainer.removeClass('d-none');
    }
});
$('.sweet-alert-confirm').click(async function (e) {
    e.preventDefault();
    var href = $(this).attr("href");
    const result = await Swal.fire({
        title: "آیا از انجام این عملیات اطمینان دارید؟",
        showDenyButton: true,
        confirmButtonText: "تایید",
        denyButtonText: `لغو`,
        text: "پس از انجام عملیات امکان بازگشت وجود ندارد",
        icon: 'warning'
    });

    if (result.isConfirmed) {
        var response = await Get(href);
        if (response.status === "Success") {
            const result = await Swal.fire(response.message, "", "success");
            location.reload();
        }
    } else if (result.isDenied) {
        Swal.fire("عملیات لغو شد", "", "info");
    }
});
