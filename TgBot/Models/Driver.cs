#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TgBot.Models
{
    public class Driver
    {
        public Driver(string driverId, string name)
        {
            DriverId = driverId;
            Name = name;

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

        public Route? OrdinalRoute { get; set; }

        public List<CurRoute> HistoryRoutes { get; protected set; } = new();



        public async void NewRoute(bool direction)
        {

            if (HistoryRoutes.Count == 0 || HistoryRoutes.Last().IsFinished())
            {
                await using (var db = new DriverContextFactory().CreateDbContext())
                {
                    db.Update(this);
                    HistoryRoutes.Add(new CurRoute(direction,DateTime.Now, OrdinalRoute,this));
                    await db.SaveChangesAsync();
                }
            }

            else
            {
                throw new ArgumentException("Bad Call");
            }

        }
        public async Task<bool> SetRoute(string routeId)
        {
            if (routeId == OrdinalRoute.RouteId) return true;
            await using (var db = new DriverContextFactory().CreateDbContext())
            {
                db.Update(this);
                var t = db.MyRoutes.Find(routeId);
                if (t == null) return false;
                OrdinalRoute = t;
                db.SaveChanges();

                return true;
            }
        }


        public override string ToString()
        {
            return DriverId;
        }


    }
}