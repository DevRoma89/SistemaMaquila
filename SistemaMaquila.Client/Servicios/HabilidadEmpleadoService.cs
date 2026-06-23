using SistemaMaquila.Client.Extensiones;
using SistemaMaquila.Shared.Entidades.HabilidadEmpleadoFolder;
using SistemaMaquila.Shared.Entidades.OperacionFolder;

namespace SistemaMaquila.Client.Servicios
{
    public class HabilidadEmpleadoService
    {
        private readonly IRepositorio repositorio;
        private const string Base = "api/HabilidadEmpleado";

        public HabilidadEmpleadoService(IRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<HttpResponseWrapper<List<HabilidadEmpleadoGetDTO>>> GetPorEmpleado(int empleadoId)
            => await repositorio.Get<List<HabilidadEmpleadoGetDTO>>($"{Base}/por-empleado/{empleadoId}");

        public async Task<HttpResponseWrapper<List<OperacionGetDTO>>> GetOperaciones()
            => await repositorio.Get<List<OperacionGetDTO>>("api/Operacion");

        public async Task<HttpResponseWrapper<object>> Post(HabilidadEmpleadoPostDTO dto)
            => await repositorio.Post(Base, dto);

        public async Task<HttpResponseWrapper<object>> Put(int id, HabilidadEmpleadoPutDTO dto)
            => await repositorio.Put(Base + $"/{id}", dto);

        public async Task<HttpResponseWrapper<object>> Delete(int id)
            => await repositorio.Delete($"{Base}/{id}");
    }
}
