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
          //  RoutesList ??= new List<CurRoute>();
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



        public List<CurRoute> RoutesList { get; set; }




        public override string ToString()
        {
            return DriverId;
        }


    }
}