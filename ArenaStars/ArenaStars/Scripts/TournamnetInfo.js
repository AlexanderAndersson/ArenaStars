$(document).ready(function () {
    $(".tournamentList").on("click", function () {
        var id = $(this).attr("id");
        window.location.href = '/Tournament/TournamentInfo?id=' + id;
    });
});