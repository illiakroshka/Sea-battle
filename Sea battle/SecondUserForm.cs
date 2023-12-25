using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_battle
{
    public partial class SecondUserForm : Form
    {
        public SecondUserForm()
        {
            InitializeComponent();
            secondPlayer = new Player(false);
        }
        Player secondPlayer;

        public void ShowMaps(int[,] userMap, int[,] enemyMap, int mapSize, int cellSize, Player player, Player enemyPLayer)
        {
            Map.DisplayMaps(mapSize, cellSize, userMap, enemyMap, this.Controls, player, enemyPLayer);
        }
    }
}
