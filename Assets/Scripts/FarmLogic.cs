using System;

namespace FarmAdventure
{
    public static class FarmLogic
    {
        public static string FeedCows()
        {
            if (FarmCore.NumberOfCows <= 0)
            {
                return "You don't have any cows to feed.";
            }
            if (FarmCore.BagsOfCowFood < FarmCore.NumberOfCows)
            {
                return "You don't have enough cow food.";
            }

            FarmCore.BagsOfCowFood -= FarmCore.NumberOfCows;
            FarmCore.LitresPerCowRemainingToBeMilked += FarmCore.MilkLitresPerFood;
            return $"You fed your {(FarmCore.NumberOfCows == 1 ? "cow" : "cows")}.";
        }

        public static string MilkCows()
        {
            if (FarmCore.LitresPerCowRemainingToBeMilked <= 0)
            {
                return $"Your {(FarmCore.NumberOfCows == 1 ? "cow is" : "cows are")} too hungry to be milked!";
            }

            FarmCore.LitresOfMilk += FarmCore.NumberOfCows * FarmCore.LitresPerCowRemainingToBeMilked;
            FarmCore.LitresPerCowRemainingToBeMilked = 0;
            return $"You milked your {(FarmCore.NumberOfCows == 1 ? "cow" : "cows")}.";
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
            if (FarmCore.LitresOfMilk <= 0)
            {
                return "You don't have any milk to sell.";
            }

            FarmCore.Money += FarmCore.CalculateTotalMilkSellPrice();
            FarmCore.LitresOfMilk = 0;
            return "You sold all your milk.";
        }

        public static string BuyCowFood(int foodToBuy)
        {
            if (FarmCore.Money < FarmCore.CowFoodBagBuyPrice * foodToBuy)
            {
                return $"You don't have enough money to buy {foodToBuy} cow food.";
            }

            FarmCore.BagsOfCowFood += foodToBuy;
            FarmCore.Money -= FarmCore.CowFoodBagBuyPrice * foodToBuy;
            return $"You bought {foodToBuy} bag{(foodToBuy == 1 ? "" : "s")} of cow food.";
        }
        
        public static void BuyOneFoodPerCow()
        {
            // if can't afford one food per cow, buy max affordable
            int foodToBuy = Math.Min(FarmCore.NumberOfCows, FarmCore.Money / FarmCore.CowFoodBagBuyPrice);
            FarmCore.BagsOfCowFood += foodToBuy;
            FarmCore.Money -= FarmCore.CowFoodBagBuyPrice * foodToBuy;
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
            FarmCore.NumberOfCows++;
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
