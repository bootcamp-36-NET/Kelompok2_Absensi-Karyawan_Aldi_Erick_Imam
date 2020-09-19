var table = null;

function callTable() {
    table = $('#myTable').DataTable({
        "ajax": {
            'url': "/Divisions/Load",
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
            { "data": "department.name" },
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
        "order": [[1, 'asc']],

        initComplete: function () {
            this.api().columns(2).every(function () {
                var column = this;
                var select = $('<select><option value="">All Departments</option></select>')
                    .appendTo($(column.header()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        }
    });

    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

}

$('#insertButton').click(function () {
    $('.clearFields').val('');
    $.getJSON('/Departments/Load', { Id: $(this).val() }, function (data) {
        var options = '';
        for (var x = 0; x < data.length; x++) {
            options += '<option value="' + data[x]['id'] + '">' + data[x]['name'] + '</option>';
        }
        $('#departmentId').html(options);
    });
});

$(document).ready(function () {
    debugger;
    $('.select2').select2({
        placeholder: "Select a department",
        allowClear: true
    });
    callTable();
    $.getJSON('/Departments/Load', { Id: $(this).val() }, function (data) {
        var options = '<option></option>';
        for (var x = 0; x < data.length; x++) {
            options += '<option value="' + data[x]['Id'] + '">' + data[x]['Name'] + '</option>';
        }
        $('#departmentId').html(options);
    });
});



$('#insert').click(function () {
    debugger;
    $.ajax({
        url: "/Divisions/Insert/" + $('#divisionId').val(),
        type: "post",
        data: {
            'name': $('#divisionName').val(),
            'departmentId': $('#departmentId').children("option:selected").val()
            
        },
        dataType: "json",
        success: function (response) {
            if (response.success === true) {
                swal.fire("Success!", "Data is inserted", "success");
                table.ajax.reload();
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
        url: "/Divisions/Load/" + id,
        type: "get",
        success: function (response) {
            $('#divisionId').val(response.id);
            $('#divisionName').val(response.name);
            $('#departmentId').val(response.department.id);
            $('#departmentId').trigger('change');
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
                url: "/Divisions/Delete/" + id,
                type: "post",
                success: function (result) {

                }
            });
            swal.fire("Your file has been deleted!", {
                icon: "success",
            });
            table.ajax.reload();
        } else {

        }
    });
};



$('#excel').click(function () {
    debugger;
    $.ajax({
        url: "/Reports/DivisionExcel/",
        type: "get",
        success: function (response) {
            window.location = "/Reports/DivisionExcel/";
        },
        error: function () {
            swal.fire("Error!", "Excel is not downloaded", "error");
        }
    });

});