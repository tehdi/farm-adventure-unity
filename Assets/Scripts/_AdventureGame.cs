// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace FarmAdventure
// {
//     public class AdventureGame
//     {
//         private Player Player;
//         public List<Town> Towns { get; private set; }

//         public int PlayerXLocation { get { return Player.XLocation; } }
//         public int PlayerYLocation { get { return Player.YLocation; } }
//         public int PlayerMoney { get { return Player.Money; } }
//         public int PlayerCowFood { get { return Player.CowFood; } }
//         public int PlayerMilk { get { return Player.Milk; } }

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
//             // if moving would go out of bounds, then don't move
//             if (Player.YLocation + deltaY < 0)
//             {
//                 deltaY = 0;
//             }
//             if (Player.YLocation + deltaY > maxY)
//             {
//                 deltaY = 0;
//             }

//             if (Player.XLocation + deltaX < 0)
//             {
//                 deltaX = 0;
//             }
//             if (Player.XLocation + deltaX > maxX)
//             {
//                 deltaX = 0;
//             }

//             if (deltaX == 0 && deltaY == 0)
//             {
//                 return false;
//             }

//             Player.YLocation += deltaY;
//             Player.XLocation += deltaX;
//             PlayerMovesMadeSinceLastLeavingFarm++;
//             return true;
//         }

//         public void CompleteQuestsForDestination(Town town)
//         {
//             // before completing the quests, generate new quests in the about-to-be-completed quests' origins
//             var completableQuests = Player.ActiveQuestsForDestination(town);
//             var questOrigins = completableQuests.Select(q => q.Origin).ToList();
//             QuestFactory.LoadUpSomeQuests(questOrigins, Towns);

//             Player.CompleteQuestsForDestination(town);
//         }

//         public List<Quest> ActivePlayerQuestsForDestination(Town town)
//         {
//             return Player.ActiveQuestsForDestination(town);
//         }

//         public List<Quest> PlayerQuestsForDestinationsOtherThan(Town town)
//         {
//             return Player.QuestsForDestinationsOtherThan(town);
//         }

//         public bool PlayerHasQuestWithDestination(Town town)
//         {
//             return Player.HasQuestWithDestination(town);
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
