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
        public int destroyedShips { get; private set; }

        public bool Shoot(int[,] map, int cellSize, Button pressedButton, Control.ControlCollection control, Label label)
        {
            bool hit = false;

            int delta = 0;
            if (pressedButton.Location.X > 350)
            {
                delta = 350;
            }
            if (map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] == ShipMatrix.shipIndex)
            {
                hit = true;
                pressedButton.BackColor = Color.Blue;
                pressedButton.Text = "X";
                map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] = ShipMatrix.testShipIndex;
                bool status= ShipMatrix.ValidateShip(map);
                if (status)
                {
                    ShipMatrix.TransformForBorder(map);
                    Map.DisplayFields(map, 30, control, 350);
                    destroyedShips++;
                    label.Text = destroyedShips.ToString();
                }
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
