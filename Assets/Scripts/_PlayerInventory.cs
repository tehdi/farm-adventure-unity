// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace FarmAdventure
// {
//     public class PlayerInventory
//     {
//         public int Money { get; set; }
//         public int CowFood { get; set; }
//         public int Milk { get; set; }

//         public override string ToString()
//         {
//             int amount = Money != 0 ? Money
//                 : CowFood != 0 ? CowFood
//                 : Milk != 0 ? Milk
//                 : 0;
//             string name = Money!=0? "money"
//                 : CowFood != 0 ? "cow food"
//                 : Milk != 0 ? "milk"
//                 : "";
//             return $"{amount} {name}";
//         }

//         public void Add(PlayerInventory inventory)
//         {
//             Money += inventory.Money;
//             CowFood += inventory.CowFood;
//             Milk += inventory.Milk;
//         }

//         public void Clear()
//         {
//             Money = 0;
//             CowFood = 0;
//             Milk = 0;
//         }
//     }
// }
