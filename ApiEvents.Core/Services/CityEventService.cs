using ApiEvents.Core.Interfaces;
using ApiEvents.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEvents.Core.Services
{
    public class CityEventService : ICityEventService
    {
        private readonly ICityEventRepository _cityEventRepository;

        public CityEventService(ICityEventRepository eventRepository)
        {
            _cityEventRepository = eventRepository;
        }

        public List<CityEvent> GetCityEvents()
        {
            return _cityEventRepository.GetCityEvents();
        }

        public CityEvent GetCityEventByTitleAndLocal(string title, string local)
        {
            return _cityEventRepository.GetCityEventByTitleAndLocal(title, local);
        }

        public List<CityEvent> GetCityEventsByTitle(string title)
        {
            return _cityEventRepository.GetCityEventsByTitle(title);
        }

        public List<CityEvent> GetCityEventsByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date)
        {
            return _cityEventRepository.GetCityEventsByPriceAndDate(priceMin, priceMax, date);
        }

        public List<CityEvent> GetCityEventsByLocalAndDate(string local, DateTime date)
        {
            return _cityEventRepository.GetCityEventsByLocalAndDate(local, date);
        }

        public bool InsertCityEvent(CityEvent cityEvent)
        {
            return _cityEventRepository.InsertCityEvent(cityEvent);
        }

        public bool UpdateCityEvent(CityEvent cityEvent, long idvent)
        {
            return _cityEventRepository.UpdateCityEvent(cityEvent,idvent);
        }

        public bool DeleteCityEvent(string title, string local)
        {
           return _cityEventRepository.DeleteCityEvent(title, local);
        }

        public CityEvent GetCityEventByIdEvent(long idEvent)
        {
            return _cityEventRepository.GetCityEventByIdEvent(idEvent);
        }

        public int ReservationQuantity(long idEvent)
        {
            return _cityEventRepository.ReservationQuantity(idEvent);
        }

        public bool DeleteCityEvent(long idEvent)
        {
            return _cityEventRepository.DeleteCityEvent(idEvent);
        }

        public bool disableStatus(long idEvent)
        {
            return _cityEventRepository.disableStatus(idEvent);
        }

        public CityEvent GetCityEventByTitleAndLocalAndDate(string title, string local, DateTime date)
        {
            return _cityEventRepository.GetCityEventByTitleAndLocalAndDate(title,local,date);
        }

        public CityEvent GetCityEventInactivated(long idEvent)
        {
            return _cityEventRepository.GetCityEventInactivated(idEvent);
        }
    }
}
