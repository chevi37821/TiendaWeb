// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var principal = new Principal();//objeto de la clase principal

/*Codigo de usuarios*/
var user = new User();

//creo funcion imageuser le paso parametro el eevento del input y dentro el obejto user llama la metodo archivo de uploadpicture y le pasamos
//evt contiene la informacion del input y la etiqueta que contendra la nueva imagen

var imageUser = (evt) => {
    user.archivo(evt, "imageUser");
}

//llamamos al metodo ready que siempre se ejecutar cada vez que pasemos a las paginas de la aplicacion y adentro creamos funcion anonima y aqui creo un objeto
//local urlactual y le asignamos metodo windows para llamar al metodo location y pathname (parametros de la url /user/registrer )
$().ready(() => {
    let URLactual = window.location.pathname;
    principal.userLink(URLactual);
})