﻿
function renderPieChart(canvas, data, title) {
    var sum = 0;
    data.forEach(function (item) {
        sum += item.y;
    });
    if (sum == 1) {
        sum = 0;
    }

    var option = {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: title + ": " + (sum).toLocaleString('en-US', {style: 'currency', currency: 'USD',}),
        },
        legend: {
            horizontalAlign: "right",
            verticalAlign: "center"
        },
        data: [{
            type: "pie",
            showInLegend: true,
            toolTipContent: "<b>{name}</b>: ${y} (#percent%)",
            indexLabel: "{name}",
            legendText: "{name} (#percent%)",
            indexLabelPlacement: "inside",
            dataPoints: data
        }]
    };
    $(canvas).CanvasJSChart(option);
}

function toogleDataSeries(e) {
    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
        e.dataSeries.visible = false;
    } else {
        e.dataSeries.visible = true;
    }
    e.chart.render();
}

function renderAreaChart(cost, income) {
    console.log(cost);
    console.log(income);
    console.log("log v2");
    cost.sort(function (a, b) {
        var keyA = new Date(a.x),
            keyB = new Date(b.x);
        // Compare the 2 dates
        if (keyA < keyB) return -1;
        if (keyA > keyB) return 1;
        return 0;
    });

    income.sort(function (a, b) {
        var keyA = new Date(a.x),
            keyB = new Date(b.x);
        // Compare the 2 dates
        if (keyA < keyB) return -1;
        if (keyA > keyB) return 1;
        return 0;
    });
    console.log(cost);
    console.log(income);
    cost.forEach(function (item) {
        item.x = new Date(Date.parse(item.x));
    });
    income.forEach(function (item) {
        item.x = new Date(Date.parse(item.x));
    });
        var options = {
            animationEnabled: true,
            theme: "light2",
            title: {
                text: "Cost/Income by Time"
            },
            axisY: {
                title: "Money",
                valueFormatString: "#,##0",
                includeZero: true,
                suffix: "",
                prefix: "$"
            },
            legend: {
                cursor: "pointer",
                itemclick: toogleDataSeries
            },
            toolTip: {
                shared: true
            },
            data: [{
                type: "area",
                name: "Cost (Chi)",
                markerSize: 5,
                showInLegend: true,
                xValueFormatString: "MM/DD/YYYY hh:mm tt",
                yValueFormatString: "$#,##0",
                dataPoints: cost
            }, {
                type: "area",
                name: "Income (Thu)",
                markerSize: 5,
                showInLegend: true,
                xValueFormatString: "MM/DD/YYYY hh:mm tt",
                yValueFormatString: "$#,##0",
                dataPoints: income
            }]
        };
        $("#chartContainer").CanvasJSChart(options);
}

    

    

function initChart() {
    $.ajax({
        type: "GET",
        url: "/Index?handler=Filter",
        data: { "AccountId": $("#AccountId").val() },
        success: function (response) {
            var cost = [];
            var income = [];
            response.forEach(function (item) {
                if (item.type == true) {
                    income.push(item);
                } else {
                    cost.push(item);
                }
            });
            console.log(response);
            console.log(income);
            console.log(cost);
            console.log("render pie chart finish");
            renderPieChart("#chartCostContainer", cost, "Cost (chi)");
            renderPieChart("#chartIncomeContainer", income, "Income (Thu)");
        }
    });

    $.ajax({
        type: "GET",
        url: "/Index?handler=TranFilter",
        data: { "AccountId": $("#AccountId").val() },
        success: function (response) {
            var cost = [];
            var income = [];
            response.forEach(function (item) {
                if (item.type == true) {
                    income.push(item);
                } else {
                    cost.push(item);
                }
            });
            console.log(response);
            console.log(income);
            console.log(cost);
            console.log("render area chart finish");
            //renderPieChart("#chartCostContainer", cost, "Cost (chi)");
            //renderPieChart("#chartIncomeContainer", income, "Income (Thu)");
            renderAreaChart(cost, income);
        }
    });
}