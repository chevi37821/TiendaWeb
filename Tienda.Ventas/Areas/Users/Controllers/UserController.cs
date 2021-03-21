using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.Ventas.Areas.Users.Controllers
{
    
    /// indicarle al controlador en que area se encuentra    
    [Area("Users")]
    public class UserController : Controller
    {
        public IActionResult User()
        {
            return View();
        }
    }
}
