#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static TgBot.Program;

namespace TgBot.Models
{
    public class Driver : IEqualityComparer
    {
        public Driver(string driverId, string name)
        {
            MyPaths = new List<CurPath>();
            foreach (var variable in GeneralContext.CurPaths)
                if (variable.Driver.DriverId == DriverId)
                    MyPaths.Add(variable);
            DriverId = driverId;
            Name = name;
        }

        [Key] public string DriverId { get; }

        [Column(TypeName = "nvarchar(256)")] public string Name { get; }

        [Column(TypeName = "nvarchar(256)")] string? OrdinalPathId { get; set; }

        [BackingField("OrdinalPathId")]
        public Path? OrdinalPath
        {
            get => GeneralContext.Paths.Find(OrdinalPathId);
            private set => OrdinalPathId = value?.PathId;
        }

        public List<CurPath> MyPaths { get; set; }


        public new bool Equals(object? x, object? y)
        {
            if (x == null | y == null) return false;
            if (x is Driver & y is Driver)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                return ((Driver)x).DriverId == ((Driver)y).DriverId;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            return false;
        }


        public int GetHashCode(object obj)
        {
            return HashCode.Combine(obj);
        }

        public void NewRoute()
        {
            if (MyPaths.Last().IsFinished())
            {
                MyPaths.Add(new CurPath(this, OrdinalPath, DateTime.Today));
                GeneralContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Bad Call");
            }
        }


        public void SetPath(Path path)
        {
            foreach (var variable in GeneralContext.Paths)
                if (variable == path)
                {
                    OrdinalPath = path;
                    return;
                }

            throw new ArgumentException("Bad path id");
        }

        public override string ToString()
        {
            return DriverId;
        }
    }
}