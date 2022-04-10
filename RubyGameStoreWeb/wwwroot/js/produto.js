$(document).ready(function () {
    carregarDataTable();
});

function carregarDataTable() {
    dataTable = $('#indexProdutos').DataTable({
        "ajax": {
            "url": "/Admin/Produto/GetAll"
        },
        "responsive": "true",
        "columns": [
            { "data": "titulo" },
            { "data": "desenvolvedor" },
            { "data": "distribuidor" },
            { "data": "genero" },
            { "data": "plataforma" },
            { "data": "preco" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="btn-group">
                            <a href="/Admin/Produto/Upsert?id=${data}" class="btn btn-warning mx-2"><i class="bi bi-pencil-square"></i></a>
                            <a onClick=ApagarProduto('/Admin/Produto/Delete?id=${data}') class="btn btn-danger"><i class="bi bi-x-square"></i></a>
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