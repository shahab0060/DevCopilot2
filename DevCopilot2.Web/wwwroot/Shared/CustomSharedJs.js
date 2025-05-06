function open_waiting(selector = 'body') {
    $(selector).waitMe({
        effect: 'stretch',
        text: 'لطفا صبر کنید ...',
        bg: 'rgba(255,255,255,0.7)',
        color: '#000'
    });
}

function close_waiting(selector = 'body') {
    $(selector).waitMe('hide');
}

function FillPageId(pageId) {
    $('#PageId').val(+pageId);
    $('.FilterForm').submit();
}
function ToAmount(number) {
    // Validate input (ensure it's a positive number)
    if (typeof number !== 'number' || isNaN(number) || number < 0) {
        console.error('Invalid input. Please provide a positive number.');
        return;
    }

    // Format the number with commas
    const formattedNumber = number.toLocaleString();

    // Add "تومان" at the end
    const result = formattedNumber + ' تومان';

    return result;
}
var propsIndex = 0;
var resizesIndex = 0;
var areasIndex = 0;
$(document).ready(function () {
    var editors = $("[realckeditor]");
    if (editors.length > 0) {
        $.getScript('/Shared/ckeditor.js', function () {
            $(editors).each(function (index, value) {
                var id = $(value).attr('realckeditor');
                ClassicEditor.create(document.querySelector('[realckeditor="' + id + '"]'),
                    {
                        toolbar: {
                            items: [
                                'heading',
                                '|',
                                'bold',
                                'italic',
                                'link',
                                '|',
                                'fontSize',
                                'fontColor',
                                '|',
                                'imageUpload',
                                'blockQuote',
                                'insertTable',
                                'undo',
                                'redo',
                                'codeBlock'
                            ]
                        },
                        language: 'fa',
                        table: {
                            contentToolbar: [
                                'tableColumn',
                                'tableRow',
                                'mergeTableCells'
                            ]
                        },
                        licenseKey: '',
                        simpleUpload: {
                            // The URL that the images are uploaded to.
                            uploadUrl: '/Uploader/UploadImage'
                        }

                    })
                    .then(editor => {
                        window.editor = editor;
                    }).catch(err => {
                        console.error(err);
                    });
            });
        });
    }

    $('.check-all-input').each(function (index, element) {
        HandleCheckAllCheckBox(element);
    });
    $('.check-all-input').change(function (e) {
        HandleCheckAllCheckBox($(this));
    });

    $('#submit-btn').click(function (e) {
        $('#create-form').submit();
    });

    $('.add-default-sub-form').each(function (index, element) {
        var name = $(this).data('name');
        var index = $(`.single-sub-form[data-name="${name}"]`).length;
        if (index === 0) {
            AddSubItem(name);
        }
    });
});
$(document).on("click", ".sub-form-container h3", function () {
    $(this).siblings(".single-sub-form").slideToggle();
    $(this).toggleClass("collapsed"); // Rotates the icon
});
$(document).on("change", ".Image-input, .image-input", function () {

    let file = this.files[0]; // Get uploaded file
    if (file) {
        let reader = new FileReader();
        let inputElement = $(this); // Capture reference to input element

        reader.onload = function (e) {
            let dataValue = inputElement.data("value"); // Get data-value

            if (dataValue) { // Ensure data-value exists
                let imageTag = $(`[data-name="${dataValue}"]`); // Find corresponding image tag
                if (imageTag.length) {
                    imageTag.attr("src", e.target.result); // Set image source
                }
            }
        };

        reader.readAsDataURL(file);
    }
});
function AddSubItem(name) {

    var index = $(`.single-sub-form[data-name="${name}"]`).length;
    var newName = `${name}[${index}]`;
    var template = GetTemplate(name);
    var replaceFrom = ChangeBrakcetsIndexesToZero(`${name}[0]`);
    var replaceFrom2 = ChangeBrakcetsIndexesToZero(`${name}`);
    //this code is for reArranging all indexes
    template = AdvanceReplaceAll(template, replaceFrom, newName);
    //this code is for changing indexes im remove buttons becuase they do not contain [index] at the end
    template = AdvanceReplaceAll(template, `${replaceFrom2}"`, `${name}"`);
    $(`.sub-form-container[data-name="${name}"]`)
        .append($(template));
}

function ChangeAddSubItemBtnsBracketsIndexesToZero() {
    $(".add-sub-item-btn").each(function () {
        var originalOnClick = $(this).attr("onclick");
        if (originalOnClick && originalOnClick.startsWith("AddSubItem")) {
            var modifiedOnClick = originalOnClick.replace(/\[(\d+)\]/g, '[0]');
            $(this).attr("onclick", modifiedOnClick);
        }
    });
}

function ChangeBrakcetsIndexesToZero(text) {
    return text.replace(/\[(\d+)\]/g, '[0]');
}

function GetTemplate(name) {
    var nameLastPart = GetLastPart(name);
    var inputsHtml = $(`.sub-form-template[data-form-name="${nameLastPart}"]`).html();
    var replaceFrom = $(`.sub-form-template[data-form-name="${nameLastPart}"]`).data('replace-from');
    var replaceTo = $(`.sub-form-template[data-form-name="${nameLastPart}"]`).data('replace-to');
    inputsHtml = ReplaceAll(inputsHtml, replaceFrom, replaceTo);
    inputsHtml = ReplaceAll(inputsHtml, 'is-template', '');
    var subTemplates = GetSubTemplates(name);
    var template = `
         <div class="single-sub-form row g-5" data-name="${name}">
                            ${inputsHtml}
         </div>
    `;
    template = prependBeforeLastOccurrence(template, 'buttons-container', subTemplates);
    return template;
}

function GetSubTemplates(name) {

    var nameLastPart = GetLastPart(name);
    element = $(`.sub-form-template[data-form-name="${nameLastPart}"]`);
    var template = '';
    console.log('*****');

    $.each(element[0].attributes, function (i, attr) {
        if (attr.name.startsWith('data-child')) {
            console.log(attr);
            var value = attr.value;
            var valueLastPart = GetLastPart(value);
            var title = $(`.sub-form-template[data-form-name="${valueLastPart}"]`).data('title');
            var subTemplate = GetTemplate(value);
            template += `<div class="sub-form-container" data-name="${value}">
                              <h3>${title}</h3>
                              ${subTemplate}
                        </div>`;
        }
    });
    if (template === undefined || template === null) return '';
    return template;
}

function prependBeforeLastOccurrence(htmlString, className, newHtml) {
    let tempDiv = document.createElement("div");
    tempDiv.innerHTML = htmlString; // Parse the string into actual HTML elements

    let elements = tempDiv.querySelectorAll(`.${className}`); // Select elements by class name

    if (elements.length > 0) {
        let lastElement = elements[elements.length - 1]; // Get the last matched element
        lastElement.insertAdjacentHTML("beforebegin", newHtml); // Insert newHtml before it
    }

    return tempDiv.innerHTML; // Return modified HTML string
}



function GetLastPart(text, seperator = '.') {
    try {
        return text.split(seperator).pop();
    } catch (e) {
        return text;
    }
}
function replaceLastBracketedNumber(text) {
    return text.replace(/(\[\d+\])(?!.*\[\d+\])/, '');
}

function RemoveSubItem(element) {
    var parentDataName = $(element).parent().parent().attr('data-name');
    var name = $(element).data('name');
    $(`input[name*='${parentDataName}.']`).remove();
    $(element).closest(`.single-sub-form`).remove();
    SetSubFormInputsValueAttrs();
    var replaceFrom = name;
    $(`.single-sub-form[data-name="${name}"]`).each(function (index, newElement) {
        var replaceTo = `${name}[${index}]`;
        var html = $(this).html();
        var newHtml = ReplaceAllWithBracketsInText(html, replaceFrom, replaceTo);
        $(this).html(newHtml);
    });
}

function SetSubFormInputsValueAttrs() {
    $(".sub-form-input").each(function (index, element) {
        var value = $(this).val();
        $(this).attr('value', value);
    });
}

function LoadSubItem(index, template, subFormName, subTemplates) {

    var $template = $(template);
    $template.find(".remove-sub-item-btn").each(function (e) {
        var newName = subFormName;
        $(this).attr('data-form-name', newName);
    });

    $template.find(".sub-form-input").each(function (e) {
        var suffix = $(this).data('suffix');
        var oldName = $(this).attr('name');
        var oldId = $(this).attr('id');

        var newName = `${subFormName}[${index}].${suffix}`;
        var newId = `${subFormName}${index}_${suffix}`;
        var type = $(this).attr('type');
        if (type != 'hidden')
            $(this).val('');

        var validationSpan = $template.find("span[data-valmsg-for]").filter(function () {
            return $(this).attr("data-valmsg-for") === oldName;
        }).first();
        var label = $template.find("label[for]").filter(function () {
            return $(this).attr("for") === oldId;
        }).first();

        $(this).attr('name', newName);
        if (validationSpan.length) {
            validationSpan.attr('data-valmsg-for', newName);
        }
        if (label.length) {
            label.attr('for', newId);
        } $(this).attr('id', newId);
    });
    $(`.sub-form-container[data-name="${subFormName}"]`)

        .append($template);
}

function ReplaceSubItem(template, replaceFrom, replaceTo, subFormName, container) {
    var updatedTemplate = ReplaceInTemplate(template, replaceFrom, replaceTo); // Replace across full template

    $(container).append(
        $('<div class="single-sub-form row" />')
            .attr("data-name", subFormName)
            .append(updatedTemplate)
    );
}
function replaceSpecificIndex(inputString, replaceFrom, replaceTo) {
    var regex = new RegExp(replaceFrom + '\\[\\d+\\]', 'g');
    return inputString.replace(regex, replaceTo);
}
function ReplaceInTemplate(template, replaceFrom, replaceTo) {
    var templateHtml = $(template).html(); // Get the HTML content as a string
    var updatedHtml = ReplaceAllWithBracketsInText(templateHtml, replaceFrom, replaceTo);
    return $(updatedHtml); // Convert back to a jQuery object
}

function ReplaceAllWithBracketsInText(text, replaceFrom, replaceTo) {
    // Escape special characters
    var escapedReplaceFrom = replaceFrom.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&');

    // Match the pattern with any number inside brackets
    var regex = new RegExp(escapedReplaceFrom + '\\[\\d+\\]', 'g');

    return text.replace(regex, replaceTo);
}


function ReplaceAll(text, from, to) {
    return text.replace(new RegExp(from, "g"), to);
}

function AdvanceReplaceAll(html, from, to) {
    let regex = new RegExp(from.replace(/\[/g, "\\[").replace(/\]/g, "\\]"), "g");
    return html.replace(regex, to);
}


function HandleCheckAllCheckBox(checkbox) {
    var isChecked = $(checkbox).is(':checked')
    $("input[type=checkbox]").prop('checked', isChecked);
}
$('[ajax-url-button]').on('click', function (e) {
    e.preventDefault();
    var url = $(this).attr('href');
    var itemId = $(this).attr('ajax-url-button');
    $.get(url).then(result => {
        if (result.status === 'Success') {
            $('#ajax-url-item-' + itemId).hide(500);
            setTimeout(function () {
                $('#ajax-url-item-' + itemId).remove();
                ShowSuccessMessage(result.message);
                ShowInfoMessage("پس از لود مجدد صفحه آیتم ها به شما نمایش داده خواهند شد");
            }, 500);
        } else {
            ShowErrorMessage(result.message);
        }
    });
});


function ShowMessage(title, text, theme) {
    window.createNotification({
        closeOnClick: true,
        positionClass: "nfc-top-right",
        showDuration: 3000,
        hideDuration: 1000,
        timeOut: 5000,
        extendedTimeOut: 1000,
        theme: theme
    })({
        title: title,
        message: decodeURI(text)
    });
}


$(".persian-date-picker").persianDatepicker({
    months: ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"],
    dowTitle: ["شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه"],
    shortDowTitle: ["ش", "ی", "د", "س", "چ", "پ", "ج"],
    showGregorianDate: !1,
    persianNumbers: !0,
    formatDate: "YYYY/MM/DD",
    selectedBefore: !1,
    selectedDate: null,
    startDate: null,
    endDate: null,
    prevArrow: '\u25c4',
    nextArrow: '\u25ba',
    theme: 'default',
    alwaysShow: !1,
    selectableYears: null,
    selectableMonths: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
    cellWidth: 25, // by px
    cellHeight: 20, // by px
    fontSize: 13, // by px
    isRTL: !1,
    calendarPosition: {
        x: 0,
        y: 0,
    },
    onShow: function () { },
    onHide: function () { },
    onSelect: function () { },
    onRender: function () { }
});

function ShowSuccessMessage(text) {
    ShowMessage('اعلان موفقیت', text, 'success');
}

function ShowInfoMessage(text) {
    ShowMessage('اعلان اطلاعیه', text, 'info');
}

function ShowWarningMessage(text) {
    ShowMessage('اعلان اخطار', text, 'warning');
}

function ShowErrorMessage(text) {
    ShowMessage('اعلان خطا', text, 'error');
}


function ReloadPage() {
    location.reload();
}

//const swalWithBootstrapButtons = Swal.mixin({
//    customClass: {
//        confirmButton: 'btn btn-success',
//        cancelButton: 'btn btn-danger'
//    },
//    buttonsStyling: false
//})
document.addEventListener('DOMContentLoaded', function () {
    var inputs = document.querySelectorAll('[data-val-required]');
    inputs.forEach(function (input) {
        var label = document.querySelector('label[for="' + input.id + '"]');
        if (label) {
            label.innerHTML += ' <span style="color: red;">*</span>';
        }
    });
    $('input[name="Price"]').each(function () {
        AddAmountText(this);
    });
    $('input[name="Amount"]').each(function () {
        AddAmountText(this);
    });
    $('.price-input').each(function () {
        AddAmountText(this);
    });

    function AddAmountText(element) {
        var $span = $('<p>').css('color', 'grey').insertAfter(element);

        // Define the change event for the input
        $(element).keyup(function () {
            // Get the input value, convert it to a number, and format it
            var price = parseFloat($(element).val().replace(/,/g, ''));
            var formattedPrice = isNaN(price) ? '' : price.toLocaleString('fa-IR') + ' تومان';

            // Update the text of the span
            $span.text(formattedPrice);
        });
    }

});

function IsUserAuthenticated() {
    return $('#is-user-authenticated').val();
} function ToUrl(url) {
    return url.toLowerCase()
        .trim()
        .replace(/ /g, "-")
        .replace(/%/g, "-")
        .replace(/&/g, "")
        .replace(/--+/g, "-");
}
$('.export-btn').click(function (e) {
    e.preventDefault();
    var href = $(this).attr('href');
    $('.FilterForm').attr('action', href);
    $('.FilterForm').submit();
});


function AddHiddenObject(name, hiddenInputName, splitIndex = -1) {
    const elements = $('[id^=' + name + ']');
    var uniqueId = crypto.randomUUID();
    // Iterate through each element
    elements.each(function () {

        const element = $(this);
        var baseInputValue = element.val(); // Get the base input value
        var elementName = element.attr('name');
        if (elementName != undefined)
            if (+splitIndex >= +0) {
                elementName = elementName.split(".")[splitIndex];
                if (elementName == undefined)
                    elementName = element.attr('name');
            }
        const type = element.attr('type');
        if (type === "checkbox") {
            baseInputValue = element.is(":checked");
        }
        // Create a hidden input element
        const hiddenInput = $('<input type="hidden">');
        if (elementName == undefined)
            hiddenInput.attr('name', `${hiddenInputName}`);
        else
            hiddenInput.attr('name', `${hiddenInputName}.${elementName}`);
        hiddenInput.val(baseInputValue); // Set the value attribute
        hiddenInput.attr('data-id', uniqueId);
        // Append the hidden input to the #form element
        $('#create-form').append(hiddenInput);
        // Clear the base input value
        if (type === "text")
            element.val("");
        if (type === "checkbox")
            element.val(false);
    });
    ShowSuccessMessage("آیتم مورد نظر با موفقیت اضافه شد");
}
