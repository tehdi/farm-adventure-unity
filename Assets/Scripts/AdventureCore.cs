using System.Collections.Generic;

namespace FarmAdventure
{
    public class AdventureCore
    {
        private static Player Player = new Player();

        public static int PlayerXLocation;
        public static int PlayerYLocation;
        public static int PlayerMoney { get { return Player.Money; } }
        public static int PlayerMilk { get { return Player.Milk; } }
        public static int PlayerCowFood { get { return Player.CowFood; } }

        public static Town CurrentTown { get; }

        // pass-throughs to Player values
        public static List<Quest> ActiveQuestsForOtherLocations { get { return Player.QuestsForDestinationsOtherThan(CurrentTown); } }
        public static List<Quest> CompletableQuestsForCurrentLocation { get { return Player.ActiveQuestsForDestination(CurrentTown); } }

        public static void UpdatePlayerLocation(int newX, int newY)
        {
            PlayerXLocation = newX;
            PlayerYLocation = newY;
        }

        public static bool CanAcceptQuest() =>
            CurrentTown != null && CurrentTown.Quest != null;

        public static bool CanCompleteQuest() =>
            CurrentTown != null && Player.HasQuestWithDestination(CurrentTown);

        public static bool CanEnterFarm() =>
            CurrentTown != null && CurrentTown.Home;
    }
}
