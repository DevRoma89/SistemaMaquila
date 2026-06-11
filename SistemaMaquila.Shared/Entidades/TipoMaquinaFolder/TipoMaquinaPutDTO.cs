using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.TipoMaquinaFolder
{
    public class TipoMaquinaPutDTO
    {

        public int Id { get; set; }
        public string Nombre { get; set; }

        public static void DtoToEntity(TipoMaquinaPutDTO dto, TipoMaquina ent) => ent.Nombre = dto.Nombre.ToUpper();
            

    }
}
