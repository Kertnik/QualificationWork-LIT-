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

        public bool? Direction { get; private set; }
        [Column(TypeName = "varchar(256)")]
        public string? NumberOfLeaving { get; private set; }

        [Column(TypeName = "varchar(256)")]
        public string? NumberOfIncoming { get; private set; }


        public DateTime Day { get; private set; }
        public Route Route { get; private set; }
        public Driver Driver { get; private set; }
        public CurRoute(string driverId, DateTime day, string routeId)
        {
            using (var db = new DriverContextFactory().CreateDbContext())
            {
                Driver = db.MyDrivers.Find(driverId);
                Route = db.MyRoutes.Find(routeId);
                db.Entry(Route).State = EntityState.Detached;
                Day = day;
            }
        }
        public CurRoute(Driver driver, DateTime day, Route route)
        {
            Route = route;
            Driver = driver;
            Day = day;

        }


        public bool IsFinished() => Route.NumberOfStops == (string.IsNullOrEmpty(TimeOfStops) ? 0 : TimeOfStops.Split(";").Length);


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