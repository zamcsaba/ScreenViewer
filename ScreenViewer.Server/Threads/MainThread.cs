using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ScreenViewer.Server.Threads
{
    public class MainThread
    {
        static TcpListener listener;

        public static void Initialize()
        {
            listener = new TcpListener(IPAddress.Parse(Utils.Utils.GetLocalIPAddress()), 3000);
            listener.Start();
        }

        static void MainLoop()
        {
            while (true)
            {
                Socket socket = listener.AcceptSocket();
                Thread thr = new Thread(new ParameterizedThreadStart())
            }
        }
    }
}
