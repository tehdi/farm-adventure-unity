using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmAdventure
{
    public class QuestFactory
    {
        private static Random random = new Random();

        public static void LoadUpSomeQuests(List<Town> towns)
        {
            LoadUpSomeQuests(towns, towns);
        }

        public static void LoadUpSomeQuests(List<Town> origins, List<Town> potentialDestinations)
        {
            ValidateTownCombinations(origins, potentialDestinations);

            // towns only support one quest each at the moment
            foreach (var origin in origins)
            {
                // a quest isn't allowed to have its origin as its destination
                var validDestinationsForThisOrigin = new List<Town>(potentialDestinations);
                validDestinationsForThisOrigin.Remove(origin);
                Town destination = validDestinationsForThisOrigin.ElementAt(random.Next(validDestinationsForThisOrigin.Count));

                int rewardItemId = random.Next(3);
                PlayerInventory reward = new PlayerInventory();
                string rewardName;
                if (rewardItemId % 3 == 0)
                {
                    reward.Money = 3;
                    rewardName = "3 Money";
                }
                else if (rewardItemId % 3 == 1)
                {
                    reward.CowFood = 1;
                    rewardName = "1 Cow Food";
                }
                else
                {
                    reward.Milk = 2;
                    rewardName = "2 Milk";
                }
                Quest quest = new Quest(origin, destination)
                {
                    OfferText = $"I need you to go to {destination.Name}. You'll get {rewardName} when you get there.",
                    InProgressText = $"You've agreed to go to {destination.Name} in exchange for {rewardName}.",
                    CompletionText = $"You've arrived at {destination.Name}! Find your contact to receive {rewardName}.",
                    Reward = reward
                };
                origin.Quest = quest;
            }
        }

        private static void ValidateTownCombinations(List<Town> origins, List<Town> potentialDestinations)
        {
            if (origins.Count == 0 || potentialDestinations.Count == 0)
            {
                throw new Exception("Need at least one origin and one potential destination to create quests");
            }

            if (potentialDestinations.Count == 1 && origins.Contains(potentialDestinations.ElementAt(0)))
            {
                throw new Exception("Can't create a quest where origin matches destination");
            }
        }
    }
}
