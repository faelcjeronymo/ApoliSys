$(window).on("load", function () {
    abrirCancelarApolice();
    abrirRemoverApolice();
})

function abrirCancelarApolice() {
    $(".cancelar").on("click", function () {
        let idApolice = $(this).data("idApolice");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-cancelar">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow">
                    <div class="modal-header border-0">
                        <h4 class="modal-title text-primary fw-semibold">Atenção!</h4>
                    </div>
                    <div class="modal-body">
                        <span>Deseja mesmo cancelar esta apólice?</span>
                    </div>
                    <div class="modal-footer border-0">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Não</button>
                        <button onclick="cancelarApolice(${idApolice})" class="btn btn-primary">Sim</button>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalCancelarApolice = new bootstrap.Modal($("#modal-cancelar")[0]);

        modalCancelarApolice.show();
    })
}

function cancelarApolice(idApolice) {
    if (idApolice == null) {
        alert("Coloque o Id da Apólice Irmão!");
        return false;
    }

    $.ajax({
        url: `/Apolice/Cancelar/${idApolice}`,
        type: "PUT",
        processData: false,
        cache: false,
        contentType: false,
        success: (respostaReq) => {
            $("#modal-cancelar").modal("hide");
            
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

function abrirRemoverApolice() {
    $(".remove").on("click", function () {
        let idApolice = $(this).data("idApolice");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-remover">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow">
                    <div class="modal-header border-0">
                        <h4 class="modal-title text-danger fw-semibold">Cuidado!</h4>
                    </div>
                    <div class="modal-body">
                        <span>Deseja mesmo remover esta apólice?</span>
                    </div>
                    <div class="modal-footer border-0">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Não</button>
                        <a onclick="removerInformacao('Apolice', ${idApolice})" class="btn btn-danger">Sim</a>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalRemoverApolice = new bootstrap.Modal($("#modal-remover")[0]);

        modalRemoverApolice.show();
    })
}
