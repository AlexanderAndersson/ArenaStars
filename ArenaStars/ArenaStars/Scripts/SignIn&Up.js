$(document).ready(function () {

    $(".logInBtn").on("click", function () {
        $("#login-modal").modal("show")
        $("#register-modal").modal("hide")
    });

    $(".registerBtn").on("click", function () {
        $("#register-modal").modal("show")
        $("#login-modal").modal("hide")
    });

    $("#registerSubmitButton").on("click", function () {
        let uname = $("#registerInputUsername").val();
        let email = $("#registerInputEmail").val();
        let pass = $("#registerInputPassword").val();
        let pass2 = $("#registerInputPassword2").val();

        SubmitRegister(uname, email, pass, pass2);
    });

    $("#loginSubmitButton").on("click", function () {
        let uname = $("#loginInputUsername").val();
        let pass = $("#loginInputPassword").val();

        SubmitLogin(uname, pass);
    });

    $(document).on('keypress', function (e) {
        let tag = e.target.tagName.toLowerCase();
        if (e.which === 13 && tag == 'input' && tag != 'textarea')
            if ($('#register-modal').is(':visible')) {
                let uname = $("#registerInputUsername").val();
                let email = $("#registerInputEmail").val();
                let pass = $("#registerInputPassword").val();
                let pass2 = $("#registerInputPassword2").val();

                SubmitRegister(uname, email, pass, pass2);
            }
            else if ($('#login-modal').is(':visible')) {
                let uname = $("#loginInputUsername").val();
                let pass = $("#loginInputPassword").val();

                SubmitLogin(uname, pass);
            }
    });

});


function SubmitRegister(uname, email, pass, pass2) {

    $.ajax({
        type: 'post',
        url: '/User/Register/',
        dataType: 'json',
        data: {
            username: uname,
            email: email,
            password: pass,
            password2: pass2
        },
        success: function (data) {
            var errorList = data.errorList;

            let errorOutput = $("#errorOutputRegister"); //Gets div that errors display in.
            errorOutput.html(""); //Clears div from previous errors.

            //Cycles through all the errors and appends each of them to the error div.
            for (let i = 0; i < errorList.length; i++) {
                errorOutput.append('<span class="errorMessage">' + errorList[i] + '</span><br />')
            }

            if (errorList.length == 0) {
                location.reload(); //Refreshes page.
            }
            else {
                errorOutput.append("<br /><br />");
            }

                
        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
        }
    });
};

function SubmitLogin(uname, pass) {

    $.ajax({
        type: 'post',
        url: '/User/Login/',
        dataType: 'json',
        data: {
            username: uname,
            password: pass
        },
        success: function (data) {
            var errorList = data.errorList;

            let errorOutput = $("#errorOutputLogin"); //Gets div that errors display in.
            errorOutput.html(""); //Clears div from previous errors.

            //Cycles through all the errors and appends each of them to the error div.
            for (let i = 0; i < errorList.length; i++) {
                errorOutput.append('<span class="errorMessage">' + errorList[i] + '</span><br />')
            }

            if (errorList.length == 0) {
                location.reload(); //Refreshes page.
            }
                
        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
        }
    });
};