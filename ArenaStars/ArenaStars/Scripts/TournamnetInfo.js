$(document).ready(function () {
    $(".tournamentList").on("click", function () {
        var id = $(this).attr("id");
        window.location.href = '/Tournament/TournamentInfo?id=' + id;
    });

    //On click, go to users profile
    $(".tUsername").on("click", function () {
        var username = $(this).attr("id");
        window.location.href = '/User/Profile?username=' + username;
    });

    $(".joinBtn").on("click", function () {
        var id = $(this).attr("id");
        $.ajax({
            type: 'post',
            url: '/Tournament/JoinTournament/',
            dataType: 'json',
            data: {
                pId: id,
            },
            success: function (data) {
                let info = data.info;
                var rankPic = "/Images/Rank/" + info.Rank + ".png";

                var html =
                '<div>'
                    + '<span class="bold">Player ' + info.Count + '</span>'
                    + '<span id="' + info.Username + '" class="tUsername">' + info.Username + " " + '<img src=' + rankPic + '></span>'
                + '</div>'
                + '<hr />'

                if (info.IntRank < info.MinRank) {
                    $('.btn.btn-success.joinBtn').css("display", "none");
                    $('#errorBtn').css("display", "initial");
                    $('#errorBtn').html("TOO LOW RANK");
                }
                else if (info.IntRank > info.MaxRank) {
                    $('.btn.btn-success.joinBtn').css("display", "none");
                    $('#errorBtn').css("display", "initial");
                    $('#errorBtn').html("TOO HIGH RANK");
                }
                else if (info.Count == 1) {
                    $('.btn.btn-success.joinBtn').css("display", "none");
                    $('.btn.btn-danger.leaveBtn').css("display", "initial");
                    $("#firstJoinedUser").html("");
                    $(html).hide().prependTo("#newParticipant").fadeIn(300);
                }
                else {
                    $('.btn.btn-success.joinBtn').css("display", "none");
                    $('.btn.btn-danger.leaveBtn').css("display", "initial");
                    $(html).hide().prependTo("#newParticipant").fadeIn(300);
                }

                setTimeout(function () {
                    //On click, go to users profile
                    $(".tUsername").on("click", function () {
                        var username = $(this).attr("id");
                        window.location.href = '/User/Profile?username=' + username;
                    });
                }, 150);
            },
            error: function (jqXHR, statusText, errorThrown) {
                console.log('Ett fel inträffade: ' + statusText);
            }
        });
    });

    $(".leaveBtn").on("click", function () {
        var id = $(this).attr("id");
        $.ajax({
            type: 'post',
            url: '/Tournament/LeaveTournament/',
            dataType: 'json',
            data: {
                pId: id,
            },
            success: function () {
                location.reload();
            },
            error: function (jqXHR, statusText, errorThrown) {
                console.log('Ett fel inträffade: ' + statusText);
            }
        });
    });
});