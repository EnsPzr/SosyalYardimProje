$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/GeriBildirim/TumGeriBildirimleriGetir",
        data: "",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.BasariliMi) {
                Liste = "";
                if (parseInt(result.GeriBildirimSayisi) == 0) {
                    $('#myModal').modal('hide');
                    var yazilacak = "<tr class='even'>" +
                        "<td class='col-md-12 col-xs-12'> Herhangi bir geri bildirim yok. </td></tr>";
                    $("#tablo_tbody").html("");
                    $("#tablo_tbody").html(yazilacak);
                } else {
                    console.log("gir");
                    for (var i = 0; i < parseInt(result.GeriBildirimSayisi); i++) {
                        if (parseInt(i) % 2 == 0) {
                            console.log(i);
                            Liste = Liste + "<tr class='even'>" +
                                "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].KullaniciAdiSoyadi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].KullaniciTel + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].TarihStr + "</td>" +
                                "<td class='col-md-3 hidden-xs'>" + result.GeriBildirimList[i].Konu + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.GeriBildirimList[i].DurumStr + "</td>" +
                                "<td class='col-md-1 col-xs-3'><a class='glyphicon glyphicon-eye-open' href='/Esya/Detay/" + result.GeriBildirimList[i].GeriBildirimId + "'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].KullaniciAdiSoyadi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].KullaniciTel + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].TarihStr + "</td>" +
                                "<td class='col-md-3 hidden-xs'>" + result.GeriBildirimList[i].Konu + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.GeriBildirimList[i].DurumStr + "</td>" +
                                "<td class='col-md-1 col-xs-3'><a class='glyphicon glyphicon-eye-open' href='/Esya/Detay/" + result.GeriBildirimList[i].GeriBildirimId + "'></a></td></tr >";
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
        console.log("çalıştı");
        var data = {
            aranan: $("#ara").val(),
            sehirId: $("#sehirId").val(),
            tarih: $("#tarih").val()
        };
        $.ajax({
            type: "GET",
            url: "/GeriBildirim/FiltreliGeriBildirimleriGetir",
            data: data,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.BasariliMi) {
                    Liste = "";
                    if (parseInt(result.GeriBildirimSayisi) == 0) {
                        var yazilacak = "<tr class='even'>" +
                            "<td class='col-md-12 col-xs-12'> Filtreniz ile eşleşen geri bildirim bulanamadı. </td></tr>";
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(yazilacak);
                        $('#myModal').modal('hide');
                    } else {
                        for (var i = 0; i < parseInt(result.GeriBildirimSayisi); i++) {
                            if (parseInt(i) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].KullaniciAdiSoyadi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].KullaniciTel + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].TarihStr + "</td>" +
                                    "<td class='col-md-3 hidden-xs'>" + result.GeriBildirimList[i].Konu + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.GeriBildirimList[i].DurumStr + "</td>" +
                                    "<td class='col-md-1 col-xs-3'><a class='glyphicon glyphicon-eye-open' href='/Esya/Detay/" + result.GeriBildirimList[i].GeriBildirimId + "'></a></td></tr >";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].KullaniciAdiSoyadi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].KullaniciTel + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.GeriBildirimList[i].TarihStr + "</td>" +
                                    "<td class='col-md-3 hidden-xs'>" + result.GeriBildirimList[i].Konu + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.GeriBildirimList[i].DurumStr + "</td>" +
                                    "<td class='col-md-1 col-xs-3'><a class='glyphicon glyphicon-eye-open' href='/Esya/Detay/" + result.GeriBildirimList[i].GeriBildirimId + "'></a></td></tr >";
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
