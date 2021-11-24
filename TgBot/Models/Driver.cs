#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TgBot.Models
{
    public class Driver
    {
        public Driver(string driverId, string name)
        {
            DriverId=driverId;
            Name=name;

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

        public List<CurRoute> HistoryRoutes { get; protected set; }



        public async void NewRoute()
        {
            
                if (HistoryRoutes.Count==0||HistoryRoutes.Last().IsFinished())
                {
                    using (var db = new DriverContextFactory().CreateDbContext())
                    {
                        db.Update(this);
                        db.UpdateRange(HistoryRoutes);
                        HistoryRoutes.Add(new CurRoute(this, DateTime.Today, OrdinalRoute));
                        await db.SaveChangesAsync();
                    }
                }

                else
                {
                    throw new ArgumentException("Bad Call");
                }
            
        }
        public async void SetRoute(string routeId)
        {
            using (var db = new DriverContextFactory().CreateDbContext())
            {
                db.Update<Driver>(this);
                OrdinalRoute=db.MyRoutes.Find(routeId)??throw new ArgumentNullException();

               // db.MyDrivers.Find(this.DriverId).OrdinalRoute=OrdinalRoute;
                await db.SaveChangesAsync();
            }
        }



        public override string ToString()
        {
            return DriverId;
        }


    }
}