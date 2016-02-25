#region Using Statements
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
#endregion

namespace Butts
{
    class Enemy
    {
        //Position and life vars
        public Vector2 eLoc = new Vector2();
        public bool alive = true;
        //AI type variables
        public int type = 0;
        public List<int> posTypes = new List<int>();
        public Color color;
        public Enemy(int score)
        {
            Random r = new Random();
            //Constructor enemy is probably alive when spawned (maybe?) (ZOMBIES???)
            alive = true;
            //Add default type
            posTypes.Add(0);
            if (score >= 10)
            {
                //If score >= 10 add type 2 AI
                posTypes.Add(1);
                if(score >= 15)
                    posTypes.Add(2);
                if (score >= 25)
                    posTypes.Add(3);
                if (score >= 50)
                    posTypes.Add(4);
            }
            int typeI = r.Next(posTypes.Count);
            type = posTypes[typeI];
            switch (typeI)
            {
                //pick type
                case (0):
                    color = Color.Purple;
                    break;
                case (1):
                    color = Color.Yellow;
                    break;
                case(2):
                    color = Color.Green;
                    break;
                case (3):
                    color = Color.Orange;
                    break;
                case (4):
                    color = Color.Blue;
                    break;
            }
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
           switch(type)
            {
                case (0):
                    type0Update(pLoc);
                    break;
                case (1):
                    type1Update(pLoc);
                    break;
               case(2):
                    type2Update(pLoc);
                    break;
                case (3):
                    type3Update(pLoc);
                    break;
               case (4):
                   type4Update(pLoc);
                   break;
                default:
                    type0Update(pLoc);
                    break;
            }
            //If hit by attack die
            if (Game1._attack && (eLoc.X >= pLoc.X - 100 && eLoc.X <= pLoc.X + 100) && (eLoc.Y >= pLoc.Y - 100 && eLoc.Y <= pLoc.Y + 100))
                  alive = false;
            //If enemy hits player, player dies
            if ((eLoc.X >= Player.hiLocation.X - 50 && eLoc.X <= Player.hiLocation.X + 50) && (eLoc.Y >= Player.hiLocation.Y - 50 && eLoc.Y <= Player.hiLocation.Y + 50))
                PositionChecker.dead = true;
        }
        public void type0Update(Vector2 pLoc)
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
        }
        public void type1Update(Vector2 pLoc)
        {
            //AI does an update
            if (eLoc.X > pLoc.X)
                eLoc.X -= 5;
            if (eLoc.X < pLoc.X)
                eLoc.X += 5;
            if (eLoc.X <= pLoc.X + 50 && eLoc.X >= pLoc.X - 50)
            {
                if (eLoc.Y > pLoc.Y)
                    eLoc.Y -= 2;
                if (eLoc.Y < pLoc.Y)
                    eLoc.Y += 2;
            }
        }
        public void type2Update(Vector2 pLoc)
        {
            //AI does an update
            if (eLoc.Y > pLoc.Y)
                eLoc.Y -= 5;
            if (eLoc.Y < pLoc.Y)
                eLoc.Y += 5;
            if (eLoc.Y <= pLoc.Y + 50 && eLoc.Y >= pLoc.Y - 50)
            {
                if (eLoc.X > pLoc.X)
                    eLoc.X -= 2;
                if (eLoc.X < pLoc.X)
                    eLoc.X += 2;
            }
        }
        public void type3Update(Vector2 pLoc)
        {
            //AI does an update
            if (eLoc.X > pLoc.X)
                eLoc.X -= (float)Math.Log(eLoc.X) - 3;
            if (eLoc.X < pLoc.X)
                eLoc.X += (float)Math.Log(eLoc.X) - 3;
            if (eLoc.Y > pLoc.Y)
                eLoc.Y -= 5*(float)Math.Sin(eLoc.Y) + 3;
            if (eLoc.Y < pLoc.Y)
                eLoc.Y += 5*(float)Math.Sin(eLoc.Y) + 3;
        }
        public void type4Update(Vector2 pLoc)
        {
            //AI does an update
            if (eLoc.X > pLoc.X)
                eLoc.X -= (eLoc.X - 7 < pLoc.X)? eLoc.X-pLoc.X : 7;
            if (eLoc.X < pLoc.X)
                eLoc.X += (eLoc.X + 7 > pLoc.X) ? pLoc.X-eLoc.X: 7;
            if (eLoc.Y > pLoc.Y)
                eLoc.Y -= (eLoc.Y - 7 < pLoc.Y) ? eLoc.Y - pLoc.Y : 7;
            if (eLoc.Y < pLoc.Y)
                eLoc.Y += (eLoc.Y + 7 > pLoc.Y) ? pLoc.Y - eLoc.Y : 7;
        }
    }
}
