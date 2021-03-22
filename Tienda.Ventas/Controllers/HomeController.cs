using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tienda.Ventas.Models;

namespace Tienda.Ventas.Controllers
{
    public class HomeController : Controller
    {
      // IServiceProvider _serviceProvider;//si necestiamos crear mas roles activamos esto

        public HomeController(IServiceProvider serviceProvider)//como parametro el objeto para hacer la inyeccion dp
        {
        //   _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Index()//convertir asyncrono
        {
         //  await CreateRolesAsync(_serviceProvider);//llamamos al metodo create
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task CreateRolesAsync(IServiceProvider serviceProvider)//recibe parametro de la interfaceserviceprovide
        {
            //creo objeto asiganmos el parametro de la interface(serviceprovider) para llamar al metodo getrequiredservice asi obtener el servicio 
            //identityrole usando la clase rolemanager
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //creo arreglo de tipo string lo inicicializo con los roles que ire a manejar
            String[] rolesName = { "Administrador", "Vendedor" };
            //recorrer los roles 
            foreach (var item in rolesName)//recorremos roles mediante la variable item
            {
                //creo objeto de tipo bool retorna valor verdadero o falso await(esepre) al metodo CreateRolesAsync que realizae la tarea roleExistsAsync
                var roleExist = await roleManager.RoleExistsAsync(item);//verificar el role que tiene item y si existe retorna true si no false
                if (!roleExist)//si esta variable tiene el valor verdadero es como si fuera falso--como crear role
                {
                    await roleManager.CreateAsync(new IdentityRole(item));

                }


            }

                                                                        
        }
    }
}
