$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Yetki/KullanicilariGetir",
        data: "",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.BasariliMi) {
                Liste = "";
                if (parseInt(result.KullaniciSayisi) == 0) {
                    $('#myModal').modal('hide');
                } else {
                    for (var i = 0; i < parseInt(result.KullaniciSayisi); i++) {
                        if (parseInt(result.KullaniciModelList[i].Sira) % 2 == 0) {
                            Liste = Liste + "<tr class='even'>" +
                                "<td class='col-md-4 col-xs-4'>" + result.KullaniciModelList[i].KullaniciAdi + " " + result.KullaniciModelList[i].KullaniciSoyadi+ "</td>" +
                                "<td class='col-md-4 col-xs-4'>" + result.KullaniciModelList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Yetki/YetkiListesi/" + result.KullaniciModelList[i].KullaniciId + "'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-4 col-xs-4'>" + result.KullaniciModelList[i].KullaniciAdi + " " + result.KullaniciModelList[i].KullaniciSoyadi + "</td>" +
                                "<td class='col-md-4 col-xs-4'>" + result.KullaniciModelList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Yetki/YetkiListesi/" + result.KullaniciModelList[i].KullaniciId + "'></a></td></tr >";
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
            SehirId: $("#sehirId").val()
        };
        $.ajax({
            type: "GET",
            url: "/Yetki/FiltreliKullanicilariGetir",
            data: data,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.BasariliMi) {
                    Liste = "";
                    if (parseInt(result.KullaniciSayisi) == 0) {
                        $('#myModal').modal('hide');
                        var yazilacak = "<tr class='even'>" +
                            "<td class='col-md-12 col-xs-12'> Filtreniz ile eşleşen kimse bulunamadı. </td></tr>";
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(yazilacak);
                    } else {
                        for (var i = 0; i < parseInt(result.KullaniciSayisi); i++) {
                            if (parseInt(result.KullaniciModelList[i].Sira) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-4 col-xs-4'>" + result.KullaniciModelList[i].KullaniciAdi + " " + result.KullaniciModelList[i].KullaniciSoyadi + "</td>" +
                                    "<td class='col-md-4 col-xs-4'>" + result.KullaniciModelList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Yetki/YetkiListesi/" + result.KullaniciModelList[i].KullaniciId + "'></a></td></tr >";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-4 col-xs-4'>" + result.KullaniciModelList[i].KullaniciAdi + " " + result.KullaniciModelList[i].KullaniciSoyadi + "</td>" +
                                    "<td class='col-md-4 col-xs-4'>" + result.KullaniciModelList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Yetki/YetkiListesi/" + result.KullaniciModelList[i].KullaniciId + "'></a></td></tr >";
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