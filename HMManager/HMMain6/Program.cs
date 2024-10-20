﻿using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMMain6
{
    internal class Program
    {
        public static Data dt;
        public static RoomMainF.RoomMain rm;
        static void Main(string[] args)
        {
            var version = "6.0.1";
            string Text = $@"
版本号{version}
主要实现功能是寻宝、攻击、收集一体化。这是为前台提供新的服务！
";



            Console.WriteLine($"版本号：{version}");

            Console.WriteLine(@"
--------- main --运行主程序  
--------- updateImageAndModel --运行主程序  
");
            var select = Console.ReadLine();

            switch (select.ToUpper())
            {
                case "":
                case "MAIN":
                    {

                        ServerMain();
                    }; break;
                case "UPDATEIMAGEANDMODEL":
                    {
                        UpdateImageAndModel.updateImageAndModel();
                    }; break;


            }



        }



        private static void ServerMain()
        {
            {
                Console.Write("输入密码:");
                var pass = string.Empty;
                ConsoleKey key;
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        Console.Write("\b \b");
                        pass = pass[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        pass += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
                DalOfAddress.Connection.SetPassWord(pass);

                Program.dt = new Data();
                dt.LoadFPAndMap();

                var ip = "127.0.0.1";
                int tcpPort = 11100;

                Console.WriteLine($"输入ip,如“{ip}”");
                var inputIp = Console.ReadLine();
                if (string.IsNullOrEmpty(inputIp)) { }
                else
                {
                    ip = inputIp;
                }

                Console.WriteLine($"输入端口≠15000,如“{tcpPort}”");
                var inputWebsocketPort = Console.ReadLine();
                if (string.IsNullOrEmpty(inputWebsocketPort)) { }
                else
                {
                    int num;
                    if (int.TryParse(inputWebsocketPort, out num))
                    {
                        tcpPort = num;
                    }
                }
                Program.rm = new RoomMainF.RoomMain(Program.dt);
                Thread startTcpServer = new Thread(() => Listen.IpAndPort(ip, tcpPort));
                startTcpServer.Start();


                //Thread startMonitorTcpServer = new Thread(() => Listen.IpAndPortMonitor(ip, 30000 - tcpPort));
                //startMonitorTcpServer.Start();

                Thread th = new Thread(() => PlayersSysOperate(Program.dt));
                th.Start();

            }
            {
                while (true)
                {
                    if (Console.ReadLine().ToLower() == "exit")
                    {
                        int countOfPlayersOnline = 0;
                        if (Program.rm._Groups != null)
                        {
                            foreach (var groupItem in Program.rm._Groups)
                            {
                                foreach (var playerItem in groupItem.Value._PlayerInGroup)
                                {
                                    if (playerItem.Value.IsOnline())
                                    {
                                        countOfPlayersOnline++;
                                    }
                                }
                            }
                        }
                        if (countOfPlayersOnline > 0)
                        {
                            Console.WriteLine($"当前有{countOfPlayersOnline}人在线，未能退出！");
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                Environment.Exit(0);
            }
        }


        private static void PlayersSysOperate(GetRandomPos grp)
        {
            while (true)
            {

                // Program.rm.SetReturn(grp);
                Program.rm.ClearPlayers();
                // Program.rm.SetNPC();
                Thread.Sleep(30 * 1000);

            }
            //  throw new NotImplementedException();
        }
    }
}

