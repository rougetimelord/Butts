using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Butts.Position_Changers
{
    class Tilt
    {
        //someday I'll have tilt controls
        //static float _thresh = 10;
        //Deadzone for tilt
        public static bool _r = false;
        //Bool to tell main game if the update should happen
        public static void TiltTest()
        {
            //var motion = new Motion();
        }
        public static void Update()
        {
            //var motion = Accelerometer.GetDefault();
            //AccelerometerReading reading = motion.GetCurrentReading();
            //if (reading.AccelerationX >= _thresh)
                //Player.hiLocation.X += 5;
            //if (reading.AccelerationX <= -_thresh)
                //Player.hiLocation.X -= 5;
        }
    }
}
