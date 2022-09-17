using ApiEvents.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiEvents.Filters
{
    public class ActionFilterCheckInactivatedEvent : ActionFilterAttribute
    {

        private readonly ICityEventService _cityEventService;

        public ActionFilterCheckInactivatedEvent(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var id = (long)context.ActionArguments["IdEvent"];

            var reservationqtd = _cityEventService.ReservationQuantity(id);
            var eventreservation = _cityEventService.GetCityEventInactivated(id);
            if(reservationqtd > 0 && eventreservation != null)
            {
                var inactivated = new ObjectResult(new { error = "O evento ja foi inativado e continua com reservas associadas"}) { StatusCode = StatusCodes.Status400BadRequest };
                context.Result = inactivated;
            }
        }
    }
}
