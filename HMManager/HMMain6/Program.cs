using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMMain6
{
    internal class Program
    {
        public static Data dt;
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
");
            var select = Console.ReadLine();

            switch (select.ToUpper())
            {
                case "MAIN":
                    {
                        ServerMain();
                    }; break;
            }



        }

        private static void ServerMain()
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
            Thread startTcpServer = new Thread(() => Listen.IpAndPort(ip, tcpPort));
            startTcpServer.Start();

            Thread startMonitorTcpServer = new Thread(() => Listen.IpAndPortMonitor(ip, 30000 - tcpPort));
            startMonitorTcpServer.Start();

        }
    }
}

