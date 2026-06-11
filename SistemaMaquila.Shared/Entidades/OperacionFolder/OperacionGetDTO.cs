using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.OperacionFolder
{
    public class OperacionGetDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal SAMEstimado { get; set; }
        public int TipoMaquinaId { get; set; }
        public string TipoMaquina { get; set; }

        public static OperacionGetDTO EntityToDto(Operacion ent)
        {

            return new OperacionGetDTO
            {
                Id = ent.Id,
                Descripcion = ent.Descripcion,
                SAMEstimado = ent.SAMEstimado,
                TipoMaquinaId = ent.TipoMaquinaId,
                TipoMaquina = ent.TipoMaquina.Nombre
            }; 

        }

    }

}
