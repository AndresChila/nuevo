﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;


namespace Logica
{
    public class Loguearse
    {
        public Loguearse()
        {

        }
        public UUsuario loguear(string cedula, string clave) {
            Validaciones val = new Validaciones();
            UUsuario user = new UUsuario();
            if (val.validarNumeros(cedula) == true)
            {
                MAC a = new MAC();
                user.Usuario = cedula;
                user.Clave = clave;
                user.Ip = "192.168.1.1";
                //user.Ip = HttpContext.Current.Request.UserHostAddress;
                user.Mac = a.traerMac();
                return user;
            }
            return user;
        }
    }
}
