$(document).ready(function () {

    $(".logInBtn").on("click", function () {
        $("#login-modal").modal("show")
        $("#register-modal").modal("hide")
    });

    $(".registerBtn").on("click", function () {
        $("#register-modal").modal("show")
        $("#login-modal").modal("hide")
    });

    $("#registerSubmit").on("click", function () {
        let uname = $("#registerInputUsername").val();
        let email = $("#registerInputEmail").val();
        let pass = $("#registerInputPassword").val();
        let pass2 = $("#registerInputPassword2").val();

        SubmitRegister(uname, email, pass, pass2);
    })

});


function SubmitRegister(uname, email, pass, pass2) {

    $.ajax({

        url: '/User/Register/',
        dataType: 'json',
        data: {
            username: uname,
            email: email,
            password: pass,
            password2: pass2
        },
        success: function (data) {
            var errors = data.errorList;


            
        },
        error: function (jqXHR, statusText, errorThrown) {
            $('#coinflipGameList').html('Ett fel inträffade: <br>'
                + statusText);
        }
    });
}