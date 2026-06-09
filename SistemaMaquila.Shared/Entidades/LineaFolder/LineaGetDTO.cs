using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.LineaFolder
{
    public class LineaGetDTO
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal EficienciaHistorica { get; set; }

        public static LineaGetDTO EntityToDto( Linea ent)
        {

            return new LineaGetDTO
            {
                Id = ent.Id,
                Nombre = ent.Nombre,
                EficienciaHistorica = ent.EficienciaHistorica
            }; 
            
        }

    }
}
