using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.LineaFolder
{
    public class Linea
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal EficienciaHistorica { get; set; }
        public bool Visible { get; set; }   = true; 
    }
}
