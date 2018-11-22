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
                                "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Kullanici/Duzenle/" + result.KullaniciModelList[i].KullaniciId + "'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-4 col-xs-4'>" + result.KullaniciModelList[i].KullaniciAdi + " " + result.KullaniciModelList[i].KullaniciSoyadi + "</td>" +
                                "<td class='col-md-4 col-xs-4'>" + result.KullaniciModelList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Kullanici/Duzenle/" + result.KullaniciModelList[i].KullaniciId + "'></a></td></tr >";
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