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
    static class KeyHandler
    {
        //Change player location if right key is pushed 
        #region KeyHandler
        public static void Handler(KeyboardState KeyState, GameTime gameTime)
        {
            //Clear attack state on every update
            Game1._attack = false;
            if (KeyState.IsKeyDown(Keys.W) | KeyState.IsKeyDown(Keys.Up))
            {
                Player.hiLocation.Y = Player.hiLocation.Y - Game1._speed;
                //Move up
            }
            if (KeyState.IsKeyDown(Keys.A) | KeyState.IsKeyDown(Keys.Left))
            {
                //Move left
                Player.hiLocation.X = Player.hiLocation.X - Game1._speed;
            }
            if (KeyState.IsKeyDown(Keys.S) | KeyState.IsKeyDown(Keys.Down))
            {
                //Move down
                Player.hiLocation.Y = Player.hiLocation.Y + Game1._speed;
            }
            if (KeyState.IsKeyDown(Keys.D) | KeyState.IsKeyDown(Keys.Right))
            {
                //Move right
                Player.hiLocation.X = Player.hiLocation.X + Game1._speed;
            }
            if (KeyState.IsKeyDown(Keys.Space))
            {
                //Attack
                Game1._attack = true;
            }
            //Validate new position
            PositionChecker.PosChecker();
        }
        #endregion
    }
}
