using System.Collections.Generic;
using System;
using System.Linq;

namespace FarmAdventure
{
    public class AdventureCore
    {
        private static Player Player;
        public static Dictionary<Tuple<int, int>, Town> Towns;
        public static Town CurrentTown { get { return Towns.GetValueOrDefault(Player.Location); } }

        public static int PlayerXLocation { get { return Player.XLocation; } }
        public static int PlayerYLocation { get { return Player.YLocation; } }
        public static int PlayerMoney { get { return Player.Money; } }
        public static int PlayerMilk { get { return Player.Milk; } }
        public static int PlayerCowFood { get { return Player.CowFood; } }

        private static Random Random = new Random();

        // pass-throughs to Player values
        public static List<Quest> ActiveQuestsForOtherLocations { get { return Player.QuestsForDestinationsOtherThan(CurrentTown); } }
        public static List<Quest> CompletableQuestsForCurrentLocation { get { return Player.ActiveQuestsForDestination(CurrentTown); } }

        public static void InitializeNewGame(int minX, int maxX, int minY, int maxY, int scale, int minTowns, int maxTowns)
        {
            InitializeTowns(minX, maxX, minY, maxY, scale, minTowns, maxTowns);
            InitializePlayer();
            QuestFactory.LoadUpSomeQuests(Towns.Values.ToList());
        }

        // town locations are unscaled, but cleanly divisible by the scale.
        // if your map is 480x352 with a 32p tile size (scale), then you could have a town that says it's at 200-whatever px.
        private static void InitializeTowns(int minX, int maxX, int minY, int maxY, int scale, int minTowns, int maxTowns)
        {
            int xRange = (maxX - minX) / scale;
            int yRange = (maxY - minY) / scale;
            int townChance = xRange * yRange / minTowns;

            do
            {
                Towns = new Dictionary<Tuple<int, int>, Town>();
                for (int y = minY; y < maxY; y += scale)
                {
                    for (int x = minX; x < maxX; x += scale)
                    {
                        if (Random.Next() % townChance == 0)
                        {
                            Town town = TownFactory.CreateTown(x, y);
                            Towns.Add(new Tuple<int, int>(x, y), town);
                        }
                    }
                }
            } while (Towns.Count < minTowns || Towns.Count > maxTowns);
        }

        private static void InitializePlayer()
        {
            Town startingTown = Towns.Values.ToList().ElementAt(Random.Next(0, Towns.Count));
            Player = new Player(startingTown.XLocation, startingTown.YLocation);
            startingTown.MakeHome();
        }

        public static void MovePlayerTo(int xLocation, int yLocation)
        {
            Player.XLocation = xLocation;
            Player.YLocation = yLocation;
        }

        public static bool CanAcceptQuest() =>
            CurrentTown != null && CurrentTown.Quest != null;

        public static bool CanCompleteQuest() =>
            CurrentTown != null && Player.HasQuestWithDestination(CurrentTown);

        public static bool CanEnterFarm() =>
            CurrentTown != null && CurrentTown.Home;
    }
}
