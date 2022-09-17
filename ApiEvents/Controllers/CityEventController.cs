using ApiEvents.Core.Interfaces;
using ApiEvents.Core.Models;
using ApiEvents.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [Authorize]
    public class CityEventController : ControllerBase
    {
        private readonly ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("eventos/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]       
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetCityEvents()
        {
            return Ok(_cityEventService.GetCityEvents());
        }

        [HttpGet("eventos/consultar/{title}/{local}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public ActionResult<CityEvent> GetCityEventByTitleAndLocal(string title, string local)
        {

            var cictyEvent = _cityEventService.GetCityEventByTitleAndLocal(title, local);
            if (cictyEvent == null)
            {
                return NotFound();
            }
            return Ok(cictyEvent);
        }
        [HttpGet("eventos/consultar/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetCityEventByTitle(string title)
        {

            var cictyEvents = _cityEventService.GetCityEventsByTitle(title);
            if (cictyEvents.Count == 0)
            {
                return NotFound();
            }
            return Ok(cictyEvents);
        }



        [HttpGet("eventos/consultar/preço/{priceMin}And{priceMax}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetCityEventsByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date)
        {
            var cityEvents = _cityEventService.GetCityEventsByPriceAndDate(priceMin, priceMax, date);
            if (cityEvents.Count == 0)
            {
                return NotFound();
            }
            return Ok(cityEvents);
        }

        [HttpGet("eventos/consultar/local/{local}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetCityEventsByLocalAndDate(string local, DateTime date)
        {
            var cityEvents = _cityEventService.GetCityEventsByLocalAndDate(local, date);
            if (cityEvents.Count == 0)
            {
                return NotFound();
            }
            return Ok(cityEvents);
        }


        [HttpPost("eventos/inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ServiceFilter(typeof(ActionFilterEventDuplicated))]
        [Authorize(Roles = "admin")]
        public IActionResult InsertCityEvent(CityEvent cityEvent)
        {
            if (!_cityEventService.InsertCityEvent(cityEvent))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(InsertCityEvent), cityEvent);
        }

        [HttpPut("enventos/atuaizar/{idEvent}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "admin")]
        public ActionResult<List<CityEvent>> UpdateCityEvent(CityEvent cityEvent, long idEvent)
        {
            List<CityEvent> cityEvents = new();
            var oldCityEvent = _cityEventService.GetCityEventByIdEvent(idEvent);
            if (oldCityEvent == null)
            {
                return NotFound();
            }
            cityEvents.Add(oldCityEvent);
            _cityEventService.UpdateCityEvent(cityEvent, idEvent);
            cityEvent.IdEvent = idEvent;
            cityEvents.Add(cityEvent);
            return Accepted(cityEvents);
        }

        


        [HttpDelete("eventos/delete/{title}/{local}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteCityEvent(string title, string local)
        {
            if (!_cityEventService.DeleteCityEvent(title, local))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("eventos/delete/{IdEvent}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ServiceFilter(typeof(ActionFilterCheckInactivatedEvent))]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteCityEventByID(long IdEvent)
        {
            var reservationsQtt = _cityEventService.ReservationQuantity(IdEvent);

            if (reservationsQtt == 0)
            {
                if (!_cityEventService.DeleteCityEvent(IdEvent))
                {
                    return NotFound();
                }
                return NoContent();
            }
            else
            {
                var disableValidation =_cityEventService.disableStatus(IdEvent);
                return NoContent();
            }

        }

    }
}
