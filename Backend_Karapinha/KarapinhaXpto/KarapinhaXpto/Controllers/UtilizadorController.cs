using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;
using KarapinhaAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AutoMapper;
using KarapinhaAPI.DTO;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Net.Sockets;
using MailKit.Security;
using System.Net.Sockets;
using KarapinhaAPI.DAL;
using MailKit.Net.Smtp;
using MimeKit;
namespace KarapinhaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizadorController : ControllerBase
    {
        private readonly IUtilizadorInterface _utilizadorInterface;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public UtilizadorController(IUtilizadorInterface utilizadorInterface, IMapper mapper, DataContext context)
        {
            _utilizadorInterface = utilizadorInterface;
            _mapper = mapper;
            _context = context; 
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Utilizador>))]
        public IActionResult GetUtilizadores()
        {

            var utilizadores = _mapper.Map<List<UtilizadorDTO>>(_utilizadorInterface.GetUtilizadores());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

                return Ok(utilizadores);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Utilizador))]
        [ProducesResponseType(400)]
        public IActionResult GetUtilizador(int id) {
            if (!_utilizadorInterface.UtilizadorExiste(id))
                return NotFound();
            var utilizador = _utilizadorInterface.GetUtilizador(id);
            var utilizadorDTO = _mapper.Map<UtilizadorDTO>(utilizador);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(utilizadorDTO);


        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateUtilizador([FromBody] UtilizadorDTO createutilizador)
        {
            if (createutilizador == null)
                return BadRequest(ModelState);
            var categoria = _utilizadorInterface.GetUtilizadores().Where(c => c.BilheteIdentidade 
            == createutilizador.BilheteIdentidade).FirstOrDefault();
            if (categoria != null)
            {
                ModelState.AddModelError("", "Utilizador já existe");
                return StatusCode(422, ModelState);

            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var utilizadorMap = _mapper.Map<Utilizador>(createutilizador);

            if (!_utilizadorInterface.CreateUtilizador(utilizadorMap))
            {
                ModelState.AddModelError("", "Algo correu mal durante o salvamento");

                return StatusCode(500, ModelState);
            }
            return Ok("Utilizador criado");

        }
        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(UtilizadorDTO))]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return BadRequest(ModelState);

            var utilizador = _utilizadorInterface.GetUtilizadores()
                .FirstOrDefault(u => u.NomeUtilizador == loginDTO.NomeUtilizador && u.Senha == loginDTO.Senha);

            if (utilizador == null)
            {
                ModelState.AddModelError("", "Nome de utilizador ou senha incorretos");
                return Unauthorized(ModelState);
            }

            var utilizadorDTO = _mapper.Map<UtilizadorDTO>(utilizador);

            return Ok(utilizadorDTO);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateServico([FromBody] UtilizadorDTO updateutilizador)
        {
            if (updateutilizador == null)
                return BadRequest(ModelState);
            if (updateutilizador.IDUtilizador != updateutilizador.IDUtilizador)
                return BadRequest(ModelState);
            if (!_utilizadorInterface.UtilizadorExiste(updateutilizador.IDUtilizador))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var utilizadorMap = _mapper.Map<Utilizador>(updateutilizador);
            if (!_utilizadorInterface.UpdateUtilizador(utilizadorMap))
            {
                ModelState.AddModelError("", "Algo correu mal durante a actualização, tente novamente");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpPut("ativar/{utilizadorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AtivarFuncionario(int utilizadorId)
        {
            if (!_utilizadorInterface.UtilizadorExiste(utilizadorId))
                return NotFound();

            var utilizador = _utilizadorInterface.GetUtilizador(utilizadorId);
            if (utilizador == null)
                return NotFound();

            utilizador.Status = StatusUtilizador.Ativo;
            utilizador.Tipo = TipoUtilizador.Registado;

            if (!_utilizadorInterface.UpdateUtilizador(utilizador))
            {
                ModelState.AddModelError("", "Algo correu mal durante a atualização, tente novamente");
                return StatusCode(500, ModelState);
            }

            try
            {
                EnviarEmailAtivacao(utilizador);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Conta ativada, mas falha ao enviar e-mail: {ex.Message}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        private void EnviarEmailAtivacao(Utilizador utilizador)
        {
            var clienteEmail = utilizador.Email;

            if (string.IsNullOrEmpty(clienteEmail))
            {
                throw new InvalidOperationException("E-mail do cliente não encontrado.");
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Seu Nome ou Empresa", "gueibysilva2@gmail.com"));
            message.To.Add(new MailboxAddress("Cliente", clienteEmail));
            message.Subject = "Conta Ativada";
            message.Body = new TextPart("plain")
            {
                Text = $"Sua conta foi ativada com sucesso!\n\nDetalhes:\nNome: {utilizador.NomeCompleto}\nEmail: {utilizador.Email}\nStatus: {utilizador.Status}"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true; // Temporário para testes
                    Console.WriteLine("Conectando ao servidor SMTP...");
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    Console.WriteLine("Autenticando...");
                    client.Authenticate("gueibysilva2@gmail.com", "rnpn oesm addf jxpt");
                    Console.WriteLine("Enviando e-mail...");
                    client.Send(message);
                    Console.WriteLine("E-mail enviado com sucesso!");
                }
                catch (AuthenticationException ex)
                {
                    // Log ou trate o erro de autenticação
                    Console.WriteLine($"Erro de autenticação: {ex.Message}");
                    throw new InvalidOperationException("Erro de autenticação ao enviar e-mail.", ex);
                }
                catch (SocketException ex)
                {
                    // Log ou trate erros de rede
                    Console.WriteLine($"Erro de rede: {ex.Message}");
                    throw new InvalidOperationException("Erro de rede ao enviar e-mail.", ex);
                }
                catch (Exception ex)
                {
                    // Log ou trate outros erros
                    Console.WriteLine($"Erro geral: {ex.Message}");
                    throw new InvalidOperationException("Erro ao enviar e-mail.", ex);
                }
                finally
                {
                    client.Disconnect(true);
                    Console.WriteLine("Desconectado do servidor SMTP.");
                }
            }
        }
    }
}


