$(window).on("load", function () {
    desabilitarQuilometragem();
});

function desabilitarQuilometragem () {
    $("select[name*='ZeroKm']").on("change", function () {
        let zeroKm = $(this).val() == 1 ? true : false;
        let inputKm = $("input[name*='Km']");

        if (zeroKm == true) {
            inputKm.attr("disabled", true);
            inputKm.val("0");
        } else {
            if (inputKm.attr("disabled") != null) {
                inputKm.removeAttr("disabled");
            }
        }
    });
}