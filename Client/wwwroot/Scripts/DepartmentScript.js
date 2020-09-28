var table = null;

function callTable() {
    table = $('#myTable').DataTable({
        "ajax": {
            'url': "/Departments/Load",
            'type': "GET",
            'dataType': "json",
            'dataSrc': ""
        },
        "columns": [
            {
                "width": "5%",
                "data": "id", defaultContent: ''
            },
            { "data": "name" },
            {
                "data": "createdDate",
                "render": function (jsonDate) {
                    var date = moment(jsonDate).format("DD MMMM YYYY HH:mm:ss");
                    return date;
                }
            },
            {
                "data": "updatedDate",
                "render": function (jsonDate) {
                    if (!moment(jsonDate).isBefore("1000-01-01")) {
                        var date = moment(jsonDate).format("DD MMMM YYYY HH:mm:ss");
                        return date;
                    }
                    return "Not updated yet";
                }
            },
            {
                "sortable": false,
                "render": function myFunction(data, type, row) {
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<button id="update" class="btn btn-warning" data-toggle="tooltip" data-placement="left" title="Update" onClick="Update(' + row.id + ')"><i class="fa fa-pen"></i></button>' +
                        '&nbsp;' +
                        '<button id="delete" class="btn btn-danger" data-toggle="tooltip" data-placement="right" title="Delete" onClick="Delete(' + row.id + ')"><i class="fa fa-trash"></i></button>'
                }
            }],
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "order": [[1, 'asc']]
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

$(document).ready(function () {
    debugger;
    callTable();
});

$('#insertButton').click(function () {
    $('.clearFields').val('');
});

$('#insert').click(function () {
    debugger;
    $.ajax({
        url: "/Departments/Insert/" + $('#departmentId').val(),
        type: "post",
        data: {
            'name': $('#departmentName').val()
        },
        dataType: "json",
        success: function (response) {
            if (response.success === true) {
                swal.fire("Success!", "Data is inserted", "success");
                table.ajax.reload(null, false);
                $('.clearFields').val('');
            }
            else {
                swal.fire("Error!", "Data is not inserted", "error");
            }
        },
        error: function () {
            swal.fire("Error!", "Data is not inserted", "error");
        }
    });

});


function Update(id) {
    debugger;
    $('#insertModal').modal('show');
    $.ajax({
        url: "/Departments/Load/" + id,
        type: "get",
        success: function (response) {
            $('#departmentId').val(response.id);
            $('#departmentName').val(response.name);
        }
    });
};

function Delete(id) {
    debugger;
    swal.fire({
        title: "Are you sure?",
        text: "This cannot be undone!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        reverseButtons: true,

    }).then((willDelete) => {
        if (willDelete.isConfirmed) {
            $.ajax({
                url: "/Departments/Delete/" + id,
                type: "post",
                success: function (result) {

                }
            });
            swal.fire("Your file has been deleted!", {
                icon: "success",
            });
            table.ajax.reload(null, false);
        } else {

        }
    });
};