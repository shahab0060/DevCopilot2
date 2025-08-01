﻿$(document).ready(function (e) {
    ActivatePageSizeOptions();
    $('.page-size-option').click(function (e) {
        if ($(this).hasClass('active')) return;
        var dataValue = $(this).attr('data-value');
        $('#TakeEntity').val(+dataValue);
        $('#PageId').val(1);
        ActivatePageSizeOptions();
        $('#filterForm').submit();
    });
    $('.option-value-checkbox').change(function () {
        var selectedIds = [];
        $('.option-value-checkbox:checked').each(function () {
            var html = `<input type="hidden" name="OptionValueIds" value="${$(this).val()}" />`;
            $('#filterForm').append(html);
        });
        $('#OptionValueIds').val(selectedIds.join(','));
        $('#filterForm').submit();
    });
    $('#SortProducts').change(function (e) {
        $('#filterForm').submit();
    })
    $('.category-filter').click(function (e) {
        var dataValue = $(this).attr('data-value');
        $('#PageId').val(1);
        $('#CategoryId').val(dataValue);
        $('#filterForm').submit();
    });
});

function ActivatePageSizeOptions() {
    var pageSize = $('#TakeEntity').val();
    $('.page-size-option').removeClass('active');
    $(`.page-size-option[data-value="${pageSize}"]`).addClass('active');
}