$(document).ready(function() {
    $.ajax({
        type: "GET",
        url: "/Esya/TumEsyalariGetir",
        data:"",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.BasariliMi) {
                Liste = "";
                if (parseInt(result.EsyaSayisi) == 0) {
                    $('#myModal').modal('hide');
                    var yazilacak = "<tr class='even'>" +
                        "<td class='col-md-12 col-xs-12'> Herhangi bir eşya kayıt edilmemiş. </td></tr>";
                    $("#tablo_tbody").html("");
                    $("#tablo_tbody").html(yazilacak);
                } else {
                    console.log("gir");
                    for (var i = 0; i < parseInt(result.EsyaSayisi); i++) {
                        if (parseInt(i) % 2 == 0) {
                            console.log(i);
                            Liste = Liste + "<tr class='even'>"+
                                "<td class='col-md-8 col-xs-8'>" + result.EsyaList[i].EsyaAdi+"</td>"+
                                "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Esya/Duzenle/" + result.EsyaList[i].EsyaId+"'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Esya/Detay/" + result.EsyaList[i].EsyaId  +"'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Esya/Sil/" + result.EsyaList[i].EsyaId +"'></a></td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-8 col-xs-8'>" + result.EsyaList[i].EsyaAdi + "</td>" +
                                "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Esya/Duzenle/" + result.EsyaList[i].EsyaId + "'></a> | " +
                                "<a class='glyphicon glyphicon-eye-open' href='/Esya/Detay/" + result.EsyaList[i].EsyaId + "'></a> | " +
                                "<a class='glyphicon glyphicon-trash' href='/Esya/Sil/" + result.EsyaList[i].EsyaId + "'></a></td></tr >";
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
            aranan: $("#ara").val()
        };
        $.ajax({
            type: "GET",
            url: "/Esya/FiltreliEsyalariGetir",
            data: data,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.BasariliMi) {
                    Liste = "";
                    if (parseInt(result.EsyaSayisi) == 0) {
                        var yazilacak = "<tr class='even'>" +
                            "<td class='col-md-12 col-xs-12'> Herhangi bir eşya kayıt edilmemiş. </td></tr>";
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(yazilacak);
                        $('#myModal').modal('hide');
                    } else {
                        for (var i = 0; i < parseInt(result.EsyaSayisi); i++) {
                            if (parseInt(i) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-8 col-xs-8'>" + result.EsyaList[i].EsyaAdi+"</td>" +
                                    "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Esya/Duzenle/" + result.EsyaList[i].EsyaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Esya/Detay/" + result.EsyaList[i].EsyaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Esya/Sil/" + result.EsyaList[i].EsyaId + "'></a></td></tr >";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-8 col-xs-8'>" + result.EsyaList[i].EsyaAdi + "</td>" +
                                    "<td class='col-md-4 col-xs-4'><a class='glyphicon glyphicon-pencil' href='/Esya/Duzenle/" + result.EsyaList[i].EsyaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-eye-open' href='/Esya/Detay/" + result.EsyaList[i].EsyaId + "'></a> | " +
                                    "<a class='glyphicon glyphicon-trash' href='/Esya/Sil/" + result.EsyaList[i].EsyaId + "'></a></td></tr >";
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
