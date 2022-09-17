using ApiEvents.Core.Interfaces;
using ApiEvents.Core.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEvents.Infra.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConnectionDataBase _dataBase;
        private readonly ILogger<EventReservationRepository> _logger;
        public string DbEror { get; private set; } = "Erro ao comunicar-se com banco de dados";

        public EventReservationRepository(IConnectionDataBase connectionData, ILogger<EventReservationRepository> logger )
        {
            _dataBase = connectionData;
            _logger = logger;
        }


        public List<EventReservation> GetEventReservations()
        {
            var query = "SELECT * FROM eventReservation";
            try
            {
                using var conn = _dataBase.CreateConnection();
                return conn.Query<EventReservation>(query).ToList();
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

        public EventReservation GetEventReservationByNameAndIdEvent(long idEvent, string personName)
        {
            var query = "SELECT * FROM eventReservation WHERE idEvent = @idEvent and personName = @personName";
            var parameters = new DynamicParameters(new { idEvent, personName });
            try
            {
                using var conn = _dataBase.CreateConnection();
                return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
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

        public List<EventReservation> GetEventReservationByNameAndEventTitle(string personName, string title)
        {
            var query = @"SELECT er.idReservation, er.idEvent, er.personName, er.quantity FROM cityEvent AS ce
INNER JOIN eventReservation AS er ON ce.idEvent = er.idEvent
WHERE title LIKE CONCAT('%', @title,'%') AND personName = @personName";
            var parameters = new DynamicParameters(new { title, personName });
            using var conn = _dataBase.CreateConnection();

            return conn.Query<EventReservation>(query, parameters).ToList();
        }


        public bool InsertEventReservation(EventReservation eventReservation)
        {
            var query = "INSERT INTO eventReservation VALUES(@idEvent, @personName, @quantity)";
            var parameters = new DynamicParameters();
            parameters.Add("idEvent", eventReservation.IdEvent);
            parameters.Add("personName", eventReservation.PersonName);
            parameters.Add("quantity", eventReservation.Quantity);

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


        public bool UpdateEventReservation(long idEvent, string personName, EventReservation eventReservation)
        {
            var query = @"UPDATE eventReservation 
SET idEvent = @idEvent, personName = @personName, quantity = @quantity
WHERE idEvent = @idEventUpdate and personName = @personNameUpdate";
            var parameters = new DynamicParameters();
            parameters.Add("idEvent", eventReservation.IdEvent);
            parameters.Add("personName", eventReservation.PersonName);
            parameters.Add("quantity", eventReservation.Quantity);
            parameters.Add("idEventUpdate", idEvent);
            parameters.Add("personNameUpdate", personName);

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

        public bool UpdateEventReservationQuantity(long idEvent, string personName,int quantity)
        {
            var query = @"UPDATE eventReservation 
SET quantity = @quantity
WHERE idEvent = @idEventUpdate and personName = @personNameUpdate";
            var parameters = new DynamicParameters();
            parameters.Add("quantity", quantity);
            parameters.Add("idEventUpdate", idEvent);
            parameters.Add("personNameUpdate", personName);

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

        public bool DeleteEventReservation(long idEvent, string personName)
        {
            var query = "DELETE FROM eventReservation WHERE idEvent = @idEvent AND personName = @personName";
            var parameters = new DynamicParameters(new { idEvent, personName });

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

    }
}
