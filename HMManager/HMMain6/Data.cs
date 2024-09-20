using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6
{
    public partial class Data
    {
        List<ModelBase.Data.FPPosition> _allFp;
        internal void LoadFPAndMap()
        {
            // throw new NotImplementedException();
            var rootPath = System.IO.Directory.GetCurrentDirectory();
            var fpDictionary = $"{rootPath}\\DBPublish\\";
            this._allFp = GetAllFp(fpDictionary);
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
}
