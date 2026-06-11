using SistemaMaquila.Client.Extensiones;
using SistemaMaquila.Shared.Entidades.EmpleadoFolder;
using SistemaMaquila.Shared.Entidades.LineaFolder;

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

        

    }
}
