using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmCore : MonoBehaviour
{
        // per cow:
        //  2 litres milk per food
        //      * 2 money per litre milk = GROSS 4 money per feed-milk cycle
        //      - 3 money per food = NET 1 money per feed-milk cycle
        public static readonly int CowFoodBagBuyPrice = 3;
        private static readonly int CowBuyPriceMultiplier = 10;
        private static readonly int MilkLitreSellPrice = 2;
        public static readonly int MilkLitresPerFood = 2;
        private static readonly int FarmManagerSalary = 1;

        public static int Money = 0;
        public static int Debt = 0;

        public static int NumberOfCows = 1;
        public static int BagsOfCowFood = 1;

        public static int LitresOfMilk = 0;
        public static int LitresPerCowRemainingToBeMilked = 0;

        public static bool FarmManagerHired = false;

        public static int CalculateTotalMilkSellPrice() =>
            LitresOfMilk * MilkLitreSellPrice;

        public static int CalculateNextCowBuyPrice() =>
            NumberOfCows * CowBuyPriceMultiplier;

        public static bool CanBuyCowFood(int amountOfFood) =>
            Money >= CowFoodBagBuyPrice * amountOfFood;

        public static bool CanBuyEnoughFoodToFeedAllCows()
        {
            int foodShortage = NumberOfCows - BagsOfCowFood; // this can be negative. it doesn't matter
            int moneyRequiredToTopUp = foodShortage * CowFoodBagBuyPrice;
            return Money >= moneyRequiredToTopUp;
        }

        public static bool CanFeedCows() =>
            BagsOfCowFood >= NumberOfCows;

        public static bool CanMilkCows() =>
            LitresPerCowRemainingToBeMilked > 0;

        public static bool CanSellMilk() =>
            LitresOfMilk > 0;

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
                && NumberOfCows >= 2;

        public static bool CanGoOnAdventure() =>
            FarmManagerHired;

        public static bool HasFarmManager() =>
            FarmManagerHired;
}
