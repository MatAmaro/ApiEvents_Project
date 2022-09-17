using ApiEvents.Core.Models;

namespace ApiEvents.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        EventReservation GetEventReservationByNameAndIdEvent(long idEvent, string personName);
        List<EventReservation> GetEventReservations();
        List<EventReservation> GetEventReservationByNameAndEventTitle(string personName, string title);
        bool InsertEventReservation(EventReservation eventReservation);
        bool UpdateEventReservation(long idEvent, string personName, EventReservation eventReservation);
        bool UpdateEventReservationQuantity(long idEvent, string personName, int quantity);
        bool DeleteEventReservation(long idEvent, string personName);
    }
}