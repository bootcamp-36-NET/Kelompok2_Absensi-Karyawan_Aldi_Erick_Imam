var table = null;

$(document).ready(function () {
    debugger;
    table = $('#tableHome').DataTable({
        "ajax": {
            'url': "/Absence/Get",
            'type': "GET",
            'dataType': "json",
            'dataSrc': ""
        },
        "columns": [
            {
                "data": "user.userName"
            },
            {
                "data": "user.employee.name"
            },
            {
                "data": "timeIn",
                "render": function (jsonDate) {
                    var date = moment(jsonDate).format("DD MMMM YYYY");
                    return date;
                }
            },
            {
                "data": "timeIn",
                "render": function (jsonDate) {
                    var date = moment(jsonDate).format("HH:mm");
                    return date;
                }
            },
            {
                "data": "timeOut",
                "render": function (jsonDate) {
                    if (!moment(jsonDate).isBefore("1000-01-01")) {
                        var date = moment(jsonDate).format("HH:mm");
                        return date;
                    }
                    return "No Check Out";
                }
            }
        ]

    });
});

var pieDatas;
var label = [];
var datas = [];
var color = [];

function getRandomColor() {
    var letters = '0123456789ABCDEF'.split('');
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
};

$.getJSON("/Absence/PieChart", function (json) {
    debugger;
    pieDatas = json;
    console.log(pieDatas);

    pieDatas.map((item) => {
        label.push(item.division);
        datas.push(item.count);
        color.push(getRandomColor());
    });

    var pieChartCanvas = $('#pieChart').get(0).getContext('2d')
    var pieData = {
        datasets: [{
            data: datas,
            backgroundColor: color
        }],

        // These labels appear in the legend and in the tooltips when hovering different arcs
        labels: label
    };
    var pieOptions = {
        maintainAspectRatio: false,
        responsive: true,
    }
    //Create pie or douhnut chart
    // You can switch between pie and douhnut using the method below.
    var pieChart = new Chart(pieChartCanvas, {
        type: 'pie',
        data: pieData,
        options: pieOptions
    })

});

var barDatas;
var labelBar = [];
var dataBar = [];

$.getJSON("/Absence/BarChart", function (json) {
    debugger;
    barDatas = json;
    console.log(barDatas);

    barDatas.map((item) => {
        labelBar.push(item.dateIn);
        dataBar.push(item.count);
        color.push(getRandomColor());
    });

    var barChartCanvas = $('#barChart').get(0).getContext('2d')
    var barData = {
        labels: labelBar,
        datasets: [{
            label: '# Person Attended',
            data: dataBar,
            backgroundColor: color
        }]
    };

    var options = {
        responsive: false,
        scales: {
            xAxes: [{
                ticks: {
                    maxRotation: 90,
                    minRotation: 80
                }
            }],
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    };
    //Create pie or douhnut chart
    // You can switch between pie and douhnut using the method below.
    var barChart = new Chart(barChartCanvas, {
        type: 'bar',
        data: barData,
        options: options
    });

});