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
        Error('Tus nombres no pueden tener numeros o caracteres especiales','¿ #$%&@*!/= ?');
        permiso = false;
        error = true;
    }

    if (document.getElementsByClassName('password')[0].value != document.getElementsByClassName('password')[1].value && permiso) {
        Error('Las contraseñas no coinciden');
        error = true;
    }

    if (!error) {
        Registrar(
            {
                NOMBRE: nombre.replace(/\b\w/g, l => l.toUpperCase()),
                APELLIDO: apellido.replace(/\b\w/g, l => l.toUpperCase()),
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
        res.tipo ? Exito(res.mensaje, obj) : Error(res.mensaje);
    });   
}


function Error(mensaje, titulo) {

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

function Exito(mensaje, usuario) {
    const img = '../img/caminandoaltrabajo.gif';
    Swal.fire({
        title: 'ENHORABUENA',
        html: `<div class="bg-success text-white p-2 rounded-pill fw-bold w-100 m-auto">¡ ${mensaje} !</div> <div class="spinner-border text-primary mt-3" role="status"></div>`,
        imageUrl: `${img}`,
        imageWidth: 320,
        imageHeight: 200,
        imageAlt: 'foto-mensaje',
        showDenyButton: false,
        showConfirmButton: false,
        allowOutsideClick: false
    });

    setTimeout(() => {
        const form = document.createElement('form');
        form.method = 'POST';
        form.action = 'Login';
        form.controller = 'Home';
        form.style.display = 'none';

        const email = document.createElement('input');
        email.setAttribute('name', 'EMAIL');
        email.setAttribute('type', 'hidden');
        email.setAttribute('value', usuario.EMAIL);
  
        const contra = document.createElement('input');
        contra.setAttribute('name', 'CONTRASENA');
        contra.setAttribute('type', 'text');
        contra.setAttribute('value', usuario.CONTRASENA);

        const token = document.getElementsByName("__RequestVerificationToken")[0].cloneNode(true);

        form.appendChild(email);
        form.appendChild(contra);
        form.appendChild(token);

        document.body.appendChild(form);
        form.submit();
    }, 3000);
}
