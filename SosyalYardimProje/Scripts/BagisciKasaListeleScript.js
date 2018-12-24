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
                            Liste = Liste +
                                "<tr class='even'>" +
                                "<td class='col-md-3 col-xs-3'>" +
                                result.KasaList[i].KullaniciAdiSoyadi +
                                "</td>" +
                                "<td class='col-md-2 col-xs-2'>" +
                                result.KasaList[i].Sehir.SehirAdi +
                                "</td>" +
                                "<td class='col-md-4 col-xs-4'>" +
                                result.KasaList[i].Aciklama +
                                "</td>" +
                                "<td class='col-md-3 col-xs-3'>" +
                                result.KasaList[i].Miktar +
                                "</td></tr >";
                        } else {
                            Liste = Liste +
                                "<tr class='odd'>" +
                                "<td class='col-md-3 col-xs-3'>" +
                                result.KasaList[i].KullaniciAdiSoyadi +
                                "</td>" +
                                "<td class='col-md-2 col-xs-2'>" +
                                result.KasaList[i].Sehir.SehirAdi +
                                "</td>" +
                                "<td class='col-md-4 col-xs-4'>" +
                                result.KasaList[i].Aciklama +
                                "</td>" +
                                "<td class='col-md-3 col-xs-3'>" +
                                result.KasaList[i].Miktar +
                                "</td></tr >";
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
});