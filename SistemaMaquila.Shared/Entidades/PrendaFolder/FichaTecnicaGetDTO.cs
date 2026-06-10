using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.PrendaFolder
{
    public class FichaTecnicaGetDTO
    {

        public PrendaGetDTO Prenda { get; set; }
        public decimal SamTotal { get; set; }


        public static FichaTecnicaGetDTO EntityToDto(Prenda ent)
        {

            return new FichaTecnicaGetDTO
            {
                Prenda = PrendaGetDTO.EntityToDto(ent),
                SamTotal = ent.Operaciones.Sum(x => x.SAMReal)
            }; 

        }

    }
}
