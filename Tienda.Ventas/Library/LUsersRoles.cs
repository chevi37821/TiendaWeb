using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.Ventas.Library
{
    public class LUsersRoles
    {
        //procedimiento para poder obterner los roles y visualizarlos en el registro
        //retorna collecion de la clase list,rolemanager administrar roles 
        public List<SelectListItem> getRoles(RoleManager<IdentityRole> roleManager)
        {
            //creo objeto de la clase list llamada seleclist que contendra una coleccion de objeto de la clase seleclistitem usando propiedad text y value =id role
            List<SelectListItem> _selecList = new List<SelectListItem>();
            //creo objeto asginamos el parametro rolemanager donde llamara roles que contiene los rolesy el tolist para convertir en coleccion de datos 
            //y lo guardara en el objeto roles
            var roles = roleManager.Roles.ToList();
            //creo un objeto foreach de tipo roles recorremos el roles luego creamos objetos para obtener la coleccion (item) el selectlist agregaremos 
            //coleccion de datos en seleclistitem dandole las propiedades value y text como lo mencionamos en arriba creando el objeto list.
            roles.ForEach(item =>
            {
                _selecList.Add(new SelectListItem
                {
                    Value = item.Id,
                    Text = item.Name

                });
                
            });
            return _selecList;
           

           


        }
    }
}
