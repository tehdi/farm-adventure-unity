// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace FarmAdventure
// {
//     public class FarmGame
//     {
//         public void StartAdventure()
//         {
//             GameManager.StartAdventure();
//         }

//         public void BuyOneFoodPerCow()
//         {
//             // if can't afford one food per cow, buy max affordable
//             int foodToBuy = Math.Min(NumberOfCows, Money / CowFoodBagBuyPrice);
//             BagsOfCowFood += foodToBuy;
//             Money -= CowFoodBagBuyPrice * foodToBuy;
//         }

//         public bool PayManagerSalary()
//         {
//             Money -= FarmManagerSalary;

//             if (Money < 0)
//             {
//                 // you couldn't afford to pay your manager,
//                 // so the manager secured a lien against your farm.
//                 int moneyToBorrow = -Money;
//                 ForceBorrowMoney(moneyToBorrow, moneyToBorrow + 1);
//                 return false;
//             }

//             return true;
//         }

//         private void ForceBorrowMoney(int borrowAmount, int repayAmount)
//         {
//             // the farm manager needs to get paid even if it means taking out another loan
//             Debt += repayAmount;
//             Money += borrowAmount;
//         }

//         public void DepositAdventureGains(PlayerInventory playerInventory)
//         {
//             Money += playerInventory.Money;
//             BagsOfCowFood += playerInventory.CowFood;
//             LitresOfMilk += playerInventory.Milk;

//             playerInventory.Clear();
//         }
//     }
// }
