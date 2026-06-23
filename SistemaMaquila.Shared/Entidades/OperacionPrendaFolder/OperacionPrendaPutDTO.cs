using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.OperacionPrendaFolder
{
    public class OperacionPrendaPutDTO
    {
        public int Id { get; set; }
        public int PrendaId { get; set; }
        public int OperacionId { get; set; }
        public int OrdenSecuencia { get; set; }
        public decimal SAMReal { get; set; }
    }
}
