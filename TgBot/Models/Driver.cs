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
        public Driver(string driverId, string name, string ordinalRouteId)
        {
            MyRoutes = new List<CurRoute>();

            foreach (var variable in GeneralContext.MyCurRoutes.Local)
                if (variable.Driver.DriverId == DriverId)
                    MyRoutes.Add(variable);
            DriverId = driverId;
            Name = name;
            OrdinalRouteId = ordinalRouteId;
        }

        [Key]
        [Required]
        public string DriverId
        {
            get;
            private set;
        }

        [Column(TypeName = "nvarchar(256)")]
        [Required]
        public string Name { get; private set; }
        [ForeignKey("OrdinalRoute")]
        public string? OrdinalRouteId
        {
            get => OrdinalRoute.RouteId;
            set => OrdinalRoute = GeneralContext.MyRoutes.Find(value);
        }


        public Route? OrdinalRoute
        {
            get;
            private set;
        }
        public List<CurRoute> MyRoutes { get; set; }


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
            if (MyRoutes.Count == 0 || MyRoutes.Last().IsFinished())
            {
                MyRoutes.Add(new CurRoute(DriverId, DateTime.Today));
                GeneralContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Bad Call");
            }
        }


        public void SetRoute(Route? route)
        {
            foreach (var variable in GeneralContext.MyRoutes)
                if (variable == route)
                {
                    OrdinalRoute = route;
                    return;
                }

            throw new ArgumentException("Bad Route id");
        }

        public override string ToString()
        {
            return DriverId;
        }
    }
}