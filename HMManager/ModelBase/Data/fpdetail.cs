using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelBase.Data
{
    public class FpDetail
    {
        public string BitcoinAddr { get; set; }
        public string FPCode { get; set; }
        public int Height { get; set; }
        public bool CanGetScore { get; set; }
        /// <summary>
        /// 单位是弧度，不是角度，这样可以直接在Three.js中不经变换直接使用。
        /// </summary>
        public double ObjInSceneRotation { get; set; }
    }
}
