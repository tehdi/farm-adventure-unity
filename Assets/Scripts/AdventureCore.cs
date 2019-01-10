using System;
using System.Collections.Generic;

namespace FarmAdventure
{
    public class AdventureCore
    {
        public static Player Player;
        public static Dictionary<Tuple<int, int>, Town> Towns;
        public static Town CurrentTown { get { return Towns.GetValueOrDefault(Player.Location); } }

        public static int PlayerXLocation { get { return Player.XLocation; } }
        public static int PlayerYLocation { get { return Player.YLocation; } }
        public static int PlayerMoney { get { return Player.Money; } }
        public static int PlayerMilk { get { return Player.Milk; } }
        public static int PlayerCowFood { get { return Player.CowFood; } }

        // pass-throughs to Player values
        public static List<Quest> ActiveQuestsForOtherLocations { get { return Player.QuestsForDestinationsOtherThan(CurrentTown); } }
        public static List<Quest> CompletableQuestsForCurrentLocation { get { return Player.ActiveQuestsForDestination(CurrentTown); } }

        public static bool CanAcceptQuest() =>
            CurrentTown != null && CurrentTown.Quest != null;

        public static bool CanCompleteQuest() =>
            CurrentTown != null && Player.HasQuestWithDestination(CurrentTown);

        public static bool CanEnterFarm() =>
            CurrentTown != null && CurrentTown.Home;
    }
}
