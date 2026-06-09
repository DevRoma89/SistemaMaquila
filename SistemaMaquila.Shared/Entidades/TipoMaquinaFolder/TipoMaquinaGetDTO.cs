using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.TipoMaquinaFolder
{
    public class TipoMaquinaGetDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public static TipoMaquinaGetDTO EntityToDto( TipoMaquina ent)
        {

            return new TipoMaquinaGetDTO
            {
                Id = ent.Id,    
                Nombre  = ent.Nombre,
            };

        }

    }
}
