using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TgBot.Program;

namespace TgBot.Models
{
    public class Path
    {
        public List<CurPath> HistoryOfPath;

        public Path(string pathId, string stops, byte numberOfStops)
        {
            PathId = pathId;
            Stops = stops;
            NumberOfStops = numberOfStops;
            HistoryOfPath = new List<CurPath>();
            foreach (var variable in GeneralContext.CurPaths)
                if (variable.Path.PathId == pathId)
                    HistoryOfPath.Add(variable);
        }

        [Key] public string PathId { get; }

        [Column(TypeName = "nvarchar(max)")] public string Stops { get; }

        [Column(TypeName = "tinyint")] public byte NumberOfStops { get; }
    }
}