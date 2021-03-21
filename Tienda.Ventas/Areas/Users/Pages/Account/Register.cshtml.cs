using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tienda.Ventas.Areas.Users.Models;
using Tienda.Ventas.Library;

namespace Tienda.Ventas.Areas.Users.Pages.Account
{
    public class RegisterModel : PageModel
    {
        //creo los objetos de las clases  siginmanager que administra la informacion de los usuarios que ingresan sesion
        //objeto UserManager administra la informacion de los usuarios que tengo registrados
        //objeto RoleManager administra los roles que hemos creado
        //
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _UserManager;
        private RoleManager<IdentityRole> _roleManager;
        private LUsersRoles _userRole;

        //creo construtro para hacer iyeccion de clases que recibira como parametros objetos de las clases usermanager signmanager y rolemanager
        public RegisterModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> UserManager,
            RoleManager<IdentityRole> roleManager)
        {
            //aca las inicializamos
            _UserManager = UserManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            //creo objeto de topo LUsersRoles para llamar al metodo de esa clase => metodo on get
            _userRole = new LUsersRoles();
        }

        public void OnGet()
        {
            //creo objeto input que lo inicializo con la instancio de la clase inputmodel y dentro llamaos la propiedad roleslista para inicializar 
            //de acuerdo ala informacion que tenga el metodo getroles donde lo llamamos con el _userRoles que se inicializa en el constructor y que recibe
            //como parametro el _rolemanager que esta inicializado en el constructor.
            Input = new InputModel
            {
                rolesLista = _userRole.getRoles(_roleManager)//la informacion que tenga aqui la vamos a enviar control en la pagina registrar para mostrar
            };
        }

        //esta propiedad tipo inputModel para poder acceder a los elementos de la clase InputoModel desde la vista
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegister
        {
            public IFormFile AvatarImage { get; set; }//propiedad para el input del registro
            public string ErrorMessage { get; set; }
            public List<SelectListItem> rolesLista { get; set; }
        }
    }
}
