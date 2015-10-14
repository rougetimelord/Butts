using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Butts
{
    class PositionChecker
    {
        public static bool dd = false;
        public static void PosChecker()
        {
            //Check top
            if (Player.hiLocation.X < 0)
                Player.hiLocation.X = 0;
            //Check left
            if (Player.hiLocation.Y < 0)
                Player.hiLocation.Y = 0;
            //Check right
            if (Player.hiLocation.X >= Game1._fullscreen.X - 50)
                Player.hiLocation.X = Game1._fullscreen.X - 50;
            //Check bottom
            if (Player.hiLocation.Y >= Game1._fullscreen.Y - 50)
                Player.hiLocation.Y = Game1._fullscreen.Y - 50;
        }
    }
}
