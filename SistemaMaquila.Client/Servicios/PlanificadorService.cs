using SistemaMaquila.Client.Extensiones;
using SistemaMaquila.Shared.Entidades.ProgramacionDiariaFolder;

namespace SistemaMaquila.Client.Servicios
{
    public class PlanificadorService
    {
        private readonly IRepositorio repositorio;

        public PlanificadorService(IRepositorio repositorio) => this.repositorio = repositorio;

        public async Task<HttpResponseWrapper<PlanificacionDiariaResultDTO>> GetSamPrenda(
            int prendaId, int lineaId)
            => await repositorio.Get<PlanificacionDiariaResultDTO>(
                $"api/Planificador/sam-prenda?prendaId={prendaId}&lineaId={lineaId}");
    }
}
