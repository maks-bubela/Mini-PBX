using System;
using System.Threading;

namespace Mini_PBX
{
    class Program
    {
        static ServerObject server; // сервер
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            try
            {
                server = new ServerObject();
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
            server.CloseApp(false);
        }
    }
}
