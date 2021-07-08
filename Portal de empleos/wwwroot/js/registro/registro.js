import { EnviarDatos } from './peticion.js';

// variables

const formulario = document.querySelector('form');
const visor = document.querySelector('.password-visor');


// listeners
formulario.addEventListener('submit', (event) => {

    event.preventDefault();

    const nombre = document.querySelector('form > div:first-child .form-control').value;
    const apellido = document.querySelector('form > div:nth-child(2) .form-control').value;
    let permiso = true;
    let error = false;

    if ((nombre+apellido).match(/[`!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/) || (nombre+apellido).match(/\d+/)) {
        NotificacionError('Tus nombres no pueden tener numeros o caracteres especiales','¿ #$%&@*!/= ?');
        permiso = false;
        error = true;
    }

    if (document.getElementsByClassName('password')[0].value != document.getElementsByClassName('password')[1].value && permiso) {
        NotificacionError('Las contraseñas no coinciden');
        error = true;
    }

    if (!error) {
        Registrar(
            {
                NOMBRE: nombre,
                APELLIDO: apellido,
                EMAIL: document.getElementById('EMAIL').value,
                CONTRASENA: document.getElementById('CONTRASENA').value,
                URL_SITIO: document.getElementById('URL_SITIO').value
            }
        );                  
    }
});

visor.addEventListener('click', () => {
    var input_p = document.querySelector('.input_password');
    var eye_c = document.querySelector('.eye-closed');
    var eye_o = document.querySelector('.eye-open');

    if (input_p.type === 'password') {
        input_p.type = "text";
        eye_c.style.display = "none";
        eye_o.style.display = "block";
    } else {
        input_p.type = "password";
        eye_c.style.display = "block";
        eye_o.style.display = "none";
    }
})

// funciones

function Registrar(obj) {
    EnviarDatos(obj)
    .then((res) => {
        return res.json();
    })
    .then((res) => {
    res.tipo ? NotificacionExito(res.mensaje, obj) : NotificacionError(res.mensaje);
    });   
}


function NotificacionError(mensaje, titulo) {

    let img;
    if (mensaje === 'Ya existe un usuario con ese email') { img = '../img/correoexiste.png'; }
    else {img = '../img/pinguinotriste.png'; }

    Swal.fire({
        title: !titulo ? 'ATENCION' : titulo,
        html: `<strong style="color:red; font-size:18px;">¡ ${mensaje} !</strong>`,
        imageUrl: `${img}`,
        imageWidth: 140,
        imageHeight: 140,
        imageAlt: 'foto-mensaje',
        showDenyButton: true,
        denyButtonText: 'Cerrar',
        showConfirmButton: false
    });
}

function NotificacionExito(mensaje, usuario) {
    const img = '../img/gifpersonas.gif';
    Swal.fire({
        title: 'ENHORABUENA',
        html: `<strong style="color:green; font-size:17px; font-family:Verdana;">¡ ${mensaje} !</strong> <br> <div class="spinner-border text-primary mt-3" role="status"></div> <div class="spinner-border text-warning mt-3" role="status"></div>`,
        imageUrl: `${img}`,
        imageWidth: 320,
        imageHeight: 200,
        imageAlt: 'foto-mensaje',
        showDenyButton: false,
        showConfirmButton: false,
        allowOutsideClick: false
    });

}
