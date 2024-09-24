using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ModelBase.Data
{
    public class FPPosition
    {
        public string fPCode { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public int baseHeight { get; set; }
        public int Height { get; set; }
        public string BitcoinAddr { get; set; }
        public bool CanGetScore { get; set; }
        public string fPName { get; set; }
        public double ObjInSceneRotation { get; set; }
        public string lineStr
        {
            get
            {
                return
  $"fPCode:{fPCode}  position:{lon},{lat}  baseHeight:{baseHeight}  Height:{Height}  BitcoinAddr:{BitcoinAddr}  CanGetScore:{CanGetScore}  fPName{fPName}";
            }
        }


    }
}
