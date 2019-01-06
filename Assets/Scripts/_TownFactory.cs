using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAdventure
{
    public class TownFactory
    {
        private static Random random = new Random();

        private static readonly string[] TownNameStart = {
            "Silver", "Old", "New", "Errant", "Oak",
            "North", "East", "South", "West"
        };

        private static readonly string[] TownNameEnd = {
            "Shire", "Grove", "Bluff", "Meadow", "Creek",
            "River", "Mountain", "Lake", "Castle", "Thicket",
            "Refuge", "Lodge", "Hall", "Moor", "Barrow"
        };

        public static Town CreateTown(int xLocation, int yLocation)
        {
            return new Town(GenerateTownName(), xLocation, yLocation);
        }

        private static string GenerateTownName()
        {
            var start = TownNameStart[random.Next(TownNameStart.Length)];
            var end = TownNameEnd[random.Next(TownNameEnd.Length)];
            return $"{start} {end}";
        }
    }
}
