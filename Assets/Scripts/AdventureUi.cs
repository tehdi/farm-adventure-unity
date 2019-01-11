using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace FarmAdventure
{
    public class AdventureUi : MonoBehaviour
    {
        public static int MoveCount = 0;

        // player is 32x32 and (0, 0) is the middle of a 480x416 map (32*15 x 32*13)
        private static readonly int PIXELS_PER_UNIT = 32;
        private static readonly int MIN_X = -224; // (224 + (32 / 2)) * 2 = 480
        private static readonly int MAX_X = 224;
        private static readonly int MIN_Y = -192; // (192 + (32 / 2)) * 2 = 416
        private static readonly int MAX_Y = 192;
        private static readonly int MIN_TOWNS = 6;
        private static readonly int MAX_TOWNS = 9;

        public GameObject AdventureMapPanel;
        public GameObject PlayerPrefab;
        private GameObject Player;
        public GameObject TownWithoutPlayerPrefab;
        public GameObject TownWithPlayerPrefab;

        public Text CurrentLocationText;
        public Text QuestLogText;
        public Button AcceptQuestButton;
        public Button CompleteQuestButton;

        public Text MoneyAmountText;
        public Text MilkAmountText;
        public Text CowFoodAmountText;

        public Button EnterYourFarmButton;

        private Town CurrentTown { get { return AdventureCore.CurrentTown; } }
        private Dictionary<Town, GameObject> Towns; // so I can redraw towns when the player enters or leaves them

        void Start()
        {
            SceneSwapper.ActiveScene = SceneSwapper.ADVENTURE_SCENE_INDEX;
            
            if (AdventureCore.FirstLoad)
            {
                AdventureLogic.InitializeNewGame(MIN_X, MAX_X, MIN_Y, MAX_Y, PIXELS_PER_UNIT, MIN_TOWNS, MAX_TOWNS);
                AdventureCore.FirstLoad = false;
            }

            MoveCount = 0;
            DrawMap();
        }

        void Update()
        {
            MovePlayer();

            CurrentLocationText.text = PrepareCurrentLocationText();
            QuestLogText.text = PrepareQuestLogText();
            AcceptQuestButton.interactable = AdventureCore.CanAcceptQuest();
            CompleteQuestButton.interactable = AdventureCore.CanCompleteQuest();

            MoneyAmountText.text = $"{AdventureCore.PlayerMoney}";
            MilkAmountText.text = $"{AdventureCore.PlayerMilk}";
            CowFoodAmountText.text = $"{AdventureCore.PlayerCowFood}";

            EnterYourFarmButton.interactable = AdventureCore.CanEnterFarm();
        }

        public void AcceptQuest()
        {
            AdventureLogic.AcceptCurrentTownQuest();
        }

        public void CompleteQuests()
        {
            AdventureLogic.CompleteQuestsForCurrentTown();
        }

        private void DrawMap()
        {
            Player = Instantiate(PlayerPrefab, AdventureMapPanel.transform, false);
            Player.transform.localPosition = new Vector2(AdventureCore.PlayerXLocation, AdventureCore.PlayerYLocation);
            Player.SetActive(CurrentTown == null); // if player is in a town, player image isn't shown

            Towns = new Dictionary<Town, GameObject>();
            foreach (var town in AdventureCore.Towns.Values)
            {
                if (town == CurrentTown)
                {
                    AddTownToMap(town, TownWithPlayerPrefab);
                }
                else
                {
                    AddTownToMap(town, TownWithoutPlayerPrefab);
                }
            }
        }

        private void MovePlayer()
        {
            // I had some trouble with floats when I tried to use transform.translate,
            // so I went the slightly more complicated route of sorting out the numbers myself and passing a direct "move to" command.

            bool playerMoved = false;
            Town startTown = CurrentTown;

            // local position is the centre point of the player image within its parent panel, regardless of anchor point
            int startX = AdventureCore.PlayerXLocation;
            int startY = AdventureCore.PlayerYLocation;

            // in one frame, you can move either horizontally or vertically. not both
            if (Input.GetButtonDown("Horizontal"))
            {
                int horizontalMovement = PIXELS_PER_UNIT * GetSignMultiplierForAxis("Horizontal");
                int newX = startX + horizontalMovement;
                if (IsBetweenInclusive(newX, MIN_X, MAX_X))
                {
                    Player.transform.localPosition = new Vector2(newX, startY);
                    playerMoved = true;
                }
            }
            else if (Input.GetButtonDown("Vertical"))
            {
                int verticalMovement = PIXELS_PER_UNIT * GetSignMultiplierForAxis("Vertical");
                int newY = startY + verticalMovement;
                if (IsBetweenInclusive(newY, MIN_Y, MAX_Y))
                {
                    Player.transform.localPosition = new Vector2(startX, newY);
                    playerMoved = true;
                }
            }

            if (playerMoved)
            {
                AdventureLogic.MovePlayerTo((int)Player.transform.localPosition.x, (int)Player.transform.localPosition.y);
                Player.SetActive(CurrentTown == null); // player image is only shown if player is not in a town
                MoveCount++;

                if (startTown != null)
                {
                    // player moved out of startTown
                    Destroy(Towns[startTown]);
                    AddTownToMap(startTown, TownWithoutPlayerPrefab);
                }
                if (CurrentTown != null)
                {
                    // player moved into CurrentTown
                    Destroy(Towns[CurrentTown]);
                    AddTownToMap(CurrentTown, TownWithPlayerPrefab);
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

        private void AddTownToMap(Town town, GameObject townPrefab)
        {
            GameObject townImage = Instantiate(townPrefab, AdventureMapPanel.transform, false);
            Tooltip tooltip = townImage.transform.Find("TooltipScript").GetComponent<Tooltip>();
            tooltip.SetText(town.Name);
            tooltip.ResizeToFitText();
            
            townImage.transform.localPosition = new Vector2(town.XLocation, town.YLocation);
            Towns[town] = townImage;
        }
    }
}
