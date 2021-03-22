using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tienda.Ventas.Areas.Users.Models;
using Tienda.Ventas.Data;
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
        private static InputModel _DataInput;
        private ApplicationDbContext _context;
        private Uploadimage _uploadimage;
        private IWebHostEnvironment _environment;

        //creo construtro para hacer iyeccion de clases que recibira como parametros objetos de las clases usermanager signmanager y rolemanager
        public RegisterModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> UserManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            //aca las inicializamos
            _UserManager = UserManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _environment = environment;
          
            _uploadimage = new Uploadimage();//objeto de la clase uploadimage
            //creo objeto de topo LUsersRoles para llamar al metodo de esa clase => metodo on get
            _userRole = new LUsersRoles();
        }

        public void OnGet() //capturar informacion
        {
            //evaluamos datainput si es distinto a null es por que contiene dato y tendra algun error al registrar usuario y estaremos nuevamente en register
            if (_DataInput!=null)
            {
                Input = _DataInput;//para mostrar informacion en el formulario si el datainput tiene 
                Input.rolesLista = _userRole.getRoles(_roleManager);//llamamos roleslista para poder asignar la informacion que tiene userroles
                Input.AvatarImage = null;//llamar al objeto avatar y le asignamos null
            }
            else
            {
                //creo objeto input que lo inicializo con la instancio de la clase inputmodel y dentro llamaos la propiedad roleslista para inicializar 
                //de acuerdo ala informacion que tenga el metodo getroles donde lo llamamos con el _userRoles que se inicializa en el constructor y que recibe
                //como parametro el _rolemanager que esta inicializado en el constructor.
                Input = new InputModel
                {
                    rolesLista = _userRole.getRoles(_roleManager)//la informacion que tenga aqui la vamos a enviar control en la pagina registrar para mostrar
                };
            }
          
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

        //metodo para capturar informacion y se va a sincronizar con una tarea
        public async Task<IActionResult> OnPost()
        {
            if (await SaveAsync())//indica que tiene que esperar a crear retorna true o false si true insertado el usuario en las tb de la bda
            {
                return Redirect("/User/User?area=Users");// returna al controlador User, al metodo de accion user y a la area users
            }
            else
            {
                return Redirect("/Users/Register");//si el saveasync es falso nuevamente la vista de registre
            }

        }

        public async Task<bool> SaveAsync()//retorna tarea false o true
        {

            //obejto que se inicializa con el obejto inputmodel y la variable
            _DataInput = Input;
            var valor = false;
            if (ModelState.IsValid)//si el dato ingresado no es igual a seleccione un rol
            {
                //con este objeto userlist le asignamos el objeto Usermanager que llamara a la tabla User de la BDA y metodo where para hacer una consulta
                //creo objeto u que manejara la infor de nuestros usuario y este objeto llamara a la propiedad email y se comparara y si ya hay este obejto 
                //contendra la informacion de ese usuario si esta registradro no podra registrase y si es el contrario lo convertirar en una lista
                var userList = _UserManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                if (userList.Count.Equals(0))//aqui evaluare userlisst contaremos cuantos elementos tienen este objeto si es 0 no existe el usuario
                {
                    //este objeto utilizara transacciones para poder administrar los SP para insertar datos a las tablas
                    var strategy = _context.Database.CreateExecutionStrategy();
                    await strategy.ExecuteAsync(async () => {
                        using (var transaction=_context.Database.BeginTransaction())
                        {
                            try
                            {
                                var user = new IdentityUser //clase que administra los usuarios identityuser
                                {
                                    UserName = Input.Email,
                                    Email = Input.Email,
                                    PhoneNumber = Input.PhoneNumber
                                };
                                var resultado = await _UserManager.CreateAsync(user, Input.Password);
                                if (resultado.Succeeded)
                                {
                                    await _UserManager.AddToRoleAsync(user, Input.Role);
                                    //el datauser le asignamos usermanger para realizar consulta en la tabla users del ultimo usuario que ya se registro
                                    var datauser = _UserManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList().Last();
                                    //manejar la foto onj imagebyte dela clase _uploadimage llamamos al metodobyte convertir image en byte e insertar informacion en tabla 
                                    var imageByte = await _uploadimage.ByteAvatarImageAsync(Input.AvatarImage, _environment, "images/images/login.png");
                                    var t_user = new TUsers
                                    {
                                        Name=Input.Name,
                                        LastName = Input.LastName,
                                        Documento=Input.Cedula,
                                        Telefono=Input.PhoneNumber,
                                        Email=Input.Email,
                                        IdUser= datauser.Id,//obtener el ultimo usuario
                                        Image=imageByte,
                                        //esta info la vamos a insertar en la tabla Tuser
                                    };
                                    await _context.AddAsync(t_user);//agramos usuario
                                    _context.SaveChanges();

                                    transaction.Commit();//este metodo todos lo sp de insertado se han ejecutado satisfactoriamente en las dos tablas de user
                                    _DataInput = null;
                                    valor = true;//esto indica la insercion del usuario

                                }
                                else
                                {
                                    foreach (var item in resultado.Errors)
                                    {
                                        _DataInput.ErrorMessage = item.Description;
                                    }
                                    valor = false;
                                    transaction.Rollback();
                                }
                            }
                            catch (Exception ex)
                            {

                                _DataInput.ErrorMessage = ex.Message;
                                transaction.Rollback();
                                valor = false;
                            }

                        }
                    });

                }
                else
                {
                    _DataInput.ErrorMessage = $"El {Input.Email} ya esta registrado";
                    valor = false;//indicnado que este usuarioi no puede registrarse
                }

            }
            else
            {
                //objeto mosState capturar la informacion que suministrar el modelState
                foreach (var modelState in ModelState.Values)
                {
                    //para obtener la propiedad errors que contiene la coleccion de errores usando el modelState
                    foreach (var error in modelState.Errors)
                    {
                        //capturar todos los errores y se almacenaran el el Erros
                        _DataInput.ErrorMessage += error.ErrorMessage;

                    }
                }
               
                valor = false;
            }


            return valor;
        }
    }
}
