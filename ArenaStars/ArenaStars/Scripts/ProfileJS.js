$(document).ready(function () {

    SetWinsLossesColor();


    $("#editBio").on("click", function () {
        alert("Feature not implemented yet!");
    });

});



function SetWinsLossesColor() {
    let games = $("#lastFiveGamesScore");
    let char = "";
    let winLossCharacters = "";
    let gamesArray = [];

    for (let i = 0; i < games.html().length; i++) {
        winLossCharacters += games.html()[i];
    }

    games.html("");

    for (let i = 0; i < winLossCharacters.length; i++) {
        char = winLossCharacters[i];

        if (char === "W") {
            char = '<span class="text-success">W</span>';
            gamesArray.push(char);
        }
        else if (char === "L") {
            char = '<span class="text-danger">L</span>';
            gamesArray.push(char);
        }
        else {
            char = '<span class="">-</span>';
            gamesArray.push(char);
        }
    }

    for (let i = 0; i < gamesArray.length; i++) {
        games.html(games.html()
            + gamesArray[i]
            + " "
            );
    }

};