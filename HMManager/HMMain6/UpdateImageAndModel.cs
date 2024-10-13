using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6
{
    internal class UpdateImageAndModel
    {
        public static void updateImageAndModel()
        {
            //  throw new NotImplementedException();
            loadImageToOSS();
            Console.WriteLine($"图片上传完毕");
            Console.ReadLine();
        }

        private static void loadImageToOSS()
        {
            var dt = new Data();
            dt.LoadFPAndMap();
            for (int i = 0; i < dt.GetFpCount(); i++)
            {
                var fp = dt.GetFpByIndex(i);
                var code = fp.fPCode;
                var height = fp.Height;

                string fpCode = code;
                //regex.Matches(pathValue).
                var rootPath = System.IO.Directory.GetCurrentDirectory();
                //document.getElementById("base64Image").src = "data:image/jpeg;base64," + base64String;
                string px, py, pz, nx, ny, nz;

                string[] stringType = { "px", "py", "pz", "nx", "ny", "nz" };
                Dictionary<string, string> picValue = new Dictionary<string, string>();

                bool exitData = true;

                for (int j = 0; j < stringType.Length; j++)
                {
                    bool exitPic;
                    var b64 = GetBase64(rootPath, fpCode, height, stringType[j], out exitPic);
                    picValue.Add(stringType[j], b64);
                    exitData = exitData && exitPic;
                }
                if (exitData)
                {
                    var obj = new
                    {
                        px = picValue["px"],
                        py = picValue["py"],
                        pz = picValue["pz"],
                        nx = picValue["nx"],
                        ny = picValue["ny"],
                        nz = picValue["nz"],
                        fpCode = fpCode,
                        height = height,
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                    //var path=
                    var success = Aliyun.Json.AddAndCheck(
                        $"h6_0/bgImg/{fpCode}_{height}.json",
                        json,
                        (string json1, string json2) =>
                        {
                            return CommonClass.Random.GetSha256FromStr(json1) == CommonClass.Random.GetSha256FromStr(json2);
                        });
                    if (success)
                    {
                        Console.WriteLine($"h6_0/bgImg/{fpCode}_{height}.json  成功");
                    }
                    else
                    {
                        Console.WriteLine($"h6_0/bgImg/{fpCode}_{height}.json  失败,按回车继续");
                        Console.ReadLine();
                    }
                }
                else
                {
                    if (Aliyun.Json.Existed($"h6_0/bgImg/{fpCode}_{height}.json"))
                    {
                        Console.WriteLine($"h6_0/bgImg/{fpCode}_{height}.json  本次未能修改。已存在，本次无数据");
                    }
                    else
                    {
                        Console.WriteLine($"h6_0/bgImg/{fpCode}_{height}.json  没有上传数据，数据库中也没有数据。想要默认数据覆盖,输入Y");
                        if (Console.ReadLine().ToUpper() == "Y")
                        {
                            var obj = new
                            {
                                px = picValue["px"],
                                py = picValue["py"],
                                pz = picValue["pz"],
                                nx = picValue["nx"],
                                ny = picValue["ny"],
                                nz = picValue["nz"],
                                fpCode = fpCode,
                                height = height,
                            };
                            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                            //var path=
                            var success = Aliyun.Json.AddAndCheck(
                                $"h6_0/bgImg/{fpCode}_{height}.json",
                                json,
                                (string json1, string json2) =>
                                {
                                    return CommonClass.Random.GetSha256FromStr(json1) == CommonClass.Random.GetSha256FromStr(json2);
                                });
                            if (success)
                            {
                                Console.WriteLine($"h6_0/bgImg/{fpCode}_{height}.json  成功");
                            }
                            else
                            {
                                Console.WriteLine($"h6_0/bgImg/{fpCode}_{height}.json  失败,按回车继续");
                                Console.ReadLine();
                            }
                        };
                    }
                }
            }
        }

        private static string GetBase64(string rootPath, string fpCode, int height, string picType, out bool exitPic)
        {
            var filePath = $"{rootPath}\\bgImg\\{fpCode}\\h{height}\\{picType}.jpg";
            if (File.Exists(filePath))
            {
                exitPic = true;
            }
            else
            {
                filePath = $"{rootPath}\\bgImg\\{picType}.jpg";
                exitPic = false;
            }
            var bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }
    }
}
