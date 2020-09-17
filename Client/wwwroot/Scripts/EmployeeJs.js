var table = null;

$(document).ready(function () {
    //debugger;
    table = $("#employee").DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {

            url: "/employees/LoadEmploy",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                "data": "employeeId",
                render: function (data, type, row, meta) {
                    //console.log(row);
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "name" },
            { "data": "phone" },
            { "data": "address" },
            {
                "data": "createDate",
                'render': function (jsonDate) {
                    var date = new Date(jsonDate);
                    if (date.getFullYear() != 0001) {
                        return moment(date).format('lll')
                    }
                    return date;
                }
            },
            {
                "sortable": false,
                "render": function (data, type, row, meta) {
                    //console.log(row);
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<button class="btn btn-outline-info btn-circle" data-placement="left" data-toggle="tooltip" data-animation="false" title="Detail" onclick="return GetById(' + meta.row + ')" ><i class="fa fa-lg fa-info"></i></button>'
                        + '&nbsp;' +
                        '<button class="btn btn-outline-warning btn-circle" data-placement="right" data-toggle="tooltip" data-animation="false" title="Update" onclick="return GetByIdUD(' + meta.row + ')" ><i class="fa fa-lg fa-edit"></i></button>'
                    //'<button class="btn btn-outline-warning btn-circle" data-placement="left" data-toggle="tooltip" data-animation="false" title="edit" onclick="return getbyid(' + row.id + ')" ><i class="fa fa-lg fa-edit"></i></button>'
                    //    + '&nbsp;'
                    //    +
                }
            }
        ]
    });
});

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#update').hide();
    $('#add').show();
}

//function Delete(number) {
//    var id = table.row(number).data().employeeId;
//    Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this!",
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!',
//    }).then((resultSwal) => {
//        if (resultSwal.value) {
//            //debugger;
//            $.ajax({
//                url: "/employees/Delete/",
//                data: { Id: id }
//            }).then((result) => {
//                //debugger;
//                if (result.statusCode == 200) {
//                    //debugger;
//                    Swal.fire({
//                        position: 'center',
//                        icon: 'success',
//                        title: 'Delete Successfully',
//                        showConfirmButton: false,
//                        timer: 1500,
//                    });
//                    table.ajax.reload(null, false);
//                } else {
//                    Swal.fire('Error', 'Failed to Delete', 'error');
//                    ClearScreen();
//                }
//            })
//        };
//    });
//}

function Update() {
    debugger;
    var Emp = new Object();
    Emp.employeeId = $('#Id').val();
    Emp.name = $('#Name').val();
    Emp.phone = $('#Phone').val();
    Emp.address = $('#Address').val();  
    
    $.ajax({
        type: 'POST',
        url: "/employees/InsertOrUpdate/",
        cache: false,
        dataType: "JSON",
        data : Emp
    }).then((result) => {
        debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data Updated Successfully',
                showConfirmButton: false,
                timer: 1500,
            });
            table.ajax.reload(null, false);
        } else {
            Swal.fire('Error', 'Failed to Update', 'error');
            ClearScreen();
        }
    })
}

function GetById(number) {
    //debugger;
    //console.log(table.row(number).data());
    var id = table.row(number).data().employeeId;
    $.ajax({
        url: "/employees/GetById/",
        data: { Id: id }
    }).then((result) => {
        //debugger;
        $('#IdEmp').text(result.employeeId);
        $('#NameEmp').text(result.name);
        $('#AddressEmp').text(result.address);
        $('#PhoneEmp').text(result.phone);

        var date = new Date(result.createDate);
        $('#HireDate').text(moment(date).format('lll'));

        //$('#add').hide();
        //$('#update').show();
        $('#myModal').modal('show');
    })
}

function GetByIdUD(number) {
    //debugger;
    //console.log(table.row(number).data());
    var id = table.row(number).data().employeeId;
    $.ajax({
        url: "/employees/GetById/",
        data: { Id: id }
    }).then((result) => {
        //debugger;
        $('#Id').val(result.employeeId);
        $('#Name').val(result.name);
        $('#Address').val(result.address);
        $('#Phone').val(result.phone);

        

        //$('#add').hide();
        $('#update').show();
        $('#updateEmployee').modal('show');
    })
}