$(document).ready(function () {

    
    $("#searchInput").on("keyup", function () {
        let searchString = $("#searchInput").val();
        if (searchString.length > 1) {
            SearchForUser(searchString);
            $("#searchResultBox").show();

            setTimeout(function () {
                $("#showAllUserResults").on("click", function () {
                    ClickShowAllResults();
                });

                $(".searchUserItem").on("click", function () {
                    let clickedUsername = $(this).find('.searchUserUsernameSpan').html();
                    ClickUser(clickedUsername);
                });
            }, 75);
            


        }
        else {
            $("#searchResultBox").hide();
        }
        
    });





});



function ClickShowAllResults() {
    let searchString = $("#searchInput").val();
    window.location.href = "/Home/UserShowAll?searchString=" + searchString;
}

function ClickUser(username) {
    window.location.href = "/User/Profile?username=" + username;
};

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

            //<a href="/Home/UserShowAll?searchString=' + searchString + '" >Show all results</a>
            outputBox.append(
                  '<span id="showAllUserResults" class="searchAllResultsSpan">Show all results</span>'
                + '<hr />'
                );
            

            if (userList.length > 0) {
                for (let i = 0; i < userList.length; i++) {
                    outputBox.append(
                          '<div id="' + userList[i].Username + '" class="searchUserItem">'
                            + '<div class="">'
                                + '<span><img src="' + userList[i].ProfilePic + '" class="searchUserProfilePic" alt="fixplz" /></span>'
                                + '<span><img src="/Images/Country/' + userList[i].Country + '.png" class="searchUserCountryPic" alt="fixplz" /></span>'
                                + '<span class="searchUserUsernameSpan">' + userList[i].Username + '</span>'
                                + '<span class="searchUserRankSpan"><img src="/Images/Rank/' + userList[i].RankString + '.png" class="searchUserRankPic" alt="fixplz" /></span>'
                            + '</div>'
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