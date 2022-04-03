$(document).ready(function () {
    carregarDataTable();
});

function carregarDataTable() {
    dataTable = $('#indexPedidos').DataTable({
        "ajax": {
            "url": "/User/MeusPedidos/GetAll"
        },
        "language": {
            "info": "Exibindo _START_ a _END_ de _TOTAL_ entradas",
            "lengthMenu": "Exibir _MENU_ entradas",
            "paginate": {
                "first": "Início",
                "last": "Fim",
                "next": "Próximo",
                "previous": "Anterior"
            }
        },
        "responsive": "true",
        "order": [[0, "desc"]],
        "searching": false,
        "columns": [
            { "data": "id" },
            { "data": "dataPedido" },
            { "data": "statusPedido" },
            { "data": "statusPagamento" },
            {
                "data": "totalPedido",
                "render": DataTable.render.number(',', '.', 2, 'R$ ')
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="btn-group">
                            <a href="/User/MeusPedidos/Detalhes?id=${data}" class="btn btn-primary mx-2">Detalhes</a>
                        </div>
                    `
                }
            },
            
        ],
    });
}

