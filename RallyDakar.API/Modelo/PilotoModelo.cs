using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Modelo
{
    public class PilotoModelo
    {        
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "É de preenchimento obrigatório.")]
        [MinLength(5,ErrorMessage = "Deve ter no mínimo 5 caracteres.")]
        [MaxLength(50,ErrorMessage = "Deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; }
        [MinLength(5, ErrorMessage = "Sobrenome deve ter no mínimo 5 caracteres.")]
        [MaxLength(50, ErrorMessage = "Sobrenome deve ter no máximo 50 caracteres.")]
        public string SobreNome { get; set; }
        public int EquipeId { get; set; }
        public string NomeCompleto 
        { 
            get { return $"{Nome} {SobreNome}"; } 
        }
    }
}
