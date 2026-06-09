using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.OperacionFolder
{
    public class Operacion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal SAMEstimado { get; set; }
        public int TipoMaquinaId { get; set; }
        public TipoMaquina  TipoMaquina { get; set; }
        public bool Visible { get; set; } = true; 
    }
}
