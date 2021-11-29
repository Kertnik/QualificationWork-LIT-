using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TgBot.Models
{
    public class Route
    {
        public Route(string routeId, string stops, byte numberOfStops)
        {
            RouteId = routeId;
            Stops = stops;
            NumberOfStops = numberOfStops;

        }

        [Key] public string RouteId { get; private set; }
        [Column(TypeName = "nvarchar(max)")] public string Stops { get; private set; }

        [Column(TypeName = "tinyint")] public byte NumberOfStops { get; private set; }
    }
}