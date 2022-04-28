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

function verificaFrete(tagId) {
    if (document.getElementById(tagId) != null) {
        Swal.fire({
            title: 'Escolha uma forma de entrega!',
            confirmButtonColor: '#aa0000',
            icon: 'warning'
        })
    }
    else {
        window.location.replace('/User/Carrinho/CriarPedido')
    }
}

function frete(valor) {
    window.location.replace('/User/Carrinho/DefinirFrete?idFrete=' + valor)
}