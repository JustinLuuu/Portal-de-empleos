const imgDiv = document.querySelector('.update-img');
const img = document.querySelector('#update-img-perfil');
const file = document.querySelector('#update-image-file');
const uploadBtn = document.querySelector('#descargar-foto-label');


file.addEventListener('change', function(){
    const choosedFile = this.files[0];

    if (choosedFile) {

        const reader = new FileReader(); 

        reader.addEventListener('load', function(){
            img.setAttribute('src', reader.result);
        });

        reader.readAsDataURL(choosedFile);
    }
});