namespace FarmAdventure
{
    public class PlayerInventory
    {
        public int Money { get; set; }
        public int CowFood { get; set; }
        public int Milk { get; set; }

        public void Add(PlayerInventory inventory)
        {
            Money += inventory.Money;
            CowFood += inventory.CowFood;
            Milk += inventory.Milk;
        }

        public void Clear()
        {
            Money = 0;
            CowFood = 0;
            Milk = 0;
        }
    }
}
