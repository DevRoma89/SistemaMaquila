using SistemaMaquila.Shared.Entidades.OperacionFolder;
using SistemaMaquila.Shared.Entidades.PrendaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.OperacionPrendaFolder
{
    public class OperacionPrendaGetDTO
    {
        public int Id { get; set; }
        public int PrendaId { get; set; } 
        public int OrdenSecuencia { get; set; }
        public int OperacionId { get; set; }
        public string Operacion { get; set; }
        public string TipoMaquina{ get; set; }
        public decimal SAMReal { get; set; }
    
        public static OperacionPrendaGetDTO EntityToDto (OperacionPrenda ent)
        {

            return new OperacionPrendaGetDTO
            {
                Id = ent.Id,
                PrendaId = ent.PrendaId, 
                OrdenSecuencia = ent.OrdenSecuencia,
                OperacionId = ent.OperacionId,
                Operacion = ent.Operacion.Descripcion,
                TipoMaquina = ent.Operacion.TipoMaquina.Nombre,
                SAMReal = ent.SAMReal
            }; 

        }

    }
}
