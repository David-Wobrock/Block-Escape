using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class Ground : UIObject
    {
        public Ground(int x, int y)
            : base(x, y)
        {
            ImageName = "Ground.jpg";
            Height = 150;
            Width = 1000;
        }
    }
}
