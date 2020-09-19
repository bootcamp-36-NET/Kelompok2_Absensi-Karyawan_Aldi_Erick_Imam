$(document).ready(function () {
    debugger;
    var id = $("#idUser").text();
    // console.log(id);
    if (id === "") {
        $("#nameUser").text("Guest");
    }
    else {
        $.getJSON("Employees/Load/" + id, function (data) {
            $("#nameUser").text(data.name);
        });
    }
});