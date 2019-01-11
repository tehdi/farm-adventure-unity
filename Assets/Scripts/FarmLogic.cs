using System;

namespace FarmAdventure
{
    public static class FarmLogic
    {
        public static void StartNewGame()
        {
            FarmCore.FirstLoad = true;
            FarmCore.Money = 0;
            FarmCore.Debt = 0;
            FarmCore.CowAmount = 1;
            FarmCore.CowFoodAmount = 1;
            FarmCore.MilkAmount = 0;
            FarmCore.MilkUnitsPerCowRemainingToBeMilked = 0;
            FarmCore.FarmManagerHired = false;
        }

        public static string FeedCows()
        {
            if (FarmCore.CowAmount <= 0)
            {
                return "You don't have any cows to feed.";
            }
            if (FarmCore.CowFoodAmount < FarmCore.CowAmount)
            {
                return "You don't have enough cow food.";
            }

            FarmCore.CowFoodAmount -= FarmCore.CowAmount;
            FarmCore.MilkUnitsPerCowRemainingToBeMilked += FarmCore.MilkUnitsPerFood;
            return $"You fed your {(FarmCore.CowAmount == 1 ? "cow" : "cows")}.";
        }

        public static string MilkCows()
        {
            if (FarmCore.MilkUnitsPerCowRemainingToBeMilked <= 0)
            {
                return $"Your {(FarmCore.CowAmount == 1 ? "cow is" : "cows are")} too hungry to be milked!";
            }

            FarmCore.MilkAmount += FarmCore.CowAmount * FarmCore.MilkUnitsPerCowRemainingToBeMilked;
            FarmCore.MilkUnitsPerCowRemainingToBeMilked = 0;
            return $"You milked your {(FarmCore.CowAmount == 1 ? "cow" : "cows")}.";
        }

        public static string BorrowMoney(int borrowAmount, int repayAmount)
        {
            if (!FarmCore.CanBorrowMoney())
            {
                return $"You already owe the bank {FarmCore.Debt}. They won't loan you any more until you pay it back.";
            }

            FarmCore.Debt += repayAmount;
            FarmCore.Money += borrowAmount;
            return $"You borrowed {borrowAmount} from the bank and have to pay back {repayAmount} before you can buy a new cow.";
        }

        public static string PayDebt()
        {
            if (FarmCore.Debt <= 0)
            {
                return "You don't owe any money.";
            }
            if (FarmCore.Money < FarmCore.Debt)
            {
                return "You don't have enough money to repay your debt.";
            }

            FarmCore.Money -= FarmCore.Debt;
            FarmCore.Debt = 0;
            return "You repaid your debt. You're now able to buy more cows again.";
        }

        public static string SellMilk()
        {
            if (FarmCore.MilkAmount <= 0)
            {
                return "You don't have any milk to sell.";
            }

            FarmCore.Money += FarmCore.CalculateTotalMilkSellPrice();
            FarmCore.MilkAmount = 0;
            return "You sold all your milk.";
        }

        public static string BuyCowFood(int foodToBuy)
        {
            if (FarmCore.Money < FarmCore.CowFoodUnitBuyPrice * foodToBuy)
            {
                return $"You don't have enough money to buy {foodToBuy} cow food.";
            }

            FarmCore.CowFoodAmount += foodToBuy;
            FarmCore.Money -= FarmCore.CowFoodUnitBuyPrice * foodToBuy;
            return $"You bought {foodToBuy} bag{(foodToBuy == 1 ? "" : "s")} of cow food.";
        }
        
        public static void BuyOneFoodPerCow()
        {
            // if can't afford one food per cow, buy max affordable
            int foodToBuy = Math.Min(FarmCore.CowAmount, FarmCore.Money / FarmCore.CowFoodUnitBuyPrice);
            FarmCore.CowFoodAmount += foodToBuy;
            FarmCore.Money -= FarmCore.CowFoodUnitBuyPrice * foodToBuy;
        }

        public static string BuyACow()
        {
            if (FarmCore.Debt > 0)
            {
                return $"You have to pay back the {FarmCore.Debt} you owe the bank before you can buy a new cow.";
            }
            if (FarmCore.Money < FarmCore.CalculateNextCowBuyPrice())
            {
                return "You don't have enough money to buy a new cow.";
            }

            FarmCore.Money -= FarmCore.CalculateNextCowBuyPrice();
            FarmCore.CowAmount++;
            return "You bought a new cow!";
        }

        public static string HireAFarmManager()
        {
            if (!FarmCore.CanHireFarmManager())
            {
                return "You can't hire a farm manager right now.";
            }

            FarmCore.FarmManagerHired = true;
            return "You hired someone to manage your farm when you're away.";
        }

        public static bool PayManagerSalary()
        {
            FarmCore.Money -= FarmCore.FarmManagerSalary;

            if (FarmCore.Money < 0)
            {
                // you couldn't afford to pay your manager,
                // so the manager secured a lien against your farm.
                int moneyToBorrow = -FarmCore.Money;
                ForceBorrowMoney(moneyToBorrow, moneyToBorrow + 1);
                return false;
            }

            return true;
        }

        private static void ForceBorrowMoney(int borrowAmount, int repayAmount)
        {
            // the farm manager needs to get paid even if it means taking out another loan
            FarmCore.Debt += repayAmount;
            FarmCore.Money += borrowAmount;
        }
    }
}
