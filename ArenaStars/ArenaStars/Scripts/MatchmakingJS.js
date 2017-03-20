var timeSearched = 0;
var gameId = 0;
var matchmakingInterval = null;

$(document).ready(function () {

    if (sessionStorage.timeSearched) {
        timeSearched = sessionStorage.timeSearched;
    }
    else {
        timeSearched = 0;
    }

    if (sessionStorage.isSearchingForGame) {
        if (sessionStorage.isSearchingForGame == 'true') {
            console.log("isSearchingForGame IS true");
            $("#matchmakingButton").hide();
            $("#stopMatchmakingButton").show();

            matchmakingInterval = setInterval(function () {
                CheckIfFoundGame();
                MatchmakingSearcher();
            }, 5000);
        }
        else {
            $("#matchmakingButton").show();
        }
    }
    else {
        $("#matchmakingButton").show();
    }

    console.log("MatchmakingJS.js LOADED!");

    $("#matchmakingButton").on("click", function () {
        StartMatchmakingSearch();
    });

    $("#stopMatchmakingButton").on("click", function () {
        CancelMatchSearch();
    });
});


function CheckIfFoundGame() {

    $.ajax({
        type: 'post',
        url: '/Home/CheckIfFoundGame/',
        dataType: 'json',
        data: {},
        success: function (data) {
            let response = data.response;

            console.log("CIFG foundGame: " + response.foundGame);
            console.log("CIFG gameId: " + response.gameId);
            if (response.foundGame == true) {
                console.log("Match found!");
                sessionStorage.isSearchingForGame = false;
                sessionStorage.timeSearched = 0;
                clearInterval(matchmakingInterval);
                window.location.href = "/Home/GameRoom?gameId=" + response.gameId;
            }
        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
            console.log("jqXHR: " + jqXHR);
            console.log("errorThrown: " + errorThrown);
        }
    });
};


function MatchmakingSearcher() {

    $.ajax({
        type: 'post',
        url: '/Home/MatchMakeSearch/',
        dataType: 'json',
        data: {
            timeSearched: timeSearched
        },
        success: function (data) {
            let searchData = data.searchData;
            let errors = searchData.errors;

            if (errors.length == 0) {
                console.log("Match found!");
                sessionStorage.isSearchingForGame = false;
                sessionStorage.timeSearched = 0;
                clearInterval(matchmakingInterval);
                window.location.href = "/Home/GameRoom?gameId=" + searchData.gameId;
            }
            else {
                for (let i = 0; i < errors.length; i++)
                {
                    console.log(errors[i]);
                }
                timeSearched += 5;
            }
        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
            console.log("jqXHR: " + jqXHR);
            console.log("errorThrown: " + errorThrown);
        }
    });
};


function StartMatchmakingSearch() {

    $.ajax({
        type: 'get',
        url: '/Home/StartMatchMakeSearch/',
        dataType: 'json',
        data: {},
        success: function (data) {
            let errorList = data.errors;

            let outputErrorBox = $("#alreadyInQueuePopUp");
            let outputStartBow = $("#enterQueuePopUp");
            outputErrorBox.html("");
            outputStartBow.html("");

            if (errorList.length == 0)
            {
                $("#exitQueuePopUp").css("display", "none");

                outputStartBow.append(
                'Searching for game'
                ).fadeIn();

                setTimeout(function () {
                    outputStartBow.fadeOut();
                }, 3000);

                console.log("You are now searching for a game...");
                sessionStorage.isSearchingForGame = true;
                sessionStorage.timeSearched = 0;

                $("#matchmakingButton").hide();
                $("#stopMatchmakingButton").show();

                matchmakingInterval = setInterval(function () {
                    CheckIfFoundGame();
                    MatchmakingSearcher();
                }, 5000);
            }
            else
            {
                for (let i = 0; i < errorList.length; i++)
                {
                    outputErrorBox.append(
                        errorList[i] + '<br />'
                    ).fadeIn();

                    setTimeout(function () {
                        outputErrorBox.fadeOut();
                    }, 3000);
                }
            }
        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
            console.log("jqXHR: " + jqXHR);
            console.log("errorThrown: " + errorThrown);
        }
    });
};

function CancelMatchSearch() {

    $.ajax({
        type: 'get',
        url: '/Home/StopMatchMakeSearch/',
        dataType: 'json',
        data: {},
        success: function (data) {
            let errorList = data.errors;

            let outputErrorBox = $("#matchmakingErrorBox");
            outputErrorBox.html("");
            let goodPopup = $("#exitQueuePopUp");
            
            if (errorList.length == 0) {
                $("#stopMatchmakingButton").hide();
                $("#matchmakingButton").show();

                clearInterval(matchmakingInterval);

                sessionStorage.isSearchingForGame = false;
                sessionStorage.timeSearched = 0;

                console.log("Exited the queue");

                $("#enterQueuePopUp").css("display", "none");
                goodPopup.html("");
                goodPopup.append(
                    'You have exited the queue!'
                    ).fadeIn();

                setTimeout(function () {
                    goodPopup.fadeOut();
                }, 3000);
            }
            else {
                for (let i = 0; i < errorList.length; i++)
                {
                    outputErrorBox.append(
                        errorList[i] + '<br />'
                    ).fadeIn();

                    setTimeout(function () {
                        outputErrorBox.fadeOut();
                    }, 3000);
                }
            }         
        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
            console.log("jqXHR: " + jqXHR);
            console.log("errorThrown: " + errorThrown);
        }
    });
};

function MatchSearch(timeSearched) {

};