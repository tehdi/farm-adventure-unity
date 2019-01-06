// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace FarmAdventure
// {
//     public class FarmGame
//     {
//         public string FeedCows()
//         {
//             if (NumberOfCows <= 0)
//             {
//                 return "You don't have any cows to feed.";
//             }
//             if (BagsOfCowFood < NumberOfCows)
//             {
//                 return "You don't have enough cow food.";
//             }

//             BagsOfCowFood -= NumberOfCows;
//             LitresPerCowRemainingToBeMilked += MilkLitresPerFood;
//             return $"You fed your {(NumberOfCows == 1 ? "cow" : "cows")}";
//         }

//         public string MilkCows()
//         {
//             if (LitresPerCowRemainingToBeMilked <= 0)
//             {
//                 return $"Your {(NumberOfCows == 1 ? "cow is" : "cows are")} too hungry to be milked!";
//             }

//             LitresOfMilk += NumberOfCows * LitresPerCowRemainingToBeMilked;
//             LitresPerCowRemainingToBeMilked = 0;
//             return $"You milked your {(NumberOfCows == 1 ? "cow" : "cows")}.";
//         }

//         public string BorrowMoney(int borrowAmount, int repayAmount)
//         {
//             if (!CanBorrowMoney())
//             {
//                 return $"You already owe the bank {Debt}. They won't loan you any more until you pay it back.";
//             }

//             Debt += repayAmount;
//             Money += borrowAmount;
//             return $"You borrowed {borrowAmount} from the bank and have to pay back {repayAmount} before you can buy a new cow.";
//         }

//         public string PayDebt()
//         {
//             if (Debt <= 0)
//             {
//                 return "You don't owe any money.";
//             }
//             if (Money < Debt)
//             {
//                 return "You don't have enough money to repay your debt.";
//             }

//             Money -= Debt;
//             Debt = 0;
//             return "You repaid your debt. You're now able to buy more cows again.";
//         }

//         public string SellMilk()
//         {
//             if (LitresOfMilk <= 0)
//             {
//                 return "You don't have any milk to sell.";
//             }

//             Money += CalculateTotalMilkSellPrice();
//             LitresOfMilk = 0;
//             return "You sold all your milk.";
//         }

//         public string BuyCowFood(int foodToBuy)
//         {
//             if (Money < CowFoodBagBuyPrice * foodToBuy)
//             {
//                 return $"You don't have enough money to buy {foodToBuy} cow food.";
//             }

//             BagsOfCowFood += foodToBuy;
//             Money -= CowFoodBagBuyPrice * foodToBuy;
//             return $"You bought {foodToBuy} bag{(foodToBuy == 1 ? "" : "s")} of cow food.";
//         }

//         public string BuyCow()
//         {
//             if (Debt > 0)
//             {
//                 return $"You have to pay back the {Debt} you owe the bank before you can buy a new cow.";
//             }
//             if (Money < CalculateNextCowBuyPrice())
//             {
//                 return "You don't have enough money to buy a new cow.";
//             }

//             Money -= CalculateNextCowBuyPrice();
//             NumberOfCows++;
//             return "You bought a new cow!";
//         }

//         public string HireFarmManager()
//         {
//             if (!CanHireFarmManager())
//             {
//                 return "You can't hire a farm manager right now.";
//             }

//             FarmManagerHired = true;
//             return "You hired someone to manage your farm when you're away.";
//         }

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
