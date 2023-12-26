using System.CodeDom;
using System.Reflection.Metadata;
using System.Windows.Forms;

namespace Sea_battle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            secondUserForm = new SecondUserForm();
            firstUserForm = new FirstUserForm();
            firstMap = Map.createMapArray(mapSize);
            secondMap = Map.createMapArray(mapSize);
            firstPlayer = new Player(true);
            secondPlayer = new Player(false);
            createMaps();
        }
        Player firstPlayer;
        Player secondPlayer;
        SecondUserForm secondUserForm;
        FirstUserForm firstUserForm;
        private const int mapSize = 11;
        private int cellSize = 30;
        private int[,] firstMap;
        private int[,] secondMap;
        IWeapon[] weapons = { new Bomb(), new DoubleBomb() };


        private void createMaps()
        {
            this.Width = mapSize * 2 * cellSize + 70;
            this.Height = (mapSize + 1) * cellSize + 20;
            Map.createMaps(mapSize, cellSize, firstMap, secondMap, this.Controls);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            firstUserForm.Show();
            firstUserForm.ShowMaps(firstMap, secondMap, mapSize, cellSize, firstPlayer, secondPlayer);
            secondUserForm.Show();
            secondUserForm.ShowMaps(secondMap, firstMap, mapSize, cellSize, secondPlayer, firstPlayer);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            firstPlayer.SetWeapon(weapons[0]);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            firstPlayer.SetWeapon(weapons[1]);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            secondPlayer.SetWeapon(weapons[0]);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            secondPlayer.SetWeapon(weapons[1]);
        }
    }
}