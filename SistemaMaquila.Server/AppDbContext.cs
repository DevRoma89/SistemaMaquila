using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SistemaMaquila.Shared.Entidades.EmpleadoFolder;
using SistemaMaquila.Shared.Entidades.HabilidadEmpleadoFolder;
using SistemaMaquila.Shared.Entidades.InventarioLineaFolder;
using SistemaMaquila.Shared.Entidades.LineaFolder;
using SistemaMaquila.Shared.Entidades.OperacionFolder;
using SistemaMaquila.Shared.Entidades.OperacionPrendaFolder;
using SistemaMaquila.Shared.Entidades.PrendaFolder;
using SistemaMaquila.Shared.Entidades.ProgramacionDiariaFolder;
using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;
using System.Security.Principal;

namespace SistemaMaquila.Server
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<Linea> Lineas { get; set; }
        public DbSet<Empleado> Empleados{ get; set; }
        public DbSet<TipoMaquina> TipoMaquinas{ get; set; }
        public DbSet<InventarioLinea> InventarioLineas { get; set; }
        public DbSet<Prenda> Prendas { get; set; }
        public DbSet<Operacion> Operaciones     { get; set; }
        public DbSet<OperacionPrenda> OperacionPrendas { get; set; }
        public DbSet<HabilidadEmpleado> HabilidadesEmpleados { get; set; }
        public DbSet<ProgramacionDiaria> ProgracionDiaria { get; set; }
    }
}
