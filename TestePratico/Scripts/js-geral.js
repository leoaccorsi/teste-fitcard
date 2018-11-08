$(document).ready(function () {
    $('.table').DataTable();
    $("input").attr("autocomplete", "off");
});

$(".btnDelete").click(function (e) {
    e.preventDefault();

    swal({
        title: 'Você tem certeza?',
        text: "Você não terá mais acesso a este registro!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, excluir!',
        cancelButtonText: 'Cancelar'
    }).then(function (result) {
        if (result.value) {
            window.location.href = e.target.href;
        }
    });

});