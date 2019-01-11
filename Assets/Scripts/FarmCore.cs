namespace FarmAdventure
{
    public class FarmCore
    {
        public static bool FirstLoad = true;
        
        // per cow:
        //  2 milk per food
        //      * 2 money per milk = GROSS 4 money per feed-milk cycle
        //      - 3 money per food = NET 1 money per feed-milk cycle
        public static readonly int CowFoodUnitBuyPrice = 3;
        private static readonly int CowBuyPriceMultiplier = 10;
        public static readonly int MilkUnitSellPrice = 2;
        public static readonly int MilkUnitsPerFood = 2;
        public static readonly int FarmManagerSalary = 1;

        public static int Money = 0;
        public static int Debt = 0;

        public static int CowAmount = 1;
        public static int CowFoodAmount = 1;

        public static int MilkAmount = 0;
        public static int MilkUnitsPerCowRemainingToBeMilked = 0;

        public static bool FarmManagerHired = false;

        public static int CalculateTotalMilkSellPrice() =>
            MilkAmount * MilkUnitSellPrice;

        public static int CalculateCowFoodBuyPrice(int cowFoodAmount) =>
            cowFoodAmount * CowFoodUnitBuyPrice;

        public static int CalculateNextCowBuyPrice() =>
            CowAmount * CowBuyPriceMultiplier;

        public static bool CanBuyCowFood(int amountOfFood) =>
            Money >= CowFoodUnitBuyPrice * amountOfFood;

        public static bool CanBuyEnoughFoodToFeedAllCows()
        {
            int foodShortage = CowAmount - CowFoodAmount; // this can be negative. it doesn't matter
            int moneyRequiredToTopUp = foodShortage * CowFoodUnitBuyPrice;
            return Money >= moneyRequiredToTopUp;
        }

        public static bool CanFeedCows() =>
            CowFoodAmount >= CowAmount;

        public static bool CanMilkCows() =>
            MilkUnitsPerCowRemainingToBeMilked > 0;

        public static bool CanSellMilk() =>
            MilkAmount > 0;

        public static bool CanBuyCow() =>
            Money >= CalculateNextCowBuyPrice()
                && Debt <= 0;

        public static bool CanBorrowMoney() =>
            Debt <= 0;

        public static bool CanPayDebt() =>
            Debt > 0
                && Money >= Debt;

        public static bool CanHireFarmManager() =>
            !FarmManagerHired
                && CowAmount >= 2;

        public static bool CanGoOnAdventure() =>
            FarmManagerHired;

        public static bool HasFarmManager() =>
            FarmManagerHired;
    }
}
