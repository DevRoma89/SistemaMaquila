using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.OperacionFolder
{
    public class OperacionPutDTO
    {

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal SAMEstimado { get; set; }
        public int TipoMaquinaId { get; set; }

        public static void DtoToEntity(OperacionPutDTO dto, Operacion ent)
        {
            
            ent.Descripcion = dto.Descripcion.ToUpper();
            ent.SAMEstimado = dto.SAMEstimado;
            ent.TipoMaquinaId = dto.TipoMaquinaId; 

        }


    }
}
