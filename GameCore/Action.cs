using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    [Flags]
    public enum Action : byte
    {
        None = 1,                   // 00000001
        Left = 2,                   // 00000010
        Right = 4,                  // 00000100
        Jump = 8                     // 00001000
    }

}
