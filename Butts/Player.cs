#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace Butts
{
    class Player    
    {
        public static Vector2 hiLocation;
        public Player()
        {
            hiLocation = new Vector2(400, 300);
        }
    }
}
