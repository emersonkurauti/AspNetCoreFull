using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Modelo
{
    public class TelemetriaModelo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Identificador da equipe é obrigatória.")]
        public int EquipeId { get; set; }

        [Required]
        public DateTime Data { get; set; }
        [Required]
        public TimeSpan Hora { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public double AlitudeNivelMar { get; set; }

        public decimal PercentualCombustivel { get; set; }
        public double Velocidade { get; set; }
        public double RPM { get; set; }
        public int TemperaturaExterna { get; set; }
        public int TemperatudaMotor { get; set; }

        public bool PedalAcelerador { get; set; }
        public bool PedalFreio { get; set; }
    }
}
