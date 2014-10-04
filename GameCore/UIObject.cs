using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public abstract class UIObject
    {
        public UIObject(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        protected static String ImageUriBegining = "pack://application:,,,/GameCore;Component/Images/";

        protected String ImageName = null;

        public int X
        {
            internal set;
            get;
        }

        public int Y
        {
            internal set;
            get;
        }

        public int Width
        {
            protected set;
            get;
        }

        public int Height
        {
            protected set;
            get;
        }

        /// <summary>
        /// Returns the uri of the image
        /// </summary>
        /// <returns>Uri of the image</returns>
        public virtual Uri getImageUri()
        {
            return new Uri(String.Format("{0}{1}", ImageUriBegining, ImageName, UriKind.RelativeOrAbsolute));
        }
    }
}
