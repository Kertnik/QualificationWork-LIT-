using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TgBot.Models
{
    public class Route
    {
        public List<CurRoute> RoutesHistory { get; private set; }
        [Key] public string RouteId { get; private set; }
        [Column(TypeName = "nvarchar(max)")] public string Stops { get; private set; }

    }
}