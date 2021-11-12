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
            MyPaths = new List<CurRoute>();
            foreach (var variable in GeneralContext.MyCurRoutes)
                if (variable.Driver.DriverId == DriverId)
                    MyPaths.Add(variable);
            DriverId = driverId;
            Name = name;
        }

        [Key][Required] public string DriverId
        { get;
            private set;
        }

        [Column(TypeName = "nvarchar(256)")][Required]
        public string Name { get; private set; }

        [Column(TypeName = "nvarchar(256)")] string? OrdinalRouteId { get; set; }

        [BackingField("OrdinalRouteId")]
        public Route? OrdinalRoute
        {
            get => GeneralContext.MyRoutes.Find(OrdinalRouteId);
            private set => OrdinalRouteId = value?.RouteId;
        }

        public List<CurRoute> MyPaths { get; set; }


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
                MyPaths.Add(new CurRoute(DriverId, OrdinalRoute.RouteId, DateTime.Today));
                GeneralContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Bad Call");
            }
        }


        public void SetPath(Route route)
        {
            foreach (var variable in GeneralContext.MyRoutes)
                if (variable == route)
                {
                    OrdinalRoute = route;
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