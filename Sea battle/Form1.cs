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
        private bool isPlaying = false;


        private void createMaps()
        {
            this.Width = mapSize * 2 * cellSize + 70;
            this.Height = (mapSize + 1) * cellSize + 20;
            Map.createMaps(mapSize, cellSize, firstMap, secondMap, this.Controls);
        }

        private void Start(object sender, EventArgs e)
        {
            isPlaying = true;
        }

        private void ConfigureShips(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if (isPlaying)
            {
                pressedButton.BackColor = Color.Red;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            firstPlayer = new Player(true);
            secondPlayer = new Player(false);
            firstUserForm.Show();
            firstUserForm.ShowMaps(firstMap, secondMap,mapSize, cellSize, firstPlayer, secondPlayer);
            secondUserForm.Show();
            secondUserForm.ShowMaps(secondMap, firstMap, mapSize, cellSize, secondPlayer, firstPlayer);
        }
    }
}