$(document).ready(function() {
    $("#buton").click(function() {
        var adet1 = $("#adet_0").val();
        var adet2 = $("#adet_1").val();
        var adet3 = $("#adet_2").val();
        var adet4 = $("#adet_3").val();
        var adet5 = $("#adet_4").val();
        if (adet1 !== "" || adet2 !== "" || adet3 !== "" || adet4 !== "" || adet5 !== "") {
            var sayac = 0;
            var sayac2 = 0;
            if (adet1 !== "") {
                var resim1 = $("#resim1_0").val();
                console.log("adet1");
                if (resim1 !== "") {
                    sayac++;
                    var ext = resim1.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_0").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                } else {
                    $("#panel_0").removeClass("panel-default").addClass("panel-danger");
                    $("#panelFooter_0").text("Adet girdiğiniz eşya için en az 1 resim eklenmelidir.");
                }

                var resim2 = $("#resim2_0").val();
                if (resim2 !== "") {
                    sayac++;
                    var ext = resim2.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_0").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                }
                var resim3 = $("#resim3_0").val();
                if (resim3 !== "") {
                    sayac++;
                    var ext = resim3.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_0").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                }
            }
            if (adet2 !== "") {
                var resim1 = $("#resim1_1").val();
                console.log("adet2");
                if (resim1 !== "") {
                    sayac++;
                    var ext = resim1.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_1").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                } else {
                    $("#panel_1").removeClass("panel-default").addClass("panel-danger");
                    $("#panelFooter_1").text("Adet girdiğiniz eşya için en az 1 resim eklenmelidir.");
                }

                var resim2 = $("#resim2_1").val();
                if (resim2 !== "") {
                    sayac++;
                    var ext = resim2.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_1").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                }
                var resim3 = $("#resim3_1").val();
                if (resim3 !== "") {
                    sayac++;
                    var ext = resim3.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_1").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                }
            }
            if (adet3 !== "") {
                var resim1 = $("#resim1_2").val();
                if (resim1 !== "") {
                    sayac++;
                    var ext = resim1.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_2").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                } else {
                    $("#panel_2").removeClass("panel-default").addClass("panel-danger");
                    $("#panelFooter_2").text("Adet girdiğiniz eşya için en az 1 resim eklenmelidir.");
                }

                var resim2 = $("#resim2_2").val();
                if (resim2 !== "") {
                    sayac++;
                    var ext = resim2.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_2").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                }
                var resim3 = $("#resim3_2").val();
                if (resim3 !== "") {
                    sayac++;
                    var ext = resim3.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_2").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                }
            }
            if (adet4 !== "") {
                var resim1 = $("#resim1_3").val();
                if (resim1 !== "") {
                    sayac++;
                    var ext = resim1.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_3").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                } else {
                    $("#panel_3").removeClass("panel-default").addClass("panel-danger");
                    $("#panelFooter_3").text("Adet girdiğiniz eşya için en az 1 resim eklenmelidir.");
                }

                var resim2 = $("#resim2_3").val();
                if (resim2 !== "") {
                    sayac++;
                    var ext = resim2.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_3").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                }
                var resim3 = $("#resim3_3").val();
                if (resim3 !== "") {
                    sayac++;
                    var ext = resim3.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_3").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                }
            }
            if (adet5 !== "") {
                var resim1 = $("#resim1_4").val();
                if (resim1 !== "") {
                    sayac++;
                    var ext = resim1.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_4").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                } else {
                    $("#panel_4").removeClass("panel-default").addClass("panel-danger");
                    $("#panelFooter_4").text("Adet girdiğiniz eşya için en az 1 resim eklenmelidir.");
                }

                var resim2 = $("#resim2_4").val();
                if (resim2 !== "") {
                    sayac++;
                    var ext = resim2.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_4").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                }
                var resim3 = $("#resim3_4").val();
                if (resim3 !== "") {
                    sayac++;
                    var ext = resim3.split('.').pop().toLowerCase();
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                        $("#panelFooter_4").text("Sadece resim dosyası yükleyebilirsiniz.");
                    } else {
                        sayac2++;
                    }
                }
            }

            if (sayac == sayac2) {
                if (confirm(
                    "Yapmak istediğiniz bağış kayıt edilecek. Kayıttan sonra güncelleme yapılamaz. Kayıdı onaylıyor musunuz?")
                ) {
                    $("#form").submit();
                }
            }
        } else {
            $("#hata").show();
            $("#hata").text("Bağışlamak istediğiniz eşyaların adetlerini girmelisiniz.")
        }
    });
});