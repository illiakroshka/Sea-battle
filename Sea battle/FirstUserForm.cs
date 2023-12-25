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
    public partial class FirstUserForm : Form
    {
        public FirstUserForm()
        {
            InitializeComponent();
        }

        public void ShowMaps(int[,] userMap, int[,] enemyMap, int mapSize, int cellSize, Player player, Player enemyPlayer)
        {
            Map.Display(mapSize, cellSize, userMap, enemyMap, this.Controls, player, enemyPlayer, label2);
        }
    }
}
