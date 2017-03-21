var shownGames = 0;

var monthsShort = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

$(document).ready(function () {

    GetGames();

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

            let outputGames = $("#gameList");

            
            for (let i = 0; i < gameList.length; i++) {
                let htmlString = "";

                console.log("Month: " + gameList[i].PlayedDate);
                console.log("month number: " + GetMonth(gameList[i].PlayedDate));

                htmlString += '<hr />'
                    + '<div id="' + gameList[i].Id + '" class="singleGame row">'
                    + '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">'
                        + '<span>' + GetDay(gameList[i].PlayedDate) + ' ' + monthsShort[GetMonth(gameList[i].PlayedDate) - 1].toLowerCase() + ' - ' + GetHourAndMinute(gameList[i].PlayedDate) + '</span>'
                    + '</div>'
                    + '<div class="col-lg-1 col-md-2 col-sm-2 col-xs-2">'
                        + '<span>' + gameList[i].Type + '</span>'
                    + '</div>';

                if (gameList[i].Winner == getParameterByName('username')) {
                    htmlString +=
                          '<div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">'
                            + '<span class="text-success">WIN</span>'
                        + '</div>'
                        + '<div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">'
                            + '<span>' + gameList[i].Kills + ' / ' + gameList[i].Deaths + '</span>'
                        + '</div>';
                }
                else {
                    htmlString +=
                          '<div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">'
                            + '<span class="text-danger">LOSS</span>'
                        + '</div>'
                        + '<div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">'
                            + '<span>' + gameList[i].Deaths + ' / ' + gameList[i].Kills + '</span>'
                        + '</div>';
                }

                htmlString +=
                  '<div class="col-lg-2 col-md-6 col-sm-4 col-xs-4">'
                    + '<span>' + gameList[i].Map + '</span> '
                    + '<span><img src="/Images/Map/' + gameList[i].Map + '.png" alt="map pic" class="img-responsive" /></span>'
                + '</div>'
                + '</div>';

                outputGames.append(htmlString);

                //outputGames.append(
                //      '<div>'
                //        + '<span>' + GetDay(gameList[i].PlayedDate) + ' ' + monthsShort[GetMonth(gameList[i].PlayedDate) - 1].toLowerCase() + ' - ' + GetHourAndMinute(gameList[i].PlayedDate) + '</span>'
                //    + '</div>'
                //    + '<div>'
                //        + '<span>' + gameList[i].Type + '</span>'
                //    + '</div>'
                //    );
                //if (gameList[i].Winner == getParameterByName('username')) {
                //    outputGames.append(
                //          '<div>'
                //            + '<span class="text-success">WIN</span>'
                //        + '</div>');
                //}
                //else {
                //    outputGames.append(
                //          '<div>'
                //            + '<span class="text-danger">LOSS</span>'
                //        + '</div>');
                //}
                    
                shownGames++;
            }

        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
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
