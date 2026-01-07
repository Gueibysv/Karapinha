using AutoMapper;
using KarapinhaAPI.DTO;
using KarapinhaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;
using System.Linq;

namespace KarapinhaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUtilizadorInterface _utilizadorInterface;
        private readonly IProfissionalInterface _profissionalInterface;
        private readonly IMapper _mapper;

        public AuthController(IUtilizadorInterface utilizadorInterface, IProfissionalInterface profissionalInterface, IMapper mapper)
        {
            _utilizadorInterface = utilizadorInterface;
            _profissionalInterface = profissionalInterface;
            _mapper = mapper;
        }

        [HttpPost("login-universal")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public IActionResult UniversalLogin([FromBody] UniversalLoginDTO loginDTO)
        {
            if (loginDTO == null)
                return BadRequest(ModelState);

            // Verifica se é um Utilizador
            var utilizador = _utilizadorInterface.GetUtilizadores()
                .FirstOrDefault(u => u.NomeUtilizador == loginDTO.NomeUtilizador && u.Senha == loginDTO.Senha);

            if (utilizador != null)
            {
                var utilizadorDTO = _mapper.Map<UtilizadorDTO>(utilizador);
                return Ok(new { Tipo = "Utilizador", Dados = utilizadorDTO });
            }

            // Verifica se é um Profissional
            var profissional = _profissionalInterface.GetProfissionais()
                .FirstOrDefault(p => p.Email == loginDTO.NomeUtilizador && p.Senha == loginDTO.Senha);

            if (profissional != null)
            {
                var profissionalDTO = _mapper.Map<ProfissionalDTO>(profissional);
                return Ok(new { Tipo = "Profissional", Dados = profissionalDTO });
            }

            ModelState.AddModelError("", "Nome de utilizador ou senha incorretos");
            return Unauthorized(ModelState);
        }
    }
}
