export async function PeticionDatos(tipoOrden) {
    return fetch(`OrdenarOfertas/${tipoOrden}`, {
        method: 'GET'  
    });
}