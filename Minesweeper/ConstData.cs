using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class ConstData
    {
        public static Dictionary<Level, Size> BoardSizes = new Dictionary<Level, Size>()
        {
            { Level.Easy, Size.Create(10, 10)},
            { Level.Intermediate, Size.Create(16, 16)},
            { Level.Difficult, Size.Create(16, 30)}
        };
        public static Dictionary<Level, int> MinesNumber = new Dictionary<Level, int>()
        {
            { Level.Easy, 10},
            { Level.Intermediate, 10},
            { Level.Difficult, 40}
        };
    }

    public class Size
    {
        public  int Height { get; set; }
        public  int Width { get; set; }
        public static Size Create(int h, int w)
        {
            return new Size()
            {
                Height = h,
                Width = w,
            };
        }
    }
    
}
