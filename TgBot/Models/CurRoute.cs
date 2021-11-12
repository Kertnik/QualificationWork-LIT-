using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static TgBot.Program;

namespace TgBot.Models
{
    public class CurRoute : IDisposable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column]
        public long RecordID { get; private set; }

        [Column(TypeName = "varchar(max)")]
        string? _timeOfStops { get; set; }

        [Column(TypeName = "varchar(256)")]
        string? _numberOfLeaving { get; set; }

        [Column(TypeName = "varchar(256)")]
        string? _numberOfIncoming { get; set; }


        public DateTime Day;

      [BackingField("Driver")]
      public   string DriverId
        {
            get => Driver.DriverId;
            set=>Driver=GeneralContext.MyDrivers.Find(value);
        }
      [BackingField("Route")]
      
       public  string RouteId    {
            get => Route.RouteId;
            set=>Route=GeneralContext.MyRoutes.Find(value);
        }

         public Route Route;

        public Driver Driver;
      

        public CurRoute(string driverId, string RouteId, DateTime day)
        {
            DriverId = driverId;
            RouteId=RouteId;
            Day = day;
        }


        public void Dispose()
        {
            GeneralContext.SaveChanges();
        }

        public bool IsFinished()
        {
            return Route.NumberOfStops == TimeOfStops.Count;
        }


        #region Properties


        [NotMapped]

        [BackingField("_timeOfStops")]
        public List<DateTime> TimeOfStops
        {
            get => _timeOfStops.Split(";").Select(Convert.ToDateTime).ToList();
            set => _timeOfStops = string.Join(";", value);
        }
        [NotMapped]

        [BackingField("_numberOfLeaving")]
        public List<byte> NumberOfLeaveing
        {
            get => _numberOfLeaving.Split(";").Select(variable => Convert.ToByte(variable)).ToList();
            set => _numberOfLeaving = string.Join(";", value);
        }
        [NotMapped]
        [BackingField("_numberOfIncoming")]
        public List<byte> NumberOfIncoming
        {
            get => _numberOfIncoming.Split(";").Select(variable => Convert.ToByte(variable)).ToList();
            set => _numberOfIncoming = string.Join(";", value);
        }

        #endregion
    }
}