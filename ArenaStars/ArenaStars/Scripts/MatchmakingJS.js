var timeSearched = 0;

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
        console.log("You have started the timer interval...");
        setInterval(function () {
            console.log("Interval is still going...");
        }, 1000);
        StartMatchmakingSearch();
    });

    $("#stopMatchmakingButton").on("click", function () {
        CancelMatchSearch();
    });

});



function MatchmakingStarted() {

};


function StartSearch() {

    //To use some sort of interval. Starts if sessionStorage.isSearchingForGame is true or matchmakingButton is pressed, can be cancelled in CancelSearch().

    //var refresher = setInterval(function () {

    //    UpdateShit(refresher);

    //}, 300);
};


function StartMatchmakingSearch() {

    $.ajax({
        type: 'get',
        url: '/Home/StartMatchMakeSearch/',
        dataType: 'json',
        data: {},
        success: function (data) {
            let errorList = data.errors;

            let outputErrorBox = $("#matchmakingErrorBox");
            outputErrorBox.html("");

            if (errorList.length == 0)
            {
                console.log("You are now searching for a game...");
                sessionStorage.isSearchingForGame = true;
                sessionStorage.timeSearched = 0;

                $("#matchmakingButton").hide();
                $("#stopMatchmakingButton").show();
            }
            else
            {
                for (let i = 0; i < errorList.length; i++)
                {
                    outputErrorBox.append(
                        errorList[i] + '<br />'
                        );
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
            let goodPopup = $("#goodPopupBox");
            
            if (errorList.length == 0) {
                $("#stopMatchmakingButton").hide();
                $("#matchmakingButton").show();

                sessionStorage.isSearchingForGame = false;
                sessionStorage.timeSearched = 0;

                goodPopup.html("");
                goodPopup.append(
                    'You have exited the queue!'
                    );
                goodPopup.show();

                setTimeout(function () {
                    goodPopup.fadeOut();
                }, 3000);


            }
            else {
                for (let i = 0; i < errorList.length; i++)
                {
                    outputErrorBox.append(
                        errorList[i] + '<br />'
                    );
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