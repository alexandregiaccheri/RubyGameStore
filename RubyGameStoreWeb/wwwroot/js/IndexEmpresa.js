$(document).ready(function () {
    carregarDataTable();
});

function carregarDataTable() {
    dataTable = $('#index').DataTable({
        "ajax": {
            "url": "/Admin/Empresa/GetAll"
        },
        "responsive": "true",
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
        "columns": [
            { "data": "nomeEmpresa" },
            { "data": "cnpjEmpresa" },
            { "data": "telefoneEmpresa" },
            { "data": "cidadeEmpresa" },
            { "data": "cepEmpresa" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="btn-group">
                            <a href="/Admin/Empresa/Upsert?id=${data}" class="btn btn-outline-light mx-2"><i class="bi bi-pencil-square"></i></a>
                            <a onClick=Apagar('/Admin/Empresa/Delete?id=${data}') class="btn btn-outline-light"><i class="bi bi-x-square"></i></a>
                        </div>
                    `
                }
            }
        ]
    });
}

function Apagar(url) {
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