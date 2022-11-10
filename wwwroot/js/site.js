// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(window).on("load", function () {
    aplicar_mascaras_formatacao();
    avancar_etapa_formulario();
});

function aplicar_mascaras_formatacao() {
    $("input[name='CpfCnpj']").mask("000.000.000-00");
    $("input[name='Cnh']").mask("00000000000");
    $("input[name='Celular']").mask("(00) 00000-0000");
    $("input[name='Cep']").mask("00000-000");
}

//Etapas do formulario
function avancar_etapa_formulario() {
    $("form").on("submit", function (e) {
        e.preventDefault();
        let form = $("form.step-form")[0];

        if (!form.checkValidity()) {
            $(form).addClass("was-validated");
        }

        $(".form-step-link").each((index, elem) => {
            if ($(elem).hasClass("active")) {
                $(elem).addClass("step-finished");

                if ($(".form-step-link:not(.step-finished)").length > 0) {
                    $(elem).removeClass("active")
                }
            }
        });

        $(".tab-pane").each((index, elem) => {
            if ($(elem).hasClass("active")) {
                $(elem).addClass("step-finished");

                if ($(".tab-pane:not(.step-finished)").length > 0) {
                    $(elem).removeClass("active show")
                }

            }
        });

        if ($(".form-step-link:not(.step-finished)").length > 0) {
            $(".form-step-link:not(.step-finished)").addClass("active")
        }

        if ($(".tab-pane:not(.step-finished)").length > 0) {
            $(".tab-pane:not(.step-finished)").addClass("show active")
        }
    })
}

var $selectize = $("select").selectize({
    placeholder: "Selecione",
    allowEmptyOption: true,
});