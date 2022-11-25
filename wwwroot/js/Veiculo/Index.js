$(window).on("load", function () {

    $(".remove").on("click", function () {
        let idVeiculo = $(this).data("idVeiculo");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-remover-veiculo">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow">
                    <div class="modal-header border-0">
                        <h4 class="modal-title text-danger fw-semibold">Cuidado!</h4>
                    </div>
                    <div class="modal-body">
                        <span>Deseja mesmo remover este veículo?</span>
                    </div>
                    <div class="modal-footer border-0">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Não</button>
                        <a href="Veiculo/Remover/${idVeiculo}" role="button" class="btn btn-danger">Sim</a>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalRemoverVeiculo = new bootstrap.Modal($("#modal-remover-veiculo")[0]);

        modalRemoverVeiculo.show();
    })

})