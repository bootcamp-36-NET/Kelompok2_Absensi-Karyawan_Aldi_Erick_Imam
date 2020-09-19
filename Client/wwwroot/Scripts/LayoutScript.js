$(document).ready(function () {
    debugger;
    var id = $("#idUser").text();
    // console.log(id);
    if (id === "") {
        $("#nameUser").text("Guest");
    }
    else {
        $.getJSON("/Employees/GetById/" + id, function (data) {
            $("#nameUser").text(data.name);
            $("#nameUser0").text(data.name);
        });
    }
});