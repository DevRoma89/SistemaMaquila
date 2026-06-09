using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.PrendaFolder
{
    public class PrendaPostDTO
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int TiempoCambioLineaMinutos { get; set; }

        public static Prenda DtoToEntity(PrendaPostDTO dto)
        {

            return new Prenda
            {
                Nombre = dto.Nombre.ToUpper(),
                Codigo = dto.Codigo.ToUpper(),
                TiempoCambioLineaMinutos = dto.TiempoCambioLineaMinutos
            };

        }

    }
}
