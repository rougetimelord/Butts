#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
#endregion

namespace Butts
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    [Guid("564B1F2B-09E4-4C30-B2E9-3BB82AAC76DF")]
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
