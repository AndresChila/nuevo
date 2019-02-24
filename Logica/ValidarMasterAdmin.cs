using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ValidarMasterAdmin
    {
        public string validarSession(string nombre, string clave, string rol, string sede)
        {
            if (nombre == null || clave == null || rol == null || sede == null)
            {
                return ("../Login-Rec/NuevoLogin.aspx");
            }

            return ("../Tienda/CRUDVendedor.aspx");
        }
    }
}
