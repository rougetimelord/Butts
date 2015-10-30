using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace Butts
{
    class Weed
    {
        Random _r = new Random();
        public Vector2 position = new Vector2(), hPos = new Vector2();
        public static Vector2 _ho;
        public static Texture2D _hit, _sprite0, _sprite1, _sprite2, _sprite3;
        //Sprites are in index
        public float rot, scale;
        public Color c;
        public Texture2D sprite;
        int time = 0, hTime = 0, hThresh;
        public Weed()
        {
            position.Y = 0;
            position.X = _r.Next((int)Game1._fullscreen.X);
            rot = _r.Next(361);
            c = new Color(_r.Next(10, 256), _r.Next(10, 256), _r.Next(10, 256), _r.Next(200, 256));
            scale = ((float)_r.Next(10, 100) / 100);
            hThresh = _r.Next(5, 25);
            switch(_r.Next(4))
            {
                //Pick sprite
                //R.Next result = sprite index
                case(0):
                    sprite = _sprite0;
                    break;
                case(1):
                    sprite = _sprite1;
                    break;
                case(2):
                    sprite = _sprite2;
                    scale *= .5F;
                    break;
                case(3):
                    sprite = _sprite3;
                    scale *= .5F;
                    break;
            }
        }
        public static void Sprites(Texture2D w, Texture2D g, Texture2D d, Texture2D m, Texture2D h)
        {
            //Sprite handler
            //On sprite load this method sets the classes sprites to the ones loaded
            //Simply add new sprites that should be in weed joke to conent load and to Weed.Sprite() call
            _hit = h;
            _sprite0 = w;
            _sprite1 = g;
            _sprite2 = d;
            _sprite3 = m;
            _ho = new Vector2(_hit.Width / 2, _hit.Height / 2);
        }
        public void Update()
        {
            time++;
            hTime++;
            if (time == 20)
            {
                rot--;
                time = 0;
            }
            position.Y++;
            if (hTime >= hThresh)
            {
                hPos.X = _r.Next((int)Game1._fullscreen.X);
                hPos.Y = _r.Next((int)Game1._fullscreen.Y);
                hTime = 0;
            }
        }
    }
}
