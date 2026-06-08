using SistemaMaquila.Shared.Entidades.LineaFolder;
using SistemaMaquila.Shared.Entidades.PrendaFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaMaquila.Shared.Entidades.ProgramacionDiariaFolder
{
    public class ProgramacionDiaria
    {

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int LineaId { get; set; }
        public Linea Linea { get; set; }
        public int PrendaId { get; set; }
        public Prenda Prenda { get; set; }
        public int SecuenciaDia { get; set; }
        public int CantidadObjetivo { get; set; }   


    }
}
