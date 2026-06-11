using SistemaMaquila.Client.Extensiones;
using SistemaMaquila.Shared.Entidades.LineaFolder;
using SistemaMaquila.Shared.Entidades.OperacionFolder;

namespace SistemaMaquila.Client.Servicios
{
    public class OperacionService
    {
        private readonly IRepositorio repositorio;

        public OperacionService(IRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<HttpResponseWrapper<List<OperacionGetDTO>>> GetOperaciones()
            => await repositorio.Get<List<OperacionGetDTO>>("api/operacion");

        public async Task<HttpResponseWrapper<object>> PostOperaciones(OperacionPostDTO dto)
            => await repositorio.Post("api/operacion", dto);

        public async Task<HttpResponseWrapper<object>> PutOperaciones(int id, OperacionPutDTO dto)
            => await repositorio.Put($"api/operacion/{id}", dto);

        public async Task<HttpResponseWrapper<object>> DeleteOperaciones(int id)
            => await repositorio.Delete($"api/operacion/{id}");

    }
}
