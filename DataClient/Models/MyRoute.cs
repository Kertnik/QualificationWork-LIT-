using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataClient.Models;

public class MyRoute
{
    [Key] [StringLength(450)] public string RouteId { get; set; }

    [Required] public string Stops { get; set; }

    public virtual List<MyCurRoute> MyCurRoutes { get; set; }
}