using System;
using System.Collections.Generic;

namespace StatisticViewer.Models
{
    public partial class MyDriver
    {
        public MyDriver()
        {
            MyCurRoutes = new HashSet<MyCurRoute>();
        }

        public string DriverId { get;private set; } = null!;
        public string Name { get;private set; } = null!;


        public static MyDriver CreateInstance(string driverId, string name)
        {
            var ans = new MyDriver()
            {
                DriverId = driverId,
                Name = name
            };
            using (var db=new TgBotContext())
            {
                
            }
            return ans;
        }

        public virtual ICollection<MyCurRoute>? MyCurRoutes { get; set; }
    }
}
