
$(document).ready(async function (e) {
    await LoadComments();
    $('#view-more-btn').click(async function (e) {
        open_waiting('#view-more-btn');
        await LoadComments();
        close_waiting('#view-more-btn');
    });
    $('#reviewForm').submit(async function (e) {
        e.preventDefault();
        var result = await GetFormResult('reviewForm');
        if (result.status === 'Danger') {
            ShowErrorMessage(result.message);
            return;
        }
        ShowSuccessMessage(result.message);
    });
});

async function LoadComments() {
    open_waiting('#submit-review-form-container');
    var formId = 'filter-comments-form';
    var data = await GetFormData(formId);
    var comments = data.productComments;
    if (!comments.length) {
        $('#view-more-btn').attr('disabled', 'true');
        $('#view-more-btn').text('موردی برای نمایش وجود ندارد');
    }
    else {
        $(comments).each(function (index, element) {
            var commentHtml = `
            <div class="media review row">
                  <div class="flex-shrink-0 me-4 me-xl-5 col-md-2 col-sm-12">
                  <h5 class="mt-2 mb-1">${element.comment.userTitle}</h5>
                  <div class="mb-2">
                  ${GetRatingsHtml(element.comment.rate)}
                    </div>
                    <span class="text-uppercase text-muted">${toShamsi(element.createDate)}</span>
                    </div>
                  <div class="col-md-9 col-sm-12">

                    <h4 class="d-sm-mt-4">${element.comment.title}</h4>
                    <p class="text-muted">
                   ${element.comment.description}
                   </p>
                  </div>
                </div>`;
            $(commentHtml).insertBefore('#view-more-btn');
            var currentPageId = $('#comments-page-id').val();
            $('#comments-page-id').val(+currentPageId + +1);
        });
    }
    close_waiting('#submit-review-form-container');
}
function GetRatingsHtml(rate) {
    var html = '';
    for (var i = 1; i <= 5; i++) {
        var singleHtml = '';
        if (+rate >= i) {
            singleHtml = '<i class="fa fa-xs fa-star text-warning"></i>';
        }
        else {
            singleHtml = '<i class="fa fa-xs fa-star text-gray-200"></i>';
        }
        html += singleHtml;
    }
    return html;
}
