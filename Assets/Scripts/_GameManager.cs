using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAdventure
{
    class GameManager
    {
        public static FarmGame FarmGame { get; private set; } = new FarmGame();
        private static AdventureGame AdventureGame = new AdventureGame();

        public static FarmForm FarmForm { get; private set; } = new FarmForm(FarmGame);
        public static AdventureForm AdventureForm { get; private set; } = new AdventureForm(AdventureGame);

        public static void StartAdventure()
        {
            AdventureForm.Show();
            FarmForm.Hide();
        }

        public static void ReturnToFarm(int movesMade, PlayerInventory inventory)
        {
            FarmManager.Farm(movesMade);
            FarmManager.DepositAdventureGains(inventory);
            
            FarmForm.Show();
            AdventureForm.Hide();
        }
    }
}
