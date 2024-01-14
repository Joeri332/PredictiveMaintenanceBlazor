function createChart(chartId, chartType, data, labels) {
    var ctx = document.getElementById(chartId).getContext('2d');
    new Chart(ctx, {
        type: chartType,
        data: {
            labels: labels,
            datasets: [{
                label: 'Aug scores',
                data: data,
            }]
        }
    });
}
function createLineChart(chartId, labels, datasets) {
    var ctx = document.getElementById(chartId).getContext('2d');
    // Destroy the old chart instance if it exists to avoid memory leaks and canvas misbehavior
    if (window.myCharts && window.myCharts[chartId]) {
        window.myCharts[chartId].destroy();
    }
    window.myCharts = window.myCharts || {};

    // Create a new chart instance
    window.myCharts[chartId] = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: datasets,
        },
        options: {
            responsive: false,
            maintainAspectRatio: false, // Allow the chart to be responsive without maintaining aspect ratio
            // Add other chart options as needed
        },
    });
}
