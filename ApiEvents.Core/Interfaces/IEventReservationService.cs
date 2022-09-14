using ApiEvents.Core.Models;

namespace ApiEvents.Core.Interfaces
{
    public interface IEventReservationService
    {
        EventReservation GetEventReservationByNameAndIdEvent(long idEvent, string personName);
        List<EventReservation> GetEventReservations();
        List<EventReservation> GetEventReservationByNameAndEventTitle(string personName, string title);
        bool InsertEventReservation(EventReservation eventReservation);
        bool UpdateEventReservation(long idEvent, string personName, EventReservation eventReservation);
        bool DeleteEventReservation(long idEvent, string personName);

    }
}