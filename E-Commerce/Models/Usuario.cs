using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Usuario
    {
            public string nombre_completo { get; set; }
            public string contraseña { get; set; }
            public string correo { get; set; }
            public string dni { get; set; }
            public string ConfirmarClave { get; set; }
            

            //public string CerraSesion { get; set; }
    }
}