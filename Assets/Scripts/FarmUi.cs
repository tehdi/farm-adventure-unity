using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmUi : MonoBehaviour
{
    public GameObject FeedCowsButton;
    public GameObject MilkCowsButton;
    public GameObject BorrowMoneyButton;
    public GameObject PayDebtButton;
    public GameObject SellMilkButton;
    public GameObject BuyCowFoodButton;
    public GameObject CowFoodBuyAmountSlider;
    public GameObject BuyCowButton;
    public GameObject FarmManagerText;
    public GameObject HireFarmManagerButton;
    public GameObject GoOnAnAdventureButton;

    void Update()
    {
        FeedCowsButton.GetComponent<Button>().interactable = FarmCore.CanFeedCows();
        MilkCowsButton.GetComponent<Button>().interactable = FarmCore.CanMilkCows();
        BorrowMoneyButton.GetComponent<Button>().interactable = FarmCore.CanBorrowMoney();
        PayDebtButton.GetComponent<Button>().interactable = FarmCore.CanPayDebt();
        SellMilkButton.GetComponent<Button>().interactable = FarmCore.CanSellMilk();
        CowFoodBuyAmountSlider.GetComponent<Slider>().interactable = FarmCore.CanBuyCowFood(1);
        BuyCowFoodButton.GetComponent<Button>().interactable = FarmCore.CanBuyCowFood((int)CowFoodBuyAmountSlider.GetComponent<Slider>().value);
        BuyCowButton.GetComponent<Button>().interactable = FarmCore.CanBuyCow();
        FarmManagerText.SetActive(FarmCore.HasFarmManager());

        HireFarmManagerButton.SetActive(!FarmCore.HasFarmManager());
        HireFarmManagerButton.GetComponent<Button>().interactable = FarmCore.CanHireFarmManager();
        GoOnAnAdventureButton.SetActive(FarmCore.HasFarmManager());
        GoOnAnAdventureButton.GetComponent<Button>().interactable = FarmCore.CanGoOnAdventure();
    }
}
