using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.PrendaFolder
{
    public class Prenda
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int  TiempoCambioLineaMinutos    { get; set; }
        public bool Visible { get; set; }   

    }
}
