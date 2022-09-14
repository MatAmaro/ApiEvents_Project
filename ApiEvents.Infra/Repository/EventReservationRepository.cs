using ApiEvents.Core.Interfaces;
using ApiEvents.Core.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEvents.Infra.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConnectionDataBase _dataBase;

        public EventReservationRepository(IConnectionDataBase connectionData)
        {
            _dataBase = connectionData;
        }


        public List<EventReservation> GetEventReservations()
        {
            var query = "SELECT * FROM eventReservation";
            using var conn = _dataBase.CreateConnection();

            return conn.Query<EventReservation>(query).ToList();
        }

        public EventReservation GetEventReservationByNameAndIdEvent(long idEvent, string personName)
        {
            var query = "SELECT * FROM eventReservation WHERE idEvent = @idEvent and personName = @personName";
            var parameters = new DynamicParameters(new { idEvent, personName });
            using var conn = _dataBase.CreateConnection();

            return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
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

            using var conn = _dataBase.CreateConnection();
            return conn.Execute(query, parameters) == 1;
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

            using var conn = _dataBase.CreateConnection();
            return conn.Execute(query,parameters) == 1;
        }

        public bool DeleteEventReservation(long idEvent, string personName)
        {
            var query = "DELETE FROM eventReservation WHERE idEvent = @idEvent AND personName = @personName";
            var parameters = new DynamicParameters(new { idEvent, personName });
            using var conn = _dataBase.CreateConnection();

            return conn.Execute(query, parameters) == 1;

        }


    }
}
