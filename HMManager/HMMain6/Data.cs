using Aliyun;
using CommonClass;
using ModelBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static CommonClass.BradCastSelections;

namespace HMMain6
{
    public interface GetRandomPos
    {
        //GetFpCount
        public int GetFpCount();
        public ModelBase.Data.FPPosition GetFpByIndex(int indexValule);
        public List<ModelBase.Data.FPPosition> GetAFromB(int start, int end, out List<int> fpIndex);

        public List<ModelBase.Data.FPPosition> GetAFromB(int start, int end)
        {
            List<int> fpIndex;
            return this.GetAFromB(start, end, out fpIndex);
        }
        //   public OssModel.SaveRoad.RoadInfo GetItemRoadInfo(OssModel.MapGo.nyrqPosition nyrqPosition);
        //public OssModel.SaveRoad.RoadInfo GetItemRoadInfo(string roadCode, int roadOrder);
        //public OssModel.SaveRoad.RoadInfo GetItemRoadInfo(string roadCode, int roadOrder, out bool existed);
        //public void GetAFromBPoint(List<OssModel.MapGo.nyrqPosition> dataResult, OssModel.MapGo.nyrqPosition position, int speed, ref List<int> result, ref int startT, bool speedImproved, RoomMainF.RoomMain rmain);
        public int FindIndexByID(string fpCode, int height)
        {
            var count = this.GetFpCount();
            for (int i = 0; i < count; i++)
            {
                var fp = this.GetFpByIndex(i);
                if (fp.fPCode == fpCode.Trim() && fp.Height == height)
                {
                    return i;
                }
            }
            return -1;
        }

        // Data.UserSDouyinGroup GetDouyinNameByFpID(string fastenPositionID, ref Random rm);
        string getMusic(string fastenPositionID);
        List<int> GetSelections(int index);
        List<int> GetEnegy(int targetFpIndex);
        CompassPosition GetCompassPosition(int targetFpIndex);
        CompassPosition GetGoldOjb(int targetFpIndex);
        CompassPosition GetTurbine(int targetFpIndex);
        CompassPosition Satelite(int ttargetFpIndexi);
        CompassPosition GetBtcPosition(int targetFpIndex);
        CompassPosition GetRedDiamond(int targetFpIndex);
        CompassPosition GetBlueDiamond(int targetFpIndex);
        long GetSatoshi(string bTCAddress, int targetFpIndex);
        //  void LoadStock(ModelStock sa);
    }
    public partial class Data
    {
        ConfigClass config;
        Dictionary<string, bool> objPlaceWhereWhetherHasValue = new Dictionary<string, bool>();
        Dictionary<string, CompassPosition> objPlaceWhereValue = new Dictionary<string, CompassPosition>();
        List<ModelBase.Data.FPPosition> _allFp;
        Dictionary<int, List<int>> map;
        Dictionary<int, List<int>> enegy;

        List<int> allPathData;
        internal void LoadFPAndMap()
        {
            // throw new NotImplementedException();
            var rootPath = System.IO.Directory.GetCurrentDirectory();
            var fpDictionary = $"{rootPath}\\DBPublish\\";
            this._allFp = GetAllFp(fpDictionary);

            this.map = GetAllConnection(fpDictionary);
            this.enegy = GetAllEnegy(fpDictionary);

            this.allPathData = GetAllPathData(fpDictionary);


            var configJson = File.ReadAllText($"{rootPath}\\config\\config.json");
            config = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigClass>(configJson);
            objPlaceWhereWhetherHasValue = new Dictionary<string, bool>();
            objPlaceWhereValue = new Dictionary<string, CompassPosition>();
        }

        private List<int> GetAllPathData(string fpDictionary)
        {
            var path = $"{fpDictionary}resultOrder.json";
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(content);
                if (data.Count == this.GetFpCount() * this.GetFpCount())
                {
                    return data;
                }
                else
                {
                    throw new Exception($"resultOrder.json数据错误");
                }
            }
            else
            {
                throw new Exception($"不存在文件{path}");
            }
        }

        private Dictionary<int, List<int>> GetAllEnegy(string fpDictionary)
        {
            var path = $"{fpDictionary}enegyData.json";
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, List<int>>>(content);
            }
            else
            {
                throw new Exception($"不存在文件{path}");
            }
        }

        private Dictionary<int, List<int>> GetAllConnection(string fpDictionary)
        {
            //   throw new NotImplementedException();
            var path = $"{fpDictionary}connectionsData.json";
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, List<int>>>(content);
            }
            else
            {
                throw new Exception($"不存在文件{path}");
            }
        }

        private List<ModelBase.Data.FPPosition> GetAllFp(string fpDictionary)
        {
            List<ModelBase.Data.FPPosition> result = new List<ModelBase.Data.FPPosition>();
            int startIndex = 0;

            var path = $"{fpDictionary}fpindex\\fp_{startIndex}.txt";
            while (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var fpObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelBase.Data.FPPosition>(json);
                result.Add(fpObj);
                startIndex++;
                path = $"{fpDictionary}fpindex\\fp_{startIndex}.txt";
            }
            Console.WriteLine($"输入了{result.Count}个地点");

            return result;
            //fpDictionary
            // throw new NotImplementedException();
            //$"E:\\DB\\DBPublish\\fpindex\\fp_{i}.txt";
        }


    }

    public partial class Data : GetRandomPos
    {
        public List<FPPosition> GetAFromB(int start, int end, out List<int> fpIndex)
        {
            fpIndex = new List<int>();
            var ItemLength = this.GetFpCount();
            List<int> resultRead = new List<int>();

            var insertValue = end;
            resultRead.Insert(0, insertValue);

            for (int i = 0; i < this.GetFpCount(); i++)
            {
                insertValue = this.allPathData[ItemLength * start + insertValue];

                resultRead.Insert(0, insertValue);
                if (insertValue == start)
                {
                    break;
                }

            }
            List<FPPosition> result = new List<FPPosition>();

            for (int i = 0; i < resultRead.Count; i++)
            {
                result.Add(this.GetFpByIndex(resultRead[i]));
                fpIndex.Add(resultRead[i]);
            }
            return result;
        }

        public FPPosition GetFpByIndex(int indexValule)
        {
            while (indexValule < 0)
                indexValule += this._allFp.Count;
            while (indexValule > this._allFp.Count)
                indexValule -= this._allFp.Count;
            return this._allFp[indexValule];
        }

        public int GetFpCount()
        {
            return this._allFp.Count;
            //throw new NotImplementedException();
        }

        public string getMusic(string fastenPositionID)
        {
            return "";
            //throw new NotImplementedException();
        }

        public List<int> GetSelections(int index)
        {
            return this.map[index];
            //  throw new NotImplementedException();
        }
        public List<int> GetEnegy(int index)
        {
            return this.enegy[index];
            //  throw new NotImplementedException();
        }

        public CompassPosition GetCompassPosition(int targetFpIndex)
        {
            const string objName = "compass";
            if (this.config.isDebug == 1)
            {
                var fp = this.GetFpByIndex(targetFpIndex);
                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}{objName}.json";
                if (File.Exists(fpDictionary))
                {
                    var json = File.ReadAllText(fpDictionary);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                }
            }
            else
            {

                var fp = this.GetFpByIndex(targetFpIndex);

                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}{objName}.json";
                if (this.objPlaceWhereWhetherHasValue.ContainsKey(fpDictionary))
                {
                    if (this.objPlaceWhereWhetherHasValue[fpDictionary])
                    {
                        return this.objPlaceWhereValue[fpDictionary];
                    }
                }
                else
                {
                    if (File.Exists(fpDictionary))
                    {
                        var json = File.ReadAllText(fpDictionary);

                        var p = Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                        this.objPlaceWhereValue.Add(fpDictionary, p);
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, true);
                        return p;
                    }
                    else
                    {
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, false);
                    }

                }

            }
            return new CompassPosition()
            {
                x = 6,
                y = 5,
                z = 4,
                rx = 0,
                ry = 0,
                rz = 0,
                s = 0.2
            };
        }

        public CompassPosition GetGoldOjb(int targetFpIndex)
        {
            const string objName = "goldobj";
            if (this.config.isDebug == 1)
            {
                var fp = this.GetFpByIndex(targetFpIndex);
                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}{objName}.json";
                if (File.Exists(fpDictionary))
                {
                    var json = File.ReadAllText(fpDictionary);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                }
            }
            else
            {

                var fp = this.GetFpByIndex(targetFpIndex);

                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}{objName}.json";
                if (this.objPlaceWhereWhetherHasValue.ContainsKey(fpDictionary))
                {
                    if (this.objPlaceWhereWhetherHasValue[fpDictionary])
                    {
                        return this.objPlaceWhereValue[fpDictionary];
                    }
                }
                else
                {
                    if (File.Exists(fpDictionary))
                    {
                        var json = File.ReadAllText(fpDictionary);

                        var p = Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                        this.objPlaceWhereValue.Add(fpDictionary, p);
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, true);
                        return p;
                    }
                    else
                    {
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, false);
                    }

                }

            }
            return new CompassPosition()
            {
                x = 6,
                y = 0,
                z = 4,
                rx = 0,
                ry = 0,
                rz = 0,
                s = 0.2
            };
        }

        public CompassPosition GetRedDiamond(int targetFpIndex)
        {
            const string objName = "reddiamond";
            if (this.config.isDebug == 1)
            {
                var fp = this.GetFpByIndex(targetFpIndex);
                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}{objName}.json";
                if (File.Exists(fpDictionary))
                {
                    var json = File.ReadAllText(fpDictionary);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                }
            }
            else
            {

                var fp = this.GetFpByIndex(targetFpIndex);

                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}{objName}.json";
                if (this.objPlaceWhereWhetherHasValue.ContainsKey(fpDictionary))
                {
                    if (this.objPlaceWhereWhetherHasValue[fpDictionary])
                    {
                        return this.objPlaceWhereValue[fpDictionary];
                    }
                }
                else
                {
                    if (File.Exists(fpDictionary))
                    {
                        var json = File.ReadAllText(fpDictionary);

                        var p = Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                        this.objPlaceWhereValue.Add(fpDictionary, p);
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, true);
                        return p;
                    }
                    else
                    {
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, false);
                    }

                }

            }

            return GetGoldOjb(targetFpIndex);
            // return new CompassPosition()
            //{
            //    x = 12,
            //    y = 0,
            //    z = 16,
            //    rx = 0,
            //    ry = 0,
            //    rz = 0,
            //    s = 0.4
            //};
        }

        public CompassPosition GetBlueDiamond(int targetFpIndex)
        {
            const string objName = "bluediamond";
            if (this.config.isDebug == 1)
            {
                var fp = this.GetFpByIndex(targetFpIndex);
                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}{objName}.json";
                if (File.Exists(fpDictionary))
                {
                    var json = File.ReadAllText(fpDictionary);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                }
            }
            else
            {

                var fp = this.GetFpByIndex(targetFpIndex);

                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}{objName}.json";
                if (this.objPlaceWhereWhetherHasValue.ContainsKey(fpDictionary))
                {
                    if (this.objPlaceWhereWhetherHasValue[fpDictionary])
                    {
                        return this.objPlaceWhereValue[fpDictionary];
                    }
                }
                else
                {
                    if (File.Exists(fpDictionary))
                    {
                        var json = File.ReadAllText(fpDictionary);

                        var p = Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                        this.objPlaceWhereValue.Add(fpDictionary, p);
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, true);
                        return p;
                    }
                    else
                    {
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, false);
                    }

                }

            }
            return new CompassPosition()
            {
                x = 12,
                y = 0,
                z = 16,
                rx = 0,
                ry = 0,
                rz = 0,
                s = 0.4
            };
        }

        public CompassPosition GetTurbine(int targetFpIndex)
        {
            const string objName = "turbine";
            if (this.config.isDebug == 1)
            {
                var fp = this.GetFpByIndex(targetFpIndex);
                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}{objName}.json";
                if (File.Exists(fpDictionary))
                {
                    var json = File.ReadAllText(fpDictionary);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                }
            }
            else
            {

                var fp = this.GetFpByIndex(targetFpIndex);

                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}{objName}.json";
                if (this.objPlaceWhereWhetherHasValue.ContainsKey(fpDictionary))
                {
                    if (this.objPlaceWhereWhetherHasValue[fpDictionary])
                    {
                        return this.objPlaceWhereValue[fpDictionary];
                    }
                }
                else
                {
                    if (File.Exists(fpDictionary))
                    {
                        var json = File.ReadAllText(fpDictionary);

                        var p = Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                        this.objPlaceWhereValue.Add(fpDictionary, p);
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, true);
                        return p;
                    }
                    else
                    {
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, false);
                    }

                }

            }
            return new CompassPosition()
            {
                x = -241,
                y = -36,
                z = 197,
                rx = 0,
                ry = 3.2999999999999963,
                rz = 0,
                s = 4.048788018654354
            };
        }

        int k;
        public CompassPosition Satelite(int ttargetFpIndexi)
        {
            if (this.config.isDebug == 1)
            {
                var fp = this.GetFpByIndex(ttargetFpIndexi);
                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}satelite.json";
                if (File.Exists(fpDictionary))
                {
                    var json = File.ReadAllText(fpDictionary);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                }
            }
            else
            {

                var fp = this.GetFpByIndex(ttargetFpIndexi);

                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}satelite.json";
                if (this.objPlaceWhereWhetherHasValue.ContainsKey(fpDictionary))
                {
                    if (this.objPlaceWhereWhetherHasValue[fpDictionary])
                    {
                        return this.objPlaceWhereValue[fpDictionary];
                    }
                }
                else
                {
                    if (File.Exists(fpDictionary))
                    {
                        var json = File.ReadAllText(fpDictionary);

                        var p = Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                        this.objPlaceWhereValue.Add(fpDictionary, p);
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, true);
                        return p;
                    }
                    else
                    {
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, false);
                    }

                }

            }
            return new CompassPosition()
            {
                x = 384,
                y = 384,
                z = 44,
                rx = 0,
                ry = 1.072330292425316,
                rz = 0,
                s = 1
            };
        }

        public CompassPosition GetBtcPosition(int targetFpIndex)
        {
            if (this.config.isDebug == 1)
            {
                var fp = this.GetFpByIndex(targetFpIndex);
                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}btc.json";
                if (File.Exists(fpDictionary))
                {
                    var json = File.ReadAllText(fpDictionary);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                }
            }
            else
            {

                var fp = this.GetFpByIndex(targetFpIndex);

                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fp.fPCode}{fp.Height}btc.json";
                if (this.objPlaceWhereWhetherHasValue.ContainsKey(fpDictionary))
                {
                    if (this.objPlaceWhereWhetherHasValue[fpDictionary])
                    {
                        return this.objPlaceWhereValue[fpDictionary];
                    }
                }
                else
                {
                    if (File.Exists(fpDictionary))
                    {
                        var json = File.ReadAllText(fpDictionary);

                        var p = Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(json);
                        this.objPlaceWhereValue.Add(fpDictionary, p);
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, true);
                        return p;
                    }
                    else
                    {
                        this.objPlaceWhereWhetherHasValue.Add(fpDictionary, false);
                    }

                }

            }
            return new CompassPosition()
            {
                x = 500,
                y = 0,
                z = 500,
                rx = 0,
                ry = 5.21233029242531,
                rz = 0,
                s = 0.3
            };
        }

        public long GetSatoshi(string bTCAddress, int targetFpIndex)
        {
            var fp = this.GetFpByIndex(targetFpIndex);
            if (string.IsNullOrEmpty(fp.BitcoinAddr))
            {
                return 0;
            }
            else if (this.modelsStocks.ContainsKey(fp.BitcoinAddr))
            {
                if (this.modelsStocks[fp.BitcoinAddr].stocks.ContainsKey(bTCAddress))
                {
                    return this.modelsStocks[fp.BitcoinAddr].stocks[bTCAddress];
                }
                else return 0;
            }
            else
            {
                return 0;
            }
            //  throw new NotImplementedException();
        }

        //  object modelStockLock = new object();

    }

    public partial class Data
    {
        internal void DealWithSql(SqlCommand sq)
        {
            var sql = sq.Sql;

            string rotationPattern = @"SET\s+ObjInSceneRotation\s*=\s*([\d.]+)\s";
            Regex rotationRegex = new Regex(rotationPattern);
            Match rotationMatch = rotationRegex.Match(sql);
            double rotationValue;
            if (rotationMatch.Success)
            {
                rotationValue = double.Parse(rotationMatch.Groups[1].Value);
                Console.WriteLine("ObjInSceneRotation: " + rotationValue);
            }
            else
            {
                return;
            }

            string fpCodePattern = @"FPCode\s*=\s*'([A-Z]{10})'\s";
            Regex fpCodeRegex = new Regex(fpCodePattern);
            Match fpCodeMatch = fpCodeRegex.Match(sql);
            string fpCodeValue;
            if (fpCodeMatch.Success)
            {
                fpCodeValue = fpCodeMatch.Groups[1].Value.Trim();
                Console.WriteLine("FPCode: " + fpCodeValue);
            }
            else
            {
                return;
            }

            string heightPattern = @"Height\s*=\s*(\d+);";
            Regex heightRegex = new Regex(heightPattern);
            Match heightMatch = heightRegex.Match(sql);
            int height;
            if (heightMatch.Success)
            {
                string heightValue = heightMatch.Groups[1].Value;
                height = Convert.ToInt32(heightValue);
                Console.WriteLine("Height: " + heightValue);
            }
            else { return; }

            for (var i = 0; i < this._allFp.Count; i++)
            {
                if (this._allFp[i].fPCode == fpCodeValue && this._allFp[i].Height == height)
                {
                    this._allFp[i].ObjInSceneRotation = rotationValue;
                    var filePath = "SQLNeedTOUpdate.txt";
                    File.AppendAllText(filePath, $"#{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}{Environment.NewLine}");
                    File.AppendAllText(filePath, $"{sql}{Environment.NewLine}");
                }
            }
        }

        internal void DealWithUploadJson(UploadPositionJson upj)
        {
            string fpCodePattern = @"([A-Z]{10})";

            // 匹配Height 整数
            string heightPattern = @"(\d+)";

            // 匹配第三项 goldobj, satelite, turbine, compass
            string thirdItemPattern = @"(goldobj|satelite|turbine|compass|btc)";

            // 综合正则表达式，匹配fileName
            string pattern = fpCodePattern + heightPattern + thirdItemPattern + @"\.json";

            Regex regex = new Regex(pattern);
            Match match = regex.Match(upj.fileName);

            if (match.Success)
            {
                string fpCode = match.Groups[1].Value;  // 提取fpCode
                int height = int.Parse(match.Groups[2].Value);  // 提取Height
                string thirdItem = match.Groups[3].Value;  // 提取第三项

                Console.WriteLine("fpCode: " + fpCode);
                Console.WriteLine("Height: " + height);
                Console.WriteLine("Third Item: " + thirdItem);


                var p = Newtonsoft.Json.JsonConvert.DeserializeObject<CompassPosition>(upj.jsonString);

                var rootPath = System.IO.Directory.GetCurrentDirectory();
                var fpDictionary = $"{rootPath}\\DBPublish\\objplace\\{fpCode}{height}{thirdItem}.json";
                File.WriteAllText(fpDictionary, upj.jsonString);

                if (this.objPlaceWhereWhetherHasValue.ContainsKey(fpDictionary))
                {
                    if (this.objPlaceWhereWhetherHasValue[fpDictionary])
                    {
                        this.objPlaceWhereValue[fpDictionary] = p;
                    }
                    else
                    {
                        this.objPlaceWhereWhetherHasValue[fpDictionary] = true;
                        this.objPlaceWhereValue.Add(fpDictionary, p);
                    }
                }
                else
                {
                    this.objPlaceWhereWhetherHasValue.Add(fpDictionary, true);
                    this.objPlaceWhereValue.Add(fpDictionary, p);
                }

            }
            else
            {
                Console.WriteLine("No match found.");
            }
        }
    }

    public partial class Data
    {
        object modelStockLock = new object();
        internal Dictionary<string, long> GetDataOfOriginalStock(string bussinessAddr)
        {
            lock (modelStockLock)
            {
                if (modelsBussinessAddr.ContainsKey(bussinessAddr))
                {
                    return modelsStocks[modelsBussinessAddr[bussinessAddr]].stocksOriginal;
                }
                else
                {
                    return new Dictionary<string, long>();
                }
            }
            // throw new NotImplementedException();
        }
        Dictionary<string, string> modelsBussinessAddr = new Dictionary<string, string>();
        public Dictionary<string, CommonClass.ModelStock> modelsStocks = new Dictionary<string, CommonClass.ModelStock>();

        internal void LoadStock(CommonClass.ModelStock sa)
        {
            lock (modelStockLock)
            {
                if (modelsStocks.ContainsKey(sa.modelID))
                {
                    modelsStocks[sa.modelID] = sa;
                }
                else
                {
                    modelsStocks.Add(sa.modelID, sa);
                    modelsBussinessAddr.Add(sa.bussinessAddress, sa.modelID);
                }
            }
            //Consol.WriteLine($"接受到stock信息");
        }
    }
}
