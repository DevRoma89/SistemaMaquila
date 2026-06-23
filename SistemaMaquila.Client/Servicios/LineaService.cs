using SistemaMaquila.Client.Extensiones;
using SistemaMaquila.Shared.Entidades.EmpleadoFolder;
using SistemaMaquila.Shared.Entidades.InventarioLineaFolder;
using SistemaMaquila.Shared.Entidades.LineaFolder;
using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;

namespace SistemaMaquila.Client.Servicios
{
    public class LineaService
    {

        private readonly IRepositorio repositorio;

        public LineaService(IRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<HttpResponseWrapper<List<LineaGetDTO>>> GetLineas()
            => await repositorio.Get<List<LineaGetDTO>>("api/linea");

        public async Task<HttpResponseWrapper<object>> PostLineas(LineaPostDTO dto)
            => await repositorio.Post("api/linea", dto);

        public async Task<HttpResponseWrapper<object>> PutLineas(int id, LineaPutDTO dto)
            => await repositorio.Put($"api/linea/{id}", dto);

        public async Task<HttpResponseWrapper<object>> DeleteLineas(int id)
            => await repositorio.Delete($"api/linea/{id}");

        public async Task<HttpResponseWrapper<List<InventarioLineaGetDTO>>> GetInventarioPorLinea(int id)
            => await repositorio.Get<List<InventarioLineaGetDTO>>($"api/InventarioLinea/{id}");
        
        // POST: api/InventarioLinea
        public async Task<HttpResponseWrapper<object>> PostInventarioLinea(InventarioLineaPostDTO dto)
            => await repositorio.Post("api/InventarioLinea", dto);
        // PUT: api/InventarioLinea
        public async Task<HttpResponseWrapper<object>> PutInventarioLinea(int lineaId ,InventarioLineaPostDTO dto)
            => await repositorio.Put($"api/InventarioLinea/{lineaId}", dto);

        // DELETE: api/InventarioLinea/{id}
        public async Task<HttpResponseWrapper<object>> DeleteInventarioLinea(int id)
            => await repositorio.Delete($"api/InventarioLinea/{id}");

        // GET: api/TipoMaquina (Para rellenar el selector de disponibles)
        public async Task<HttpResponseWrapper<List<TipoMaquinaGetDTO>>> GetTipoMaquinas()
            => await repositorio.Get<List<TipoMaquinaGetDTO>>("api/TipoMaquina");



    }
}
