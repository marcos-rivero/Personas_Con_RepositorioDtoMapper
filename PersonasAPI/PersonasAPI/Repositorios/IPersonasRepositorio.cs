using PersonasAPI.Modelo.Dtos;

namespace PersonasAPI.Repositorios
{
    public interface IPersonasRepositorio
    {
        Task<List<PersonasDto>> GetPersonas();
        Task<PersonasDto> GetPersona(int id);
        Task<PersonasDto> CrearOActualizar(PersonasDto persona, int id = 0);
        Task<bool> EliminarPersona(int id);
    }
}
