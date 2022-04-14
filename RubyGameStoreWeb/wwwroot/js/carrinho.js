function remover(url) {
    Swal.fire({
        title: 'Remover do Carrinho?',
        showCancelButton: true,
        confirmButtonColor: '#c62e2e',
        cancelButtonColor: '#2b77c0',
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.replace(url)
        }
    })
}