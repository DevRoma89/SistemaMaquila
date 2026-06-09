using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.OperacionFolder
{
    public class OperacionPostDTO
    {
        public string Descripcion { get; set; }
        public decimal SAMEstimado { get; set; }
        public int TipoMaquinaId { get; set; }

        public static Operacion DtoToEntity(OperacionPostDTO dto)
        {

            return new Operacion
            { 
                Descripcion = dto.Descripcion.ToUpper(),
                SAMEstimado = dto.SAMEstimado, 
                TipoMaquinaId = dto.TipoMaquinaId
            }; 

        }
    
    }


}
