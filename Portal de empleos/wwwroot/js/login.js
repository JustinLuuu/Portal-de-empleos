// variables
const visor = document.querySelector('.password-visor');


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
});
