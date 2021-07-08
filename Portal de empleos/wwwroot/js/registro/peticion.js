export async function EnviarDatos(obj) {
     return fetch('Registro', {
        body: JSON.stringify(obj),
        method: 'POST',
        headers: {
            'RequestVerificationToken': document.getElementsByName('__RequestVerificationToken')[0].value,
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        }
     });   
}
