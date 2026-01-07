using AutoMapper;
using KarapinhaAPI.DTO;
using KarapinhaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;

namespace KarapinhaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : ControllerBase
    {

        private readonly IServicoInterface _servicoInterface;
        private readonly IMapper _mapper;
        public ServicoController(IServicoInterface servicoInterface, IMapper mapper)
        {
            _servicoInterface = servicoInterface;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Servico>))]
        public IActionResult GetServicos()
        {
            var servicos = _mapper.Map<List<ServicoDTO>>(_servicoInterface.GetServicos());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(servicos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Servico))]
        [ProducesResponseType(400)]
        public IActionResult GetServico(int id)
        {
            if (!_servicoInterface.GetServicoExiste(id))
                return NotFound();
            var servico = _servicoInterface.GetServico(id);
            var servicoDTO = _mapper.Map<ServicoDTO>(servico);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(servicoDTO);
        }
        [HttpGet("Profissionais/IDServico")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Servico>))]
        [ProducesResponseType(400)]
        public IActionResult GetServicoByProfissionais(int id)
        {

            var servicos = _mapper.Map<List<ServicoDTO>>(_servicoInterface.GetServicoByProfissionais(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(servicos);
        }

        [HttpGet("Marcacoes/IDServico")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Servico>))]
        [ProducesResponseType(400)]
        public IActionResult GetsServicoByMarcacoes(int id)
        {

            var servicos = _mapper.Map<List<ServicoDTO>>(_servicoInterface.GetsServicoByMarcacoes(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(servicos);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateServico([FromBody] ServicoDTO createservico)
        {
            if (createservico == null)
                return BadRequest(ModelState);
            var categoria = _servicoInterface.GetServicos().Where(c => c.Nome.Trim().ToUpper() ==
            createservico.Nome.TrimEnd().ToUpper()).FirstOrDefault();
            if (categoria != null)
            {
                ModelState.AddModelError("", "Servico já existe");
                return StatusCode(422, ModelState);

            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var servicoMap = _mapper.Map<Servico>(createservico);

            if (!_servicoInterface.CreateServico(servicoMap))
            {
                ModelState.AddModelError("", "Algo correu mal durante o salvamento");

                return StatusCode(500, ModelState);
            }
            return Ok("Servico criado");

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteServico(int id)
        {

            if (!_servicoInterface.GetServicoExiste(id))
            {

                return NotFound();
            }
            var servico = _servicoInterface.GetServico(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_servicoInterface.DeleteServico(servico))
            {
                ModelState.AddModelError("", "Algo correu mal durante");

            }

            return NoContent();
        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateServico( [FromBody] ServicoDTO updateservico)
        {
            if (updateservico == null)
                return BadRequest(ModelState);
            if (updateservico.IDServico != updateservico.IDServico)
                return BadRequest(ModelState);
            if (!_servicoInterface.GetServicoExiste(updateservico.IDServico))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var servicoMap = _mapper.Map<Servico>(updateservico);
            if (!_servicoInterface.UpdateServico(servicoMap))
            {
                ModelState.AddModelError("", "Algo correu mal durante a actualização, tente novamente");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
