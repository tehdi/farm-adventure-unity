// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace FarmAdventure
// {
//     public class AdventureGame
//     {
//         public List<Town> Towns { get; private set; }
//         private int PlayerMovesMadeSinceLastLeavingFarm = 0;

//         public bool MovePlayer(int deltaX, int deltaY, int maxX, int maxY)
//         {
//             PlayerMovesMadeSinceLastLeavingFarm++;
//         }

//         public void ReturnToFarm()
//         {
//             GameManager.ReturnToFarm(PlayerMovesMadeSinceLastLeavingFarm, Player.Inventory);
//             PlayerMovesMadeSinceLastLeavingFarm = 0;
//         }
//     }
// }
