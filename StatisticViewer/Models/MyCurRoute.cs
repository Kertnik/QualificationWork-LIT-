using System;
using System.Collections.Generic;

namespace StatisticViewer.Models
{
    public partial class MyCurRoute
    {
        public long RecordId { get; set; }
        public string? TimeOfStops { get; set; }
        public bool? IsFromFirstStop { get; set; }
        public string? NumberOfLeaving { get; set; }
        public string? NumberOfIncoming { get; set; }
        public DateTime Day { get; set; }
        public string DriverId { get; set; } = null!;
        public string RouteId { get; set; } = null!;

        public virtual MyDriver Driver { get; set; } = null!;
        public virtual MyRoute Route { get; set; } = null!;
    }
}
