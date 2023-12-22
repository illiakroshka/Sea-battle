using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

        public static int[,] generateShips(int[,] map, int cellSize, int delta, Control.ControlCollection control)
        {
            int[,] shipMap = generate4DeckShip(map);
            int[,] shipMap2 = generate3DecksShip(shipMap);
            int[,] shipMap3 = generate3DecksShip(shipMap2);
            int[,] shipMap4 = generate2DescksSpip(shipMap3);
            int[,] shipMap5 = generate2DescksSpip(shipMap4);
            int[,] shipMap6 = generate2DescksSpip(shipMap5);
            int[,] shipMap7 = generate1DeckShip(shipMap6);
            int[,] shipMap8 = generate1DeckShip(shipMap7);
            int[,] shipMap9 = generate1DeckShip(shipMap8);
            int[,] shipMap10 = generate1DeckShip(shipMap9);

            DisplayShips(shipMap10, cellSize, control, delta);
            return shipMap;
        }

        public static int[,] generate1DeckShip(int[,] map)
        {
            Random random = new Random();
            int startX = random.Next(1, 11);
            int startY = random.Next(1, 11);
            int decks = 2;
            int row = 1;
            bool isFree = false;
            bool isValidated = false; 

            while (isFree == false)
            {
                if (map[startX, startY] == 0)
                {
                    isFree = true;
                    map[startX, startY] = 3;
                    isValidated = Validate(map);
                    if (isValidated ==  false)
                    {
                        isFree = false;
                    }
                    else
                    {
                        map = ReplaceThreesWithOnes(map);
                        map = Transform(map);
                    }
                }
                else
                {
                    startX = random.Next(1, 11);
                    startY = random.Next(1, 11);
                }
            }
            return map; 
        }

        public static int[,] generate2DescksSpip(int[,] map)
        {
            Random random = new Random();
            int startX = random.Next(1, 11);
            int startY = random.Next(1, 11);
            int decks = 2;
            int row = 1;
            bool isDrown = false;
            bool isFree = false;

            while (isFree == false)
            {
                if (map[startX, startY] == 0)
                {
                    isFree = true;
                }
                else
                {
                    startX = random.Next(1, 11);
                    startY = random.Next(1, 11);
                }
            }

            while (isDrown == false)
            {
                if (startX - decks > 0)
                {
                    bool isAble = false;
                    for (int i = 0; i < row; i++)
                    {
                        int start = startX;
                        for (int j = 0; j < decks; j++)
                        {
                            if (map[start, startY] == 0)
                            {
                                isAble = true;
                            }
                            else
                            {
                                isAble = false;
                                isFree = false;
                                break;
                            }
                            start = start - row;
                        }
                    }
                    if (isAble)
                    {
                        isDrown = true;
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < decks; j++)
                            {
                                if (map[startX, startY] == 0)
                                {
                                    map[startX, startY] = 3;
                                    startX = startX - row;
                                }
                            }
                        }
                    }
                }
                else if (startX + decks < 9)
                {
                    bool isAble = false;
                    for (int i = 0; i < row; i++)
                    {
                        int start = startX;
                        for (int j = 0; j < decks; j++)
                        {
                            if (map[start, startY] == 0)
                            {
                                isAble = true;
                            }
                            else
                            {
                                isAble = false;
                                isFree = false;
                                break;
                            }
                            start = start + row;
                        }
                    }
                    if (isAble)
                    {
                        isDrown = true;
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < decks; j++)
                            {
                                if (map[startX, startY] == 0)
                                {
                                    map[startX, startY] = 3;
                                    startX = startX + row;
                                }
                            }
                        }
                    }
                }


                else if (startY - decks > 0)
                {
                    bool isAble = false;

                    for (int i = 0; i < row; i++)
                    {
                        int start = startY;
                        for (int j = 0; j < decks; j++)
                        {
                            if (map[startX, start] == 0)
                            {
                                isAble = true;
                            }
                            else
                            {
                                isAble = false;
                                isFree = false;
                                break;
                            }
                            start = start - row;
                        }
                    }
                    if (isAble)
                    {
                        isDrown = true;
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < decks; j++)
                            {
                                if (map[startX, startY] == 0)
                                {
                                    map[startX, startY] = 3;
                                    startY = startY - row;
                                }
                            }
                        }
                    }
                }
                else if (startY + decks < 9)
                {
                    bool isAble = false;
                    for (int i = 0; i < row; i++)
                    {
                        int start = startY;
                        for (int j = 0; j < decks; j++)
                        {
                            if (map[startX, start] == 0)
                            {
                                isAble = true;
                            }
                            else
                            {
                                isAble = false;
                                isFree = false;
                                break;
                            }
                            start = start + row;
                        }
                    }
                    if (isAble)
                    {
                        isDrown = true;
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < decks; j++)
                            {
                                if (map[startX, startY] == 0)
                                {
                                    map[startX, startY] = 3;
                                    startY = startY + row;
                                }
                            }
                        }
                    }

                }

                if (isDrown)
                {
                    bool isValid = Validate(map);
                    if (isValid)
                    {
                        map = ReplaceThreesWithOnes(map);
                        map = Transform(map);
                    }
                    else
                    {
                        isDrown = false;
                        isFree = false;
                    }
                }

                while (isFree == false)
                {
                    startX = random.Next(1, 11);
                    startY = random.Next(1, 11);

                    if (map[startX, startY] != 1)
                    {
                        isFree = true;
                    }
                }
            }
            return map;
        }

        public static int[,] generate3DecksShip(int[,] map)
        {
            Random random = new Random();
            int startX = random.Next(1, 11);
            int startY = random.Next(1, 11);
            int decks = 3;
            int row = 1;
            bool isDrown = false;
            bool isFree = false;

            while(isFree == false)
            {
                if (map[startX,startY] == 0)
                {
                    isFree = true;
                }
                else
                {
                    startX = random.Next(1, 11);
                    startY = random.Next(1, 11);
                }
            }

            while (isDrown == false)
            {
                if (startX - decks > 0)
                {
                    bool isAble = false;
                    for (int i = 0; i < row; i++)
                    {
                        int start = startX;
                        for (int j = 0; j < decks; j++)
                        {
                            if (map[start, startY] == 0)
                            {
                                isAble = true;
                            }
                            else
                            {
                                isAble = false;
                                isFree = false;
                                break;
                            }
                            start = start - row;
                        }
                    }
                    if (isAble)
                    {
                        isDrown = true;
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < decks; j++)
                            {
                                if (map[startX, startY] == 0)
                                {
                                    map[startX, startY] = 3;
                                    startX = startX - row;
                                }
                            }
                        }
                    }
                }
                else if (startX + decks < 9)
                {
                    bool isAble = false;
                    for (int i = 0; i < row; i++)
                    {
                        int start = startX; 
                        for (int j = 0; j < decks; j++)
                        {
                            if (map[start, startY] == 0)
                            {
                                isAble = true;
                            }
                            else
                            {
                                isAble = false;
                                isFree = false;
                                break;
                            }
                            start = start + row;
                        }
                    }
                    if (isAble)
                    {
                        isDrown = true;
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < decks; j++)
                            {
                                if (map[startX, startY] == 0)
                                {
                                    map[startX, startY] = 3;
                                    startX = startX + row;
                                }
                            }
                        }
                    }
                }


                else if (startY - decks > 0)
                {
                    bool isAble = false;

                    for (int i = 0; i < row; i++)
                    {
                        int start = startY;
                        for (int j = 0; j < decks; j++)
                        {
                            if (map[startX, start] == 0)
                            {
                                isAble = true;
                            }
                            else
                            {
                                isAble = false;
                                isFree = false;
                                break;
                            }
                            start = start - row;
                        }
                    }
                    if (isAble)
                    {
                        isDrown = true;
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < decks; j++)
                            {
                                if (map[startX, startY] == 0)
                                {
                                    map[startX, startY] = 3;
                                    startY = startY - row;
                                }
                            }
                        }
                    }
                }
                else if (startY + decks < 9)
                {
                    bool isAble = false;
                    for (int i = 0; i < row; i++)
                    {
                        int start = startY;
                        for (int j = 0; j < decks; j++)
                        {
                            if (map[startX, start] == 0)
                            {
                                isAble = true;
                            }
                            else
                            {
                                isAble = false;
                                isFree = false;
                                break;
                            }
                            start = start + row;
                        }
                    }
                    if (isAble)
                    {
                        isDrown = true;
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < decks; j++)
                            {
                                if (map[startX, startY] == 0)
                                {
                                    map[startX, startY] = 3;
                                    startY = startY + row;
                                }
                            }
                        }
                    }

                }

                if (isDrown)
                {
                    bool isValid = Validate(map);
                    if (isValid)
                    {
                        map = ReplaceThreesWithOnes(map);
                        map = Transform(map);
                    }
                    else
                    {
                        isDrown = false;
                        isFree = false;
                    }
                }

                while (isFree == false)
                {
                    startX = random.Next(1, 11);
                    startY = random.Next(1, 11); 

                    if (map[startX, startY] != 1)
                    {
                        isFree = true;
                    }
                }
            }
            return map;
        }

        public static int[,] generate4DeckShip(int[,] map)
        {
            Random random = new Random();
            int startX = random.Next(1, 11);
            int startY = random.Next(1, 11);
            int direction = random.Next(0, 2);
            int decks = 4;
            int row = 1;

            if (direction == 0)
            {
                if (startX - 4 > 0)
                {
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < decks; j++)
                        {
                            map[startX, startY] = 1;
                            startX = startX - row;
                        }
                    }
                }
                else if (startX + 4 < 9)
                {
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < decks; j++)
                        {
                            map[startX, startY] = 1;
                            startX = startX + row;
                        }
                    }
                }
            }
            if (direction == 1)
            {
                if (startY - 4 > 0)
                {
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < decks; j++)
                        {
                            map[startX, startY] = 1;
                            startY = startY - row;
                        }
                    }

                }
                else if (startY + 4 < 9)
                {
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < decks; j++)
                        {
                            map[startX, startY] = 1;
                            startY = startY + row;
                        }
                    }
                }
            }
            map = Transform(map);
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
                        int buttonLicationX = i * cellSize;
                        int buttonLicationY = j * cellSize;
                        DrawButton(control, new Point(buttonLicationX + delta, buttonLicationY));
                    }
                }
            }
        }

        public static void DrawButton(Control.ControlCollection Control, Point pointLocation)
        {
            foreach (Control control in Control)
            {
                if (control is Button button && button.Bounds.Contains(pointLocation))
                {
                    button.BackColor = Color.Red;
                }
            }
        }

        public static int[,] Transform(int[,] map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (map[i, j] == 1)
                    {
                        // Расставляем двойки вокруг единицы
                        for (int x = i - 1; x <= i + 1; x++)
                        {
                            for (int y = j - 1; y <= j + 1; y++)
                            {
                                if (x >= 0 && x < rows && y >= 0 && y < cols && map[x, y] != 1)
                                {
                                    map[x, y] = 2;
                                }
                            }
                        }
                    }
                }
            }
            return map;
        }

        public static bool Validate(int[,] map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (map[i, j] == 3)
                    {
                        // Проверяем вокруг тройки
                        for (int x = i - 1; x <= i + 1; x++)
                        {
                            for (int y = j - 1; y <= j + 1; y++)
                            {
                                if (x >= 0 && x < rows && y >= 0 && y < cols && map[x, y] == 1)
                                {
                                    return false; // Вокруг тройки есть двойка
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }


        static bool CheckNoTwosBesideThrees(int[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (array[i, j] == 3)
                    {
                        // Проверяем слева, справа, сверху и снизу от тройки
                        if ((i > 0 && array[i - 1, j] == 2) ||  // Сверху
                            (i < rows - 1 && array[i + 1, j] == 2) ||  // Снизу
                            (j > 0 && array[i, j - 1] == 2) ||  // Слева
                            (j < cols - 1 && array[i, j + 1] == 2))  // Справа
                        {
                            return false; // Есть двойка побокам от тройки
                        }
                    }
                }
            }

            return true; // Нет двоек побокам от всех троек
        }

        static int[,] ReplaceThreesWithOnes(int[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (array[i, j] == 3)
                    {
                        array[i, j] = 1; // Заменяем тройку на единицу
                    }
                }
            }

            return array;
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
            if (pressedButton.Location.X > 350)
            {
                delta = 350;
            }
            double first = pressedButton.Location.Y / cellSize;
            double second = (pressedButton.Location.X - delta) / cellSize;
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
            int first = pressedButton.Location.Y / cellSize;
            int second = pressedButton.Location.X / cellSize;
            
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
                    if (i == 0 || j == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j>0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = numbers[i-1].ToString();
                        }
                    }
                    else
                    {
                        button.Click += new EventHandler((sender, e) => ConfigureShips(sender, e, firstMap, cellSize));
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
                            button.Text = numbers[i - 1].ToString();
                        }
                    }
                    else
                    {
                        button.Click += new EventHandler((sender, e) => PlayerShoot(sender, e, secondMap));
                    }
                    control.Add(button);
                }
            }
            generateShips(firstMap, cellSize, 0, control);
            generateShips(secondMap, cellSize,350, control);
        }
    }
}
