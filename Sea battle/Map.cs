using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sea_battle
{
    internal class Map
    {
        public static int[,] createMapArray(int mapSize)
        {
            int[,] Map = new int[mapSize, mapSize];
            return Map;
        }

        public void sayHello()
        {

        }

        private static void PlayerShoot(object sender, EventArgs e, int[,] enemyMap)
        {
            Button pressedButton = sender as Button;
            Shoot(enemyMap, 30, pressedButton);
        }

        private static bool Shoot(int[,] map,int cellSize ,Button pressedButton)
        {
            bool hit = false;

            int delta = 0;
            if (pressedButton.Location.X > 320)
            {
                delta = 320;
            }
            if (map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta)/ cellSize] != 0)
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
            return hit;
        }

        private static void ConfigureShips(object sender, EventArgs e, int[,] map, int cellSize)
        {
            Button pressedButton = sender as Button;
            pressedButton.BackColor = Color.Red;
            
            if (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == 0)
            {
                pressedButton.BackColor = Color.Red;
                map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 1;
            }
            else
            {
                pressedButton.BackColor = Color.White;
                map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 0;
            }
        }

        public static void createMaps(int mapSize,int cellSize, int[,] firstMap, int[,] secondMap, Control.ControlCollection control)
        {
            string alphabet = "ABCDEFGHIJ";

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    firstMap[i, j] = 0;
                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j>0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = i.ToString();
                        }
                    }
                    else
                    {
                        button.Click += new EventHandler((sender, e) => ConfigureShips(sender, e, secondMap, cellSize));
                    }
                    control.Add(button);
                }
            }

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    secondMap[i, j] = 0;
                    Button button = new Button();
                    button.Location = new Point(350 + j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = i.ToString();
                        }
                    }
                    else
                    {
                        button.Click += new EventHandler((sender, e) => PlayerShoot(sender, e, secondMap));
                    }
                    control.Add(button);
                }
            }
        }
    }
}
