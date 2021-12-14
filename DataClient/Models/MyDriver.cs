using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataClient.Models;

public class MyDriver
{
    [Key] [StringLength(450)] public string DriverId { get; set; }

    [Required] [StringLength(256)] public string Name { get; set; }

    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual List<MyCurRoute> MyCurRoutes { get; set; }


    public override string ToString()
    {
        return $"{Name} ({DriverId})";
    }
}