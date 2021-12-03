using System;
using System.Collections.Generic;

namespace StatisticViewer.Models
{
    public partial class MyRoute
    {
        public MyRoute()
        {
            MyCurRoutes = new HashSet<MyCurRoute>();
        }

        public string RouteId { get; set; } = null!;
        public string Stops { get; set; } = null!;

        public virtual ICollection<MyCurRoute> MyCurRoutes { get; set; }
    }
}
