using System.CodeDom;
using System.Windows.Forms;

namespace Sea_battle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            firstMap = Map.createMapArray(mapSize);
            secondMap = Map.createMapArray(mapSize);
            createMaps();
        }
        private const int mapSize = 11;
        private int cellSize = 30;
        private int[,] firstMap;
        private int[,] secondMap;
        private bool isPlaying = false;
        
        
        private void createMaps()
        {
            this.Width = mapSize * 2 * cellSize + 70;
            this.Height = (mapSize + 1) * cellSize + 20;
            Map.createMaps(mapSize,cellSize, firstMap, secondMap, this.Controls);

            Button startButton = new Button();
            startButton.Text = "Start";
            startButton.Click += new EventHandler(Start);
            startButton.Location = new Point(0, mapSize * cellSize + 20);
            this.Controls.Add(startButton);
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
    }
}