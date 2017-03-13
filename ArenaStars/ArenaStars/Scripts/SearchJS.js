$(document).ready(function () {

    console.log("SearchJS.js LOADED!");
    $("#searchInput").on("keyup", function () {
        let searchString = $("#searchInput").val();
        if (searchString.length > 1) {
            SearchForUser(searchString);
            $("#searchResultBox").show();
        }
        else {
            $("#searchResultBox").hide();
        }
        
    });


});


function SearchForUser(searchString) {

    $.ajax({
        type: 'post',
        url: '/Home/UserSearch/',
        dataType: 'json',
        data: {
            searchString: searchString
        },
        success: function (data) {
            let userList = data.userList;

            let outputBox = $("#searchResultBox");
            outputBox.html("");

            if (userList.length > 0) {
                for (let i = 0; i < userList.length; i++) {
                    outputBox.append(
                          '<div class="searchUserItem col-lg-12">'
                        + '<span><img src="' + userList[i].ProfilePic + '" class="img-responsive searchUserPic" alt="fixplz"></span>'
                        + '<span><img src="/Images/Country/' + userList[i].Country + '.png" class="img-responsive" alt="fixplz"</span>'
                        + '<span>' + userList[i].Username + '</span>'
                        + '<span>' + userList[i].RankString + '</span>'
                        + '</div>'
                        );
                }
            }
            else {
                outputBox.append('No user with that username.');
            }

            
        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
            console.log("jqXHR: " + jqXHR);
            console.log("errorThrown: " + errorThrown);
        }
    });

};