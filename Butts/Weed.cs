using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;

namespace Butts
{
    class Weed
    {
        Random _r = new Random();
        public Vector2 position = new Vector2();
        public float rot;
        public Color c;
        int time = 0;
        public Weed()
        {
            position.Y = - 222 * (float)0.25;
            position.X = _r.Next((int)Game1._fullscreen.X);
            rot = _r.Next(361);
            c = new Color(_r.Next(10, 256), _r.Next(10, 256), _r.Next(10, 256), 255);
        }
        public void Update()
        {
            time++;
            if (time == 30)
            {
                rot--;
                time = 0;
            }
            position.Y++;
        }
    }
}
