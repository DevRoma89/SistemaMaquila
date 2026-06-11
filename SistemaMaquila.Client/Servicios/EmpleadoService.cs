using SistemaMaquila.Client.Extensiones;
using SistemaMaquila.Shared.Entidades.EmpleadoFolder;
using SistemaMaquila.Shared.Entidades.LineaFolder;

namespace SistemaMaquila.Client.Servicios
{
    public class EmpleadoService
    {
        private readonly IRepositorio repositorio;

        public EmpleadoService(IRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<HttpResponseWrapper<List<EmpleadoGetDTO>>> GetEmpleados()
            => await repositorio.Get<List<EmpleadoGetDTO>>("api/empleado");

        public async Task<HttpResponseWrapper<object>> PostEmpleado(EmpleadoPostDTO dto)
            => await repositorio.Post("api/empleado", dto);

        public async Task<HttpResponseWrapper<object>> PutEmpleado(int id, EmpleadoPutDTO dto)
            => await repositorio.Put($"api/empleado/{id}", dto);

        public async Task<HttpResponseWrapper<object>> DeleteEmpleado(int id)
            => await repositorio.Delete($"api/empleado/{id}");

        public async Task<HttpResponseWrapper<List<LineaGetDTO>>> GetLineas()
            => await repositorio.Get<List<LineaGetDTO>>("api/linea");
    }
}
