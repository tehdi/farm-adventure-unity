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
