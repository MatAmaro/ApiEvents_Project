using ApiEvents.Core.Interfaces;
using ApiEvents.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiEvents.Filters
{
    public class ActionFilterEventDuplicated : ActionFilterAttribute
    {
        private readonly ICityEventService _cityEventService;

        public ActionFilterEventDuplicated(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cityEvent = (CityEvent)context.ActionArguments["cityEvent"];

            if (_cityEventService.GetCityEventByTitleAndLocalAndDate(cityEvent.Title, cityEvent.Local, cityEvent.DateHourEvent) != null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
