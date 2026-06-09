using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.TipoMaquinaFolder
{
    public class TipoMaquinaPostDTO
    {

        public string Nombre { get; set; }

        public static TipoMaquina DtoToEntity(TipoMaquinaPostDTO dto)
        {
            return new TipoMaquina { Nombre = dto.Nombre.ToUpper() };  
        }

    }
}
