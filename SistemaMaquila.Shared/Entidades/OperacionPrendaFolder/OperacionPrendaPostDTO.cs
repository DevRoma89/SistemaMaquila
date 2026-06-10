using SistemaMaquila.Shared.Entidades.OperacionFolder;
using SistemaMaquila.Shared.Entidades.PrendaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.OperacionPrendaFolder
{
    public class OperacionPrendaPostDTO
    {
        public int PrendaId { get; set; }
        public int OrdenSecuencia { get; set; }
        public int OperacionId { get; set; }
        public decimal SAMReal { get; set; }

        public static OperacionPrenda DtoToEntity(OperacionPrendaPostDTO dto)
        {

            return new OperacionPrenda
            {
                PrendaId = dto.PrendaId,
                OrdenSecuencia = dto.OrdenSecuencia,
                OperacionId = dto.OperacionId,
                SAMReal = dto.SAMReal
            }; 

        }

    }
}
