using System;
using System.Linq;
using System.Collections.Generic;

namespace FarmAdventure
{
    public class AdventureLogic
    {
        private Town CurrentTown { get { return AdventureCore.CurrentTown; } }
        private Random Random = new Random();

        public void InitializeNewGame(int minX, int maxX, int minY, int maxY, int scale, int minTowns, int maxTowns)
        {
            InitializeTowns(minX, maxX, minY, maxY, scale, minTowns, maxTowns);
            InitializePlayer();
            QuestFactory.LoadUpSomeQuests(AdventureCore.Towns.Values.ToList());
        }

        public void MovePlayerTo(int xLocation, int yLocation)
        {
            AdventureCore.Player.XLocation = xLocation;
            AdventureCore.Player.YLocation = yLocation;
        }

        public void AcceptCurrentTownQuest()
        {
            if (CurrentTown != null)
            {
                Quest quest = CurrentTown.Quest;
                AdventureCore.Player.AcceptQuest(quest);
                CurrentTown.Quest = null;
            }
        }

        public void CompleteQuestsForCurrentTown()
        {
            if (CurrentTown != null)
            {
                // generate new quests originating on the towns that originated the quests we're about to complete
                // this makes sure we don't run out of quests
                var completableQuests = AdventureCore.Player.ActiveQuestsForDestination(CurrentTown);
                var questOrigins = completableQuests.Select(q => q.Origin).ToList();
                QuestFactory.LoadUpSomeQuests(questOrigins, AdventureCore.Towns.Values.ToList());

                AdventureCore.Player.CompleteQuests(completableQuests);
            }
        }

        // town locations are unscaled, but cleanly divisible by the scale.
        // if your map is 480x352 with a 32p tile size (scale), then you could have a town that says it's at 200-whatever px.
        private void InitializeTowns(int minX, int maxX, int minY, int maxY, int scale, int minTowns, int maxTowns)
        {
            int xRange = (maxX - minX) / scale;
            int yRange = (maxY - minY) / scale;
            int townChance = xRange * yRange / minTowns;

            do
            {
                AdventureCore.Towns = new Dictionary<Tuple<int, int>, Town>();
                for (int y = minY; y < maxY; y += scale)
                {
                    for (int x = minX; x < maxX; x += scale)
                    {
                        if (Random.Next() % townChance == 0)
                        {
                            Town town = TownFactory.CreateTown(x, y);
                            var location = new Tuple<int, int>(x, y);
                            AdventureCore.Towns[location] = town;
                        }
                    }
                }
            } while (AdventureCore.Towns.Count < minTowns || AdventureCore.Towns.Count > maxTowns);
        }

        private void InitializePlayer()
        {
            var towns = AdventureCore.Towns;
            Town startingTown = towns.Values.ToList().ElementAt(Random.Next(0, towns.Count));
            AdventureCore.Player = new Player(startingTown.XLocation, startingTown.YLocation);
            startingTown.MakeHome();
        }
    }
}
