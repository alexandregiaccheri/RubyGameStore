$(document).ready(function () {
    carregarDataTable();
});

function carregarDataTable() {
    dataTable = $('#indexProdutos').DataTable({
        "ajax": {
            "url": "/Admin/Produto/GetAll"
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
        "responsive": "true",
        "columns": [
            { "data": "titulo" },
            { "data": "desenvolvedor" },
            { "data": "genero" },
            { "data": "plataforma" },
            { "data": "precoNormal" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="btn-group">
                            <a href="/Admin/Produto/Upsert?id=${data}" class="btn btn-outline-light mx-2"><i class="bi bi-pencil"></i></a>
                            <a onClick=ApagarProduto('/Admin/Produto/Delete?id=${data}') class="btn btn-outline-light"><i class="bi bi-x-lg"></i></a>
                        </div>
                    `
                }
            }
        ]
    });
}

function ApagarProduto(url) {
    Swal.fire({
        title: 'Você tem certeza?',
        text: "Não será possivel desfazer esta ação!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#c62e2e',
        cancelButtonColor: '#2b77c0',
        confirmButtonText: 'Sim, apague!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.sucesso == true) {
                        dataTable.ajax.reload();
                        toastr.success(data.mensagem);
                    }
                    else {
                        toastr.error(data.mensagem);
                    }
                }
            })
        }
    })
}