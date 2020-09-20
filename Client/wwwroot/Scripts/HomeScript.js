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
                    var date = moment(jsonDate).format("HH:mm");
                    return date;
                }
            },
            {
                "data": null
            }
        ]

    });
})