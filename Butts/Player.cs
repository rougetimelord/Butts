#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace Butts
{
    class Player    
    {
        public static Vector2 hiLocation;
        public Player()
        {
            hiLocation = new Vector2(Game1._fullscreen.X / 2, Game1._fullscreen.Y / 2);
            //Set initial position in middle of screen
        }
    }
}
