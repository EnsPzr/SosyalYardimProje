$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Kasa/TumKasaGetir",
        data: "",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.BasariliMi) {
                Liste = "";
                if (parseInt(result.KasaSayisi) == 0) {
                    $('#myModal').modal('hide');
                    var yazilacak = "<tr class='even'>" +
                        "<td class='col-md-12 col-xs-12'> Kasa işlemi bulunmamaktadır. </td></tr>";
                    $("#tablo_tbody").html("");
                    $("#tablo_tbody").html(yazilacak);
                } else {
                    for (var i = 0; i < parseInt(result.KasaSayisi); i++) {
                        if (parseInt(i) % 2 == 0) {
                            Liste = Liste + "<tr class='even'>" +
                                "<td class='col-md-3 col-xs-3'>" + result.KasaList[i].KullaniciAdiSoyadi + "</td>" +
                                "<td class='col-md-3 col-xs-3'>" + result.KasaList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.KasaList[i].Aciklama + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.KasaList[i].Miktar + "</td>" +
                                "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Kasa/Duzenle/" + result.KasaList[i].KasaId + "'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Kasa/Detay/" + result.KasaList[i].KasaId + "'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Kasa/Sil/" + result.KasaList[i].KasaId + "'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-3 col-xs-3'>" + result.KasaList[i].KullaniciAdiSoyadi + "</td>" +
                                "<td class='col-md-3 col-xs-3'>" + result.KasaList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.KasaList[i].Aciklama + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.KasaList[i].Miktar + "</td>" +
                                "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Kasa/Duzenle/" + result.KasaList[i].KasaId + "'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Kasa/Detay/" + result.KasaList[i].KasaId + "'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Kasa/Sil/" + result.KasaList[i].KasaId + "'></a></td></tr >";
                        }
                    };
                    $("#tablo_tbody").html("");
                    $("#tablo_tbody").html(Liste);
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
        $("#tablo_tbody").html("");
        var data = {
            aranan: $("#ara").val(),
            sehirId: $("#sehirId").val(),
            tarih: $("#tarih").val(),
            gelirGider: $("#gelirGiderId").val()
        };
        $.ajax({
            type: "GET",
            url: "/Kasa/FiltreliKasaGetir",
            data: data,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.BasariliMi) {
                    Liste = "";
                    if (parseInt(result.KasaSayisi) == 0) {
                        var yazilacak = "<tr class='even'>" +
                            "<td class='col-md-12 col-xs-12'> Filtreniz ile eşleşen bulunmamaktadır. </td></tr>";
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(yazilacak);
                        $('#myModal').modal('hide');
                    } else {
                        for (var i = 0; i < parseInt(result.KasaSayisi); i++) {
                            if (parseInt(i) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.KasaList[i].KullaniciAdiSoyadi + "</td>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.KasaList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.KasaList[i].Aciklama + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.KasaList[i].Miktar + "</td>" +
                                    "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Kasa/Duzenle/" + result.KasaList[i].KasaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Kasa/Detay/" + result.KasaList[i].KasaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Kasa/Sil/" + result.KasaList[i].KasaId + "'></a></td></tr >";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.KasaList[i].KullaniciAdiSoyadi + "</td>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.KasaList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.KasaList[i].Aciklama + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.KasaList[i].Miktar + "</td>" +
                                    "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Kasa/Duzenle/" + result.KasaList[i].KasaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Kasa/Detay/" + result.KasaList[i].KasaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Kasa/Sil/" + result.KasaList[i].KasaId + "'></a></td></tr >";
                            }
                        };
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(Liste);
                        $('#myModal').modal('hide');
                    }
                } else {

                    $('#myModal').modal('hide');
                }
            },
            error: function (result) {
                alert("hata oluştu");
                $('#myModal').modal('hide');
            },
            beforeSend: function () {
                $('#myModal').modal('show');
            }
        });
    });
});
