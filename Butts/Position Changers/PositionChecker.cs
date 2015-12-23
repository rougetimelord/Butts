using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Butts
{
    class PositionChecker
    {
        public static bool dead = false;
        public static void PosChecker()
        {
            //Check top
            if (Player.hiLocation.X < 0 + 25)
                Player.hiLocation.X = 0 + 25;
            //Check left
            if (Player.hiLocation.Y < 0 + 25)
                Player.hiLocation.Y = 0 + 25;
            //Check right
            if (Player.hiLocation.X >= Game1._fullscreen.X - 25)
                Player.hiLocation.X = Game1._fullscreen.X - 25;
            //Check bottom
            if (Player.hiLocation.Y >= Game1._fullscreen.Y - 25)
                Player.hiLocation.Y = Game1._fullscreen.Y - 25;
        }
    }
}
