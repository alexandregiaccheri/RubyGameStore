function Cancelar(id) {
    Swal.fire({
        title: 'Cancelar o Pedido?',
        icon: 'warning',
        text: 'Não será possível desfazer esta ação!',
        showCancelButton: true,
        confirmButtonColor: '#c62e2e',
        cancelButtonColor: '#2b77c0',
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.replace(`/Admin/Pedidos/Cancelar/${id}`)
        }
    })
}

function Processar(id) {
    Swal.fire({
        title: 'Processar o Pedido?',
        icon: 'info',
        text: 'Não será possível desfazer esta ação!',
        showCancelButton: true,
        confirmButtonColor: '#c62e2e',
        cancelButtonColor: '#2b77c0',
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.replace(`/Admin/Pedidos/Processar/${id}`)
        }
    })
}