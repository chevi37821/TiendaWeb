//creo una clase pero adentro creo el metodo ARCHIV que recibe como parametro un evento del input y un id de un elemento y en las llave
//creo el objeto donde le asigno el parametro evt. qe contiene toda la informacion del input donde invoca la propiedad target que sera la informacion del inputfile
//
class Uploadpicture {
    archivo(evt, id) {
        let files = evt.target.files;//este files es una coleccion de objetos
        let f = files[0];//obtenderemos el dato de la coleccion cero y se guradara en f
        if (f.type.match('image'))
        { 
            //este if verificara el tipo de arhivo cargado que sea solo imagen 'image', true entonces crea un objeto de la clase filereader para asignar una funcion
            //para cargar la imagen que recibe como parametro thefile luego en la funcion anonima llamaremos el metodo getelemt y es de acuerdo al id.en registrar
            let reader = new FileReader();
            reader.onload = ((thefile) => {
                return (e) => {
                    document.getElementById(id).innerHTML = ['<img class="imageUser" src="',
                        e.target.result, '" title="', escape(thefile.name), '"/>'].join('');
                }
            })(f);
            reader.readAsDataURL(f);
        }
    }
}