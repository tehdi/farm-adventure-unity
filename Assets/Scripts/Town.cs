using System.Collections.Generic;

namespace FarmAdventure
{
    public class Town
    {
        public string Name { get; set; }
        public bool Home { get; set; }

        public Quest Quest { get; set; }

        public Town(string name)
        {
            Name = name;
        }

        public void MakeHome()
        {
            Name += " (Home)";
            Home = true;
        }
    }
}
