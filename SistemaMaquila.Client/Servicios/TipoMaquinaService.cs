using SistemaMaquila.Client.Extensiones;
using SistemaMaquila.Shared.Entidades.OperacionFolder;
using SistemaMaquila.Shared.Entidades.TipoMaquinaFolder;

namespace SistemaMaquila.Client.Servicios
{
    public class TipoMaquinaService
    {

        private readonly IRepositorio repositorio;

        public TipoMaquinaService(IRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<HttpResponseWrapper<List<TipoMaquinaGetDTO>>> Get()
            => await repositorio.Get<List<TipoMaquinaGetDTO>>("api/tipoMaquina");

        public async Task<HttpResponseWrapper<object>> Post(TipoMaquinaPostDTO dto)
            => await repositorio.Post("api/TipoMaquina", dto);

        public async Task<HttpResponseWrapper<object>> Put(int id, TipoMaquinaPutDTO dto)
            => await repositorio.Put($"api/TipoMaquina/{id}", dto);

        public async Task<HttpResponseWrapper<object>> Delete(int id)
            => await repositorio.Delete($"api/TipoMaquina/{id}");

    }
}
