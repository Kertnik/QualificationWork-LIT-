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
        public long RecordID { get; private set; }

        [Column(TypeName = "varchar(max)")]
        [BackingField("TimeOfStops")]
        public string? TimeOfStops { get; private set; }
        [Column(TypeName = "bit")]
        public bool? IsFromFirstStop { get; set; }
        [Column(TypeName = "varchar(256)")]
        public string? NumberOfLeaving { get; private set; }

        [Column(TypeName = "varchar(256)")]
        public string? NumberOfIncoming { get; private set; }
        public DateTime Day { get; private set; }
        public Route Route { get; private set; }
        public Driver Driver { get; private set; }
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

      

        public bool IsFinished() => Route.Stops.Split(";").Length == (string.IsNullOrEmpty(TimeOfStops) ? 0 : TimeOfStops.Split(";").Length);


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