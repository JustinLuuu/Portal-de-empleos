import { PeticionDatos } from './peticion.js';

// variables
const cardFiltro = document.querySelector('.card-form-filtro');
const cardFiltroOpcion = document.querySelector('.selectOpt');
const perfilUpdateForm = document.querySelector('.perfil-update-form');
const passwordUpdateForm = document.querySelector('.password-update-form');
const buttonsCancel = document.getElementsByClassName('cancel');
const logout = document.querySelector('.end-session');
const imgPerfil = document.querySelector('#update-img-perfil');
const imgFile = document.querySelector('#update-image-file');
const selectOrdenar = document.querySelector('.select-ordenar') ? document.querySelector('.select-ordenar') : null;

// listeners
document.addEventListener('DOMContentLoaded', () => {
    const containerFluid = document.querySelector('.container-fluid');
    containerFluid.classList.add('p-0', 'px-lg-0', 'px-md-0');
    valorFormFiltro();
    AgregarEventIconoBasura();
});

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

    const select_pr = document.querySelector(".selectOpt");
    var array = document.getElementsByClassName("search");

    if (select_pr.selectedIndex == 0 || !Validar(array)) {
        event.preventDefault();
        Error('Debes aplicar el filtro correctamente');
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

passwordUpdateForm.addEventListener('submit', (event) => {
    if (document.getElementsByClassName('new-password')[0].value != document.getElementsByClassName('new-password')[1].value) {
        event.preventDefault();
        Error('Las nuevas contraseñas no coinciden');
    }
});

buttonsCancel[0].addEventListener('click', () => {
    ResetForms();
    ResetFotoUpdatePerfil();
});

buttonsCancel[1].addEventListener('click', () => {
    ResetForms();
});

document.addEventListener('click', (eventTrigger) => {
    if (eventTrigger.target.id === 'modal-update-perfil') {
        ResetForms();
        ResetFotoUpdatePerfil();
    }
    if (eventTrigger.target.id === 'modal-update-password') {
        ResetForms();
    }

    eventTrigger.stopPropagation();
});

imgFile.addEventListener('change', function () {
    const choosedFile = this.files[0];

    if (choosedFile) {

        const reader = new FileReader();

        reader.addEventListener('load', function () {
            imgPerfil.setAttribute('src', reader.result);
        });

        reader.readAsDataURL(choosedFile);
    }
    document.querySelector('.btn-update-profile').disabled = false;
});

logout.addEventListener('click', () => {
    Swal.fire({
        text: '¿ Seguro que quieres cerrar sesión ?',
        imageUrl: `../img/logout.png`,
        imageWidth: 140,
        imageHeight: 140,
        imageAlt: 'puerta-salir',
        showDenyButton: true,
        denyButtonText: 'olvídalo..',
        denyButtonColor: '#E19216',
        showConfirmButton: true,
        confirmButtonColor: '#D14529',
        confirmButtonText: 'Cerrar Sesión '
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = 'BorrarCookie';
        }
    });

});

if (selectOrdenar) {
    selectOrdenar.addEventListener('change', () => {
        const tipoOrden = selectOrdenar.options[selectOrdenar.selectedIndex].value;
        if (tipoOrden != "null") {
            EnviarPeticion(tipoOrden);
        }
    });
}

// funciones
function valorFormFiltro() {
    const filtroValor = document.querySelector('#filtroValor');
    const searchInputs = document.querySelectorAll(".search");
    searchInputs.forEach(x => {
        if (x.tagName == 'SELECT') {
            x.addEventListener('change', (e) => {
                filtroValor.value = e.target.options[e.target.selectedIndex].value;
            });
        }
        else {
            x.addEventListener('input', (e) => {
                filtroValor.value = e.target.value;
            });
        }
    });
}

function Error(mensaje) {
    Swal.fire({
        title: `ATENCION`,
        html: `<strong style="color:red;">¡ ${mensaje} !</strong>`,
        imageUrl: `../img/pinguinotriste.png`,
        imageWidth: 140,
        imageHeight: 140,
        imageAlt: 'pinguino-desanimado',
        showDenyButton: true,
        denyButtonText: 'Cerrar',
        showConfirmButton: false
    });
}

function DisabledReactivo() {
    var array = document.getElementsByClassName("input-profile");

    if (array[0].files.length != 0) {
        document.querySelector('.btn-update-profile').disabled = false;
    }
    else if (array[1].value.toUpperCase() == array[1].defaultValue.toUpperCase() &&
        array[2].value.toUpperCase() == array[2].defaultValue.toUpperCase() &&
        array[3].value.toUpperCase() == array[3].defaultValue.toUpperCase() &&
        array[4].value.toUpperCase() == array[4].defaultValue.toUpperCase() &&
        array[5].value.toUpperCase() == array[5].defaultValue.toUpperCase()) {
        document.querySelector('.btn-update-profile').disabled = true;
    }
    else {
        document.querySelector('.btn-update-profile').disabled = false;
    }
}

function ResetForms() {
    perfilUpdateForm.reset();
    passwordUpdateForm.reset();
    document.querySelector('.btn-update-profile').disabled = true;
}

function ResetFotoUpdatePerfil() {
    const imgSrc = document.querySelector('.img-profile').src;
    document.querySelector('#update-img-perfil').src = imgSrc;
}

function AgregarEventIconoBasura() {
    const tdIconos =
        document.querySelectorAll('table tr td:last-child') ?
            document.querySelectorAll('table tr td:last-child .fa-trash-alt') : null;
    if (tdIconos) {
        tdIconos.forEach(x => {
            x.addEventListener('click', EliminarOferta);
        });
    }
}

function EliminarOferta(eventTrigger) {
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
                const idOferta = eventTrigger.target.getAttribute('data-idOferta');
                const form = document.createElement('form');
                form.method = 'POST';
                form.action = 'EliminarOferta';
                form.controller = 'Acceso';
                form.style.display = 'none';

                const id = document.createElement('input');
                id.setAttribute('name', 'ID');
                id.setAttribute('type', 'hidden');
                id.setAttribute('value', idOferta);
                const token = document.getElementsByName("__RequestVerificationToken")[0].cloneNode(true);

                form.appendChild(id);
                form.appendChild(token);
                document.body.appendChild(form);
                form.submit();
            }
        });
    }
}

function EnviarPeticion(tipoOrden) {
    PeticionDatos(tipoOrden).then((res) => {
        return res.json();
    })
    .then((res) => {
        OrdenarTabla(res);
        CambiarTituloOrden(tipoOrden);
        AgregarEventIconoBasura();
    });
}

function OrdenarTabla(array) {
    const tabla = document.querySelector('.table tbody');
    while (tabla.firstChild) {
        tabla.removeChild(tabla.firstChild);
    }

    array.forEach(oferta => {
        const fila = document.createElement('tr');
        fila.innerHTML = `
            <td>
                <div class="d-flex align-items-center">
                    <div class="m-r-10">
                        <img src="${oferta.logo}"
                                alt="logo-de-${oferta.empresa}" width="35" class="rounded-circle">
                    </div>
                    <div>
                        <h4 class="m-b-0 font-14">${oferta.empresa}</h4>
                    </div>
                </div>
            </td>

            <td>
                ${oferta.titulo.length > 26 ? oferta.titulo.substring(0, 26) + '..' : oferta.titulo}                
            </td>

            <td>
                ${oferta.categoria.nombre}
            </td>

            <td>
                ${oferta.provincia.nombre}
            </td>

            <td>
                ${oferta.modalidad}
            </td>

            <td>
                ${oferta.sueldo}
                <i class="fas fa-dollar-sign"></i>
            </td>

            <td>
               ${oferta.fechaabreviado}
            </td>

            <td>
                <i class="fas fa-eye" title="Ver detalle" data-bs-toggle="modal"
                    data-bs-target="#modal-oferta" onclick='OfertaInfo(${oferta.id})'></i>
                <a href=""><i class="fas fa-pencil-alt" title="Editar"></i></a>
                <i class="fas fa-trash-alt" data-idOferta="${oferta.id}" title="Eliminar"></i>
            </td>
        `;
        tabla.appendChild(fila);
    });
}

function CambiarTituloOrden(tipoOrden) {
    const ordenTitulo = document.querySelector('.titulo-tipo-orden');

    switch (tipoOrden) {
        case '1':
            ordenTitulo.textContent = 'Vista de últimas ofertas ordenadas por título';
        break;

        case '2':
            ordenTitulo.textContent = 'Vista de últimas ofertas ordenadas por sueldo';
        break;

        case '3':
            ordenTitulo.textContent = 'Vista de últimas ofertas agrupadas por modalidad';
        break;

        case '4':
            ordenTitulo.textContent = 'Vista de últimas ofertas agrupadas por categoría';
        break;

        case '5':
            ordenTitulo.textContent = 'Vista de últimas ofertas agrupadas por provincia';
        break;
    }
}

// tooltip bootstrap
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
});