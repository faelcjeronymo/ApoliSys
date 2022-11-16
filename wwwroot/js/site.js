// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(window).on("load", function () {
    aplicarMascarasFormatacao();
    avancarEtapaFormulario();
    mudarMascaraCpfCnpj();
    buscarEnderecoViaCep();
});

/**
 * Fução para aplicar máscaras de formatação 
 * em campos de entrada de dados
 */
function aplicarMascarasFormatacao() {
    //Mascara de formatacao dinamica para CPF e CNPJ
    let inputCpfCnpj = $("input[name*='CpfCnpj']");
    let inputCnh = $("input[name*='Cnh']");
    let inputCelular = $("input[name*='Celular']");
    let inputCep = $("input[name*='Cep']");
    
    inputCpfCnpj.mask("000.000.000-00");
    inputCnh.mask("00000000000");
    inputCelular.mask("(00) 00000-0000");
    inputCep.mask("00000-000");
}

/**
 * Função para fazer validação de etapas
 * de formulários.
 */
function avancarEtapaFormulario() {
    $("form").on("submit", function (e) {
        e.preventDefault();
        let form = $("form.step-form")[0];
        /**
         * Url da ação (Action) à ser executada ao enviar
         * o formulário. Use a Url relativa à raiz do projeto.
         * Será usada como um parâmetro da função de envio de
         * formulários.
         */
        let urlAcao = $(this).attr("action");

        if (urlAcao == "") {
            console.log("Coloque o Atributo Action no Formulário");
            return false
        }

        //Verifica se todos os dados foram preenchidos, se não for, para a execução
        if (!form.checkValidity()) {
            $(form).addClass("was-validated");
        }

        //Verifica se existem botões de etapas para aplicar os estilos
        $(".form-step-link").each((index, elem) => {
            if ($(elem).hasClass("active")) {
                $(elem).addClass("step-finished");

                //Remove estilo de botão de etapa ja concluída
                if ($(".form-step-link:not(.step-finished)").length > 0) {
                    $(elem).removeClass("active")
                }
            }
        });

        //Verifica se ainda existem etapas a serem concluidas e fecha a etapa anterior
        $(".tab-pane").each((index, elem) => {
            if ($(elem).hasClass("active")) {
                $(elem).addClass("step-finished");

                if ($(".tab-pane:not(.step-finished)").length > 0) {
                    $(elem).removeClass("active show")
                }

            }
        });

        if ($(".form-step-link:not(.step-finished)").length > 0) {
            let nextFormStepLink = $(".form-step-link:not(.step-finished)")[0];
            $(nextFormStepLink).addClass("active");
        }

        if ($(".tab-pane:not(.step-finished)").length > 0) {
            let nextTabPane = $(".tab-pane:not(.step-finished)")[0]
            $(nextTabPane).addClass("show active");
        }

        if ($(".tab-pane:not(.step-finished)").length == 0) {
            enviarFormulario(urlAcao);
        }
    })
}

function enviarFormulario (urlAcao) {
    let formulario = $("form")[0];
    let formularioValores = new FormData(formulario);

    //Enviando requisição
    $.ajax({
        url: urlAcao,
        type: "POST",
        data: formularioValores,
        processData: false,
        cache: false,
        contentType: false,
        success: (respostaReq) => {
            console.log(respostaReq);
        },
        error: (requisicao, status, erro) => {
            console.log(erro);
        }

    });
}

function buscarEnderecoViaCep () {
    let inputCep = $("input[name*='Cep']");
    let preencherEndereco = inputCep.data("preencherEndereco");

    if (preencherEndereco == true) {
        //aO digitar O cep vai buscar informações em uma API
        inputCep.on("keyup", function () {
            /**
             * Recuperando Cep Sem máscara de formatacao
             * ou espaços usando o método cleanVal e replace
             */
            let cepDigitado = $(this).cleanVal().replace(" ", "");

            if (cepDigitado.length == 8) {
                const endpointViaCepApi = `https://viacep.com.br/ws/${cepDigitado}/json/?callback=?`;

                //Enviando requisição
                $.getJSON(endpointViaCepApi, function (dados) {
                    if (!dados.erro) {
                        const inputCidade = $("input[name*='Cidade']");
                        const inputEstado = $("select[name*='Estado']");
                        const inputBairro = $("input[name*='Bairro']");
                        const inputRua = $("input[name*='Rua']");
                        const inputComplemento = $("input[name*='Complemento']");
                        inputCep.removeClass("is-invalid");

                        let selectizeEstado = $("select[name*='Estado']").selectize();

                        inputCidade.val() == "" ? inputCidade.val(dados.localidade) : "";
                        inputEstado.val() == 0 ? selectizeEstado[0].selectize.setValue(dados.uf) : "";
                        inputBairro.val() == "" ? inputBairro.val(dados.bairro) : "";
                        inputRua.val() == "" ? inputRua.val(dados.logradouro) : "";
                        inputComplemento.val() == "" ? inputComplemento.val(dados.complemento) : "";
                    } else {
                        inputCep.addClass("is-invalid");
                    }
                });
            }
        })
    }
}

/**
 * Função será usada em uma checkbox Mencionando se
 * no campo Cpf/Cnpj será digitado um Cnpj, por padrão
 * o campo receberá a máscara de CPF, caso a checkbox
 * seja marcada, mudará a máscara para CNPJ
 */
 function mudarMascaraCpfCnpj () {
    let inputCpfCnpj = $("input[name*='CpfCnpj']");

    $("#usar-cnpj").on("click", function () {
        let usarCnpj = $("#usar-cnpj").is(":checked");

        if (usarCnpj == true) {
            //input recebe mascara CNPJ
            inputCpfCnpj.mask("00.000.000/0000-00");
        } else {
            inputCpfCnpj.mask("000.000.000-00");
        }
    })

}

/**
 * Inicia Plugin Select Box com caixa
 * de pesquisa
 */
var $selectize = $("select").selectize({
    placeholder: "Selecione",
    allowEmptyOption: true,
});