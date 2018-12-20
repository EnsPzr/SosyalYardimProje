$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/IhtiyacSahibi/TumIhtiyacSahipleriniGetir",
        data: "",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.BasariliMi) {
                Liste = "";
                if (parseInt(result.IhtiyacSahibiSayisi) == 0) {

                    $('#myModal').modal('hide');
                } else {
                    for (var i = 0; i < parseInt(result.IhtiyacSahibiSayisi); i++) {
                        if (parseInt(i) % 2 == 0) {
                            Liste = Liste + "<tr class='even'>" +
                                "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiAdiSoyadi + "</td>" +
                                "<td class='col-md-1 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiTelNo + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.IhtiyacSahibiKontrolListe[i].MuhtacMi + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].EklenmeTarihiStr + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].KontrolTarihStr + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].TahminiTeslimTarihiStr + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.IhtiyacSahibiKontrolListe[i].TeslimTamamlandiMi + "</td>" +
                                "<td class='col-md-1 col-xs-1'><a class='glyphicon glyphicon-pencil' href='/IhtiyacSahibi/Kontrol/" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiKontrolId + "'></a></td ></tr > ";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiAdiSoyadi + "</td>" +
                                "<td class='col-md-1 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiTelNo + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.IhtiyacSahibiKontrolListe[i].MuhtacMi + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].EklenmeTarihiStr + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].KontrolTarihStr + "</td>" +
                                "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].TahminiTeslimTarihiStr + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.IhtiyacSahibiKontrolListe[i].TeslimTamamlandiMi + "</td>" +
                                "<td class='col-md-1 col-xs-1'><a class='glyphicon glyphicon-pencil' href='/IhtiyacSahibi/Kontrol/" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiKontrolId + "'></a></td ></tr > ";
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
            aranan: $("#ara").val(),
            sehirId: $("#sehirId").val(),
            tarih:$("#tarih").val()
        };
        $.ajax({
            type: "GET",
            url: "/IhtiyacSahibi/FiltreliKontrolIhtiyacSahipleriniGetir",
            data: data,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.BasariliMi) {
                    Liste = "";
                    if (parseInt(result.IhtiyacSahibiSayisi) == 0) {
                        $('#myModal').modal('hide');
                        $("#tablo_tbody").html("");
                        var yazilacak = "<tr class='even'>" +
                            "<td class='col-md-12 col-xs-12'> Filtreniz ile eşleşen ihtiyaç sahibi bulunamadı. </td></tr>";
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(yazilacak);
                    } else {
                        for (var i = 0; i < parseInt(result.IhtiyacSahibiSayisi); i++) {
                            if (parseInt(i) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiAdiSoyadi + "</td>" +
                                    "<td class='col-md-1 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiTelNo + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.IhtiyacSahibiKontrolListe[i].MuhtacMi + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].EklenmeTarihiStr + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].KontrolTarihStr + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].TahminiTeslimTarihiStr + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.IhtiyacSahibiKontrolListe[i].TeslimTamamlandiMi + "</td>" +
                                    "<td class='col-md-1 col-xs-1'><a class='glyphicon glyphicon-pencil' href='/IhtiyacSahibi/Kontrol/" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiKontrolId + "'></a></td ></tr > ";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiAdiSoyadi + "</td>" +
                                    "<td class='col-md-1 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiTelNo + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.IhtiyacSahibiKontrolListe[i].MuhtacMi + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].EklenmeTarihiStr + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].KontrolTarihStr + "</td>" +
                                    "<td class='col-md-2 col-xs-2'>" + result.IhtiyacSahibiKontrolListe[i].TahminiTeslimTarihiStr + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.IhtiyacSahibiKontrolListe[i].TeslimTamamlandiMi + "</td>" +
                                    "<td class='col-md-1 col-xs-1'><a class='glyphicon glyphicon-pencil' href='/IhtiyacSahibi/Kontrol/" + result.IhtiyacSahibiKontrolListe[i].IhtiyacSahibiKontrolId + "'></a></td ></tr > ";
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