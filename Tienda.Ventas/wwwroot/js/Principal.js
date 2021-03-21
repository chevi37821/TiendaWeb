//en el metodo le pasaremos como parametro la url que obtengamos de la imagen
class Principal {
    userLink(URLactual) {
        let url = "";
        let cadena = URLactual.split("/");//split devolvera un array de acuerdo al caracter /
        for (var i = 0; i < cadena.length; i++) { /*empieza en cero y sera hasta la longitud de la array cadena*/
            if (cadena[i] != "Index") { //obtendermo el dato que este en cadena y lo compararemos con el dato en cadena si estamos en index y no habra de ejecturael if
                url += cadena[i];
            }
        }
        //en este switch para poder evaluar la variable url y en el case contendra userregistrar y si la url contiene userregistrer ejecutamos obtendra 
        //el id files del input imagen y el evento cambiar change y ejecutamos la funcion js en site.js
        switch (url) {
            case "UsersRegister":
                document.getElementById('files').addEventListener('change', imageUser, false)
                break;
            
        }
    }
    
}