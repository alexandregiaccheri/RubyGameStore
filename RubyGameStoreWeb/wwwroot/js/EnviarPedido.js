function Validar() {
    if (document.getElementById("transportadora").value == "") {
        Swal.fire({
            icon: 'error',
            title: 'Esqueceu de algo?',
            text: 'Você deve informar a transportadora!'
        })
        return false
    };
    if (document.getElementById("rastreio").value == "") {
        Swal.fire({
            icon: 'error',
            title: 'Esqueceu de algo?',
            text: 'Você deve informar o rastreio!'
        })
        return false
    }
    return true
}