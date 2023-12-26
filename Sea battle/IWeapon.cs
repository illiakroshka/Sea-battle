using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_battle
{

    public interface IWeapon
    {
        int DamageRows { get; }
        int DamageColumns { get; }

        int[,] Fire(int[,] map, Button button, int cellSize, int delta, out bool hit); 
    }
}
