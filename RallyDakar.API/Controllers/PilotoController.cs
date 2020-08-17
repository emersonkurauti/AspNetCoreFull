using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RallyDakar.API.Modelo;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Controllers
{
    [ApiController]
    [Route("api/pilotos")]
    public class PilotoController : ControllerBase
    {
        private readonly IPilotoRepositorio _pilotoRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<PilotoController> _logger;

        public PilotoController(IPilotoRepositorio pilotoRepositorio, IMapper mapper, ILogger<PilotoController> logger)
        {
            _pilotoRepositorio = pilotoRepositorio;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(_pilotoRepositorio.ObterTodos());
        }

        /*[HttpGet("{nome}", Name = "ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            return Ok(_pilotoRepositorio.ObterPorNome(nome));
        }*/

        [HttpPost]
        public IActionResult Adicionar([FromBody]PilotoModelo pilotoModelo)
        {
            try
            {
                _logger.LogInformation("Mapeando PilotoModelo");
                var piloto = _mapper.Map<Piloto>(pilotoModelo);

                _logger.LogInformation($"Verificando piloto existe ID: {piloto.Id}");
                if (_pilotoRepositorio.Existe(piloto.Id))
                {
                    _logger.LogInformation($"Piloto já existe com o mesmo ID: {piloto.Id}");
                    return StatusCode(409, "Piloto já existe com o mesmo ID.");
                }

                _logger.LogInformation("Adicionando piloto");
                _pilotoRepositorio.Adicionar(piloto);

                _logger.LogInformation("Mapeando piolo");
                var pilotoModeloRetorno = _mapper.Map<PilotoModelo>(piloto);
                _logger.LogInformation("Criando rota");
                return CreatedAtRoute("Obter", new { id = piloto.Id}, pilotoModeloRetorno);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno, favor entrar em contato com o administrador.");
            }
        }

        [HttpGet("{id}", Name = "Obter")]
        public IActionResult Obter(int id)
        {
            try
            {
                var piloto = _pilotoRepositorio.Obter(id);
                if (piloto == null) 
                    return NotFound();

                var pilotoModelo = _mapper.Map<PilotoModelo>(piloto);

                return Ok(pilotoModelo);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno, favor entrar em contato com o administrador.");
            }
        }

        [HttpPut]
        public IActionResult Alterar([FromBody]PilotoModelo pilotoModelo)
        {
            try
            {
                if (!_pilotoRepositorio.Existe(pilotoModelo.Id))
                    return NotFound();

                var piloto = _mapper.Map<Piloto>(pilotoModelo);

                _pilotoRepositorio.Atualizar(piloto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno, favor entrar em contato com o administrador.");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult AlterarParcial(int id, [FromBody] JsonPatchDocument<PilotoModelo> patchPilotoModelo)
        {
            try
            {
                if (!_pilotoRepositorio.Existe(id))
                    return NotFound();

                var piloto = _pilotoRepositorio.Obter(id);
                var pilotoModelo = _mapper.Map<PilotoModelo>(piloto);

                patchPilotoModelo.ApplyTo(pilotoModelo);

                piloto = _mapper.Map(pilotoModelo, piloto);

                _pilotoRepositorio.Atualizar(piloto);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno, favor entrar em contato com o administrador.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var piloto = _pilotoRepositorio.Obter(id);
                if (piloto == null)
                    return NotFound();

                _pilotoRepositorio.Deletar(piloto);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno, favor entrar em contato com o administrador.");
            }
        }

        [HttpOptions]
        public IActionResult ListarOperacoesPermitidas()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
            return Ok();
        }
    }
}
