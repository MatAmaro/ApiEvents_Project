using ApiEvents.Core.Models;

namespace ApiEvents.Core.Interfaces
{
    public interface ICityEventRepository
    {
        List<CityEvent> GetCityEvents();
        CityEvent GetCityEventByTitleAndLocal(string title, string local);
        public CityEvent GetCityEventByIdEvent(long idEvent);
        List<CityEvent> GetCityEventsByTitle(string title);
        List<CityEvent> GetCityEventsByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date);
        List<CityEvent> GetCityEventsByLocalAndDate(string local, DateTime date);
        bool InsertCityEvent(CityEvent cityEvent);
        bool UpdateCityEvent(CityEvent cityEvent, long idEvent);
        bool DeleteCityEvent(string title, string local);
        public int ReservationQuantity(long idEvent);
        bool DeleteCityEvent(long idEvent);
        bool disableStatus(long idEvent);
        
    }
}