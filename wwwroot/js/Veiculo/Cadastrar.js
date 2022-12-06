$(window).on("load", function () {
    desabilitarQuilometragem();
})

function desabilitarQuilometragem () {
    let zeroKm = $("input[name='ZeroKm']").is(":checked");
    let inputKm = $("input[name='Km']");

    if (zeroKm == true) {
        inputKm.attr("disabled", true);
        inputKm.val("0");
    } else {
        if (inputKm.attr("disabled") != null) {
            inputKm.removeAttr("disabled");
        }
    }
}