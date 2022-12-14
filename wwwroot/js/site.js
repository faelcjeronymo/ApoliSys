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

    let inputFipe = $("input[name*='CodigoFipe']");
    let inputPlaca = $("input[name*='Placa']");
    // let inputKm = $("input[name*='Km']");
    let inputValor = $("input[name*='Valor']");
    let inputAno = $("input[name*='Ano']");
    let inputRenavam = $("input[name*='Renavam']");
    
    let inputProcessoSusep = $("input[name*='ProcessoSusep']");
    let inputNumeroApolice = $("input[name*='NumeroApolice']");

    inputCpfCnpj.mask("000.000.000-00");
    inputCnh.mask("00000000000");
    inputCelular.mask("(00) 00000-0000");
    inputCep.mask("00000-000");
    inputFipe.mask("00000-00");
    inputPlaca.mask("AAA-0000");
    // inputKm.mask("000.000.000.000.000");
    inputValor.mask("000.000.000.000.000,00", {reverse: true});
    inputAno.mask("0000");
    inputRenavam.mask("000000000");

    inputProcessoSusep.mask("00000000000000000000");
    inputNumeroApolice.mask("000000000");
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
        let metodoAcao = $(this).attr("method");

        if (urlAcao == "") {
            alert("Coloque o Atributo Action no Formulário");
            return false
        }

        //Verifica se todos os dados foram preenchidos, se não for, para a execução
        if (!form.checkValidity()) {
            $(form).addClass("was-validated");
            return false;
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
            enviarFormulario(urlAcao, metodoAcao);
        }
    })
}

function buscarEnderecoViaCep () {
    let inputCep = $("input[name*='Cep']");
    let preencherEndereco = inputCep.data("preencherEndereco");

    if (preencherEndereco == true) {
        inputCep.on("blur", function () {
            inputCep.removeClass("is-invalid");
        })

        //ao digitar o cep vai buscar informações em uma API
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
            } else {
                inputCep.addClass("is-invalid");
            }
        })
    }
}

function carregandoBotao (seletorBotao = null) {
    if (seletorBotao == null) {
        seletorBotao = "button[type='submit']";
    }

    let elementoBotao = $(seletorBotao);

    if (elementoBotao.find("span.texto-auxiliar").length == 1) {

        elementoBotao.find("span.texto-auxiliar").css("visibility", "hidden")

    } else {

        let textoAnteriorBotao = elementoBotao.html();
        elementoBotao.html("");
        let textoAuxiliar = document.createElement("span");
        textoAuxiliar.classList.add("texto-auxiliar");
        textoAuxiliar.append(textoAnteriorBotao);
        textoAuxiliar.style.visibility = "hidden";
        elementoBotao.append(textoAuxiliar);
    }

    let spinner = document.createElement("span");
    spinner.classList.add("spinner-border", "spinner-border-sm");
    spinner.setAttribute("role", "status")

    elementoBotao.append(spinner);

    elementoBotao.attr("disabled", true);
}


function enviarFormulario (urlAcao, metodoAcao) {
    carregandoBotao();

    let formulario = $("form")[0];
    let formularioValores = new FormData(formulario);

    //Enviando requisição
    $.ajax({
        url: urlAcao,
        type: metodoAcao,
        data: formularioValores,
        processData: false,
        cache: false,
        contentType: false,
        success: (respostaReq) => {
            exibirNotificacao((respostaReq.sucesso == 0 ? "erro" : "sucesso"), respostaReq.mensagem);
            
            removerCarregandoBotao();
            
            exibirCarregando();
            
            /**
             * Redirecionando para o caminho especificado
             * em respostaReq.urlRedirecionamento
             */
            setTimeout(() => {
                redirecionarUrl(respostaReq.urlRedirecionamento);
            }, 1500);
        },
        error: (requisicao, status, erro) => {
            console.log(requisicao + " " + status);
            removerCarregandoBotao();
        }
    });
}

function exibirCarregando () {
    let spanSpinner = "<span class='spinner-border spinner-carregando top-50 start-50'></span>";
    let elementoApplication = $(".application");

    elementoApplication.append(spanSpinner);
    elementoApplication.addClass("carregando");
}

function exibirNotificacao(tipoNotificacao, mensagemNotificacao) {
    /**
     * Variável para armazenar o nome
     * da classe que mudará a cor de 
     * fundo da notificação.
     */
    let corNotificacao = "";
    /**
     * Variavel para armazenar o nome
     * da classe do icone de notificação.
     */
    let iconeNotificacao = "";

    switch (tipoNotificacao) {
        case "sucesso":
            corNotificacao = "bg-primary-dark";
            iconeNotificacao = "fa-circle-check";
            break;
        case "erro":
            corNotificacao = "bg-danger";
            iconeNotificacao = "fa-circle-exclamation";
            break;
        default:
            console.log("Coloque o tipo da notificação, irmão.");
            return false;
            break;
    }

    if (mensagemNotificacao == null) {
        console.log("Coloque a mensagem da notificação, irmão.");
        return false;
    }

    //Criando elemento Toast
    let toastContainer = document.createElement("div");
    let toastElement = document.createElement("div");
    let toastElementContainer = document.createElement("div");
    let toastIcon = document.createElement("i");
    let toastElementBody = document.createElement("div");
    let toastCloseButton = document.createElement("button");

    //Aplicando estilos aos elementos
    toastContainer.classList.add("toast-container", "top-0", "start-50", "translate-middle-x");

    toastElement.classList.add("toast", corNotificacao, "align-items-center", "border-0", "mt-2", "text-white");
    toastElement.setAttribute("role", "alert");

    toastElementContainer.classList.add("d-flex", "align-items-center", "justify-content-between");

    toastIcon.classList.add("fa", iconeNotificacao, "fa-lg", "ms-4");

    toastElementBody.classList.add("toast-body", "ps-0");
    toastElementBody.append(mensagemNotificacao);

    toastCloseButton.classList.add("btn-close", "btn-close-white", "me-2");
    toastCloseButton.setAttribute("data-bs-dismiss", "toast");

    //Contruindo hierarquia de elementos
    toastElementContainer.append(toastIcon, toastElementBody, toastCloseButton)

    toastElement.append(toastElementContainer);
    
    toastContainer.append(toastElement)

    $(".application").append(toastContainer);

    let toastOptions = {
        delay : 1500,
    }

    $(".toast").each(function (index, toastElem) {
        let toastNotificacao = new bootstrap.Toast(toastElem, toastOptions);

        toastNotificacao.show();

        $(toastElem).on("hidden.bs.toast", function () {
            $(this).parent().remove();
        })
    })
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

function redirecionarUrl (urlRelativaOrigem) {
    if (urlRelativaOrigem == null) {
        window.history.back();
    }

    const urlOrigem = window.location.origin;

    let urlRedirecionamento = `${urlOrigem}/${urlRelativaOrigem}`;

    window.location.href = urlRedirecionamento;
}

function removerCarregandoBotao (seletorBotao = null) {
    if (seletorBotao == null) {
        seletorBotao = "button[type='submit']";
    }

    let elementoBotao = $(seletorBotao);
    
    elementoBotao.removeAttr("disabled");
    elementoBotao.find(".spinner-border").remove();
    elementoBotao.find(".texto-auxiliar").css("visibility", "visible");
}

/**
 * Função para criar uma requisição de "Remover"
 * para as entidades do sistema.
 * @param {string} entidade Nome da entidade à ser removida.
 * @param {int} idEntidade Id da entidade à ser removida
 * @returns {void}
 */
function removerInformacao (entidade, idEntidade) {
    if ((entidade == null) || (idEntidade == null)) {
        alert("Coloque as info. da entidade à ser removida.");
        return false;
    }

    const urlEnvioRequisicao = `/${entidade}/Remover/${idEntidade}`;

    //Enviando requisição
    $.ajax({
        url: urlEnvioRequisicao,
        type: "DELETE",
        processData: false,
        cache: false,
        contentType: false,
        success: (respostaReq) => {
            $("#modal-remover").modal("hide");
            
            exibirNotificacao((respostaReq.sucesso == 0 ? "erro" : "sucesso"), respostaReq.mensagem);
            
            removerCarregandoBotao(".btn-danger");
            
            exibirCarregando();
            
            /**
             * Redirecionando para o caminho especificado
             * em respostaReq.urlRedirecionamento
             */
            setTimeout(() => {
                redirecionarUrl(respostaReq.urlRedirecionamento);
            }, 2500);
        },
        error: (requisicao, status, erro) => {
            console.log(requisicao + " " + status);
            removerCarregandoBotao();
        }
    });
}

/**
 * Inicia Plugin Select Box com caixa
 * de pesquisa
 */
var $selectize = $("select").selectize({
    placeholder: "Selecione",
    allowEmptyOption: true,
});