using SistemaMaquila.Shared.Entidades.EmpleadoFolder;
using SistemaMaquila.Shared.Entidades.OperacionFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.HabilidadEmpleadoFolder
{
    public class HabilidadEmpleado
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado Empleado{ get; set; }
        public int OperacionId { get; set; }
        public Operacion Operacion  { get; set; }
        public decimal EficienciaSocio { get; set; }
        public bool Visible { get; set; }   


    }
}
