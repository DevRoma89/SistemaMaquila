using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.LineaFolder
{
    public class LineaPutDTO
    { 
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal EficienciaHistorica { get; set; }

        public static void DtoToEntity(LineaPutDTO dto, Linea entity)
        {

            entity.Nombre = dto.Nombre.ToUpper();
            entity.EficienciaHistorica = dto.EficienciaHistorica; 

        }

    }
}
