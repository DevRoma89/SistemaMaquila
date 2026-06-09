using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.PrendaFolder
{
    public class PrendaGetDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int TiempoCambioLineaMinutos { get; set; }

        public static PrendaGetDTO EntityToDto(Prenda ent)
        {

            return new PrendaGetDTO
            {
                Id = ent.Id,
                Nombre = ent.Nombre,
                Codigo = ent.Codigo,
                TiempoCambioLineaMinutos = ent.TiempoCambioLineaMinutos
            }; 

        }

    }
}
