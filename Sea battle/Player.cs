using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Sea_battle
{
    public class Player
    {
        public Player(bool canShoot)
        {
            this.canShoot = canShoot;  
            this.isPlayerTurn = true;
        }

        public bool canShoot;
        public bool isPlayerTurn;
        public int destroyedShips;

        public bool Shoot(int[,] map, int cellSize, Button pressedButton)
        {
            bool hit = false;

            int delta = 0;
            if (pressedButton.Location.X > 350)
            {
                delta = 350;
            }
            if (map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] == 1)
            {
                hit = true;
                pressedButton.BackColor = Color.Blue;
                pressedButton.Text = "X";
            }
            else
            {
                hit = false;
                pressedButton.BackColor = Color.Black;
            }
            canShoot = hit;
            isPlayerTurn = hit;
            return hit;
        }
    }
}
