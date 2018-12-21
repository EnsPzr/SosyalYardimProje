$(document).ready(function() {
    $.ajax({
        type: "GET",
        url: "/Bagisci/TumBagiscilariGetir",
        data:"",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.BasariliMi) {
                Liste = "";
                if (parseInt(result.BagisciSayisi) == 0) {
                    $("#tablo_tbody").html("");
                    var yazilacak = "<tr class='even'>" +
                        "<td class='col-md-12 col-xs-12'> Hiç bağışçı bulunamadı. </td></tr>";
                    $("#tablo_tbody").html("");
                    $("#tablo_tbody").html(yazilacak);
                    $('#myModal').modal('hide');
                } else {
                    for (var i = 0; i < parseInt(result.BagisciSayisi); i++) {
                        if (parseInt(i) % 2 == 0) {
                            Liste = Liste + "<tr class='even'>"+
                                "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].BagisciAdi +"</td>"+
                                "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].BagisciSoyadi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].TelNo + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.BagisciList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.BagisciList[i].Adres + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.BagisciList[i].BagisciEPosta + "</td>" +
                                "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Bagisci/Duzenle/" + result.BagisciList[i].BagisciId+"'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Bagisci/Detay/" + result.BagisciList[i].BagisciId +"'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Bagisci/Sil/" + result.BagisciList[i].BagisciId +"'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].BagisciAdi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].BagisciSoyadi + "</td>" +
                                "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].TelNo + "</td>" +
                                "<td class='col-md-2 hidden-xs'>" + result.BagisciList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.BagisciList[i].Adres + "</td>" +
                                "<td class='col-md-1 hidden-xs'>" + result.BagisciList[i].BagisciEPosta + "</td>" +
                                "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Bagisci/Duzenle/" + result.BagisciList[i].BagisciId + "'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Bagisci/Detay/" + result.BagisciList[i].BagisciId + "'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Bagisci/Sil/" + result.BagisciList[i].BagisciId + "'></a></td></tr >";
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
            AraTxt: $("#ara").val(),
            SehirId: $("#sehirId").val()
        };
        $.ajax({
            type: "GET",
            url: "/Bagisci/FiltreliBagiscilariGetir",
            data: data,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.BasariliMi) {
                    Liste = "";
                    if (parseInt(result.BagisciSayisi) == 0) {
                        $("#tablo_tbody").html("");
                        var yazilacak = "<tr class='even'>" +
                            "<td class='col-md-12 col-xs-12'> Filtreniz ile eşleşen bağışçı bulunamadı. </td></tr>";
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(yazilacak);
                        $('#myModal').modal('hide');
                    } else {
                        for (var i = 0; i < parseInt(result.BagisciSayisi); i++) {
                            if (parseInt(i) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].BagisciAdi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].BagisciSoyadi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].TelNo + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.BagisciList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.BagisciList[i].Adres + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.BagisciList[i].BagisciEPosta + "</td>" +
                                    "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Bagisci/Duzenle/" + result.BagisciList[i].BagisciId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Bagisci/Detay/" + result.BagisciList[i].BagisciId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Bagisci/Sil/" + result.BagisciList[i].BagisciId + "'></a></td></tr >";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].BagisciAdi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].BagisciSoyadi + "</td>" +
                                    "<td class='col-md-2 col-xs-3'>" + result.BagisciList[i].TelNo + "</td>" +
                                    "<td class='col-md-2 hidden-xs'>" + result.BagisciList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.BagisciList[i].Adres + "</td>" +
                                    "<td class='col-md-1 hidden-xs'>" + result.BagisciList[i].BagisciEPosta + "</td>" +
                                    "<td class='col-md-2 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Bagisci/Duzenle/" + result.BagisciList[i].BagisciId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Bagisci/Detay/" + result.BagisciList[i].BagisciId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Bagisci/Sil/" + result.BagisciList[i].BagisciId + "'></a></td></tr >";
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