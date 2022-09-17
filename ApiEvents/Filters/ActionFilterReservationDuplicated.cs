using ApiEvents.Core.Interfaces;
using ApiEvents.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiEvents.Filters
{
    public class ActionFilterReservationDuplicated : ActionFilterAttribute
    {
        private readonly IEventReservationService _eventReservationService;

        public ActionFilterReservationDuplicated(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var eventReservation = (EventReservation)context.ActionArguments["eventReservation"];

            if(_eventReservationService.GetEventReservationByNameAndIdEvent(eventReservation.IdEvent,eventReservation.PersonName) != null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
