// listeners
document.addEventListener('DOMContentLoaded', () => {
    const containerFluid = document.querySelector('.container-fluid');
    containerFluid.classList.add('p-0', 'px-lg-0', 'px-md-0');
});

var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
});
