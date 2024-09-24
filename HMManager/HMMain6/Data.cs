using ModelBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6
{
    public interface GetRandomPos
    {
        //GetFpCount
        public int GetFpCount();
        public ModelBase.Data.FPPosition GetFpByIndex(int indexValule);
        public List<ModelBase.Data.FPPosition> GetAFromB(int start, int end);
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
    }
    public partial class Data
    {
        List<ModelBase.Data.FPPosition> _allFp;
        Dictionary<int, List<int>> map;
        internal void LoadFPAndMap()
        {
            // throw new NotImplementedException();
            var rootPath = System.IO.Directory.GetCurrentDirectory();
            var fpDictionary = $"{rootPath}\\DBPublish\\";
            this._allFp = GetAllFp(fpDictionary);

            this.map = GetAllConnection(fpDictionary);
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
        public List<FPPosition> GetAFromB(int start, int end)
        {
            throw new NotImplementedException();
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
    }
}
