using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenViewer.Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Thread mainThread = new Thread(new ThreadStart(Threads.MainThread.Initialize));
            mainThread.Start();
        }
    }
}
