$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/BagisciBagis/TumBagislariGetir",
        data: "",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.BasariliMi) {
                Liste = "";
                if (parseInt(result.BagisSayisi) == 0) {
                    $("#tablo_tbody").html("");
                    var yazilacak = "<tr class='even'>" +
                        "<td class='col-md-12 col-xs-12'> Hiç bağış bulunamadı. </td></tr>";
                    $("#tablo_tbody").html("");
                    $("#tablo_tbody").html(yazilacak);
                    $('#myModal').modal('hide');
                } else {
                    for (var i = 0; i < parseInt(result.BagisSayisi); i++) {
                        if (parseInt(i) % 2 == 0) {
                            Liste = Liste + "<tr class='even'>" +
                                "<td class='col-md-3 col-xs-3'>" + result.BagisList[i].EklenmeTarihiStr + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].OnaylandiMiStr + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].TeslimAlindiMi + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].TahminiTeslimAlmaStr + "</td>" +
                                "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/BagisciBagis/Duzenle/" + result.BagisList[i].BagisId + "'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='even'>" +
                                "<td class='col-md-3 col-xs-3'>" + result.BagisList[i].EklenmeTarihiStr + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].OnaylandiMiStr + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].TeslimAlindiMi + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].TahminiTeslimAlmaStr + "</td>" +
                                "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/BagisciBagis/Duzenle/" + result.BagisList[i].BagisId + "'></a></td></tr >";
                        }
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(Liste);
                    };
                    $('#myModal').modal('hide');
                }
            } else {

                $('#myModal').modal('hide');
            }
        },
        error: function (result) {
            Alert("hata oluştu");
            $('#myModal').modal('hide');
        },
        beforeSend: function () {
            $('#myModal').modal('show');
        }
    });
    $("#arama").click(function () {
        $("#aramaSecenekleri").toggle(1000);
        if ($("#arama").hasClass("glyphicon glyphicon-arrow-down")) {
            $("#arama").removeClass("glyphicon glyphicon-arrow-down");
            $("#arama").addClass("glyphicon glyphicon-arrow-up");
        }
        else {
            $("#arama").removeClass("glyphicon glyphicon-arrow-up");
            $("#arama").addClass("glyphicon glyphicon-arrow-down");
        }
    });

    $("#filtrele").click(function () {
        var data = {
            tarih: $("#tarih").val()
        };
        $.ajax({
            type: "GET",
            url: "/BagisciBagis/FiltreliBagislariGetir",
            data: data,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.BasariliMi) {
                    Liste = "";
                    if (parseInt(result.BagisSayisi) == 0) {
                        $("#tablo_tbody").html("");
                        var yazilacak = "<tr class='even'>" +
                            "<td class='col-md-12 col-xs-12'> Filtreniz ile eşleşen bağış bulunamadı. </td></tr>";
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(yazilacak);
                        $('#myModal').modal('hide');
                    } else {
                        for (var i = 0; i < parseInt(result.BagisSayisi); i++) {
                            if (parseInt(i) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.BagisList[i].EklenmeTarihiStr + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].OnaylandiMiStr + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].TeslimAlindiMi + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].TahminiTeslimAlmaStr + "</td>" +
                                    "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/BagisciBagis/Duzenle/" + result.BagisList[i].BagisId + "'></a></td></tr >";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.BagisList[i].EklenmeTarihiStr + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].OnaylandiMiStr + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].TeslimAlindiMi + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.BagisList[i].TahminiTeslimAlmaStr + "</td>" +
                                    "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/BagisciBagis/Duzenle/" + result.BagisList[i].BagisId + "'></a></td></tr >";
                            }
                            $("#tablo_tbody").html("");
                            $("#tablo_tbody").html(Liste);
                        };
                        $('#myModal').modal('hide');
                    }
                } else {

                    $('#myModal').modal('hide');
                }
            },
            error: function (result) {
                Alert("hata oluştu");
                $('#myModal').modal('hide');
            },
            beforeSend: function () {
                $('#myModal').modal('show');
            }
        });
    });
});