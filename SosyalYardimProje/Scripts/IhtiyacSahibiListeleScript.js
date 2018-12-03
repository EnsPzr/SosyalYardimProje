$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/IhtiyacSahibi/IhtiyacSahipleriniGetir",
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
                                "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiAdi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiSoyadi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiTelNo + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiAdres + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.IhtiyacSahipleri[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/IhtiyacSahibi/Duzenle/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/IhtiyacSahibi/Detay/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/IhtiyacSahibi/Sil/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiAdi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiSoyadi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiTelNo + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiAdres + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.IhtiyacSahipleri[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/IhtiyacSahibi/Duzenle/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/IhtiyacSahibi/Detay/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/IhtiyacSahibi/Sil/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a></td></tr >";
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
            IhtiyacSahibiAranan: $("#ara").val(),
            SehirId: $("#sehirId").val()
        };
        $.ajax({
            type: "GET",
            url: "/IhtiyacSahibi/FiltreliIhtiyacSahipleriniGetir",
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
                            "<td class='col-md-12 col-xs-12'> Filtreniz ile eşleşen birşey bulunamadı. </td></tr>";
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(yazilacak);
                    } else {
                        for (var i = 0; i < parseInt(result.IhtiyacSahibiSayisi); i++) {
                            if (parseInt(i) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiAdi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiSoyadi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiTelNo + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiAdres + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.IhtiyacSahipleri[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/IhtiyacSahibi/Duzenle/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/IhtiyacSahibi/Detay/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/IhtiyacSahibi/Sil/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a></td></tr >";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiAdi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiSoyadi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiTelNo + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.IhtiyacSahipleri[i].IhtiyacSahibiAdres + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.IhtiyacSahipleri[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/IhtiyacSahibi/Duzenle/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/IhtiyacSahibi/Detay/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/IhtiyacSahibi/Sil/" + result.IhtiyacSahipleri[i].IhtiyacSahibiId + "'></a></td></tr >";
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