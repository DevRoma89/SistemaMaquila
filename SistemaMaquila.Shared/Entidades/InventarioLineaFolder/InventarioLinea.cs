using SistemaMaquila.Shared.Entidades.LineaFolder;
using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.InventarioLineaFolder
{
    public class InventarioLinea
    {

        public int Id { get; set; }
        public int LineaId { get; set; }
        public Linea Linea  { get; set; }
        public int TipoMaquinaId { get; set; }
        public TipoMaquina  TipoMaquina { get; set; }
        public int CantidadDisponible { get; set;  }
        public bool Visible { get; set; }   

    }
}
