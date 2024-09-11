
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
                List<int> LastRecordResultForSave = new List<int>(fpItems.Count * mCalCount);

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
                        LastRecordResultForSave.Add(-1);
                    }
                    for (int j = 0; j < items.Count; j++)
                    {
                        var item = items[j];
                        var l = CommonClass.Geography.getLengthOfTwoPoint.GetDistance(item.StartLat, item.StartLon, item.StartHeight, item.EndLat, item.EndLon, item.EndHeight);
                        var time = Convert.ToInt32(l / 1000 / item.Speed * 3600);
                        if (time < 1) time = 1;
                        costTime.Add(time);
                    }
                }
                var LastRecordResultForSaveArray = LastRecordResultForSave.ToArray();
                CUDAGPU.Cal(costTime.ToArray(), lastFP.ToArray(), LastRecordResultForSaveArray, items.Count * mCalCount, fpItems.Count * mCalCount, mCalCount, startPosition.ToArray(), endPosition.ToArray());

                calIndexStarted += mCalCount;
            }


            //List<int> isBegain = new List<int>(items.Count * mCalCount);
            //List<int> isEnded = new List<int>(items.Count * mCalCount);

            // List<int> lastFP = new List<int>(fpItems.Count * mCalCount);//这里用于存储最后一个地址。
            //var startIndex = 0;


            //for (var i = 0; i < items.Count; i++)
            //{
            //    var item = items[i];
            //    var startKey = $"{item.FPCodeFrom}{item.StartHeight - item.StartBaseHeight}";
            //    var endKey = $"{items[startIndex].FPCodeFrom}{items[startIndex].StartHeight - items[startIndex].StartBaseHeight}";
            //    startPosition.Add(startDic[startKey]);
            //    endPosition.Add(startDic[endKey]);
            //}

            //for (var i = 0; i < items.Count; i++)
            //{
            //    var item = items[i];
            //    if (item.Speed == 0)
            //    {
            //        //costTime.Add(item.sta)
            //        throw new Exception("");
            //    }
            //    else
            //    {
            //        var l = CommonClass.Geography.getLengthOfTwoPoint.GetDistance(item.StartLat, item.StartLon, item.StartHeight, item.EndLat, item.EndLon, item.EndHeight);
            //        var time = Convert.ToInt32(l / 1000 / item.Speed * 3600);
            //        if (time < 1) time = 1;
            //        costTime.Add(time);
            //    }
            //    if (
            //        $"{item.FPCodeFrom}{item.StartHeight - item.StartBaseHeight}" ==
            //        $"{items[startIndex].FPCodeFrom}{items[startIndex].StartHeight - items[startIndex].StartBaseHeight}")
            //        isBegain.Add(1);
            //    else
            //        isBegain.Add(0);

            //    isEnded.Add(0);





            //}

            //  throw new NotImplementedException();

        }
    }
}
