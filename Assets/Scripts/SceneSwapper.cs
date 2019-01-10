using UnityEngine;
using UnityEngine.SceneManagement;

namespace FarmAdventure
{
    public class SceneSwapper : MonoBehaviour
    {
        private static readonly int MENU_SCENE_INDEX = 0;
        private static readonly int FARM_SCENE_INDEX = 1;
        private static readonly int ADVENTURE_SCENE_INDEX = 2;

        public void GoToMenu()
        {
            SceneManager.LoadScene(MENU_SCENE_INDEX);
        }

        public void GoToFarm()
        {
            FarmManager.Farm(AdventureUi.MoveCount);

            FarmCore.Money += AdventureCore.PlayerMoney;
            FarmCore.LitresOfMilk += AdventureCore.PlayerMilk;
            FarmCore.BagsOfCowFood += AdventureCore.PlayerCowFood;
            AdventureCore.Player.ClearInventory();

            SceneManager.LoadScene(FARM_SCENE_INDEX);
        }

        public void GoOnAnAdventure()
        {
            SceneManager.LoadScene(ADVENTURE_SCENE_INDEX);
        }
    }
}
