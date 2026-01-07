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
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaInterface _categoriaInterface;
        private readonly IMapper _mapper;
        public CategoriaController(ICategoriaInterface categoriaInterface, IMapper mapper)
        {
            _categoriaInterface = categoriaInterface;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Categoria>))]
        public IActionResult GetCategorias()
        {

            var categorias = _mapper.Map<List<CategoriaDTO>>(_categoriaInterface.GetCategorias());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categorias);
        }
        [HttpGet("IDCategoria")]
        [ProducesResponseType(200, Type = typeof(Categoria))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoria(int id)
        {
            if (!_categoriaInterface.GetCategoriaExiste(id))
                return NotFound();
            var categoria = _categoriaInterface.GetCategoria(id);
            var categoriaDTO = _mapper.Map<UtilizadorDTO>(categoria);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categoriaDTO);
        }

        [HttpGet("Profissionais/IDCategoria")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Categoria>))]
        [ProducesResponseType(400)]
        public IActionResult GetProfissionaisByCategoria(int id)
        {

            var categorias = _mapper.Map<List<CategoriaDTO>>(_categoriaInterface.GetProfissionaisByCategoria(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categorias);
        }
        [HttpGet("Serviços/IDCategoria")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Categoria>))]
        [ProducesResponseType(400)]
        public IActionResult GetsServicoByCategoria(int id)
        {

            var categorias = _mapper.Map<List<CategoriaDTO>>(_categoriaInterface.GetsServicoByCategoria(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categorias);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategoria([FromBody] CategoriaDTO createcategoria) { 
                if (createcategoria == null)
                return BadRequest(ModelState);  
                var categoria = _categoriaInterface.GetCategorias().Where (c => c.Nome.Trim().ToUpper() ==  
                createcategoria.Nome.TrimEnd().ToUpper()).FirstOrDefault();
            if (categoria != null) {
                ModelState.AddModelError("", "Categoria já existe");
                return StatusCode(422, ModelState);
            
                }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriaMap = _mapper.Map<Categoria>(createcategoria);

            if (!_categoriaInterface.CreateCategoria(categoriaMap)) {
                ModelState.AddModelError("", "Algo correu mal durante o salvamento");

                return StatusCode(500, ModelState);
            }
            return Ok("Categoria criada");
        
        }

        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategoria(int id)
        {

            if (!_categoriaInterface.GetCategoriaExiste(id)) {

                return NotFound();
            }
            var categoria = _categoriaInterface.GetCategoria(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_categoriaInterface.DeleteCategoria(categoria)) {
                ModelState.AddModelError("", "Algo correu mal durante");

            }

            return NoContent();
        }

        [HttpPut("CategoriaID")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategoria(int categoriaId, [FromBody] CategoriaDTO updatecategoria) {
            if (updatecategoria == null)
                return BadRequest(ModelState);
            if (categoriaId != updatecategoria.IDCategoria)
                return BadRequest(ModelState);
            if (!_categoriaInterface.GetCategoriaExiste(categoriaId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var categoriaMap = _mapper.Map<Categoria>(updatecategoria);
            if (!_categoriaInterface.UpdateCategoria(categoriaMap)) {
                ModelState.AddModelError("", "Algo correu mal durante a actualização, tente novamente");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }


    }
}
