using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
        IPilotoRepositorio _pilotoRepositorio;

        public PilotoController(IPilotoRepositorio pilotoRepositorio)
        {
            _pilotoRepositorio = pilotoRepositorio;
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
        public IActionResult Adicionar([FromBody]Piloto piloto)
        {
            try
            {
                if (_pilotoRepositorio.Existe(piloto.Id))
                    return StatusCode(409, "Piloto já existe com o mesmo ID.");

                _pilotoRepositorio.Adicionar(piloto);
                return CreatedAtRoute("Obter", new { id = piloto.Id}, piloto);
            }
            catch(Exception ex)
            {
                //_logger.info(ex.ToString());
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

                return Ok(piloto);
            }
            catch (Exception ex)
            {
                //_logger.info(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno, favor entrar em contato com o administrador.");
            }
        }

        [HttpPut]
        public IActionResult Alterar([FromBody]Piloto piloto)
        {
            try
            {
                if (!_pilotoRepositorio.Existe(piloto.Id))
                    return NotFound();

                _pilotoRepositorio.Atualizar(piloto);
                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.info(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno, favor entrar em contato com o administrador.");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult AlterarParcial(int id, [FromBody] JsonPatchDocument<Piloto> patchPiloto)
        {
            try
            {
                if (!_pilotoRepositorio.Existe(id))
                    return NotFound();

                var piloto = _pilotoRepositorio.Obter(id);

                patchPiloto.ApplyTo(piloto);
                _pilotoRepositorio.Atualizar(piloto);

                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.info(ex.ToString());
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
                //_logger.info(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno, favor entrar em contato com o administrador.");
            }
        }
    }
}
