using SistemaMaquila.Shared.Entidades.LineaFolder;
using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.InventarioLineaFolder
{
    public class InventarioLineaPutDTO
    {

        public int Id { get; set; }
        public int LineaId { get; set; }
        public int TipoMaquinaId { get; set; }
        public int CantidadDisponible { get; set; }
    }
}
