using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.HabilidadEmpleadoFolder
{
    public class HabilidadEmpleadoPutDTO
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public int OperacionId { get; set; }
        public decimal EficienciaSocio { get; set; }
    }
}
