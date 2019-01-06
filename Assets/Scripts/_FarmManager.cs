// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace FarmAdventure
// {
//     public class FarmManager
//     {
//         public static void Farm(int movesMade)
//         {
//             FarmGame farmGame = GameManager.FarmGame;

//             // to avoid interrupting the farm manager's cycle,
//             // do everything then count it as 4 moves.
//             // this means you can return after only 3 moves away, and still get a full 4-stage cycle.
//             // oh well. better than trying to guess the best place for the manager to start every time
//             for (int i = 0; i < movesMade; i += 4)
//             {
//                 if (!farmGame.CanMilkCows()
//                     && !farmGame.CanSellMilk()
//                     && !farmGame.CanFeedCows()
//                     && !farmGame.CanBuyEnoughFoodToFeedAllCows())
//                 {
//                     //  you shouldn't have left your farm, because the manager can't do anything
//                     //  but lucky you it'll just sit idle instead of paying the manager's salary into the negatives
//                     break;
//                 }

//                 farmGame.BuyOneFoodPerCow();
//                 farmGame.FeedCows();
//                 farmGame.MilkCows();
//                 farmGame.SellMilk();
//                 if (!farmGame.PayManagerSalary())
//                 {
//                     // I'm not entirely sure how you'd get here, or even if you can after the guard above,
//                     // but if your manager can't get paid then your farm will sit idle
//                     break;
//                 }
//             }
//         }

//         public static void DepositAdventureGains(PlayerInventory inventory)
//         {
//             FarmGame farmGame = GameManager.FarmGame;
//             farmGame.DepositAdventureGains(inventory);
//         }
//     }
// }
