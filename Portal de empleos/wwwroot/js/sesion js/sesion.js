// variables
const cardFiltro = document.querySelector('.card-form-filtro');
const cardFiltroOpcion = document.querySelector('.selectOpt');
const perfilUpdateForm = document.querySelector('.perfil-update-form');
const passwordUpdateForm = document.querySelector('.password-update-form');
const buttonsCancel = document.getElementsByClassName('cancel');

// listeners
cardFiltroOpcion.addEventListener('change', () => {
    var array = document.getElementsByClassName("search");
    for (var i = 0; i < array.length; i++) {
        array[i].style.display = 'none';
        array[i].selectedIndex = 0;
    }
    var myselect = document.querySelector(".selectOpt");
    document.getElementsByClassName("search")[myselect.selectedIndex].style.display = 'block';
});

cardFiltro.addEventListener('submit', (event) => {

    const array = document.getElementsByClassName("search");
    const select_pr = document.querySelector(".selectOpt");

    if (select_pr.selectedIndex == 0 || !Validar(array)) {
        event.preventDefault();
        Notificacion(false, 'Debes aplicar un filtro')
    }

    function Validar(array) {
        for (let i = 0; i < array.length; i++) {
            if (array[i].selectedIndex != 0 ||
                (array[i].getAttribute("type") == "text" && array[i].value) ||
                (array[i].getAttribute("type") == "date" && Date.parse(array[i].value))) {
                return true;
            }
        }
        return false;
    }
});

perfilUpdateForm.addEventListener('input', () => {
    DisabledReactivo();
});

passwordUpdateForm.addEventListener('submit', (event) =>{
    if(document.getElementsByClassName('new-password')[0].value != document.getElementsByClassName('new-password')[1].value){
        event.preventDefault();
        Notificacion(false, 'Las nuevas contraseñas no coinciden')
    }
});

buttonsCancel[0].addEventListener('click', () => {
    ResetForms();
});

buttonsCancel[1].addEventListener('click', () => {
    ResetForms();
});

document.addEventListener('click', (eventTrigger) => {
    if (eventTrigger.target.id === 'modal-update-perfil' || eventTrigger.target.id === 'modal-update-password') {
        ResetForms();
    }
    eventTrigger.stopPropagation();
});

document.addEventListener('DOMContentLoaded', () => {
    const containerFluid = document.querySelector('.container-fluid');
    containerFluid.classList.add('p-0', 'px-lg-0', 'px-md-0');
});

// funciones
function Notificacion(tipo, mensaje) {

    const img = tipo ? 'http://aritz-urresti.com/wp-content/uploads/2015/02/Como-obtener-el-exito-en-la-vida.jpg' :
        'https://shinemag.do/wp-content/uploads/2021/01/fracaso-940x675.png';
    const color = tipo ? 'green' : 'red';
    const title = tipo ? 'ENHORABUENA' : 'ATENCION';

    Swal.fire({
        title: `${title}`,
        html: `<strong style="color:${color};">¡ ${mensaje} !</strong>`,
        imageUrl: `${img}`,
        imageWidth: 140,
        imageHeight: 140,
        imageAlt: 'pinguino-desanimado',
        showDenyButton: true,
        denyButtonText: 'Cerrar',
        showConfirmButton: false
    });
}

function ResetForms() {
    perfilUpdateForm.reset();
    passwordUpdateForm.reset();
    document.querySelector('.btn-update-profile').disabled = true;
}

function DisabledReactivo() {
    var array = document.getElementsByClassName("input-profile");

    if (array[0].value.toUpperCase() == array[0].defaultValue.toUpperCase() && array[1].value.toUpperCase() == array[1].defaultValue.toUpperCase() &&
        array[2].value.toUpperCase() == array[2].defaultValue.toUpperCase() && array[3].value.toUpperCase() == array[3].defaultValue.toUpperCase() &&
        array[4].value.toUpperCase() == array[4].defaultValue.toUpperCase()) {
        document.querySelector('.btn-update-profile').disabled = true;
    }
    else {
        document.querySelector('.btn-update-profile').disabled = false;
    }
}
