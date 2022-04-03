$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("aprovado")) {
        carregarDataTable("aprovado");
    }
    else if (url.includes("processando")) {
        carregarDataTable("processando");
    }
    else if (url.includes("concluido")) {
        carregarDataTable("concluido");
    }
    else if (url.includes("cancelado")) {
        carregarDataTable("cancelado");
    }
    else {
        carregarDataTable("todos")
    }
});

function carregarDataTable(status) {
    dataTable = $('#indexPedidos').DataTable({
        "ajax": {
            "url": "/Admin/Pedidos/GetAll?status=" + status
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
            { "data": "usuario.email" },
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
                            <a href="/Admin/Pedidos/Detalhes?id=${data}" class="btn btn-primary mx-2">Detalhes</a>
                        </div>
                    `
                }
            },
            
        ],
    });
}

