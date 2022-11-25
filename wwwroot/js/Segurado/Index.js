$(window).on("load", function () {

    $(".remove").on("click", function () {
        let idSegurado = $(this).data("idSegurado");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-remover-segurado">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow">
                    <div class="modal-header border-0">
                        <h4 class="modal-title text-danger fw-semibold">Cuidado!</h4>
                    </div>
                    <div class="modal-body">
                        <span>Deseja mesmo remover este segurado?</span>
                    </div>
                    <div class="modal-footer border-0">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Não</button>
                        <a href="Segurado/Remover/${idSegurado}" role="button" class="btn btn-danger">Sim</a>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalRemoverSegurado = new bootstrap.Modal($("#modal-remover-segurado")[0]);

        modalRemoverSegurado.show();
    })

})