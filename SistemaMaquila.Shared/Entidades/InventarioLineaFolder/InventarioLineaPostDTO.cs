using SistemaMaquila.Shared.Entidades.LineaFolder;
using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.InventarioLineaFolder
{
    public class InventarioLineaPostDTO
    { 
        public int LineaId { get; set; } 
        public int TipoMaquinaId { get; set; } 
        public int CantidadDisponible { get; set; }

        public static InventarioLinea DtoToEntity(InventarioLineaPostDTO dto)
        {

            return new InventarioLinea
            { 
                LineaId = dto.LineaId,
                TipoMaquinaId = dto.TipoMaquinaId,
                CantidadDisponible = dto.CantidadDisponible
            }; 

        }

    }
}
