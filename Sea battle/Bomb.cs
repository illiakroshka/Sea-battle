using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_battle
{
    public class Bomb : IWeapon
    {
        public int DamageRows => 1;
        public int DamageColumns => 1;

        public int[,] Fire(int[,] map, Button pressedButton, int cellSize, int delta, out bool hit)
        {
            if (map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta)/ cellSize] == ShipMatrix.shipIndex)
            {
                hit = true;
                pressedButton.BackColor = Color.Blue;
                pressedButton.Text = "X";
                map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] = ShipMatrix.testShipIndex;
            }
            else
            {
                hit = false;
                pressedButton.BackColor = Color.Black;
            }
            return map;
        }
    }
}
