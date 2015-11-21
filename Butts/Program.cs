#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
#endregion

namespace Butts
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class G
    {
        //GUID class
        public static string Make()
        {
            string g = "";
            var p = Directory.GetCurrentDirectory() + @"\GUID.txt";
            //Probably horrible practice, pls user don't touch that file
            //Maybe write to .dll or .guid or something not .txt
            if (File.Exists(p))
                g = File.ReadAllText(p);
            //For sure horrible practice and could be simplified
            else
            {
                g = Guid.NewGuid().ToString();
                File.WriteAllText(p, g);
            }
            return g;
        }
    }
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Set GUID
            Guid g = new Guid(G.Make());
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
