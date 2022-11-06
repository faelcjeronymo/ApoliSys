// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(window).on("load", function () {

    $("input[name='CpfCnpj']").mask("000.000.000-00");
    $("input[name='Cep']").mask("00000-000");

});