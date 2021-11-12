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
        [BackingField("TimeOfStops")]
        public string? _timeOfStops { get;private set; }

        public bool? Direction { get; private set; }
        [Column(TypeName = "varchar(256)")]
        public string? _numberOfLeaving { get;private set; }

        [Column(TypeName = "varchar(256)")]
        public string? _numberOfIncoming { get;private set; }


        public DateTime Day;
        [NotMapped]
        public Route Route;
        public Driver Driver;

        public CurRoute(string driverId, DateTime day)
        {
            Driver = GeneralContext.MyDrivers.Find(driverId);
            Route = Driver.OrdinalRoute;
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
        public List<DateTime> TimeOfStops
        {
            get => _timeOfStops != null ? _timeOfStops.Split(";").Select(Convert.ToDateTime).ToList() : new List<DateTime>();
            set => _timeOfStops = string.Join(";", value);
        }

        [NotMapped]

        public List<byte> NumberOfLeaveing
        {
            get=> _numberOfLeaving != null ? _numberOfLeaving.Split(";").Select(variable => Convert.ToByte(variable)).ToList() : new List<byte>();
            
            set => _numberOfLeaving = string.Join(";", value);
        }

        [NotMapped]
        public List<byte> NumberOfIncoming
        {
            get => _numberOfIncoming != null ? _numberOfIncoming.Split(";").Select(variable => Convert.ToByte(variable)).ToList() : new List<byte>();
            
            set => _numberOfIncoming = string.Join(";", value);
        }

        #endregion
    }
}