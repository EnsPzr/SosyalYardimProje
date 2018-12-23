$(document).ready(function () {
    var mesajId = $("#mesajId").val();
    console.log(mesajId);
    var data = {
        detayId: mesajId
    };
    $.ajax({
        type: "GET",
        url: "/Mesaj/TumDetaylariGetir",
        data: data,
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.BasariliMi) {
                Liste = "";
                if (parseInt(result.MesajDetaySayisi) == 0) {
                    $('#myModal').modal('hide');
                    var yazilacak = "<tr class='even'>" +
                        "<td class='col-md-12 col-xs-12'> Hiç bir mesaj bulunamadı. </td></tr>";
                    $("#tablo_tbody").html("");
                    $("#tablo_tbody").html(yazilacak);
                } else {
                    for (var i = 0; i < parseInt(result.MesajDetaySayisi); i++) {
                        if (parseInt(i) % 2 == 0) {
                            Liste = Liste + "<tr class='even'>" +
                                "<td class='col-md-3 col-xs-3'>" + result.MesajDetayList[i].KullaniciAdiSoyadi + "</td>" +
                                "<td class='col-md-9 col-xs-9'>" + result.MesajDetayList[i].MesajMetni + "</td></tr >";
                        } else {
                            Liste = Liste + "<tr class='odd'>" +
                                "<td class='col-md-3 col-xs-3'>" + result.MesajDetayList[i].KullaniciAdiSoyadi + "</td>" +
                                "<td class='col-md-9 col-xs-9'>" + result.MesajDetayList[i].MesajMetni + "</td></tr >";
                        }
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(Liste);
                    };
                    $('#myModal').modal('hide');
                }
            } else {
                $('#myModal').modal('hide');
                var yazilacak = "<tr class='even'>" +
                    "<td class='col-md-12 col-xs-12'> Bu işlem için yetkiniz bulunmamaktadır. </td></tr>";
                $("#tablo_tbody").html("");
                $("#tablo_tbody").html(yazilacak);
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
            detayId: mesajId
        };
        $.ajax({
            type: "GET",
            url: "/Mesaj/FiltreliDetaylariGetir",
            data: data,
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.BasariliMi) {
                    Liste = "";
                    if (parseInt(result.MesajDetaySayisi) == 0) {
                        $('#myModal').modal('hide');
                        var yazilacak = "<tr class='even'>" +
                            "<td class='col-md-12 col-xs-12'> Filtreye uygun bir mesaj bulunamadı. </td></tr>";
                        $("#tablo_tbody").html("");
                        $("#tablo_tbody").html(yazilacak);
                    } else {
                        for (var i = 0; i < parseInt(result.MesajDetaySayisi); i++) {
                            if (parseInt(i) % 2 == 0) {
                                Liste = Liste + "<tr class='even'>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.MesajDetayList[i].KullaniciAdiSoyadi + "</td>" +
                                    "<td class='col-md-9 col-xs-9'>" + result.MesajDetayList[i].MesajMetni + "</td></tr >";
                            } else {
                                Liste = Liste + "<tr class='odd'>" +
                                    "<td class='col-md-3 col-xs-3'>" + result.MesajDetayList[i].KullaniciAdiSoyadi + "</td>" +
                                    "<td class='col-md-9 col-xs-9'>" + result.MesajDetayList[i].MesajMetni + "</td></tr >";
                            }
                            $("#tablo_tbody").html("");
                            $("#tablo_tbody").html(Liste);
                        };
                        $('#myModal').modal('hide');
                    }
                } else {
                    $('#myModal').modal('hide');
                    var yazilacak = "<tr class='even'>" +
                        "<td class='col-md-12 col-xs-12'> Bu işlem için yetkiniz bulunmamaktadır. </td></tr>";
                    $("#tablo_tbody").html("");
                    $("#tablo_tbody").html(yazilacak);
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