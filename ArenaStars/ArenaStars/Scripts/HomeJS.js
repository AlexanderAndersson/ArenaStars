$(document).ready(function () {
    //On click, go to specific tournament
    $(".tournamentList").on("click", function () {
        var id = $(this).attr("id");
        window.location.href = '/Tournament/TournamentInfo?id=' + id;
    });

    $("#matchmakeLogin").on("click", function () {
        $("#login-modal").modal("show");
    });

    $("#matchRoomButton").on("click", function () {
        let id = $("#hiddenActiveGameId").html();
        window.location.href = "/Home/GameRoom?gameId=" + id;
    });
	
	//On click, go to users profile
    $(".newTopPlayer").on("click", function () {
        var username = $(this).attr("id");
        window.location.href = '/User/Profile?username=' + username;
    });

    //On click, go to users profile
    $(".playerStat").on("click", function () {
        var username = $(this).attr("id");
        window.location.href = '/User/Profile?username=' + username;
    });
});


