using System.Collections.Generic;

namespace FarmAdventure
{
    public class AdventureCore
    {
        private static Player Player = new Player();

        public static int PlayerMoney { get { return Player.Money; } }
        public static int PlayerMilk { get { return Player.Milk; } }
        public static int PlayerCowFood { get { return Player.CowFood; } }

        public static Town CurrentTown { get; }

        // pass-throughs to Player values
        public static List<Quest> ActiveQuestsForOtherLocations { get { return Player.QuestsForDestinationsOtherThan(CurrentTown); } }
        public static List<Quest> CompletableQuestsForCurrentLocation { get { return Player.ActiveQuestsForDestination(CurrentTown); } }

        public static bool CanMoveNorth() =>
            true;

        public static bool CanMoveWest() =>
            true;

        public static bool CanMoveEast() =>
            true;

        public static bool CanMoveSouth() =>
            true;

        public static bool CanAcceptQuest() =>
            CurrentTown != null && CurrentTown.Quest != null;

        public static bool CanCompleteQuest() =>
            CurrentTown != null && Player.HasQuestWithDestination(CurrentTown);

        public static bool CanEnterFarm() =>
            CurrentTown != null && CurrentTown.Home;
    }
}
