using AutoMapper;
using KarapinhaAPI.DTO;
using KarapinhaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Interfaces;

namespace KarapinhaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalInterface _profissionalInterface;
        private readonly IMapper _mapper;
        public ProfissionalController(IProfissionalInterface profissionalInterface, IMapper mapper)
        {
            _profissionalInterface = profissionalInterface;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Profissional>))]
        public IActionResult GetProfissionais()
        {

            var profissionais = _mapper.Map<List<ProfissionalDTO>>(_profissionalInterface.GetProfissionais());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profissionais);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Profissional))]
        [ProducesResponseType(400)]
        public IActionResult GetProfissional(int id)
        {
            if (!_profissionalInterface.GetProssionalExiste(id))
                return NotFound();
            var profissional = _profissionalInterface.GetProfissional(id);
            var profissionalDTO = _mapper.Map<ProfissionalDTO>(profissional);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profissionalDTO);
        }

        [HttpGet("Profissionais/IDServico")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Profissional>))]
        [ProducesResponseType(400)]
        public IActionResult GetServicosByProfissional(int id)
        {

            var profissionais = _mapper.Map<List<ProfissionalDTO>>(_profissionalInterface.GetServicosByProfissional(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profissionais);
        }

        [HttpGet("Profissionais/IDMarcacao")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Profissional>))]
        [ProducesResponseType(400)]
        public IActionResult GetsMarcacoesByProfissional(int id)
        {

            var profissionais = _mapper.Map<List<ProfissionalDTO>>(_profissionalInterface.GetsMarcacoesByProfissional(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profissionais);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateProfissional([FromBody] ProfissionalDTO createProfissional)
        {
            if (createProfissional == null)
                return BadRequest(ModelState);
            Console.WriteLine($"Recebido: {JsonConvert.SerializeObject(createProfissional)}");
            var categoria = _profissionalInterface.GetProfissionais().Where(p => p.BilheteIdentidade
            == createProfissional.BilheteIdentidade).FirstOrDefault();
            if (categoria != null)
            {
                ModelState.AddModelError("", "Já existe um profissional com esse BI");
                return StatusCode(422, ModelState);

            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profissionalMap = _mapper.Map<Profissional>(createProfissional);

            if (!_profissionalInterface.CreateProfissional(profissionalMap))
            {
                ModelState.AddModelError("", "Algo correu mal durante o salvamento");

                return StatusCode(500, ModelState);
            }
            return Ok();

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProfissional(int id)
        {

            if (!_profissionalInterface.GetProssionalExiste(id))
            {

                return NotFound();
            }
            var profissional = _profissionalInterface.GetProfissional(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_profissionalInterface.DeleteProfissional(profissional))
            {
                ModelState.AddModelError("", "Algo correu mal");

            }

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProfissional( [FromBody] ProfissionalDTO updateprofissional)
        {
            if (updateprofissional == null)
                return BadRequest("null");
            if (updateprofissional.IDProfissional != updateprofissional.IDProfissional)
                return BadRequest("id diferente");
            if (!_profissionalInterface.GetProssionalExiste(updateprofissional.IDProfissional))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var profissionalMap = _mapper.Map<Profissional>(updateprofissional);
            if (!_profissionalInterface.UpdateProfissional(profissionalMap))
            {
                ModelState.AddModelError("", "Algo correu mal durante a actualização, tente novamente");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(ProfissionalDTO))]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return BadRequest(ModelState);

            var profissional = _profissionalInterface.GetProfissionais()
                .FirstOrDefault(p => p.Email == loginDTO.NomeUtilizador && p.Senha == loginDTO.Senha);

            if (profissional == null)
            {
                ModelState.AddModelError("", "Email ou senha incorretos");
                return Unauthorized(ModelState);
            }

            var profissionalDTO = _mapper.Map<ProfissionalDTO>(profissional);

            return Ok(profissionalDTO);
        }

    }
}
