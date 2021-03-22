using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.Ventas.Library
{
    public class Uploadimage
    {
        //este metodo va a ejecutar un arreglo de tipo byte recibe Interface formfile contiene datos de la imagen y con la interface de web obtendra
        //el directorio de nuestra aplicacion
        public async Task<byte[]> ByteAvatarImageAsync(IFormFile AvatarImage, IWebHostEnvironment environment, string image)
        {
            
            if (AvatarImage !=null)//evaluamos es distinto a null contiene informacion de la imagen que se ha cargado
            {
                using (var memoryStream = new MemoryStream())//este memory almacenaremos informacion
                {
                    await AvatarImage.CopyToAsync(memoryStream);//tiene que esperar al metodo copytoasync y la ifno de avatar la pasamos al memory
                    return memoryStream.ToArray();//retorno de datos de la imagen la convertimos en un array
                }

            }
            else
            {
                //si no tiene datos entonces cargara la variable image que es la imagen por defecto
                var archivoOrigen = $"{environment.ContentRootPath}/wwwroot/{image}";
                return File.ReadAllBytes(archivoOrigen);//este retorno clase file llamar al metodo readallbyte para leer la imagen en el directorio y returna el array
            }
        }

    }
}
