using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static TgBot.Program;

namespace TgBot.Models
{
    public class CurPath : IDisposable
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
        public Driver Driver { get; set; }

        public Path Path { get; set; }

        public CurPath(Driver driver, Path path, DateTime day)
        {
            Driver = driver;
            Path = path;
            Day = day;
        }


        public void Dispose()
        {
            GeneralContext.SaveChanges();
        }

        public bool IsFinished()
        {
            return Path.NumberOfStops == TimeOfStops.Count;
        }


        #region Properties




        [BackingField("_timeOfStops")]
        public List<DateTime> TimeOfStops
        {
            get => _timeOfStops.Split(";").Select(Convert.ToDateTime).ToList();
            set => _timeOfStops = string.Join(";", value);
        }

        [BackingField("_numberOfLeaving")]
        public List<byte> NumberOfLeaveing
        {
            get => _numberOfLeaving.Split(";").Select(variable => Convert.ToByte(variable)).ToList();
            set => _numberOfLeaving = string.Join(";", value);
        }

        [BackingField("_numberOfIncoming")]
        public List<byte> NumberOfIncoming
        {
            get => _numberOfIncoming.Split(";").Select(variable => Convert.ToByte(variable)).ToList();
            set => _numberOfIncoming = string.Join(";", value);
        }

        #endregion
    }
}