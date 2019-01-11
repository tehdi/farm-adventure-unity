using UnityEngine;
using UnityEngine.UI;

namespace FarmAdventure
{
    public class FarmUi : MonoBehaviour
    {
        public Button FeedMyCowsButton;
        public Text FeedMyCowsButtonText;
        public Button MilkMyCowsButton;
        public Text MilkMyCowsButtonText;
        public Text ActionLogText;

        public Button BorrowMoneyButton;
        public Text MoneyAmountText;
        public Button PayDebtButton;
        public Text DebtAmountText;

        public Button SellMilkButton;
        public Text MilkAmountText;
        public Text MilkSellPriceAmountText;

        public Button BuyCowFoodButton;
        public Text BuyCowFoodButtonText;
        public Text CowFoodAmountText;
        public Text CowFoodBuyPriceAmountText;
        public Slider CowFoodBuyAmountSlider;

        public Button BuyACowButton;
        public Text CowAmountText;
        public Text BuyACowPriceAmountText;

        public Text FarmManagerText;
        public Button HireAFarmManagerButton;
        public Button GoOnAnAdventureButton;

        void Start()
        {
            SceneSwapper.ActiveScene = SceneSwapper.FARM_SCENE_INDEX;
            
            if (!FarmCore.FirstLoad)
            {
                // you've come back from an adventure!
                ShowMessage("Welcome back!");
            }

            FarmCore.FirstLoad = false;
        }

        void Update()
        {
            FeedMyCowsButton.interactable = FarmCore.CanFeedCows();
            FeedMyCowsButtonText.text = $"Feed My Cow{(FarmCore.CowAmount == 1 ? "" : "s")}";
            MilkMyCowsButton.interactable = FarmCore.CanMilkCows();
            MilkMyCowsButtonText.text = $"Milk My Cow{(FarmCore.CowAmount == 1 ? "" : "s")}";

            BorrowMoneyButton.interactable = FarmCore.CanBorrowMoney();
            MoneyAmountText.text = $"{FarmCore.Money}";
            PayDebtButton.interactable = FarmCore.CanPayDebt();
            DebtAmountText.text = $"{FarmCore.Debt}";

            SellMilkButton.interactable = FarmCore.CanSellMilk();
            MilkAmountText.text = $"{FarmCore.MilkAmount}";
            MilkSellPriceAmountText.text = $"{FarmCore.CalculateTotalMilkSellPrice()}";

            CowFoodBuyAmountSlider.interactable = FarmCore.CanBuyCowFood(1);
            int selectedCowFoodBuyAmount = Mathf.Max(1, (int)CowFoodBuyAmountSlider.value);
            BuyCowFoodButton.interactable = FarmCore.CanBuyCowFood(selectedCowFoodBuyAmount);
            BuyCowFoodButtonText.text = $"Buy {selectedCowFoodBuyAmount} Cow Food";
            CowFoodAmountText.text = $"{FarmCore.CowFoodAmount}";
            CowFoodBuyPriceAmountText.text = $"{FarmCore.CalculateCowFoodBuyPrice(selectedCowFoodBuyAmount)}";

            BuyACowButton.interactable = FarmCore.CanBuyCow();
            CowAmountText.text = $"{FarmCore.CowAmount}";
            BuyACowPriceAmountText.text = $"{FarmCore.CalculateNextCowBuyPrice()}";

            FarmManagerText.gameObject.SetActive(FarmCore.HasFarmManager());

            HireAFarmManagerButton.gameObject.SetActive(!FarmCore.HasFarmManager());
            HireAFarmManagerButton.interactable = FarmCore.CanHireFarmManager();
            GoOnAnAdventureButton.gameObject.SetActive(FarmCore.HasFarmManager());
            GoOnAnAdventureButton.interactable = FarmCore.CanGoOnAdventure();
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
            int selectedCowFoodBuyAmount = (int)CowFoodBuyAmountSlider.value;
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
            ActionLogText.text = message;
        }
    }
}
