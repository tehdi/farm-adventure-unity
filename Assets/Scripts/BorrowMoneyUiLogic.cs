using UnityEngine;
using UnityEngine.UI;

namespace FarmAdventure
{
    public class BorrowMoneyUiLogic : MonoBehaviour
    {
        public GameObject RightSidePanel;
        public GameObject LoanConfirmationPanel;

        public Text BorrowMoneyMaxAmountText;
        public InputField RequestedLoanAmountInput;
        public Text RepayMoneyAmountText;

        public Button BorrowMoneyButton;
        public Button CancelBorrowMoneyButton;
        public Button ConfirmBorrowMoneyButton;

        void Update()
        {
            RepayMoneyAmountText.text = $"{CalculateRepaymentAmount()}";
            ConfirmBorrowMoneyButton.interactable = BorrowRequestIsValid();
        }

        public void BorrowMoney()
        {
            RightSidePanel.SetActive(false);
            LoanConfirmationPanel.SetActive(true);

            int maxAllowedLoan = CalculatedMaxAllowedLoan();
            BorrowMoneyMaxAmountText.text = $"{maxAllowedLoan}";
            RequestedLoanAmountInput.text = $"{maxAllowedLoan}";
        }

        public void CancelBorrowMoney()
        {
            RightSidePanel.SetActive(true);
            LoanConfirmationPanel.SetActive(false);
        }

        public void ConfirmBorrowMoney()
        {
            if (BorrowRequestIsValid())
            {
                FarmCore.Money += int.Parse(RequestedLoanAmountInput.text);
                FarmCore.Debt += CalculateRepaymentAmount();

                RightSidePanel.SetActive(true);
                LoanConfirmationPanel.SetActive(false);
            }
        }

        public void FuckOffInputFieldExceptions()
        {
            // if the Integer-typed input field is empty, Unity throws a hissy fit because it can't cast an empty string to an int.
            // ditto if it's a bare "-". "-8" it's fine with, but I don't want negative numbers in this field.
            // maybe there's a better way to fix this, but for now I'm going with custom text editing.
            // because clearly I wanted to write my own validation to convert a string to an int.
            // and I definitely didn't use Unity's "make this input field an integer" option so that I could avoid writing my own validation and conversion code.
            // not at all.
            RequestedLoanAmountInput.text = RequestedLoanAmountInput.text.Trim();
            if (RequestedLoanAmountInput.text == "" || RequestedLoanAmountInput.text == "-")
            {
                RequestedLoanAmountInput.text = "0";
            }
            if (RequestedLoanAmountInput.text.Length > 1)
            {
                if (RequestedLoanAmountInput.text.StartsWith("0") || RequestedLoanAmountInput.text.StartsWith("-"))
                {
                    RequestedLoanAmountInput.text = RequestedLoanAmountInput.text.Substring(1);
                }
            }
        }

        private int CalculateRepaymentAmount()
        {
            // we don't really have to worry about repayment amount:
            // there's no time frame that you have to repay debt, and interest doesn't accrue,
            // so the player can do multiple feed-milk-sell-buy cycles to get back in the green before they repay.
            // we'll make the repayment require 1 extra money for every unit of food the loan pays for.
            // this is ~30% interest if food costs 3 money each => repay 4 money each
            int requestedLoanAmount = int.Parse(RequestedLoanAmountInput.text);
            int repaymentAmount = requestedLoanAmount * (FarmCore.CowFoodUnitBuyPrice + 1) / FarmCore.CowFoodUnitBuyPrice;
            return repaymentAmount < 1 ? 1 : repaymentAmount;
        }

        private bool BorrowRequestIsValid()
        {
            int maxAllowedLoan = CalculatedMaxAllowedLoan();
            int requestedLoanAmount = int.Parse(RequestedLoanAmountInput.text);
            return requestedLoanAmount > 0 && requestedLoanAmount <= maxAllowedLoan;
        }

        private int CalculatedMaxAllowedLoan()
        {
            // how much money would it take to feed all your cows, after accounting for your current
            // stockpiles of cash, milk (including waiting-to-be-milked), and cow food? that's how much you can borrow.
            int moneyToFeedAllCows = FarmCore.CowAmount * FarmCore.CowFoodUnitBuyPrice;
            int currentFoodValue = FarmCore.CowFoodAmount * FarmCore.CowFoodUnitBuyPrice;
            int currentMilkValue = FarmCore.CalculateTotalMilkSellPrice();
            int unMilkedMilk = FarmCore.MilkUnitsPerCowRemainingToBeMilked * FarmCore.CowAmount;
            int unMilkedMilkValue = unMilkedMilk * FarmCore.MilkUnitSellPrice;
            int shortage = moneyToFeedAllCows - FarmCore.Money - currentFoodValue - currentMilkValue - unMilkedMilkValue;
            return shortage > 0 ? shortage : 0;
        }
    }
}
