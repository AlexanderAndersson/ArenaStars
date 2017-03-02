var shownTournaments = 0;

$(document).ready(function () {

    console.log("GetTournamentJS Script file loaded!");
    //GetTournaments();

    $("#showMoreTournamentsButton").on("click", function () {
        GetTournaments();
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

function GetTournaments() {

    $.ajax({
        type: 'post',
        url: '/User/GetTournaments/',
        dataType: 'json',
        data: {
            shown: shownTournaments,
            username: getParameterByName('username', window.location.href)
        },
        success: function (data) {
            let tournamentList = data.tournamentList;

            let outputTournaments = $("#tournamentHistory");
            
            for (let i = 0; i < tournamentList.length; i++)
            {
                outputTournaments.append(
                    '<div class="tournament-small">'
                        + '<span class="singleTournament">'
                            + '<b>' + tournamentList[i].Name + '</b>'
                        + '</span>'
                    + '</div>'
                    );
                shownTournaments++;
            }

        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
            console.log("jqXHR: " + jqXHR);
            console.log("errorThrown: " + errorThrown);
        }
    });
};
