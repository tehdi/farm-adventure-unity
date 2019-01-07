using System.Collections.Generic;
using System.Linq;

namespace FarmAdventure
{
    public class Player
    {
        public PlayerInventory Inventory { get; }
        public int Money { get { return Inventory.Money; } }
        public int CowFood { get { return Inventory.CowFood; } }
        public int Milk { get { return Inventory.Milk; } }

        public List<Quest> ActiveQuests { get; }

        public Player()
        {
            ActiveQuests = new List<Quest>();
            Inventory = new PlayerInventory();
        }

        public bool HasQuestWithDestination(Town town) =>
            ActiveQuests
                .Any(q => q.Destination == town);

        public List<Quest> ActiveQuestsForDestination(Town town) =>
            ActiveQuests
                .Where(q => q.Destination == town)
                .OrderBy(q => q.Origin.Name)
                .ToList();

        public List<Quest> QuestsForDestinationsOtherThan(Town town) =>
            ActiveQuests
                .Where(q => q.Destination != town)
                .OrderBy(q => q.Origin.Name)
                .ToList();

        public void CompleteQuestsForDestination(Town town)
        {
            List<Quest> completableQuests = ActiveQuests.Where(q => q.Destination == town).ToList();
            foreach (var quest in completableQuests)
            {
                Inventory.Add(quest.Reward);
                ActiveQuests.Remove(quest);
            }
        }
    }
}
