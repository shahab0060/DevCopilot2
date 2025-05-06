$(document).ready(async function () {
    const previouMonthsSalesChart = document.getElementById('previous-months-sales-chart');

    if (previouMonthsSalesChart) {
        try {
            // Fetch data asynchronously
            const thisMonthSalesChartInfo = await GetFormData('current-month-sales-form');
            const previousMonthSalesChartInfo = await GetFormData('previous-month-sales-form');
            const nextToLastMonthSalesChartInfo = await GetFormData('next-to-last-month-sales-form');
            var titles = thisMonthSalesChartInfo.titles;
            if (+previousMonthSalesChartInfo.titles.length >
                +thisMonthSalesChartInfo.titles.length)
                titles = previousMonthSalesChartInfo.titles;
            if (+nextToLastMonthSalesChartInfo.titles.length >
                +previousMonthSalesChartInfo.titles.length)
                titles = nextToLastMonthSalesChartInfo.titles;

            new Chart(previouMonthsSalesChart, {
                type: 'line',
                data: {
                    labels: titles,
                    datasets: [
                        {
                            label: 'این ماه',
                            data: thisMonthSalesChartInfo.values,
                            borderColor: '#FF0000', // Red
                            backgroundColor: 'rgba(255, 0, 0, 0.5)', // Red with 50% opacity
                        },
                        {
                            label: 'ماه قبل',
                            data: previousMonthSalesChartInfo.values,
                            borderColor: '#0000FF', // Blue
                            backgroundColor: 'rgba(0, 0, 255, 0.5)', // Blue with 50% opacity
                        },
                        {
                            label: 'ماه قبل تر',
                            data: nextToLastMonthSalesChartInfo.values,
                            borderColor: '#EF13BC', // purple
                            backgroundColor: 'rgba(239, 19, 188,0.5)', // purple with 50% opacity
                        },
                    ],
                },
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: true,
                        text: 'در آمد سه ماهه اخیر',
                    },
                },
            });
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    LoadLineChart();
    $('#line-chart-form').submit(function (e) {
        e.preventDefault();
        LoadLineChart();
    });

    async function LoadLineChart() {
        var data = await GetFormData("line-chart-form");
        const lineChart = document.getElementById('line-chart');
        if (Chart.getChart("line-chart") != undefined)
            Chart.getChart("line-chart").destroy();
        console.log(data);
        console.log('data printed');    
        new Chart(lineChart, {
            type: 'line',
            data: {
                labels: data.titles,
                datasets: [
                    {
                        label: 'مقدار',
                        data: data.values,
                        borderWidth: 1,
                        borderColor: '#FF0000', // Red
                        backgroundColor: 'rgba(255, 0, 0, 0.5)', // Red with 50% opacity
                    }
                ],
            },
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                },
                title: {
                    display: true,
                    text: '',
                },
            },
        });

    }
});
