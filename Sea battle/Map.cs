using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Sea_battle
{
    internal class Map
    {
        public static int[,] createMapArray(int mapSize)
        {
            int[,] Map = new int[mapSize, mapSize];
            return Map;
        }

        public static int[,] generateShips(int[,] map, int cellSize, int delta, Control.ControlCollection control)
        {
            int[,] shipMap = generateDecksShip(map,4,1);
            shipMap = generateDecksShip(shipMap,3,2);
            shipMap = generateDecksShip(shipMap, 2,3);
            shipMap = generateDecksShip(shipMap, 1,4);

            DisplayShips(shipMap, cellSize, control, delta);
            return shipMap;
        }

        public static int[,] generateDecksShip(int[,] map, int decks, int amount)
        {
            Random random = new Random();
            int startX;
            int startY;
            int row = 1;
            bool isDrown = false;
            int counter = 0;
            ShipMatrix.GetRandomFreePosition(map, random, out startX, out startY);

            while (isDrown == false)
            {
                map = ShipMatrix.CheckAndPlaceShip(map, startX, startY, decks, row, out isDrown);

                if (isDrown)
                {
                    bool isValid = ShipMatrix.ValidateShip(map);
                    if (isValid)
                    {
                        map = ShipMatrix.replaceTemporaryShipWithPermanent(map);
                        map = ShipMatrix.Transform(map);
                        counter++;
                        if (counter != amount)
                        {
                            isDrown = false;
                        }
                    }
                    else
                    {
                        isDrown = false;
                    }
                }
                ShipMatrix.GetRandomFreePosition(map, random,out startX, out startY);
            }
            return map;
        }

        public static void DisplayShips(int[,] map, int cellSize, Control.ControlCollection control, int delta)
        {
            int mapSize = 11;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i,j] == 1)
                    {
                        int buttonLicationX = j * cellSize;
                        int buttonLicationY = i * cellSize;
                        DrawButton(control, new Point(buttonLicationX + delta, buttonLicationY), Color.Red);
                    }
                }
            }
        }

        public static void DisplayFields(int[,] map, int cellSize, Control.ControlCollection control, int delta)
        {
            int mapSize = 11;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j] == 4)
                    {
                        int buttonLicationX = j * cellSize;
                        int buttonLicationY = i * cellSize;
                        DrawButton(control, new Point(buttonLicationX + delta, buttonLicationY), Color.Black);
                    }
                }
            }
        }

        public static void DrawButton(Control.ControlCollection Control, Point pointLocation, Color color)
        {
            foreach (Control control in Control)
            {
                if (control is Button button && button.Bounds.Contains(pointLocation))
                {
                    button.BackColor = color;
                }
            }
        }

        private static void PlayerShoot(object sender, EventArgs e, int[,] enemyMap, Player firstPlayer, Player secondPlayer, Control.ControlCollection control, Label label)
        {
            Button pressedButton = sender as Button;
            if (firstPlayer.canShoot && firstPlayer.isPlayerTurn)
            {
               bool hit = firstPlayer.Shoot(enemyMap, 30, pressedButton, control, label);

                if (!hit)
                {
                    firstPlayer.isPlayerTurn = false;
                    secondPlayer.isPlayerTurn = true;
                    secondPlayer.canShoot = true;
                }
            }
            
        }

        public static void Display(int mapSize, int cellSize,int[,] userMap, int[,]enemyMap, Control.ControlCollection control, Player firstPlayer, Player enemyPlayer, Label label)
        {
            string alphabet = "ABCDEFGHIJ";
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int value = numbers[9];

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
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
                            button.Text = numbers[i - 1].ToString();
                        }
                    }
                    control.Add(button);
                }
            }

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
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
                            button.Text = numbers[i - 1].ToString();
                        }
                    }
                    else
                    {
                        button.Click += new EventHandler((sender, e) => PlayerShoot(sender, e, enemyMap, firstPlayer, enemyPlayer, control, label));
                    }
                    control.Add(button);
                }
            }
            DisplayShips(userMap, cellSize,control,0);
        }

        public static void createMaps(int mapSize,int cellSize, int[,] firstMap, int[,] secondMap, Control.ControlCollection control)
        {
            string alphabet = "ABCDEFGHIJ";
            int[] numbers = {0,1,2,3,4,5,6,7,8,9 };
            int value = numbers[9];

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    firstMap[i, j] = 0;
                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (i == 0 && j == 0)
                    {
                        firstMap[i, j] = 5;
                    }
                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j>0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                            firstMap[i, j] = 5;
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = numbers[i-1].ToString();
                            firstMap[i, j] = 5;
                        }
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
                    if (i == 0 && j == 0)
                    {
                        secondMap[i, j] = 5;
                    }
                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                            secondMap[i, j] = 5;
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = numbers[i - 1].ToString();
                            secondMap[i, j] = 5;
                        }
                    }
                    control.Add(button);
                }
            }
            generateShips(firstMap, cellSize, 0, control);
            generateShips(secondMap, cellSize,350, control);
        }
    }
}
