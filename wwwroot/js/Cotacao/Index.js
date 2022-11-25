$(window).on("load", function () {

    $(".remove").on("click", function () {
        let idCotacao = $(this).data("idCotacao");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-remover-cotacao">
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
                        <a href="Segurado/Remover/${idCotacao}" role="button" class="btn btn-danger">Sim</a>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalRemoverCotacao = new bootstrap.Modal($("#modal-remover-cotacao")[0]);

        modalRemoverCotacao.show();
    })

    $(".aprovar").on("click", function () {
        let idCotacao = $(this).data("idCotacao");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-aprovar-cotacao">
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
                        <a href="Segurado/Remover/${idCotacao}" role="button" class="btn btn-primary">Sim</a>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalAprovarCotacao = new bootstrap.Modal($("#modal-aprovar-cotacao")[0]);

        modalAprovarCotacao.show();
    })

    $(".negar").on("click", function () {
        let idCotacao = $(this).data("idCotacao");

        let modal = `
        <div class="modal fade" tabindex="-1" id="modal-negar-cotacao">
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
                        <a href="Segurado/Remover/${idCotacao}" role="button" class="btn btn-primary">Sim</a>
                    </div>
                </div>
            </div>
        </div>
        `;

        $("body").append(modal);

        let modalNegarCotacao = new bootstrap.Modal($("#modal-negar-cotacao")[0]);

        modalNegarCotacao.show();
    })

})