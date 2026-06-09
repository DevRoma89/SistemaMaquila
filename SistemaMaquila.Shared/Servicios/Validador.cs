using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Servicios
{
    public class Validador
    {

        public static bool CampoTexto(string campo) => (string.IsNullOrEmpty(campo) || string.IsNullOrWhiteSpace(campo));  
        public static bool CampoNumerico(decimal campo) => campo <= 0;    
        public static bool CampoNumerico(int campo) => campo <= 0;   
           
    }
}
