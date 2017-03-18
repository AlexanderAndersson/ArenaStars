$(document).ready(function () {
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

});

