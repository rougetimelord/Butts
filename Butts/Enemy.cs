#region Using Statements
using System;
using Microsoft.Xna.Framework;
#endregion

namespace Butts
{
    class Enemy
    {
        //Position and life vars
        public Vector2 eLoc = new Vector2();
        public bool alive = true;
        public Enemy()
        {
            //Constructor enemy is probably alive when spawned (maybe?) (ZOMBIES???)
            Random r = new Random();
            alive = true;
            switch(r.Next(4))
            {
                //Randomly pick which axis to randomize to that enemy spawns off screen
                case(0):
                    eLoc.X = r.Next(0, (int)Game1._fullscreen.X + 1);
                    eLoc.Y = -50;
                    break;
                case(1):
                    eLoc.X = -50;
                    eLoc.Y = r.Next(0, (int)Game1._fullscreen.Y + 1);
                    break;
                case(2):
                    eLoc.X = r.Next(0, (int)Game1._fullscreen.X + 1);
                    eLoc.Y = (int)Game1._fullscreen.Y;
                    break;
                case(3):
                    eLoc.X = (int)Game1._fullscreen.X;
                    eLoc.Y = r.Next(0, (int)Game1._fullscreen.Y + 1);
                    break;
            }
        }
        public void Update(Vector2 pLoc)
        {
            //AI does an update
            if (eLoc.X > pLoc.X)
                eLoc.X--;
            if (eLoc.X < pLoc.X)
                eLoc.X++;
            if (eLoc.Y > pLoc.Y)
                eLoc.Y--;
            if (eLoc.Y < pLoc.Y)
                eLoc.Y++;
            //If hit by attack die
            if (Game1._attack && (eLoc.X >= pLoc.X - 100 && eLoc.X <= pLoc.X + 100) && (eLoc.Y >= pLoc.Y - 100 && eLoc.Y <= pLoc.Y + 100))
                  alive = false;
            //If enemy hits player, player dies
            if (((eLoc.X + 50 <= Player.hiLocation.X + 50 || eLoc.X <= Player.hiLocation.X + 50) && (eLoc.X + 50 >= Player.hiLocation.X || eLoc.X >= Player.hiLocation.X)) && ((eLoc.Y + 50 <= Player.hiLocation.Y + 50 || eLoc.Y <= Player.hiLocation.Y + 50) && (eLoc.Y + 50 >= Player.hiLocation.Y || eLoc.Y >= Player.hiLocation.Y)))
                PositionChecker.dead = true;
        }
    }
}
