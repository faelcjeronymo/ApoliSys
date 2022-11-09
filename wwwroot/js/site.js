// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(window).on("load", function () {
    aplicar_mascaras_formatacao();
    iniciar_selectize();
    avancar_etapa_formulario();
});

function aplicar_mascaras_formatacao() {
    $("input[name='CpfCnpj']").mask("000.000.000-00");
    $("input[name='Cnh']").mask("00000000000");
    $("input[name='Celular']").mask("(00) 00000-0000");
    $("input[name='Cep']").mask("00000-000");
}

function avancar_etapa_formulario() {
    $("form.step-form button").on("click", function () {
        let form = $("form.step-form")[0];

        console.log(form);

        //Validando Selects
        $(".selectize-control").each(function(index, elem){
            if ($(elem).find(".item").attr('data-value') == null) {
                console.log("Teste");
            }                
        });

        if (!form.checkValidity()) {
            $(form).addClass("was-validated");
            abrir_toast();
        }

    })
}

function abrir_toast(titulo = "Titulo", icone, conteudo, posicao) {

    let toast = `
    <div class="toast position-absolute top-0 start-50 translate-middle-x" role="alert" aria-live="assertive" aria-atomic="true">
        <div class="toast-header">
            <i class="fa fa-exclamation-circle"></i>
            <strong class="me-auto">Bootstrap</strong>
            <small class="text-muted">11 mins ago</small>
            <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
        <div class="toast-body">
            Hello, world! This is a toast message.
        </div>
    </div>
    `;

    $(".card-body .container-fluid").append(toast);

    $(".toast").each((index, elem) => {
        $(elem).on("hidden.bs.toast", () => {
            $(".toast").remove();
        })
        elem = new bootstrap.Toast(elem);
        elem.show();
    })
}

function iniciar_selectize() {
    let selectize = $("select").selectize({
        placeholder: "Selecione",
        allowEmptyOption: true,
    });

    selectize.on("open", () => {
        $(this).clear();
    })
}