using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAdventure
{
    public class Quest
    {
        public string OfferText { get; set; }
        public string InProgressText { get; set; }
        public string CompletionText { get; set; }

        public PlayerInventory Reward { get; set; }

        public Town Origin { get; private set; }
        public Town Destination { get; private set; }

        public Quest(Town origin, Town destination)
        {
            Origin = origin;
            Destination = destination;
        }
    }
}
