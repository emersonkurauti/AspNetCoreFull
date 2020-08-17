using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RallyDakar.API.Modelo;
using RallyDakar.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Controllers
{
    [ApiController]
    [Route("api/equipes/{equipeId}/telemetria")]
    public class TelemetriaController : ControllerBase
    {
        private readonly ITelemetriaRepositorio _telemetriaRepositorio;
        private readonly IEquipeRepositorio _equipeRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<TelemetriaController> _logger;

        public TelemetriaController(ITelemetriaRepositorio telemetriaRepositorio, IMapper mapper, ILogger<TelemetriaController> logger,
            IEquipeRepositorio equipeRepositorio)
        {
            _telemetriaRepositorio = telemetriaRepositorio;
            _mapper = mapper;
            _logger = logger;
            _equipeRepositorio = equipeRepositorio;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TelemetriaModelo>> Obter(int equipeId)
        {
            try
            {
                _logger.LogInformation($"Verificando se a equipe {equipeId} existe na base.");
                if (!_equipeRepositorio.Existe(equipeId))
                {
                    _logger.LogWarning($"Equipe {equipeId} não localizada.");
                    return NotFound();
                }

                var telemetrias = _telemetriaRepositorio.ObterTodosPorEquipe(equipeId);

                if(!telemetrias.Any())
                {
                    _logger.LogWarning($"Não foi encontrado dados de telemetria para a quipe {equipeId}.");
                    return NotFound("Não foi encontrado dados de telemetria na base");
                }

                var dadosTelemetriaModelo = _mapper.Map<IEnumerable<TelemetriaModelo>>(telemetrias);
                return Ok(dadosTelemetriaModelo);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Erro: {ex.ToString()}");
                return StatusCode(500, "Ocorreu uma falha no sistema. Contate o Adminsitrador.");
            }
        }
    }
}
