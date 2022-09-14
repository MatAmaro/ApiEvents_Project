using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEvents.Core.Models
{
    public class EventReservation
    {
        public long IdReservation { get; set; }

        [Required(ErrorMessage = "O id do evento é obrigatório")]
        [Range(1, long.MaxValue, ErrorMessage = "O Id do evento precisa ser maior que zero")]
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "É obrigatório informar um nome para a reserva")]
        [StringLength(1000, ErrorMessage = "O nome precisa conter entre 3 e 1000 caracteres")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a qunatidade de pessoas para a reserva")]
        [Range(1, long.MaxValue, ErrorMessage = "A quantidade miníma é 1")]
        public long Quantity { get; set; }
    }
}
