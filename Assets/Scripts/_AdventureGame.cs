// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace FarmAdventure
// {
//     public class AdventureGame
//     {
//         public List<Town> Towns { get; private set; }
//         private int PlayerMovesMadeSinceLastLeavingFarm = 0;

//         private Random random = new Random();

//         public void StartNewGame(int maxX, int maxY, int minTowns, int maxTowns)
//         {
//             CreateTowns(maxX, maxY, minTowns, maxTowns);
//             InitializePlayer();

//             QuestFactory.LoadUpSomeQuests(Towns);
//         }

//         public List<Town> CreateTowns(int maxX, int maxY, int minTowns, int maxTowns)
//         {
//             int townChance = maxX * maxY / minTowns;

//             do
//             {
//                 Towns = new List<Town>();
//                 for (int y = 0; y < maxY; y++)
//                 {
//                     for (int x = 0; x < maxX; x++)
//                     {
//                         if (random.Next() % townChance == 0)
//                         {
//                             Town town = TownFactory.CreateTown(x, y);
//                             Towns.Add(town);
//                         }
//                     }
//                 }
//             } while (Towns.Count < minTowns || Towns.Count > maxTowns);

//             return Towns;
//         }

//         public void InitializePlayer()
//         {
//             Town startingTown = Towns.ElementAt(random.Next(0, Towns.Count));
//             Player = new Player() {
//                 XLocation = startingTown.XLocation,
//                 YLocation = startingTown.YLocation
//             };

//             startingTown.MakeHome();
//         }

//         public bool MovePlayer(int deltaX, int deltaY, int maxX, int maxY)
//         {
//             PlayerMovesMadeSinceLastLeavingFarm++;
//         }

//         public void CompleteQuestsForDestination(Town town)
//         {
//             // before completing the quests, generate new quests in the about-to-be-completed quests' origins
//             var completableQuests = Player.ActiveQuestsForDestination(town);
//             var questOrigins = completableQuests.Select(q => q.Origin).ToList();
//             QuestFactory.LoadUpSomeQuests(questOrigins, Towns);

//             Player.CompleteQuestsForDestination(town);
//         }

//         public void AddActivePlayerQuest(Quest quest)
//         {
//             Player.ActiveQuests.Add(quest);
//         }

//         public void ReturnToFarm()
//         {
//             GameManager.ReturnToFarm(PlayerMovesMadeSinceLastLeavingFarm, Player.Inventory);
//             PlayerMovesMadeSinceLastLeavingFarm = 0;
//         }
//     }
// }
