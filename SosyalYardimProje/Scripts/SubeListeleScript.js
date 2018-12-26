$(document).ready(function() {
    $.ajax({
        type: "GET",
        url: "/Sube/SubeleriGetir",
        data:"",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.BasariliMi) {
                Liste = "";
                if (parseInt(result.SubeSayisi) == 0) {
                    $('#myModal').modal('hide');
                    var yazilacak = "<tr class='even'>" +
                        "<td class='col-md-12 col-xs-12'> Hiç birşey bulunamadı. </td></tr>";
                    $("#tablo_tbody").html("");
                    $("#tablo_tbody").html(yazilacak);
                } else {
                    for (var i = 0; i < parseInt(result.SubeSayisi); i++) {
                        if (parseInt(result.SubeModelList[i].Sira) % 2 == 0) {
                            Liste = Liste + "<tr class='even'>"+
                                "<td class='col-md-4 col-xs-4'>" + result.SubeModelList[i].Kullanici.KullaniciAdi + " " + result.SubeModelList[i].Kullanici.KullaniciSoyadi +"</td>"+
                                "<td class='col-md-4 col-xs-4'>" + result.SubeModelList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Sube/Duzenle/" + result.SubeModelList[i].SubeId+"'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Sube/Detay/" + result.SubeModelList[i].SubeId  +"'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Sube/Sil/" + result.SubeModelList[i].SubeId +"'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-4 col-xs-4'>" + result.SubeModelList[i].Kullanici.KullaniciAdi + " " + result.SubeModelList[i].Kullanici.KullaniciSoyadi + "</td>" +
                                "<td class='col-md-4 col-xs-4'>" + result.SubeModelList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Sube/Duzenle/" + result.SubeModelList[i].SubeId + "'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Sube/Detay/" + result.SubeModelList[i].SubeId + "'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Sube/Sil/" + result.SubeModelList[i].SubeId + "'></a></td></tr >";
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
        console.log("çalıştı");
        var data = {
            aranan: $("#ara").val()
        };
        $.ajax({
            type: "GET",
            url: "/Sube/SubeleriGetir",
            data: data,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.BasariliMi) {
                    Liste = "";
                    if (parseInt(result.SubeSayisi) == 0) {
                        console.log(parseInt(result.SubeSayisi));
                        $('#myModal').modal('hide');
                    } else {
                        for (var i = 0; i < parseInt(result.SubeSayisi); i++) {
                            if (parseInt(result.SubeModelList[i].Sira) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-4 col-xs-4'>" + result.SubeModelList[i].Kullanici.KullaniciAdi + " " + result.SubeModelList[i].Kullanici.KullaniciSoyadi + "</td>" +
                                    "<td class='col-md-4 col-xs-4'>" + result.SubeModelList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Sube/Duzenle/" + result.SubeModelList[i].SubeId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Sube/Detay/" + result.SubeModelList[i].SubeId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Sube/Sil/" + result.SubeModelList[i].SubeId + "'></a></td></tr >";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-4 col-xs-4'>" + result.SubeModelList[i].Kullanici.KullaniciAdi + " " + result.SubeModelList[i].Kullanici.KullaniciSoyadi + "</td>" +
                                    "<td class='col-md-4 col-xs-4'>" + result.SubeModelList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Sube/Duzenle/" + result.SubeModelList[i].SubeId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Sube/Detay/" + result.SubeModelList[i].SubeId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Sube/Sil/" + result.SubeModelList[i].SubeId + "'></a></td></tr >";
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
                alert("hata oluştu");
                $('#myModal').modal('hide');
            },
            beforeSend: function () {
                $('#myModal').modal('show');
            }
        });
    });
});