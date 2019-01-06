﻿// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace FarmAdventure
// {
//     public class Player
//     {
//         public int XLocation { get; set; }
//         public int YLocation { get; set; }

//         public PlayerInventory Inventory { get; private set; }
//         public int Money { get { return Inventory.Money; } }
//         public int CowFood { get { return Inventory.CowFood; } }
//         public int Milk { get { return Inventory.Milk; } }

//         public List<Quest> ActiveQuests { get; private set; }

//         public Player()
//         {
//             ActiveQuests = new List<Quest>();

//             Inventory = new PlayerInventory();
//         }

//         public bool HasQuestWithDestination(Town town) =>
//             ActiveQuests
//                 .Any(q => q.Destination == town);

//         public List<Quest> ActiveQuestsForDestination(Town town) =>
//             ActiveQuests
//                 .Where(q => q.Destination == town)
//                 .OrderBy(q => q.Origin.Name)
//                 .ToList();

//         public List<Quest> QuestsForDestinationsOtherThan(Town town) =>
//             ActiveQuests
//                 .Where(q => q.Destination != town)
//                 .OrderBy(q => q.Origin.Name)
//                 .ToList();

//         public void CompleteQuestsForDestination(Town town)
//         {
//             List<Quest> completableQuests = ActiveQuests.Where(q => q.Destination == town).ToList();
//             foreach (var quest in completableQuests)
//             {
//                 Inventory.Add(quest.Reward);
//                 ActiveQuests.Remove(quest);
//             }
//         }
//     }
// }
