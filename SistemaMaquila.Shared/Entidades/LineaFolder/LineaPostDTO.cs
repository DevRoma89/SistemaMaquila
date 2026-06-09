using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.LineaFolder
{
    public class LineaPostDTO
    {
        public string Nombre { get; set; }
        public decimal EficienciaHistorica { get; set; }

        public static Linea DtoToEntity(LineaPostDTO dto)    
        {

            return new Linea
            {
                Nombre = dto.Nombre.ToUpper(),
                EficienciaHistorica = dto.EficienciaHistorica,
            };

        }

    }
}
