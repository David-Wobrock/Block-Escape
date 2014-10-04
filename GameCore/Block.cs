using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class Block : UIObject
    {
        public static int BlockSpeed = 10;
        public const int MaxBlockSpeed = 20;

        private int XSpeed = 0;
        private int YSpeed = 0;

        public Block(int x, int y, int xSpeed, int ySpeed)
            : base(x, y)
        {
            ImageName = "Block.jpg";
            Height = Width = 40;

            XSpeed = xSpeed;
            YSpeed = ySpeed;
        }

        public void Move()
        {
            X += XSpeed;
            Y += YSpeed;
        }
    }
}
