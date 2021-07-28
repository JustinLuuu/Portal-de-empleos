// listeners
document.addEventListener('DOMContentLoaded', () => {
    const containerFluid = document.querySelector('.container-fluid');
    containerFluid.classList.add('p-0', 'px-lg-0', 'px-md-0');
    AgregarEventIconoBasura();
});

function AgregarEventIconoBasura() {
    const tdIconos =
        document.querySelectorAll('table tr td:last-child') ?
            document.querySelectorAll('table tr td:last-child .fa-trash-alt') : null;
    if (tdIconos) {
        tdIconos.forEach(x => {
            x.addEventListener('click', (eventTrigger) => {
                if (eventTrigger.target.classList.contains('fa-trash-alt')) {
                    Swal.fire({
                        text: '¿ Seguro que quieres eliminar esta oferta ?',
                        imageUrl: `../img/basura.png`,
                        imageWidth: 140,
                        imageHeight: 140,
                        imageAlt: 'bote-basura',
                        showDenyButton: true,
                        denyButtonText: 'Cancelar',
                        denyButtonColor: '#E19216',
                        showConfirmButton: true,
                        confirmButtonColor: '#D14529',
                        confirmButtonText: 'Eliminar'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            const form = document.querySelector('#form-eliminarOferta');
                            const input = document.querySelector('#idOferta');
                            input.value = eventTrigger.target.getAttribute('data-idOferta');
                            form.submit();
                        }
                    });
                }
            });
        });
    }
}

var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
});
