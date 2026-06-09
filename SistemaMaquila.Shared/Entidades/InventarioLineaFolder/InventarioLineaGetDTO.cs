using SistemaMaquila.Shared.Entidades.LineaFolder;
using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.InventarioLineaFolder
{
    public class InventarioLineaGetDTO
    {
        public int Id { get; set; }
        public int LineaId { get; set; }
        public string Linea { get; set; }
        public int TipoMaquinaId { get; set; }
        public string TipoMaquina { get; set; }
        public int CantidadDisponible { get; set; }

        public static InventarioLineaGetDTO EntityToDto (InventarioLinea ent)
        {
            return new InventarioLineaGetDTO
            {
                Id = ent.Id,
                LineaId = ent.LineaId,
                Linea = ent.Linea.Nombre,
                TipoMaquinaId = ent.TipoMaquinaId,
                TipoMaquina = ent.TipoMaquina.Nombre,
                CantidadDisponible = ent.CantidadDisponible,
            }; 
        }
    }
}
