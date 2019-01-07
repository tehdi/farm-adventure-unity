namespace FarmAdventure
{
    public class FarmLogic
    {
        public string FeedCows()
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

        public string MilkCows()
        {
            if (FarmCore.LitresPerCowRemainingToBeMilked <= 0)
            {
                return $"Your {(FarmCore.NumberOfCows == 1 ? "cow is" : "cows are")} too hungry to be milked!";
            }

            FarmCore.LitresOfMilk += FarmCore.NumberOfCows * FarmCore.LitresPerCowRemainingToBeMilked;
            FarmCore.LitresPerCowRemainingToBeMilked = 0;
            return $"You milked your {(FarmCore.NumberOfCows == 1 ? "cow" : "cows")}.";
        }

        public string BorrowMoney(int borrowAmount, int repayAmount)
        {
            if (!FarmCore.CanBorrowMoney())
            {
                return $"You already owe the bank {FarmCore.Debt}. They won't loan you any more until you pay it back.";
            }

            FarmCore.Debt += repayAmount;
            FarmCore.Money += borrowAmount;
            return $"You borrowed {borrowAmount} from the bank and have to pay back {repayAmount} before you can buy a new cow.";
        }

        public string PayDebt()
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

        public string SellMilk()
        {
            if (FarmCore.LitresOfMilk <= 0)
            {
                return "You don't have any milk to sell.";
            }

            FarmCore.Money += FarmCore.CalculateTotalMilkSellPrice();
            FarmCore.LitresOfMilk = 0;
            return "You sold all your milk.";
        }

        public string BuyCowFood(int foodToBuy)
        {
            if (FarmCore.Money < FarmCore.CowFoodBagBuyPrice * foodToBuy)
            {
                return $"You don't have enough money to buy {foodToBuy} cow food.";
            }

            FarmCore.BagsOfCowFood += foodToBuy;
            FarmCore.Money -= FarmCore.CowFoodBagBuyPrice * foodToBuy;
            return $"You bought {foodToBuy} bag{(foodToBuy == 1 ? "" : "s")} of cow food.";
        }

        public string BuyACow()
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

        public string HireAFarmManager()
        {
            if (!FarmCore.CanHireFarmManager())
            {
                return "You can't hire a farm manager right now.";
            }

            FarmCore.FarmManagerHired = true;
            return "You hired someone to manage your farm when you're away.";
        }
    }
}
