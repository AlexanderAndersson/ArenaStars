var shownGames = 0;

$(document).ready(function () {
    console.log("GetGamesJS script file loaded!");
    $("#showMoreGamesButton").on("click", function () {
        GetGames();
    });


});


function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
};

function GetGames() {

    $.ajax({
        type: 'post',
        url: '/User/GetGames/',
        dataType: 'json',
        data: {
            shown: shownGames,
            username: getParameterByName('username', window.location.href)
        },
        success: function (data) {
            let gameList = data.gameList;

            let outputGames = $("#gameHistory");

            for (let i = 0; i < gameList.length; i++) {
                outputGames.append(
                    '<div class="game-small">'
                        + '<span class="singleGame">'
                            + 'Game type: ' + gameList[i].Type + '<br />'
                            + '<b>' + gameList[i].ParticipantOne + '</b>' + ' vs ' + '<b>' + gameList[i].ParticipantTwo + '</b><br />'
                            + 'winner: <i>' + gameList[i].Winner + '</i>' 
                        + '</span>'
                    + '</div>'
                    );
                shownGames++;
            }

        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
        }
    });
};
