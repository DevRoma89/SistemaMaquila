using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.EmpleadoFolder
{
    public class EmpleadoFormModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public decimal CostoMinutoBase { get; set; }
        public int LineaId { get; set; }
    }
}
