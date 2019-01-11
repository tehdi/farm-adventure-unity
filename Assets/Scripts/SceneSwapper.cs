using UnityEngine;
using UnityEngine.SceneManagement;

namespace FarmAdventure
{
    public class SceneSwapper : MonoBehaviour
    {
        public static readonly int MENU_SCENE_INDEX = 0;
        public static readonly int FARM_SCENE_INDEX = 1;
        public static readonly int ADVENTURE_SCENE_INDEX = 2;
        public static int ActiveScene = FARM_SCENE_INDEX;

        public GameObject HowToPlayPanel;
        public GameObject AboutGamePanel;

        public void ContinueGame()
        {
            SceneManager.LoadScene(ActiveScene);
        }

        public void NewGame()
        {
            FarmLogic.StartNewGame();
            AdventureLogic.StartNewGame();

            SceneManager.LoadScene(FARM_SCENE_INDEX);
        }

        public void AboutGame()
        {
            HowToPlayPanel.SetActive(!HowToPlayPanel.activeInHierarchy);
            AboutGamePanel.SetActive(!AboutGamePanel.activeInHierarchy);
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene(MENU_SCENE_INDEX);
        }

        public void GoToFarm()
        {
            FarmManager.Farm(AdventureUi.MoveCount);

            FarmCore.Money += AdventureCore.PlayerMoney;
            FarmCore.MilkAmount += AdventureCore.PlayerMilk;
            FarmCore.CowFoodAmount += AdventureCore.PlayerCowFood;
            AdventureCore.Player.ClearInventory();

            SceneManager.LoadScene(FARM_SCENE_INDEX);
        }

        public void GoOnAnAdventure()
        {
            SceneManager.LoadScene(ADVENTURE_SCENE_INDEX);
        }
    }
}
