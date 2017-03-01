$(document).ready(function () {

    $(".logInBtn").on("click", function () {
        $("#login-modal").modal("show")
        $("#register-modal").modal("hide")
    });

    $(".registerBtn").on("click", function () {
        $("#register-modal").modal("show")
        $("#login-modal").modal("hide")
    });



});
