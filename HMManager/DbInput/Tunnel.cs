
using SharpKml.Dom;
using static CommonClass.ResistanceDisplay_V3;

namespace DbInput
{
    public class Tunnel
    {
        public static void CalculatePath()
        {
            int calIndexStarted = 0;
            int mCalCount = 3;
            Console.WriteLine($"输入并行计算的数量");
            if (int.TryParse(Console.ReadLine(), out mCalCount))
            { }
            else
            {
                mCalCount = 3;
            }
            var items = DalOfAddress.Tunel.GetAll();
            var fpItems = DalOfAddress.FP.GetAll();

            List<int> resultForSave = new List<int>();

            Dictionary<string, int> startDic = new Dictionary<string, int>();
            for (var i = 0; i < fpItems.Count; i++)
            {
                var fpItem = fpItems[i];
                var stringKey = $"{fpItem.fPCode}{fpItem.Height}";
                startDic.Add(stringKey, i);
            }

            List<int> startPosition = new List<int>(items.Count);
            List<int> endPosition = new List<int>(items.Count);

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var startKey = $"{item.FPCodeFrom}{item.StartHeight - item.StartBaseHeight}";
                var endKey = $"{item.FPCodeTo}{item.EndHeight - item.EndBaseHeight}";
                startPosition.Add(startDic[startKey]);
                endPosition.Add(startDic[endKey]);
            }

            while (calIndexStarted < fpItems.Count)
            {
                mCalCount = Math.Min(mCalCount, fpItems.Count - calIndexStarted);

                List<int> costTime = new List<int>(items.Count * mCalCount);
                List<int> lastFP = new List<int>(fpItems.Count * mCalCount);//这里用于存储最后一个地址。
                                                                            //    List<int> LastRecordResultForSave = new List<int>(fpItems.Count * mCalCount);

                for (int i = calIndexStarted; i < calIndexStarted + mCalCount; i++)
                {
                    for (int j = 0; j < fpItems.Count; j++)
                    {
                        var fpItem = fpItems[j];
                        if (j == i)
                        {
                            var stringKey = $"{fpItem.fPCode}{fpItem.Height}";
                            lastFP.Add(startDic[stringKey]);
                        }
                        else
                        {
                            lastFP.Add(-1);
                        }
                        //  LastRecordResultForSave.Add(-1);
                    }
                    for (int j = 0; j < items.Count; j++)
                    {
                        var item = items[j];
                        var l = CommonClass.Geography.getLengthOfTwoPoint.GetDistance(item.StartLat, item.StartLon, item.StartHeight, item.EndLat, item.EndLon, item.EndHeight);

                        int time;
                        if (item.IsRegionTransfer)
                        {
                            time = 1;
                        }
                        else
                            time = Convert.ToInt32(l / 1000 / item.Speed * 3600);
                        if (time < 1) time = 1;

                        costTime.Add(time);
                    }
                }
                //   var LastRecordResultForSaveArray = LastRecordResultForSave.ToArray();
                var CUDAResult = CUDAGPU.Cal(costTime.ToArray(), lastFP.ToArray(), items.Count * mCalCount, fpItems.Count * mCalCount, mCalCount * 1, startPosition.ToArray(), endPosition.ToArray());

                for (int i = 0; i < CUDAResult.Length; i++)
                {
                    if (CUDAResult[i] < 0)
                    {
                        throw new Exception("错误的结果");
                    }
                    else if (CUDAResult[i] >= fpItems.Count)
                    {
                        throw new Exception("错误的结果");
                    }
                    else
                    {
                        resultForSave.Add(CUDAResult[i]);
                    }
                }

                calIndexStarted += mCalCount;
            }
            if (resultForSave.Count != fpItems.Count * fpItems.Count)
            {
                throw new Exception("错误的结果");
            }
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(resultForSave);
            File.WriteAllText("resultOrder.json", json);

        }
        public static int GetCostTime(ModelBase.Data.Segment item)
        {
            var length = CommonClass.Geography.getLengthOfTwoPoint.GetDistance(item.StartLat, item.StartLon, item.StartHeight, item.EndLat, item.EndLon, item.EndHeight);
            int time;
            if (item.IsRegionTransfer)
            {
                time = 1;
            }
            else
                time = Convert.ToInt32(length / 1000 / item.Speed * 3600);
            if (time < 1) time = 1;
            return time;
        }
        internal static void ReadResult()
        {
            // throw new NotImplementedException();
            var fpItems = DalOfAddress.FP.GetAll();
            int? startIndex = null, endIndex = null;
            var json = File.ReadAllText("resultOrder.json");
            if (json != null)
            {
                List<int> Data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(json);

                while (true)
                {
                    Console.WriteLine($"有{fpItems.Count}出地点，请输入起始地点");
                    var input = Console.ReadLine();
                    if (input == null)
                    {
                        continue;
                    }
                    else if (input.ToUpper() == "EXIT")
                    {
                        break;
                    }
                    else if (startIndex == null)
                    {
                        startIndex = int.Parse(input);
                        Console.WriteLine($"输入了起点{startIndex},{fpItems[startIndex.Value].lineStr}");
                    }
                    else if (endIndex == null)
                    {
                        endIndex = int.Parse(input);
                        Console.WriteLine($"输入了终点{endIndex},{fpItems[endIndex.Value].lineStr}");

                        List<int> resultRead = new List<int>();

                        var insertValue = endIndex.Value;
                        resultRead.Insert(0, insertValue);

                        for (int i = 0; i < fpItems.Count; i++)
                        {
                            insertValue = Data[fpItems.Count * startIndex.Value + insertValue];

                            resultRead.Insert(0, insertValue);
                            if (insertValue == startIndex.Value)
                            {
                                break;
                            }

                        }
                        for (int i = 0; i < resultRead.Count; i++)
                        {
                            Console.WriteLine($"{fpItems[resultRead[i]].lineStr}");
                        }
                        Console.WriteLine("以上为路径");

                        startIndex = null; endIndex = null;
                    }
                }
            }
        }
    }
}
