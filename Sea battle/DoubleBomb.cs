using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_battle
{
    public class DoubleBomb : IWeapon
    {
        public int DamageRows => 2;
        public int DamageColumns => 2;
        private int Damage = 0;

        public int[,] Fire(int[,] map, Button pressedButton, int cellSize, int delta, out bool hit)
        {
            if (Damage != DamageRows)
            {
                if (map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] == ShipMatrix.shipIndex)
                {
                    if (Damage == 0)
                    {
                        Damage++;
                    }
                    hit = true;
                    pressedButton.BackColor = Color.Blue;
                    pressedButton.Text = "X";
                    map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] = ShipMatrix.testShipIndex;
                }
                else
                {
                    if (Damage == 1)
                    {
                        Damage++;
                    }
                    if (Damage != DamageRows)
                    {
                        hit = true;
                        pressedButton.BackColor = Color.Black;
                        Damage++;
                    }
                    else
                    {
                        hit = false;
                        pressedButton.BackColor = Color.Black;
                        Damage = 0;
                    }
                }
            }
            else
            {
                hit = false;
                pressedButton.BackColor = Color.Black;
                Damage = 0;
            }
            return map;
        }
    }

}
