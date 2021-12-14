using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataClient.Models;

public class MyCurRoute
{
    [Key] public long RecordID { get; set; }

    public string TimeOfStops { get; set; }


    [StringLength(256)] public string NumberOfLeaving { get; set; }

    [StringLength(256)] public string NumberOfIncoming { get; set; }

    [Column(TypeName = "datetime2")] public DateTime Day { get; set; }

    [Required] [StringLength(450)] public string DriverId { get; set; }

    [Required] [StringLength(450)] public string RouteId { get; set; }

    public virtual MyDriver MyDriver { get; set; }

    public virtual MyRoute MyRoute { get; set; }
}