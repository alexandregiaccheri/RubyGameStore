$(document).ready(function () {
    carregarDataTable();
});

function carregarDataTable() {
    dataTable = $('#index').DataTable({
        "ajax": {
            "url": "/Admin/Cupom/GetAll"
        },
        "language": {
            "info": "Exibindo _START_ a _END_ de _TOTAL_ entradas",
            "lengthMenu": "Exibir _MENU_ entradas",
            "search": "Buscar",
            "paginate": {
                "first": "Início",
                "last": "Fim",
                "next": "Próximo",
                "previous": "Anterior"
            }
        },
        "responsive": true,
        "order": [[0, "desc"]],
        "ordering": true,
        "searching": true,
        "columns": [
            { "data": "id", "orderable": true },
            { "data": "codCupom", "orderable": false },
            { "data": "tipoDesconto", "orderable": false },            
            { "data": "status", "orderable": false },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="btn-group">
                            <a href="/Admin/Cupom/Detalhes?id=${data}" class="btn btn-outline-light mx-2">Detalhes</a>
                        </div>
                    `
                }
            },
        ],
    });
}

