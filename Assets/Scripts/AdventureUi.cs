using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace FarmAdventure
{
    public class AdventureUi : MonoBehaviour
    {
        public GameObject AdventureMapPanel;

        public Button MoveNorthButton;
        public Button MoveWestButton;
        public Button MoveEastButton;
        public Button MoveSouthButton;

        public Text CurrentLocationText;
        public Text QuestLogText;
        public Button AcceptQuestButton;
        public Button CompleteQuestButton;

        public Text MoneyAmountText;
        public Text MilkAmountText;
        public Text CowFoodAmountText;

        public Button EnterYourFarmButton;

        void Update()
        {
            MoveNorthButton.interactable = AdventureCore.CanMoveNorth();
            MoveWestButton.interactable = AdventureCore.CanMoveWest();
            MoveEastButton.interactable = AdventureCore.CanMoveEast();
            MoveSouthButton.interactable = AdventureCore.CanMoveSouth();

            CurrentLocationText.text = PrepareCurrentLocationText();
            QuestLogText.text = PrepareQuestLogText();
            AcceptQuestButton.interactable = AdventureCore.CanAcceptQuest();
            CompleteQuestButton.interactable = AdventureCore.CanCompleteQuest();

            MoneyAmountText.text = $"{AdventureCore.PlayerMoney}";
            MilkAmountText.text = $"{AdventureCore.PlayerMilk}";
            CowFoodAmountText.text = $"{AdventureCore.PlayerCowFood}";

            EnterYourFarmButton.interactable = AdventureCore.CanEnterFarm();
        }

        private string PrepareCurrentLocationText()
        {
            Town currentTown = AdventureCore.CurrentTown;
            if (currentTown == null)
            {
                return "";
            }
            return $"You're in {currentTown.Name}.";
        }

        private string PrepareQuestLogText()
        {
            List<Quest> completableQuests = AdventureCore.CompletableQuestsForCurrentLocation;
            List<Quest> activeQuests = AdventureCore.ActiveQuestsForOtherLocations;
            Quest availableQuest = AdventureCore.CurrentTown == null ? null : AdventureCore.CurrentTown.Quest;

            StringBuilder questLogOutputBuilder = new StringBuilder();
            if (completableQuests.Count > 0)
            {
                questLogOutputBuilder.AppendLine("<b>Ready to Complete</b>");
                foreach (var completableQuest in completableQuests)
                {
                    questLogOutputBuilder.AppendLine(completableQuest.CompletionText);
                }
                questLogOutputBuilder.AppendLine();
            }
            if (activeQuests.Count > 0)
            {
                questLogOutputBuilder.AppendLine("<b>In Progress</b>");
                foreach (var activeQuest in activeQuests)
                {
                    questLogOutputBuilder.AppendLine(activeQuest.InProgressText);
                }
                questLogOutputBuilder.AppendLine();
            }
            if (availableQuest != null)
            {
                questLogOutputBuilder.AppendLine("<b>Available Quests</b>");
                questLogOutputBuilder.AppendLine(availableQuest.OfferText);
                questLogOutputBuilder.AppendLine();
            }
            return questLogOutputBuilder.ToString();
        }
    }
}
