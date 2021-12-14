using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using static TgBot.Program;

namespace TgBot.Models
{
    public class CurRoute
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column]
        public long RecordId { get; protected set; }

        [Column(TypeName = "varchar(max)")]
        [BackingField("TimeOfStops")]
        public string? TimeOfStops { get; protected set; }

        [Column(TypeName = "varchar(256)")]
        public string? NumberOfLeaving { get; protected set; }

        [Column(TypeName = "varchar(256)")]
        public string? NumberOfIncoming { get; protected set; }
        public DateTime Day { get; protected set; }
        public Route Route { get; protected set; }
        public Driver Driver { get; protected set; }
        [Required]
        [ForeignKey("Route")]
        public string RouteId;
        [Required]
        [ForeignKey("Driver")]
        public string DriverId;


        public CurRoute(DateTime day, string routeId, string driverId)
        {
            Day = day;
            RouteId = routeId;
            DriverId = driverId;
        }



        public bool IsFinished() => Route.Stops.Split(";").Length == (string.IsNullOrEmpty(TimeOfStops) ? 0 : TimeOfStops.Split(";").Length + 1);

        public bool DeleteFakeData()
        {
            string[] leaving = NumberOfLeaving.Split(';');

            string[] dates = TimeOfStops == null ? new[] { "" } : TimeOfStops.Split(';');
            string[] incoming = NumberOfIncoming.Split(';');
            if (leaving.Length == incoming.Length && incoming.Length == 1) return false;



            if (leaving.Length > incoming.Length)
            {
                NumberOfLeaving = string.Join(";", leaving, 0, leaving.Length - 1);
                TimeOfStops = string.Join(";", dates, 0, dates.Length - 1);
            }
            else
                        if (leaving.Length < incoming.Length) NumberOfIncoming = string.Join(";", incoming, 0, incoming.Length - 1);
            else
            {
                NumberOfLeaving = string.Join(";", leaving, 0, leaving.Length - 1);
                NumberOfIncoming = string.Join(";", incoming, 0, incoming.Length - 1);
                TimeOfStops =string.Join(";", dates, 0, dates.Length - 1);
            }


            return true;
        }

        public void AddTimeOfStop(DateTime date)
        {
            TimeOfStops = (string.IsNullOrWhiteSpace(TimeOfStops)||TimeOfStops.Length<4 ? "" : TimeOfStops + ";") + date; ;

        }
        public void AddIncoming(byte a)
        {
            NumberOfIncoming = (NumberOfIncoming == null ? "" : NumberOfIncoming + ";") + a;
        }
        public void AddLeaving(byte a)
        {
            NumberOfLeaving = (NumberOfLeaving == null ? "" : NumberOfLeaving + ";") + a;
        }
    }
}