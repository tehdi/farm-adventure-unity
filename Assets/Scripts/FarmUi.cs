using UnityEngine;
using UnityEngine.UI;

namespace FarmAdventure
{
    public class FarmUi : MonoBehaviour
    {
        public GameObject FeedMyCowsButton;
        public GameObject FeedMyCowsButtonText;
        public GameObject MilkMyCowsButton;
        public GameObject MilkMyCowsButtonText;
        public GameObject ActionLogText;

        public GameObject BorrowMoneyButton;
        public GameObject MoneyAmountText;
        public GameObject PayDebtButton;
        public GameObject DebtAmountText;

        public GameObject SellMilkButton;
        public GameObject MilkAmountText;
        public GameObject MilkSellPriceAmountText;

        public GameObject BuyCowFoodButton;
        public GameObject BuyCowFoodButtonText;
        public GameObject CowFoodAmountText;
        public GameObject CowFoodBuyPriceAmountText;
        public GameObject CowFoodBuyAmountSlider;

        public GameObject BuyACowButton;
        public GameObject CowAmountText;
        public GameObject BuyACowPriceAmountText;

        public GameObject FarmManagerText;
        public GameObject HireAFarmManagerButton;
        public GameObject GoOnAnAdventureButton;

        private FarmLogic FarmLogic = new FarmLogic();

        void Update()
        {
            FeedMyCowsButton.GetComponent<Button>().interactable = FarmCore.CanFeedCows();
            FeedMyCowsButtonText.GetComponent<Text>().text = $"Feed My Cow{(FarmCore.NumberOfCows == 1 ? "" : "s")}";
            MilkMyCowsButton.GetComponent<Button>().interactable = FarmCore.CanMilkCows();
            MilkMyCowsButtonText.GetComponent<Text>().text = $"Milk My Cow{(FarmCore.NumberOfCows == 1 ? "" : "s")}";

            BorrowMoneyButton.GetComponent<Button>().interactable = FarmCore.CanBorrowMoney();
            MoneyAmountText.GetComponent<Text>().text = $"{FarmCore.Money}";
            PayDebtButton.GetComponent<Button>().interactable = FarmCore.CanPayDebt();
            DebtAmountText.GetComponent<Text>().text = $"{FarmCore.Debt}";

            SellMilkButton.GetComponent<Button>().interactable = FarmCore.CanSellMilk();
            MilkAmountText.GetComponent<Text>().text = $"{FarmCore.LitresOfMilk}";
            MilkSellPriceAmountText.GetComponent<Text>().text = $"{FarmCore.CalculateTotalMilkSellPrice()}";

            CowFoodBuyAmountSlider.GetComponent<Slider>().interactable = FarmCore.CanBuyCowFood(1);
            int selectedCowFoodBuyAmount = Mathf.Max(1, (int)CowFoodBuyAmountSlider.GetComponent<Slider>().value);
            BuyCowFoodButton.GetComponent<Button>().interactable = FarmCore.CanBuyCowFood(selectedCowFoodBuyAmount);
            BuyCowFoodButtonText.GetComponent<Text>().text = $"Buy {selectedCowFoodBuyAmount} Cow Food";
            CowFoodAmountText.GetComponent<Text>().text = $"{FarmCore.BagsOfCowFood}";
            CowFoodBuyPriceAmountText.GetComponent<Text>().text = $"{selectedCowFoodBuyAmount * FarmCore.CowFoodBagBuyPrice}";

            BuyACowButton.GetComponent<Button>().interactable = FarmCore.CanBuyCow();
            CowAmountText.GetComponent<Text>().text = $"{FarmCore.NumberOfCows}";
            BuyACowPriceAmountText.GetComponent<Text>().text = $"{FarmCore.CalculateNextCowBuyPrice()}";

            FarmManagerText.SetActive(FarmCore.HasFarmManager());
            HireAFarmManagerButton.SetActive(!FarmCore.HasFarmManager());
            HireAFarmManagerButton.GetComponent<Button>().interactable = FarmCore.CanHireFarmManager();
            GoOnAnAdventureButton.SetActive(FarmCore.HasFarmManager());
            GoOnAnAdventureButton.GetComponent<Button>().interactable = FarmCore.CanGoOnAdventure();
        }

        public void FeedCows()
        {
            string message = FarmLogic.FeedCows();
            ShowMessage(message);
        }

        public void MilkCows()
        {
            string message = FarmLogic.MilkCows();
            ShowMessage(message);
        }

        public void BorrowMoney()
        {
            // TODO: confirmation prompt
            string message = FarmLogic.BorrowMoney(9, 10);
            ShowMessage(message);
        }

        public void PayDebt()
        {
            string message = FarmLogic.PayDebt();
            ShowMessage(message);
        }

        public void SellMilk()
        {
            string message = FarmLogic.SellMilk();
            ShowMessage(message);
        }

        public void BuyCowFood()
        {
            int selectedCowFoodBuyAmount = (int)CowFoodBuyAmountSlider.GetComponent<Slider>().value;
            string message = FarmLogic.BuyCowFood(selectedCowFoodBuyAmount);
            ShowMessage(message);
        }

        public void BuyACow()
        {
            string message = FarmLogic.BuyACow();
            ShowMessage(message);
        }

        public void HireAFarmManager()
        {
            string message = FarmLogic.HireAFarmManager();
            ShowMessage(message);
        }

        private void ShowMessage(string message)
        {
            ActionLogText.GetComponent<Text>().text = message;
        }
    }
}
