using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalOfAddress.DBModel
{
    public class FP
    {
        public string FPName { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public int baseHeight { get; set; }
        public string FPCode { get; set; }

    }
}
