using SistemaMaquila.Shared.Entidades.OperacionFolder;
using SistemaMaquila.Shared.Entidades.PrendaFolder;
using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.OperacionPrendaFolder
{
    public class OperacionPrenda
    {
        public int Id { get; set; }
        public int PrendaId { get; set; }
        public Prenda Prenda{ get; set; }
        public int OrdenSecuencia { get; set; }
        public int OperacionId { get; set; }
        public Operacion Operacion { get; set; }
        public decimal SAMReal { get; set; }
        public bool  Visible { get; set; }
         
    }
}
