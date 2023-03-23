
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
        }
    });
}