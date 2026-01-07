using AutoMapper;
using KarapinhaAPI.DTO;
using KarapinhaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;
using Shared.Repository;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using KarapinhaAPI.DAL;
using MailKit.Security;
using System.Net.Sockets;

namespace KarapinhaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcacaoController : ControllerBase
    {
        private readonly IMarcacaoInterface _marcacaoInterface;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public MarcacaoController(IMarcacaoInterface marcacaoInterface, IMapper mapper, DataContext context)
        {
            _marcacaoInterface = marcacaoInterface;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MarcacaoDTO>))]
        public IActionResult GetMarcacoes()
        {
            var marcacoes = _marcacaoInterface.GetMarcacoes();
            var marcacoesDTO = _mapper.Map<List<MarcacaoDTO>>(marcacoes);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(marcacoesDTO);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(MarcacaoDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetMarcacao(int id)
        {
            if (!_marcacaoInterface.GetMarcacaoExiste(id))
                return NotFound();

            var marcacao = _marcacaoInterface.GetMarcacao(id);
            var marcacaoDTO = _mapper.Map<MarcacaoDTO>(marcacao);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(marcacaoDTO);
        }

        [HttpGet("Servicos/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MarcacaoDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetsMarcacoesByServicos(int id)
        {
            var marcacoes = _marcacaoInterface.GetsMarcacoesByServicos(id);
            var marcacoesDTO = _mapper.Map<List<MarcacaoDTO>>(marcacoes);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(marcacoesDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMarcacao(int id)
        {
            if (!_marcacaoInterface.GetMarcacaoExiste(id))
                return NotFound();

            var marcacao = _marcacaoInterface.GetMarcacao(id);
            if (!_marcacaoInterface.DeleteMarcacao(marcacao))
            {
                ModelState.AddModelError("", "Erro ao deletar marcação.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(MarcacaoDTO))]
        [ProducesResponseType(400)]
        public IActionResult CreateMarcacao([FromBody] MarcacaoDTO createMarcacao)
        {
            if (createMarcacao == null)
                return BadRequest("Modelo de marcação não pode ser nulo");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var marcacaoMap = _mapper.Map<Marcacao>(createMarcacao);
            marcacaoMap.Hora = TimeSpan.Parse(createMarcacao.Hora); // Converter string para TimeSpan

            if (!_marcacaoInterface.CreateMarcacao(marcacaoMap))
            {
                ModelState.AddModelError("", "Algo correu mal durante o salvamento.");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Marcacao criada com sucesso" }); ;
        }

        [HttpPut("{marcacaoId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMarcacao(int marcacaoId, [FromBody] MarcacaoDTO updateMarcacao)
        {
            if (updateMarcacao == null)
                return BadRequest(ModelState);

            if (marcacaoId != updateMarcacao.IDMarcacao)
                return BadRequest(ModelState);

            if (!_marcacaoInterface.GetMarcacaoExiste(marcacaoId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var marcacaoMap = _mapper.Map<Marcacao>(updateMarcacao);
            if (!_marcacaoInterface.UpdateMarcacao(marcacaoMap))
            {
                ModelState.AddModelError("", "Algo correu mal durante a atualização.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("Profissional/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MarcacaoDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetMarcacoesByProfissional(int id)
        {
            var marcacoes = _marcacaoInterface.GetMarcacoesByProfissional(id);
            var marcacoesDTO = _mapper.Map<List<MarcacaoDTO>>(marcacoes);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(marcacoesDTO);
        }
        [HttpPost("confirmar/{id}")]
        [ProducesResponseType(200, Type = typeof(MarcacaoDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ConfirmarMarcacao(int id)
        {
            if (!_marcacaoInterface.GetMarcacaoExiste(id))
                return NotFound();

            var marcacao = _marcacaoInterface.GetMarcacao(id);
            if (marcacao == null)
                return NotFound();

            // Atualiza o status da marcação para "Confirmada"
            marcacao.Status = "Confirmada";
            var updated = _marcacaoInterface.UpdateMarcacao(marcacao);

            if (!updated)
            {
                ModelState.AddModelError("", "Erro ao atualizar marcação.");
                return StatusCode(500, ModelState);
            }

            // Enviar e-mail de confirmação
            EnviarEmailConfirmacao(marcacao);

            return Ok(marcacao);
        }

        private void EnviarEmailConfirmacao(Marcacao marcacao)
        {
            var clienteEmail = _context.Utilizadores
                .Where(u => u.IDUtilizador == marcacao.IDUtilizador)
                .Select(u => u.Email)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(clienteEmail))
            {
                throw new InvalidOperationException("E-mail do cliente não encontrado.");
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Seu Nome ou Empresa", "gueibysilva2@gmail.com"));
            message.To.Add(new MailboxAddress("Cliente", clienteEmail));
            message.Subject = "Confirmação de Marcação";
            message.Body = new TextPart("plain")
            {
                Text = $"Sua marcação foi confirmada!\n\nDetalhes:\nData: {marcacao.Data}\nHora: {marcacao.Hora}\nStatus: {marcacao.Status}"
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
