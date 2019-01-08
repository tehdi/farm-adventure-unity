using System.Collections.Generic;

namespace FarmAdventure
{
    public class Town
    {
        public string Name { get; set; }
        public bool Home { get; set; }
        public int XLocation { get; }
        public int YLocation { get; }

        public Quest Quest { get; set; }

        public Town(string name, int xLocation, int yLocation)
        {
            Name = name;
            XLocation = xLocation;
            YLocation = yLocation;
        }

        public void MakeHome()
        {
            Name += " (Home)";
            Home = true;
        }
    }
}
