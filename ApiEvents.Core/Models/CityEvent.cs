using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEvents.Core.Models
{
    public class CityEvent
    {
        public long IdEvent { get; set; }

        [Required]
        [StringLength(600, ErrorMessage = "O titulo precisa conter entre 3 e 600 caracteres", MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Só é possível inserir no máximo 1000 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Data e hora são obrigatórias")]
        public DateTime DateHourEvent { get; set; }

        [Required]
        [StringLength(600, ErrorMessage = "O local precisa conter entre 3 e 600 caracteres", MinimumLength = 3)]
        public string Local { get; set; }

        [StringLength(600, ErrorMessage = "Só é possível inserir no máximo 600 caracteres")]
        public string Address { get; set; }

        public decimal Price { get; set; }
        public bool Status { get; set; } = true;


    }
}
