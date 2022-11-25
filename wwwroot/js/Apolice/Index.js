$(window).on("load", function () {

    $(".remove").on("click", function () {
        let idApolice = $(this).data("idApolice");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-remover-apolice">
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
                        <a href="Segurado/Remover/${idApolice}" role="button" class="btn btn-danger">Sim</a>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalRemoverApolice = new bootstrap.Modal($("#modal-remover-apolice")[0]);

        modalRemoverApolice.show();
    })

    $(".cancelar").on("click", function () {
        let idApolice = $(this).data("idApolice");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-cancelar-apolice">
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
                        <a href="Segurado/Remover/${idApolice}" role="button" class="btn btn-primary">Sim</a>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalCancelarApolice = new bootstrap.Modal($("#modal-cancelar-apolice")[0]);

        modalCancelarApolice.show();
    })

})