using Microsoft.AspNetCore.Mvc;
using PersonasAPI.Modelo;
using PersonasAPI.Modelo.Dtos;
using PersonasAPI.Repositorios;

namespace PersonasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonasRepositorio _repositorioPersona;
        protected ResponseDto _response;

        public PersonasController(IPersonasRepositorio repositorioPersona)
        {
            _repositorioPersona = repositorioPersona;
            _response = new ResponseDto();
        }

        // GET: api/Personas
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetPersonas()
        {
            try
            {
                var personas = await _repositorioPersona.GetPersonas();
                _response.Result = personas;
                _response.MostrarMensaje = "Lista de Personas";
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { e.ToString()};
                return BadRequest(_response);
            }            
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetPersonas(int id)
        {
            try
            {
                var persona = await _repositorioPersona.GetPersona(id);
                if(persona == null)
                {
                    _response.IsSuccess = false;
                    _response.MostrarMensaje = "Persona no existe";
                    return NotFound(_response);
                }
                _response.Result = persona;
                _response.MostrarMensaje = "Informacion de la persona buscada";
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { e.ToString() };
                return BadRequest(_response);
            }
        }

        // PUT: api/Personas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> PutPersonas(int id, PersonasDto persona)
        {
            try
            {
                PersonasDto personaDto = await _repositorioPersona.CrearOActualizar(persona, id);
                _response.Result = personaDto;
                _response.MostrarMensaje = "Persona Actualizada";
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { e.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Personas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Personas>> PostPersonas(PersonasDto persona)
        {
            try
            {
                PersonasDto personaDto = await _repositorioPersona.CrearOActualizar(persona);
                _response.Result = personaDto;
                _response.MostrarMensaje = "Persona Agregada";
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { e.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeletePersonas(int id)
        {
            bool estaEliminado = await _repositorioPersona.EliminarPersona(id);
            if (estaEliminado)
            {
                _response.Result = estaEliminado;
                _response.MostrarMensaje = $"Persona eliminada con el id: {id}";
                return Ok(_response);
            }
            else
            {
                _response.IsSuccess = false;
                _response.MostrarMensaje = "Error al intenter eliminar la persona";
                return BadRequest(_response);
            }
        }        
    }
}
