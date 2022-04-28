$(document).ready(function () {
    $('#selecaoTipo').change(function () {
        var selecao = $('#selecaoTipo Option:Selected').text();
        if (selecao == 'Porcentagem') {
            $('#porcento').show();
            $('#reais').hide();
        }
        else {
            $('#porcento').hide();
            $('#reais').show();
        }
    });
});

tinymce.init({
    selector: 'textarea',
    toolbar_mode: 'floating',
    skin: "oxide-dark",
    content_css: "dark",
    height: 200
});