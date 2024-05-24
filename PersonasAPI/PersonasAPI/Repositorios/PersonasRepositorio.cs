using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonasAPI.Data;
using PersonasAPI.Modelo;
using PersonasAPI.Modelo.Dtos;


namespace PersonasAPI.Repositorios
{
    public class PersonasRepositorio : IPersonasRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        public PersonasRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PersonasDto> CrearOActualizar(PersonasDto personaDto, int id = 0)
        {
            Personas persona = _mapper.Map<PersonasDto, Personas>(personaDto);
            if(id > 0)
            {
                persona.Id = id;
                _context.Personas.Update(persona);
            }
            else
            {
                await _context.Personas.AddAsync(persona);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<Personas, PersonasDto>(persona);
        }

        public async Task<bool> EliminarPersona(int id)
        {
            try
            {
                Personas persona = await _context.Personas.FindAsync(id);
                if(persona == null)
                {
                    return false;
                }
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }            
        }

        public async Task<PersonasDto> GetPersona(int id)
        {
            Personas persona = await _context.Personas.FindAsync(id);
            return _mapper.Map<PersonasDto>(persona);
        }

        public async Task<List<PersonasDto>> GetPersonas()
        {
            List<Personas> personas = await _context.Personas.ToListAsync();
            return _mapper.Map<List<PersonasDto>>(personas);

        }
    }
}
