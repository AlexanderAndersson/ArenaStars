var updater = null;

$(document).ready(function () {

    SetSelectedOption();

    $("#inputProfilePic").on("keyup", function () {

        if (updater != 'null') {
            clearTimeout(updater);
        }

        var updater = setTimeout(function () {

            UpdateProfilePictures();

        }, 3000);

    });

    $("#inputBackgroundPic").on("keyup", function () {

        if (updater != 'null') {
            clearTimeout(updater);
        }

        var updater = setTimeout(function () {

            UpdateProfilePictures();

        }, 3000);

    });

});



function UpdateProfilePictures() {

    if (updater != 'null') {
        clearTimeout(updater);
    }
    
    let profilePictureInput = $("#inputProfilePic").val();
    let profileBackgroundInput = $("#inputBackgroundPic").val();

    $("#profilePic").attr("src", profilePictureInput);
    $('#profileBackground').css('background-image', 'url(' + profileBackgroundInput + ')');

};

function SetSelectedOption() {
    let countryValue = $("#hiddenCountryValue").html();
    $('#countrySelectList option[value=' + countryValue + ']').attr('selected', 'selected');
};