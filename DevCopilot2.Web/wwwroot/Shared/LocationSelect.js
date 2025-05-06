$(document).ready(async function (e) {
    await SetProvinces();
    $('.province-select').change(async function (e) {
        LoadCities($(this));
    });
});

async function SetProvinces() {
    var result = await Get("/Province/ComboJson");
    var provinces = result.data;

    var defaultOption = `<option value="NULL">لطفا انتخاب کنید</option>`;
    $('.province-select').prepend(defaultOption);
    $(provinces).each(function (index, province) {
        var html = `<option value="${province.value}">${province.title}</option>`;
        $('.province-select').append(html);
    });
    $('.province-select').each(async function (index, element) {
        var dataIndex = $(element).data('index');
        var city = $(`.city-select[data-index="${dataIndex}"]`);
        var cityId = $(city).data('value');
        if (cityId > 0) {
            var cityInformationResult = await Get(`/City/DetailJson?id=${cityId}`);
            var cityInformation = cityInformationResult.data;
            if (cityInformation) {
                $(element).val(cityInformation.provinceId);
                await LoadCities(element);
                console.log(cityInformation.provinceId);
                $(`.city-select[data-index="${dataIndex}"]`).val(cityId).trigger('change');
                console.log($(`.city-select[data-index="${dataIndex}"]`).val());
                console.log($(`.city-select[data-index="${dataIndex}"]`));
            }
        }  
    });
}

async function LoadCities(provinceSelect) {
    var provinceId = $(provinceSelect).val();
    var dataIndex = $(provinceSelect).data("index");
    $('#province-id-hidden-input').val(provinceId);
    var result = await GetFormResult('filter-cities-hidden-form');
    var cities = result.data;
    if (!cities) {
        ShowErrorMessage('مشکلی در دریافت اطلاعات پیش آمد')
        return;
    }
    $(`.city-select[data-index="${dataIndex}"]`).empty();
    $(cities).each(function (index, city) {
        var html = `<option value="${city.value}">${city.title}</option>`;
        $(`.city-select[data-index="${dataIndex}"]`).append(html);
    });
}