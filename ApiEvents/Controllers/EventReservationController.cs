using ApiEvents.Core.Interfaces;
using ApiEvents.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EventReservationController : ControllerBase
    {
        private readonly IEventReservationService _reservationService;

        public EventReservationController(IEventReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("eventos/reservas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<EventReservation>> GetEventReservations()
        {
            return Ok(_reservationService.GetEventReservations());
        }

        [HttpGet("eventos/reservas/{idEvent}/{personName}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EventReservation> GetEventReservationByNameAndIdEvent(long idEvent, string personName)
        {
            var eventReservation = _reservationService.GetEventReservationByNameAndIdEvent(idEvent, personName);
            if(eventReservation == null)
            {
                return NotFound();
            }
            return Ok(eventReservation);
        }

        [HttpGet("eventos/reservas/titulo/{title}/{personName}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<EventReservation>> GetEventReservationByNameAndEventTitle(string title, string personName)
        {
            var eventReservations = _reservationService.GetEventReservationByNameAndEventTitle(personName, title);
            if (eventReservations.Count == 0)
            {
                return NotFound();
            }
            return Ok(eventReservations);
        }

        [HttpPost("eventos/reservas/inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InsertEventReservation(EventReservation eventReservation)
        {
            if (!_reservationService.InsertEventReservation(eventReservation))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(InsertEventReservation), eventReservation);
        }

        [HttpPut("enventos/reservas/atualizar/{idEvent}/{personName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult<List<EventReservation>> UpdateEventReservation(long idEvent, string personName, EventReservation eventReservation)
        {
            List<EventReservation> eventReservations = new();
            var oldEventReservation =_reservationService.GetEventReservationByNameAndIdEvent(idEvent, personName);
            if (oldEventReservation == null)
            {
                return NotFound();
            }
            eventReservations.Add(oldEventReservation);
            _reservationService.UpdateEventReservation(idEvent, personName, eventReservation);
            eventReservations.Add(eventReservation);
            return Accepted(eventReservations);
        }

        [HttpDelete("eventos/delete/{idEvent}/{personName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEventReservavtion(long idEvent, string personName)
        {
            if(!_reservationService.DeleteEventReservation(idEvent, personName))
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}
