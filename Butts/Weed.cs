using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;

namespace Butts
{
    class Weed
    {
        Random _r = new Random(Game1._fullscreen.X.GetHashCode());
        public Vector2 position = new Vector2();
        public float rot;
        public Weed()
        {
            position.Y = Game1._fullscreen.Y;
            position.X = _r.Next((int)Game1._fullscreen.X + 1);
            rot = _r.Next(361);
        }
        public void Update()
        {
            rot--;
            position.X--;
        }
    }
}
