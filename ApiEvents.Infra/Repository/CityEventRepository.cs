using ApiEvents.Core.Interfaces;
using ApiEvents.Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEvents.Infra.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConnectionDataBase _dataBase;

        public CityEventRepository(IConnectionDataBase dataBase)
        {
            _dataBase = dataBase;
        }


        public List<CityEvent> GetCityEvents()
        {
            var query = "SELECT * FROM cityEvent";

            using var conn = _dataBase.CreateConnection();

            return conn.Query<CityEvent>(query).ToList();
        }

        public CityEvent GetCityEventByTitleAndLocal(string title, string local)
        {
            var query = "SELECT * FROM cityEvent Where title = @title AND local = @local";
            var parameters = new DynamicParameters(new {title, local });

            using var conn = _dataBase.CreateConnection();

            return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
            
        }

        public CityEvent GetCityEventByIdEvent(long idEvent)
        {
            var query = "SELECT * FROM cityEvent Where idEvent = @idEvent";
            var parameters = new DynamicParameters(new { idEvent});

            using var conn = _dataBase.CreateConnection();

            return conn.QueryFirstOrDefault<CityEvent>(query, parameters);

        }

        public List<CityEvent> GetCityEventsByTitle(string title)
        {
            var query = "SELECT * FROM cityEvent WHERE title LIKE CONCAT('%', @title,'%')";
            var parameters = new DynamicParameters(new { title });
            using var conn = _dataBase.CreateConnection();

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> GetCityEventsByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date)
        {
            var query = "SELECT * FROM cityEvent WHERE price BETWEEN @priceMin AND @priceMax AND CAST(dateHourEvent AS DATE) = CAST(@date AS DATE) AND status = 1";
            var parameters = new DynamicParameters(new { priceMin, priceMax, date });
            using var conn = _dataBase.CreateConnection();

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> GetCityEventsByLocalAndDate(string local, DateTime date)
        {
            var query = "SELECT * FROM cityEvent WHERE local = @local AND CAST(dateHourEvent AS DATE) = CAST(@date AS DATE)";
            var parameters = new DynamicParameters(new { local, date });
            using var conn = _dataBase.CreateConnection();

            return conn.Query<CityEvent>(query, parameters).ToList();

        }

        public bool InsertCityEvent(CityEvent cityEvent)
        {
            var query = "INSERT INTO cityEvent Values(@title, @description, @dateHourEvent, @local, @address, @price,1)";
            var parameters = new DynamicParameters(new {/*cityEvent.Title, cityEvent.Description, cityEvent.DateHourEvent, cityEvent.Local, cityEvent.Address,cityEvent.Price*/ cityEvent});

            using var conn = _dataBase.CreateConnection();

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateCityEvent(CityEvent cityEvent,long idEvent)
        {
            var query = @"UPDATE cityEvent
SET title = @title , description = @description, dateHourEvent = @dateHourEvent, local = @local, address = @address, price = @price
WHERE idEvent = @idEvent";
            var parameters = new DynamicParameters();
            parameters.Add("title", cityEvent.Title);
            parameters.Add("description", cityEvent.Description);
            parameters.Add("dateHourEvent", cityEvent.DateHourEvent);
            parameters.Add("local", cityEvent.Local);
            parameters.Add("address", cityEvent.Address);
            parameters.Add("price", cityEvent.Price);
            parameters.Add("idEvent", idEvent);           

            using var conn = _dataBase.CreateConnection();
            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteCityEvent(string title, string local)
        {
            var query = "DELETE FROM cityEvent WHERE title = @title AND local = @local";
            var parameters = new DynamicParameters(new { title, local });
            
           using var conn = _dataBase.CreateConnection();

            return conn.Execute(query, parameters) == 1;
        }

        public int ReservationQuantity(long idEvent)
        {
            var query = "SELECT COUNT(idReservation) AS quantidade FROM eventReservation WHERE idEvent = @idEvent";
            var parameters = new DynamicParameters(new { idEvent });

            using var conn = _dataBase.CreateConnection();

            return conn.QueryFirstOrDefault<int>(query, parameters);
        }

        public bool DeleteCityEvent(long idEvent)
        {
            var query = "DELETE FROM cityEvent WHERE idEvent = @idEvent";
            var parameters = new DynamicParameters(new { idEvent });

            using var conn = _dataBase.CreateConnection();

            return conn.Execute(query, parameters) == 1;
        }

        public bool disableStatus( long idEvent)
        {
            var query = @"UPDATE cityEvent
SET Status = 0
WHERE idEvent = @idEvent";
            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = _dataBase.CreateConnection();
            return conn.Execute(query, parameters) == 1;
        }


    }
}
