using ApiEvents.Core.Interfaces;
using ApiEvents.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEvents.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        private readonly IEventReservationRepository _reservationRepository;

        public EventReservationService(IEventReservationRepository reservationRepositoy)
        {
            _reservationRepository = reservationRepositoy;
        }

        public List<EventReservation> GetEventReservations()
        {
            return _reservationRepository.GetEventReservations();
        }

        public EventReservation GetEventReservationByNameAndIdEvent(long idEvent, string personName)
        {
            return _reservationRepository.GetEventReservationByNameAndIdEvent(idEvent, personName);
        }

        public List<EventReservation> GetEventReservationByNameAndEventTitle(string personName, string title)
        {
            return _reservationRepository.GetEventReservationByNameAndEventTitle(personName, title);
        }

        public bool InsertEventReservation(EventReservation eventReservation)
        {
            return _reservationRepository.InsertEventReservation(eventReservation);
        }

        public bool UpdateEventReservation(long idEvent, string personName, EventReservation eventReservation)
        {
            return _reservationRepository.UpdateEventReservation(idEvent, personName, eventReservation);
        }

        public bool DeleteEventReservation(long idEvent, string personName)
        {
            return _reservationRepository.DeleteEventReservation(idEvent, personName);
        }

        public bool UpdateEventReservationQuantity(long idEvent, string personName, int quantity)
        {
            return _reservationRepository.UpdateEventReservationQuantity(idEvent, personName, quantity);
        }
    }
}
