using ApiEvents.Core.Interfaces;
using ApiEvents.Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CityEventRepository> _logger;
        public string DbEror { get; private set; } = "Erro ao comunicar-se com banco de dados";


        public CityEventRepository(IConnectionDataBase dataBase, ILogger<CityEventRepository> logger)
        {
            _dataBase = dataBase;
            _logger = logger;
        }


        public List<CityEvent> GetCityEvents()
        {
            var query = "SELECT * FROM cityEvent";

            try
            {
                using var conn = _dataBase.CreateConnection();

                return conn.Query<CityEvent>(query).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }

            
        }

        public CityEvent GetCityEventByTitleAndLocal(string title, string local)
        {
            var query = "SELECT * FROM cityEvent Where title = @title AND local = @local";
            var parameters = new DynamicParameters(new {title, local });
            try
            {
                using var conn = _dataBase.CreateConnection();

                return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }

        }

        public CityEvent GetCityEventByIdEvent(long idEvent)
        {
            var query = "SELECT * FROM cityEvent Where idEvent = @idEvent";
            var parameters = new DynamicParameters(new { idEvent});
            try
            {
                using var conn = _dataBase.CreateConnection();

                return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }

        }

        public List<CityEvent> GetCityEventsByTitle(string title)
        {
            var query = "SELECT * FROM cityEvent WHERE title LIKE CONCAT('%', @title,'%')";
            var parameters = new DynamicParameters(new { title });
            try
            {
                using var conn = _dataBase.CreateConnection();

                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
        }

        public List<CityEvent> GetCityEventsByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date)
        {
            var query = "SELECT * FROM cityEvent WHERE price BETWEEN @priceMin AND @priceMax AND CAST(dateHourEvent AS DATE) = CAST(@date AS DATE) AND status = 1";
            var parameters = new DynamicParameters(new { priceMin, priceMax, date });

            try
            {
                using var conn = _dataBase.CreateConnection();

                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
        }

        public List<CityEvent> GetCityEventsByLocalAndDate(string local, DateTime date)
        {
            var query = "SELECT * FROM cityEvent WHERE local = @local AND CAST(dateHourEvent AS DATE) = CAST(@date AS DATE)";
            var parameters = new DynamicParameters(new { local, date });
            try
            {
                using var conn = _dataBase.CreateConnection();

                return conn.Query<CityEvent>(query, parameters).ToList();
            }

            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
        }

        public bool InsertCityEvent(CityEvent cityEvent)
        {
            var query = "INSERT INTO cityEvent Values(@title, @description, @dateHourEvent, @local, @address, @price,1)";
            var parameters = new DynamicParameters(new {cityEvent.Title, cityEvent.Description, cityEvent.DateHourEvent, cityEvent.Local, cityEvent.Address,cityEvent.Price});

            try
            {
                using var conn = _dataBase.CreateConnection();

                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
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

            try
            {
                using var conn = _dataBase.CreateConnection();
                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
        }

        public bool DeleteCityEvent(string title, string local)
        {
            var query = "DELETE FROM cityEvent WHERE title = @title AND local = @local";
            var parameters = new DynamicParameters(new { title, local });
            try
            {
                using var conn = _dataBase.CreateConnection();

                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
        }

        public int ReservationQuantity(long idEvent)
        {
            var query = "SELECT COUNT(idReservation) AS quantidade FROM eventReservation WHERE idEvent = @idEvent";
            var parameters = new DynamicParameters(new { idEvent });

            try
            {
                using var conn = _dataBase.CreateConnection();

                return conn.QueryFirstOrDefault<int>(query, parameters);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar-se com banco de dados \nMensagem: {ex.Message} \nStack trace: {ex.StackTrace}");

                throw;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao comunicar-se com banco de dados \nMensagem: {ex.Message} \nStack trace: {ex.StackTrace}");

                throw;
            }
        }

        public bool DeleteCityEvent(long idEvent)
        {
            var query = "DELETE FROM cityEvent WHERE idEvent = @idEvent";
            var parameters = new DynamicParameters(new { idEvent });

            try
            {
                using var conn = _dataBase.CreateConnection();

                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
        }

        public bool disableStatus( long idEvent)
        {
            var query = @"UPDATE cityEvent
SET Status = 0
WHERE idEvent = @idEvent";
            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            try
            {
                using var conn = _dataBase.CreateConnection();
                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
        }

        public CityEvent GetCityEventByTitleAndLocalAndDate(string title, string local, DateTime date)
        {
            var query = "SELECT * FROM cityEvent Where title = @title AND local = @local AND CAST(dateHourEvent AS DATE) = CAST(@date AS DATE)";
            var parameters = new DynamicParameters(new { title, local, date });

            try
            {
                using var conn = _dataBase.CreateConnection();

                return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }

        }

        public CityEvent GetCityEventInactivated(long idEvent)
        {
            var query = "SELECT * FROM cityEvent WHERE idEvent = @idEvent and status = 0";
            var parameters = new DynamicParameters(new { idEvent });
            try
            {
                using var conn = _dataBase.CreateConnection();
                return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, DbEror);
                throw;
            }
        }


    }
}
