$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Mesaj/TumMesajlariGetir",
        data: "",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.BasariliMi) {
                Liste = "";
                if (parseInt(result.BagisciSayisi) == 0) {
                    $("#tablo_tbody").html("");
                    var yazilacak = "<tr class='even'>" +
                        "<td class='col-md-12 col-xs-12'> Hiç mesaj bulunamadı. </td></tr>";
                    $("#tablo_tbody").html("");
                    $("#tablo_tbody").html(yazilacak);
                    $('#myModal').modal('hide');
                } else {
                    for (var i = 0; i < parseInt(result.MesajSayisi); i++) {
                        if (parseInt(i) % 2 == 0) {
                            Liste = Liste + "<tr class='even'>" +
                                "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].KullaniciAdiSoyadi + "</td>" +
                                "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].AliciStr + "</td>" +
                                "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].TarihStr + "</td>" +
                                "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-eye-open' href='/Mesaj/DetayListe/" + result.MesajList[i].MesajId + "'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].KullaniciAdiSoyadi + "</td>" +
                                "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].AliciStr + "</td>" +
                                "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].TarihStr + "</td>" +
                                "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-eye-open' href='/Mesaj/DetayListe/" + result.MesajList[i].MesajId + "'></a></td></tr >";
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
            arananKullaniciId: $("#kullaniciId").val(),
            tarih: $("#tarih").val(),
            kimeGonderildi: $("#kimeGonderildi").val()
        };
        $.ajax({
            type: "GET",
            url: "/Mesaj/FiltreliMesajlariGetir",
            data: data,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.BasariliMi) {
                    Liste = "";
                    if (parseInt(result.MesajSayisi) == 0) {
                        $("#tablo_tbody").html("");
                        var yazilacak = "<tr class='even'>" +
                            "<td class='col-md-12 col-xs-12'> Filtreniz ile eşleşen mesaj bulunamadı. </td></tr>";
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(yazilacak);
                        $('#myModal').modal('hide');
                    } else {
                        for (var i = 0; i < parseInt(result.MesajSayisi); i++) {
                            if (parseInt(i) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].KullaniciAdiSoyadi + "</td>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].AliciStr + "</td>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].TarihStr + "</td>" +
                                    "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-eye-open' href='/Mesaj/DetayListe/" + result.MesajList[i].MesajId + "'></a></td></tr >";
                            } else {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].KullaniciAdiSoyadi + "</td>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].AliciStr + "</td>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.MesajList[i].TarihStr + "</td>" +
                                    "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-eye-open' href='/Mesaj/DetayListe/" + result.MesajList[i].MesajId + "'></a></td></tr >";
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