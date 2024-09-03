using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DbInput
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine($"" +
                    $"选择A，录入\r\n" +
                    $"选择B，绑定编码与地址\r\n" +
                    $"选择All，查询\r\n"+
                    $"选择 UPDATELINECOMMAND ，补充绘制\r\n" +
                    $"选择 CONNECT ，连接线路\r\n");
                var switchChacter = Console.ReadLine();
                var regex = new Regex("^[A-Z]{10}$");
                if (switchChacter == null)
                {
                    return;
                }
                else if (regex.IsMatch(switchChacter))
                {
                    var code = switchChacter;
                    EditFP(code);
                    continue;
                }

                switch (switchChacter.ToUpper())
                {
                    case "A":
                        {
                            InputAddr();
                        }; break;
                    case "ALL":
                        {
                            ShowAll();
                        }; break;
                    case "DRAW":
                        {
                            GenerateMap();
                        }; break;
                    case "UPDATELINECOMMAND":
                        {
                            UpdateLine();
                        }; break;
                    case "ADDLINE": { }; break;
                    case "CONNECT":
                        {
                            ConnectTwoPoint();
                        }; break;
                    default:
                        { }; break;
                }


            }
        }

        private static void ConnectTwoPoint()
        {
            Console.WriteLine("^[A-Z]{10}\\s+\\d+\\s+[A-Z]{10}\\s+\\d+\\s+d+\\s(0|1|2)$" + "将两个地点相互连接,0删除，1单向，2双向,3单向秒达不计路程，4双向秒达不计路程");
            var regex = new Regex("^(?<fpcode1>[A-Z]{10})[ ]{1,99}(?<fpheigh1>\\d+)[ ]{1,99}(?<fpcode2>[A-Z]{10})[ ]{1,99}(?<fpheigh2>\\d+)[ ]{1,99}(?<speed>\\d+)[ ]{1,99}(?<roadType>[0-4])");
            var inputValues = Console.ReadLine();
            Match match = regex.Match(inputValues);
            if (match.Success)
            {
                string fpcode1 = match.Groups["fpcode1"].Value.Trim(); // 捕获用户名
                int fpheigh1 = int.Parse(match.Groups["fpheigh1"].Value);  // 捕获年龄
                string fpcode2 = match.Groups["fpcode2"].Value.Trim();
                int fpheigh2 = int.Parse(match.Groups["fpheigh2"].Value);
                int speed = int.Parse(match.Groups["speed"].Value);
                int roadType = int.Parse(match.Groups["roadType"].Value);
                try
                {
                    string msg;
                    DalOfAddress.Tunel.Add(fpcode1, fpheigh1, fpcode2, fpheigh2, speed, roadType, out msg);
                    Console.WriteLine(msg);
                }
                catch { }
            }
        }

        private static void UpdateLine()
        {
            DalOfAddress.Tunel.UpdateItem();
            //  throw new NotImplementedException();
        }

        private static void GenerateMap()
        {
            var pointData = DalOfAddress.FP.GetAll();
            var lineData = DalOfAddress.Tunel.GetAll();
            DrawObj.KML.Draw(pointData, lineData);
            //   throw new NotImplementedException();
        }

        private static void EditFP(string code)
        {
            Console.WriteLine("^[A-Z]{10}\\s+\\d+\\s+[13][a-km-zA-HJ-NP-Z1-9]{25,34}\\s+[01]{1}$" + "  编辑地点");
            Console.WriteLine("^[A-Z]{10}\\s+\\d+\\s+[01]{1}$" + "  编辑地点");
            Console.WriteLine("^[Dd][Ee][Ll][Ee][Tt][Ee]\\s+[A-Z]{10}\\s+(\\d+)$" + "  删除地点");
            Console.WriteLine("^[Aa][Dd][Dd]\\s+\\s+(\\d+)$" + "  新增地点");
            var inputValues = Console.ReadLine();
            //  inputValues.Split(' ');

            {
                var regex = new Regex("^([A-Z]{10})\\s+(\\d+)\\s+([13][a-km-zA-HJ-NP-Z1-9]{25,34})\\s+([01]{1})$");
                Match match = regex.Match(inputValues);
                if (match.Success)
                {
                    string fpCode = match.Groups[1].Value; // 捕获用户名
                    string height = match.Groups[2].Value;  // 捕获年龄
                    string bitcoinAddr = match.Groups[3].Value;
                    string canGetScore = match.Groups[4].Value;
                    DalOfAddress.FPDetail.UpdateItem(fpCode, height, bitcoinAddr, canGetScore);
                }
            }
            {
                var regex = new Regex("^([A-Z]{10})\\s+(\\d+)\\s+([01]{1})$");
                Match match = regex.Match(inputValues);
                if (match.Success)
                {
                    string fpCode = match.Groups[1].Value; // 捕获用户名
                    string height = match.Groups[2].Value;  // 捕获年龄
                    string bitcoinAddr = "";
                    string canGetScore = match.Groups[3].Value;
                    DalOfAddress.FPDetail.UpdateItem(fpCode, height, bitcoinAddr, canGetScore);
                }
            }
            {
                var regex = new Regex("^[Dd][Ee][Ll][Ee][Tt][Ee]\\s+([A-Z]{10})\\s+(\\d+)$");
                Match match = regex.Match(inputValues);
                if (match.Success)
                {
                    string fpCode = match.Groups[1].Value; // 捕获用户名
                    string height = match.Groups[2].Value;  // 捕获年龄
                    var success = DalOfAddress.FPDetail.DeleteItem(fpCode, height);
                    if (success)
                    {
                        Console.WriteLine($"删除成功");
                    }
                    else
                    {
                        Console.WriteLine($"删除失败");
                    }
                }
            }

            {
                var regex = new Regex("^[Aa][Dd][Dd]\\s+(\\d+)$");
                Match match = regex.Match(inputValues);
                if (match.Success)
                {
                    string fpCode = code; // 捕获用户名
                    int height = int.Parse(match.Groups[1].Value);  // 捕获年龄
                    var success = DalOfAddress.FPDetail.AddItem(fpCode, height);
                    if (success)
                    {
                        Console.WriteLine($"新增成功");
                    }
                    else
                    {
                        Console.WriteLine($"新增失败");
                    }
                }
            }
            //   throw new NotImplementedException();
        }

        static void InputAddr()
        {
            DalOfAddress.Connection.SetPassWord(Console.ReadLine());
            Console.WriteLine("输入名称");
            var addrName = Console.ReadLine();

            if (addrName == null)
            {
                return;
            }

            double lon;
            while (true)
            {
                Console.WriteLine("输入经度");
                if (double.TryParse(Console.ReadLine(), out lon))
                {
                    break;
                }
            }
            double lat;
            while (true)
            {
                Console.WriteLine("输入纬度");
                if (double.TryParse(Console.ReadLine(), out lat))
                {
                    break;
                }
            }

            int baseHeight;
            while (true)
            {
                Console.WriteLine("输入海拔高度");
                if (int.TryParse(Console.ReadLine(), out baseHeight))
                {
                    break;
                }
            }

            //int currentHeight;
            //while (true)
            //{
            //    Console.WriteLine("输入当前高度");
            //    if (int.TryParse(Console.ReadLine(), out currentHeight))
            //    {
            //        break;
            //    }
            //}

            string Code = F.GenerateCheckCode(10);

            Console.WriteLine($"产生了编码：  {Code} ");
            DalOfAddress.DBModel.FP fp = new DalOfAddress.DBModel.FP()
            {
                baseHeight = baseHeight,
                lat = lat,
                lon = lon,
                FPCode = Code,
                FPName = addrName
            };
            string msg;
            var success = DalOfAddress.FP.Insert(fp, out msg);
            if (success)
            {
                Console.WriteLine("录入成功");
            }
            else
            {
                Console.WriteLine("录入失败");
            }
            //DalOfAddress.FP.Insert
        }

        static void ShowAll()
        {
            DalOfAddress.FP.ShowAll();
        }
    }
}
