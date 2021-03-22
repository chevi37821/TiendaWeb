using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.Ventas.Areas.Users.Models
{
    public class InputModelRegister
    {
        [Required(ErrorMessage="Este Campo Nombre es Obligatorio.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="El Campo Apellidos es Requerido.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo Cedula es Obligatorio")]
        public string Cedula { get; set; }

        [Required(ErrorMessage ="El campo Telefono es Obligatorio")]
        [DataType(DataType.PhoneNumber)]//tipo de dato a almacenar
        [RegularExpression(@"\(?([0-9]{2})\)?[-.]?([0-9]{2})[-.]?([0-9]{5})$",ErrorMessage ="El formato Telefono Ingresado no es valido.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="El campo Correo Electronico es Obligartorio")]
        [EmailAddress(ErrorMessage ="los datos ingresados no coinciden con una direccion de correo valida.")]
        public string Email { get; set; }
        
        [Display(Name ="Contraseña")]
        [Required(ErrorMessage ="El campo Contraseña es Obligatorio.")]
        [StringLength(100,ErrorMessage ="El numero de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Seleccione un role.")]
        public string Role { get; set; }
    }
}
