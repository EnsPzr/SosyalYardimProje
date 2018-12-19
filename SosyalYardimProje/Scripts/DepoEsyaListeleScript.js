$(document).ready(function() {
    $.ajax({
        type: "GET",
        url: "/Depo/TumunuGetir",
        data:"",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.BasariliMi) {
                Liste = "";
                if (parseInt(result.DepoEsyaSayisi) == 0) {
                    $('#myModal').modal('hide');
                    var yazilacak = "<tr class='even'>" +
                        "<td class='col-md-12 col-xs-12'> Depoda herhangi bir eşya bulunmamaktadır. </td></tr>";
                    $("#tablo_tbody").html("");
                    $("#tablo_tbody").html(yazilacak);
                } else {
                    for (var i = 0; i < parseInt(result.DepoEsyaSayisi); i++) {
                        if (parseInt(i) % 2 == 0) {
                            Liste = Liste + "<tr class='even'>"+
                                "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].EsyaAdi + "</td>" +
                                "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].Adet + "</td>" +
                                "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Depo/Duzenle/" + result.DepoList[i].DepoEsyaId+"'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Depo/Detay/" + result.DepoList[i].DepoEsyaId  +"'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Depo/Sil/" + result.DepoList[i].DepoEsyaId +"'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].EsyaAdi + "</td>" +
                                "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].Sehir.SehirAdi + "</td>" +
                                "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].Adet + "</td>" +
                                "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Depo/Duzenle/" + result.DepoList[i].DepoEsyaId + "'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Depo/Detay/" + result.DepoList[i].DepoEsyaId + "'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Depo/Sil/" + result.DepoList[i].DepoEsyaId + "'></a></td></tr >";
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
        console.log("çalıştı");
        var data = {
            aranan: $("#ara").val(),
            sehirId: $("#sehirId").val(),
            esyaId:$("#esyaId").val()
        };
        $.ajax({
            type: "GET",
            url: "/Depo/FiltreliDepoGetir",
            data: data,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.BasariliMi) {
                    Liste = "";
                    if (parseInt(result.DepoEsyaSayisi) == 0) {
                        var yazilacak = "<tr class='even'>" +
                            "<td class='col-md-12 col-xs-12'> Deponuzda herhangi bir eşya bulunmamaktadır. </td></tr>";
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(yazilacak);
                        $('#myModal').modal('hide');
                    } else {
                        for (var i = 0; i < parseInt(result.DepoEsyaSayisi); i++) {
                            if (parseInt(i) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].EsyaAdi + "</td>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].Adet + "</td>" +
                                    "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Depo/Duzenle/" + result.DepoList[i].DepoEsyaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Depo/Detay/" + result.DepoList[i].DepoEsyaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Depo/Sil/" + result.DepoList[i].DepoEsyaId + "'></a></td></tr >";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].EsyaAdi + "</td>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].Sehir.SehirAdi + "</td>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.DepoList[i].Adet + "</td>" +
                                    "<td class='col-md-3 col-xs-3'><a class='glyphicon glyphicon-pencil' href='/Depo/Duzenle/" + result.DepoList[i].DepoEsyaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Depo/Detay/" + result.DepoList[i].DepoEsyaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Depo/Sil/" + result.DepoList[i].DepoEsyaId + "'></a></td></tr >";
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
