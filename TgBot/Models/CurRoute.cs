using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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


        public CurRoute(DateTime day,string routeId,string driverId)
        {
            Day = day;
            RouteId = routeId;
            DriverId = driverId;
        }

      

        public bool IsFinished() => Route.Stops.Split(";").Length == (string.IsNullOrEmpty(TimeOfStops) ? 0 : TimeOfStops.Split(";").Length+1);


        public void AddTimeOfStop(DateTime date)
        {
            TimeOfStops = (TimeOfStops == null ? "" : TimeOfStops + ";") + date; ;

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