var shownTournaments = 0;

var monthsShort = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

$(document).ready(function () {

    GetTournaments();

    $("#showMoreTournamentsButton").on("click", function () {
        GetTournaments();
    });

    setTimeout(function () {
        $(".tournamentList").on("click", function () {
            var id = $(this).attr("id");
            window.location.href = '/Tournament/TournamentInfo?id=' + id;
        });
    }, 200);
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

            let outputTournaments = $(".newTournaments");


            for (let i = 0; i < tournamentList.length; i++)
            {
                outputTournaments.append(
                    '<div id="' + tournamentList[i].Id + '"class="tournamentList">'
                    + '<div>'
                        + '<span class="bold">STARTS</span><br />'
                        + '<span>' + GetDay(tournamentList[i].StartDate) + ' ' + monthsShort[GetMonth(tournamentList[i].StartDate) - 1].toLowerCase() + ' - ' + GetHourAndMinute(tournamentList[i].StartDate) + '</span>'
                    + '</div>'
                    + '<div>'
                        + '<span class="bold">NAME</span><br />'
                        + '<span>' + tournamentList[i].Name + '</span>'
                    + '</div>'
                    + '<div>'
                        + '<span class="bold">TYPE</span><br />'
                        + '<span>' + tournamentList[i].Type + '</span>'
                    + '</div>'
                    + '<div>'
                        + '<span class="bold">MIN RANK</span><br />'
                        + '<span>' + tournamentList[i].MinRank + '</span>'
                    + '</div>'
                    + '<div id="participants">'
                        + '<span class="glyphicon glyphicon-user"></span><br />'
                        + '<span>' + tournamentList[i].ParticipantsCount + ' / ' + tournamentList[i].PlayerLimit + '</span>'
                    + '</div>'

                    );
                shownTournaments++;

                setTimeout(function () {
                    $(".tournamentList").on("click", function () {
                        var id = $(this).attr("id");
                        window.location.href = '/Tournament/TournamentInfo?id=' + id;
                    });
                }, 300);

            }

        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
            console.log("jqXHR: " + jqXHR);
            console.log("errorThrown: " + errorThrown);
        }
    });
};

function GetDay(date) {
    return Number(date.substring(8, 10));
};

function GetMonth(date) {
    return Number(date.substring(5, 7));
};

function GetHourAndMinute(date) {
    return date.substring(11, 16);
};
