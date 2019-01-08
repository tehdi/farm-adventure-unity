using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace FarmAdventure
{
    public class AdventureUi : MonoBehaviour
    {
        private static bool FirstLoad = true;

        // player is 32x32 and (0, 0) is the middle of a 480x352 map (32*15 x 32*11)
        private static readonly int PIXELS_PER_UNIT = 32;
        private static readonly int MIN_X = -224; // (224 + (32 / 2)) * 2 = 480
        private static readonly int MAX_X = 224;
        private static readonly int MIN_Y = -160;
        private static readonly int MAX_Y = 160;
        private static readonly int MIN_TOWNS = 5;
        private static readonly int MAX_TOWNS = 8;

        public GameObject AdventureMapPanel;
        public Image PlayerImage;

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

        private Town CurrentTown { get { return AdventureCore.CurrentTown; } }

        void Start()
        {
            if (FirstLoad)
            {
                AdventureCore.InitializeNewGame(MIN_X, MAX_X, MIN_Y, MAX_Y, PIXELS_PER_UNIT, MIN_TOWNS, MAX_TOWNS);
                FirstLoad = false;
            }

            PlayerImage.transform.localPosition = new Vector2(AdventureCore.PlayerXLocation, AdventureCore.PlayerYLocation);

            Debug.Log($"Starting town is at {CurrentTown.XLocation}, {CurrentTown.YLocation}");
            Debug.Log($"Raw player starting location is {AdventureCore.PlayerXLocation}, {AdventureCore.PlayerYLocation}");
        }

        void Update()
        {
            MovePlayer();
            AdventureCore.MovePlayerTo((int)PlayerImage.transform.localPosition.x, (int)PlayerImage.transform.localPosition.y);
            // if player is in town, sub player image for player-in-town image

            CurrentLocationText.text = PrepareCurrentLocationText();
            QuestLogText.text = PrepareQuestLogText();
            AcceptQuestButton.interactable = AdventureCore.CanAcceptQuest();
            CompleteQuestButton.interactable = AdventureCore.CanCompleteQuest();

            MoneyAmountText.text = $"{AdventureCore.PlayerMoney}";
            MilkAmountText.text = $"{AdventureCore.PlayerMilk}";
            CowFoodAmountText.text = $"{AdventureCore.PlayerCowFood}";

            EnterYourFarmButton.interactable = AdventureCore.CanEnterFarm();
        }

        private void MovePlayer()
        {
            // I had some trouble with floats when I tried to use transform.translate,
            // so I went the slightly more complicated route of sorting out the numbers myself and passing a direct "move to" command.

            // local position is the centre point of the player image within its parent panel, regardless of anchor point
            Vector2 startingPosition = PlayerImage.transform.localPosition;
            int startX = (int)startingPosition.x;
            int startY = (int)startingPosition.y;

            // in one frame, you can move either horizontally or vertically. not both
            if (Input.GetButtonDown("Horizontal"))
            {
                int horizontalMovement = PIXELS_PER_UNIT * GetSignMultiplierForAxis("Horizontal");
                int newX = startX + horizontalMovement;
                if (IsBetweenInclusive(newX, MIN_X, MAX_X))
                {
                    PlayerImage.transform.localPosition = new Vector2(newX, startY);
                }
            }
            else if (Input.GetButtonDown("Vertical"))
            {
                int verticalMovement = PIXELS_PER_UNIT * GetSignMultiplierForAxis("Vertical");
                int newY = startY + verticalMovement;
                if (IsBetweenInclusive(newY, MIN_Y, MAX_Y))
                {
                    PlayerImage.transform.localPosition = new Vector2(startX, newY);
                }
            }
        }

        private string PrepareCurrentLocationText()
        {
            if (CurrentTown == null)
            {
                return "";
            }
            return $"You're in {CurrentTown.Name}.";
        }

        private string PrepareQuestLogText()
        {
            List<Quest> completableQuests = AdventureCore.CompletableQuestsForCurrentLocation;
            List<Quest> activeQuests = AdventureCore.ActiveQuestsForOtherLocations;
            Quest availableQuest = CurrentTown == null ? null : CurrentTown.Quest;

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

        private int GetSignMultiplierForAxis(string axisName)
        {
            float axisValue = Input.GetAxis(axisName);
            return axisValue > 0 ? 1
                    : axisValue < 0 ? -1
                    : 0;
        }

        private bool IsBetweenInclusive(int newValue, int minAllowed, int maxAllowed)
        {
            return newValue >= minAllowed && newValue <= maxAllowed;
        }
    }
}
