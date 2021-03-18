using System;
using System.Threading;

namespace Mini_PBX
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerObject server = new ServerObject();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            try
            {
                server.Listen();
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            ServerObject server = new ServerObject();
            server.CloseApp(false);
        }
    }
}
