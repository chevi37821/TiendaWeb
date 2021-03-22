using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.Ventas.Areas.Users.Models
{
    public class TUsers
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string IdUser { get; set; }// alamacenar la id del regitsto de la tabla user donde almacenamos lo user registrados columna Id
        public byte[] Image { get; set; }
    }
}
