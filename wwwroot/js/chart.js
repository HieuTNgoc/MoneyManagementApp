
function renderPieChart(canvas, data, title) {
    var option = {
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: title
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
                xValueFormatString: "MM/dd/yyyy hh:mm tt",
                yValueFormatString: "$#,##0",
                dataPoints: cost
            }, {
                type: "area",
                name: "Income (Thu)",
                markerSize: 5,
                showInLegend: true,
                xValueFormatString: "MM/dd/yyyy hh:mm tt",
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
            console.log("render chart finish");
            renderPieChart("#chartCostContainer", cost, "Cost (chi)");
            renderPieChart("#chartIncomeContainer", income, "Income (Thu)");
            renderAreaChart(cost, income);
        }
    });
}