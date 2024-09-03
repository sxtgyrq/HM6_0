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

    }
}
