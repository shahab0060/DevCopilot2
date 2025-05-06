$(document).ready(async function (e) {
    $('#validate-discount-code-form').submit(async function (e) {
        e.preventDefault();
        var response = await GetFormResult('validate-discount-code-form');
        ShowMessageByResponse(response);
        if (response.status.toLowerCase() === 'success') {
            var discountCode = $('#discount-code-input').val();
            $('.discount-code-input').val(discountCode);
            $('#discount-code-detail-form').submit();
        }
    });
    $('#discount-code-detail-form').submit(async function (e) {
        e.preventDefault();
        var data = await GetFormData('discount-code-detail-form');
        var amount = $('.order-payable-amount').data('value');
        var finalPrice = amount - data.price;
        $('.order-payable-amount').text($.ToAmount(finalPrice));
        var discountCodeHtml = `
                                <tr>
                                    <th class="py-4">تخفیف اعمال شده</th>
                                    <td class="py-4 text-end text-muted">
                                            ${$.ToAmount(data.price)}
                                    </td>
                                </tr>`;
        $(discountCodeHtml).insertBefore('.order-total-amount-container');
        $('#validate-discount-code-form').addClass('d-none');
        $('#discount-code-used-message').removeClass('d-none');
    })
});