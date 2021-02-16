﻿using System;
using System.Threading;

namespace Mini_PBX
{
    class Mini_PBX
    {
        static ServerObject server; // сервер
        static Thread listenThread; // потока для прослушивания
        static void Main(string[] args)
        {
            try
            {
                server = new ServerObject();
                // работает
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start(); //старт потока
                // server.ListenAsync(); // не работает
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
