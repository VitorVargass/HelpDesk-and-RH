// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', (event) => {
    



    let table = new DataTable('.table');

    const wrapper = document.querySelector('.wrapper');
    const loginLink = document.querySelector('.login-link');
    const registerLink = document.querySelector('.register-link');
    const btnPopup = document.querySelector('.btnLogin-popup')
    const iconClose = document.querySelector('.icon-close')

    registerLink.addEventListener('click', () => {
        wrapper.classList.add('active');
    }); 

    loginLink.addEventListener('click', () => {
        wrapper.classList.remove('active');
    }); 

    btnPopup.addEventListener('click', () => {
        wrapper.classList.add('active-popup');
    });

    iconClose.addEventListener('click', () => {
        wrapper.classList.remove('active-popup');
    });

});

var btnAbrir = document.querySelector('.abrirMenu')
var menuHamburg = document.querySelector('.menuHamburg')
var btnFechar = document.querySelector('.fecharMenu')
var tabela = document.querySelector('.tabela')

btnAbrir.addEventListener('click', function () {
    menuHamburg.style.width = '250px'
    tabela.style.display = 'none'
})
btnFechar.addEventListener('click', function () {
    menuHamburg.style.width = '0'
    tabela.style.display = 'flex'
})