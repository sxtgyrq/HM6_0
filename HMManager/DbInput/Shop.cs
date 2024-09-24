using ModelBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInput
{
    internal class Shop
    {
        internal static void CalculateFP()
        {
            var dicOfFP = CalculateFPStep_Point();
            CalculateFPStep_Connect(dicOfFP);
            {

                // throw new NotImplementedException();
            }
        }
        static Dictionary<string, int> CalculateFPStep_Point()
        {
            var allItems = DalOfAddress.FP.GetAll();
            Dictionary<string, int> indexOfFP = new Dictionary<string, int>();
            for (int i = 0; i < allItems.Count; i++)
            {
                // string saveStr =  allP[i]
                var SaveStr = Newtonsoft.Json.JsonConvert.SerializeObject(allItems[i]); //ServerInfoBLL.F.Serialize(allP[i]);

                var path = $"E:\\DB\\DBPublish\\fp\\p{allItems[i].fPCode}{allItems[i].Height}.txt";//string.Format("{0}/p/{1}.txt", this.VersionNumber, );
                try
                {
                    File.WriteAllText(path, SaveStr);
                    var key = $"{allItems[i].fPCode}{allItems[i].Height}";
                    indexOfFP.Add(key, i);
                }


                //try
                //{
                //    byte[] binaryData = Encoding.UTF8.GetBytes(SaveStr);
                //    MemoryStream requestContent = new MemoryStream(binaryData);

                //    client.PutObject(this.BucketName, Key, requestContent);
                //    string msg = string.Format("完成{0}/{1}", i, allP.Count);
                //    this.cm(msg, 1);
                //    //this.cm()
                //}
                catch (Exception ex)
                {
                    string msg = "";
                    msg += ex.Data.ToString();
                    msg += "=============";
                    msg += ex.HelpLink.ToString();
                    msg += "=============";
                    msg += ex.HResult.ToString();
                    msg += "=============";
                    msg += ex.InnerException.StackTrace;
                    msg += "=============";
                    msg += ex.Message;
                    msg += "=============";
                    msg += ex.Source;
                    msg += "=============";
                    msg += ex.StackTrace;
                    msg += "=============";
                    msg += ex.TargetSite;
                    msg += "=============";
                    Console.WriteLine(msg);
                    Console.ReadLine();
                    // this.cm(msg, 2);
                }
            }

            for (int i = 0; i < allItems.Count; i++)
            {
                var SaveStr = Newtonsoft.Json.JsonConvert.SerializeObject(allItems[i]);
                var path = $"E:\\DB\\DBPublish\\fpindex\\fp_{i}.txt";
                try
                {
                    File.WriteAllText(path, SaveStr);
                }
                catch (Exception ex)
                {
                    string msg = "";
                    msg += ex.Data.ToString();
                    msg += "=============";
                    msg += ex.HelpLink.ToString();
                    msg += "=============";
                    msg += ex.HResult.ToString();
                    msg += "=============";
                    msg += ex.InnerException.StackTrace;
                    msg += "=============";
                    msg += ex.Message;
                    msg += "=============";
                    msg += ex.Source;
                    msg += "=============";
                    msg += ex.StackTrace;
                    msg += "=============";
                    msg += ex.TargetSite;
                    msg += "=============";
                    Console.WriteLine(msg);
                    Console.ReadLine();
                }
            }

            return indexOfFP;
        }

        static void CalculateFPStep_Connect(Dictionary<string, int> dicOfFP)
        {
            Dictionary<int, List<int>> connections = new Dictionary<int, List<int>>();
            var items = DalOfAddress.Tunel.GetAll();

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var startKey = $"{item.FPCodeFrom}{item.StartHeight - item.StartBaseHeight}";
                var endKey = $"{item.FPCodeTo}{item.EndHeight - item.EndBaseHeight}";
                if (connections.ContainsKey(dicOfFP[startKey])) { }
                else
                {
                    var listOfEnd = new List<int>();
                    connections.Add(dicOfFP[startKey], listOfEnd);
                }
                connections[dicOfFP[startKey]].Add(dicOfFP[endKey]);
            }
            var path = $"E:\\DB\\DBPublish\\connectionsData.json";
            var text = Newtonsoft.Json.JsonConvert.SerializeObject(connections);
            File.WriteAllText(path, text);
        }


        internal static void CheckAllConnected()
        {
            var pointData1 = DalOfAddress.FP.GetAll();
            var pointData2 = DalOfAddress.FP.GetAll();
            var lineData = DalOfAddress.Tunel.GetAll();
            // DrawObj.KML.Draw(pointData, lineData);

            for (int i = 0; i < pointData1.Count; i++)
            {
                for (int j = 0; j < pointData2.Count; j++)
                {
                    if (i != j)
                    {
                        var pointA = pointData1[i];
                        var pointB = pointData2[j];
                        if (connected(pointA, pointB, lineData.ToList()))
                        {
                            Console.WriteLine($"{pointA.fPName}({pointA.fPCode}_{pointA.Height})与{pointB.fPName}({pointB.fPCode}_{pointB.Height})连接成功");

                        }
                        else
                        {
                            Console.WriteLine($"{pointA.fPName}({pointA.fPCode}_{pointA.Height})与{pointB.fPName}({pointB.fPCode}_{pointB.Height})连接失败");
                            Console.ReadLine();
                        }
                    }
                }
            }

        }

        private static bool connected(FPPosition pointA, FPPosition pointB, List<Segment> lineData)
        {
            Dictionary<string, bool> connectDic = new Dictionary<string, bool>();
            connectDic.Add($"{pointA.fPCode}{pointA.Height}", true);
            int k = 0;
            while (true)
            {
                var operatedCountBefore = lineData.Count;

                for (int i = 0; i < lineData.Count; i++)
                {
                    if (connectDic.ContainsKey($"{lineData[i].FPCodeFrom}{lineData[i].StartHeight - lineData[i].StartBaseHeight}"))
                    {
                        if (connectDic.ContainsKey($"{lineData[i].FPCodeTo}{lineData[i].EndHeight - lineData[i].EndBaseHeight}"))
                        {


                        }
                        else
                        {
                            connectDic.Add($"{lineData[i].FPCodeTo}{lineData[i].EndHeight - lineData[i].EndBaseHeight}", true);
                        }
                        lineData.RemoveAt(i);
                        break;
                    }
                }

                var operatedCountAfter = lineData.Count;
                if (operatedCountAfter == operatedCountBefore)
                {
                    break;
                }
            }
            return connectDic.ContainsKey($"{pointB.fPCode}{pointB.Height}");
            //  throw new NotImplementedException();
        }
    }
}
