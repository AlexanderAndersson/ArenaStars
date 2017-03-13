$(document).ready(function () {
    moment.locale('en', {
        week: {
            dow: 1 // Monday is the first day of the week
        }
    });

    $(function () {
        $('#datetimepicker1').datetimepicker({
            format: 'MM/DD/YYYY HH:mm'
        });
    });

    $(function () {
        $('#datetimepicker2').datetimepicker({
            format: 'MM/DD/YYYY HH:mm',
        });
    });
});

$("#addTournamentBtn").on("click", function () {
    var typeVal = document.getElementById("type");
    var minRankVal = document.getElementById("minRank");
    var maxRankVal = document.getElementById("maxRank");

    let name = $("#name").val();
    let playerlimit = $("#playerLimit").val();
    let startdate = $("#datetimepicker1").val();
    let checkin = $("#datetimepicker2").val();
    let type = typeVal.options[typeVal.selectedIndex].value;
    let minrank = minRankVal.options[minRankVal.selectedIndex].value;
    let maxrank = maxRankVal.options[maxRankVal.selectedIndex].value;
   
    $('#tForm').find('input').val('');
    $('#tForm').find('select').val('');

    AddTournament(name, playerlimit, startdate, checkin, type, minrank, maxrank);
});

function AddTournament(name, playerlimit, startdate, checkin, type, minrank, maxrank) {
    $.ajax({
        type: 'post',
        url: '/Admin/AddTournament/',
        dataType: 'json',
        data: {
            pName: name,
            pStartDate: startdate,
            pCheckIn: checkin,
            pPlayerLimit: playerlimit,
            pType: type,
            pMinRank: minrank,
            pMaxRank: maxrank
        },
        success: function (data) {
            let newT = data.newT;
            var newdate = newT.StartDate;
            var startDate = new Date(parseInt(newdate.replace('/Date(', '')));
            locale = "en-us";
            var date = startDate.getDate();
            var month = startDate.toLocaleString(locale, { month: "short" }).toLowerCase();
            var time = startDate.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

            var html =
                '<div id="tournamentList">'
                    + '<div>'
                        + '<span class="bold">STARTS</span>' + '<br />'
                        + '<span>' + date + ' ' + month + ' - ' + time + '</span>'
                    + '</div>'
                    + '<div>'
                        + '<span class="bold">NAME</span>' + '<br />'
                        + '<span>' + newT.Name + '</span>'
                    + '</div>'
                    + '<div>'
                        + '<span class="bold">TYPE</span>' + '<br />'
                        + '<span>' + newT.Type + '</span>'
                    + '</div>'
                    + '<div>'
                        + '<span class="bold">RANKS</span>' + '<br />'
                        + '<span>' + newT.MinRank + ' - ' + newT.MaxRank + '</span>'
                    + '</div>'
                    + '<div id="participants">'
                        + '<span class="glyphicon glyphicon-user"></span>' + '<br />'
                        + '<span>' + '0 / ' + newT.PlayerLimit + '</span>'
                    + '</div>'
                + '</div>';

            $(html).hide().prependTo("#newTournaments").fadeIn(500);
        },
        error: function (jqXHR, statusText, errorThrown) {
            console.log('Ett fel inträffade: ' + statusText);
        }
    });
};