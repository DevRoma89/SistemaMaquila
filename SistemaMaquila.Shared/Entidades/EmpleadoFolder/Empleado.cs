using SistemaMaquila.Shared.Entidades.LineaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.EmpleadoFolder
{
    public class Empleado
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal CostoMinutoBase { get; set; }
        public int LineaId { get; set; }
        public Linea Linea { get; set; }
        public bool Visible { get; set; } = true; 


    }
}
