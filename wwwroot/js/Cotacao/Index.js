$(window).on("load", function () {
    abrirAprovarCotacao();
    abrirNegarCotacao();
    abrirRemoverCotacao();
})

function abrirAprovarCotacao() {
    $(".aprovar").on("click", function () {
        let idCotacao = $(this).data("idCotacao");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-aprovar">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow">
                    <div class="modal-header border-0">
                        <h4 class="modal-title text-primary fw-semibold">Atenção!</h4>
                    </div>
                    <div class="modal-body">
                        <span>Deseja mesmo aprovar esta cotacao?</span>
                    </div>
                    <div class="modal-footer border-0">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Não</button>
                        <button onclick="aprovarCotacao(${idCotacao})" class="btn btn-primary">Sim</button>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalAprovarCotacao = new bootstrap.Modal($("#modal-aprovar")[0]);

        modalAprovarCotacao.show();
    })
}

function aprovarCotacao(idCotacao) {
    if (idCotacao == null) {
        alert("Coloque o Id da Cotação Irmão!");
        return false;
    }

    $.ajax({
        url: `/Cotacao/Aprovar/${idCotacao}`,
        type: "PUT",
        processData: false,
        cache: false,
        contentType: false,
        success: (respostaReq) => {
            $("#modal-aprovar").modal("hide");
            
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

function abrirNegarCotacao() {
    $(".negar").on("click", function () {
        let idCotacao = $(this).data("idCotacao");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-negar">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow">
                    <div class="modal-header border-0">
                        <h4 class="modal-title text-primary fw-semibold">Atenção!</h4>
                    </div>
                    <div class="modal-body">
                        <span>Deseja mesmo negar esta cotacao?</span>
                    </div>
                    <div class="modal-footer border-0">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Não</button>
                        <button onclick="negarCotacao(${idCotacao})" class="btn btn-primary">Sim</button>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalNegarCotacao = new bootstrap.Modal($("#modal-negar")[0]);

        modalNegarCotacao.show();
    })
}

function negarCotacao(idCotacao) {
    if (idCotacao == null) {
        alert("Coloque o Id da Cotação Irmão!");
        return false;
    }

    $.ajax({
        url: `/Cotacao/Negar/${idCotacao}`,
        type: "PUT",
        processData: false,
        cache: false,
        contentType: false,
        success: (respostaReq) => {
            $("#modal-negar").modal("hide");
            
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

function abrirRemoverCotacao() {
    $(".remove").on("click", function () {
        let idCotacao = $(this).data("idCotacao");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-remover">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow">
                    <div class="modal-header border-0">
                        <h4 class="modal-title text-danger fw-semibold">Cuidado!</h4>
                    </div>
                    <div class="modal-body">
                        <span>Deseja mesmo remover esta cotacao?</span>
                    </div>
                    <div class="modal-footer border-0">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Não</button>
                        <button onclick="removerInformacao('Cotacao', ${idCotacao})" class="btn btn-danger">Sim</button>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalRemoverCotacao = new bootstrap.Modal($("#modal-remover")[0]);

        modalRemoverCotacao.show();
    })
}