using SistemaMaquila.Client.Extensiones;
using SistemaMaquila.Shared.Entidades.OperacionFolder;
using SistemaMaquila.Shared.Entidades.OperacionPrendaFolder;
using SistemaMaquila.Shared.Entidades.PrendaFolder;

namespace SistemaMaquila.Client.Servicios
{
    public class PrendaService
    {
        private readonly IRepositorio repositorio;
        private const string BasePrenda = "api/Prenda";
        private const string BaseOpPrenda = "api/OperacionPrenda";
        private const string BaseOperacion = "api/Operacion";

        public PrendaService(IRepositorio repositorio) => this.repositorio= repositorio;

        // Prendas
        public async Task<HttpResponseWrapper<List<PrendaGetDTO>>> GetPrendas()
            => await repositorio.Get<List<PrendaGetDTO>>(BasePrenda);

        public async Task<HttpResponseWrapper<object>> PostPrenda(PrendaPostDTO dto)
            => await repositorio.Post(BasePrenda, dto);

        public async Task<HttpResponseWrapper<object>> PutPrenda(int id, PrendaPutDTO dto)
            => await repositorio.Put(BasePrenda + $"/{id}", dto);

        public async Task<HttpResponseWrapper<object>> DeletePrenda(int id)
            => await repositorio.Delete($"{BasePrenda}/{id}");

        // Operaciones de prenda
        public async Task<HttpResponseWrapper<List<OperacionPrendaGetDTO>>> GetOperacionesPorPrenda(int prendaId)
            => await repositorio.Get<List<OperacionPrendaGetDTO>>($"{BaseOpPrenda}/por-prenda/{prendaId}");

        public async Task<HttpResponseWrapper<object>> PostOperacionPrenda(OperacionPrendaPostDTO dto)
            => await repositorio.Post(BaseOpPrenda, dto);

        public async Task<HttpResponseWrapper<object>> PutOperacionPrenda(int id, OperacionPrendaPutDTO dto)
            => await repositorio.Put(BaseOpPrenda + $"/{id}", dto);

        public async Task<HttpResponseWrapper<object>> DeleteOperacionPrenda(int id)
            => await repositorio.Delete($"{BaseOpPrenda}/{id}");

        // Operaciones base (para el select)
        public async Task<HttpResponseWrapper<List<OperacionGetDTO>>> GetOperaciones()
            => await repositorio.Get<List<OperacionGetDTO>>(BaseOperacion);
    }
}
