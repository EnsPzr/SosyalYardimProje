$(document).ready(function() {
    $.ajax({
        type: "GET",
        url: "/Kullanici/KullanicilariGetir",
        data:"",
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
                            Liste = Liste + "<tr class='even'>"+
                                "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciAdi +"</td>"+
                                "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciSoyadi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciTelNo + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.KullaniciModelList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.KullaniciModelList[i].KullaniciMerkezdeMiStr + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.KullaniciModelList[i].AktifMiStr + "</td>" +
                                "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Kullanici/Duzenle/"+result.KullaniciModelList[i].KullaniciId+"'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Kullanici/Detay/" + result.KullaniciModelList[i].KullaniciId +"'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Kullanici/Sil/" + result.KullaniciModelList[i].KullaniciId +"'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciAdi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciSoyadi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciTelNo + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.KullaniciModelList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.KullaniciModelList[i].KullaniciMerkezdeMiStr + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.KullaniciModelList[i].AktifMiStr + "</td>" +
                                "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Kullanici/Duzenle/" + result.KullaniciModelList[i].KullaniciId + "'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Kullanici/Detay/" + result.KullaniciModelList[i].KullaniciId + "'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Kullanici/Sil/" + result.KullaniciModelList[i].KullaniciId + "'></a></td></tr >";
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
        var OnayliMiCheck = false;
        if ($("#OnayliMi").is(':checked')) {
            OnayliMiCheck = true;
        }
        var MerkezdeMiCheck = false;
        if ($("#MerkezdeMi").is(':checked')) {
            MerkezdeMiCheck = true;
        }
        var AktifMiCheck = false;
        if ($("#AktifMi").is(':checked')) {
            AktifMiCheck = true;
        }
        var data = {
            AraTxt: $("#ara").val(),
            SehirId: $("#sehirId").val(),
            OnayliMi: OnayliMiCheck,
            MerkezdeMi: MerkezdeMiCheck,
            AktifMi: AktifMiCheck
        };
        $.ajax({
            type: "GET",
            url: "/Kullanici/FiltreliKullanicilariGetir",
            data: data,
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
                                    "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciAdi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciSoyadi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciTelNo + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.KullaniciModelList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.KullaniciModelList[i].KullaniciMerkezdeMiStr + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.KullaniciModelList[i].AktifMiStr + "</td>" +
                                    "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Kullanici/Duzenle/" + result.KullaniciModelList[i].KullaniciId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Kullanici/Detay/" + result.KullaniciModelList[i].KullaniciId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Kullanici/Sil/" + result.KullaniciModelList[i].KullaniciId + "'></a></td></tr >";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciAdi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciSoyadi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.KullaniciModelList[i].KullaniciTelNo + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.KullaniciModelList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.KullaniciModelList[i].KullaniciMerkezdeMiStr + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.KullaniciModelList[i].AktifMiStr + "</td>" +
                                    "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Kullanici/Duzenle/" + result.KullaniciModelList[i].KullaniciId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Kullanici/Detay/" + result.KullaniciModelList[i].KullaniciId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Kullanici/Sil/" + result.KullaniciModelList[i].KullaniciId + "'></a></td></tr >";
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