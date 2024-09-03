using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelBase.Data
{
    public class Segment
    {
        //B.lon AS StartLon,B.lat AS StartLat,A.HeightFrom AS StartHeight,
        // C.lon AS EndLon,C.lat AS EndLat,A.HeightTo AS EndHeight,
        //	A.Speed,A.FPCodeFrom,A.FPCodeTo
        public double StartLon { get; set; }
        public double StartLat { get; set; }
        public int StartHeight { get; set; }
        public int StartBaseHeight { get; set; }
        public double EndLon { get; set; }
        public double EndLat { get; set; }
        public int EndHeight { get; set; }
        public int EndBaseHeight { get; set; }
        public int Speed { get; set; }
        public string FPCodeFrom { get; set; }
        public string FPCodeTo { get; set; }
        public string SegCode
        {
            get
            {
                return $"{FPCodeFrom}{StartHeight}{FPCodeTo}{EndHeight}";
            }
        }
        public string Detail { get { return $"{SegCode}_{Speed}"; } }
    }
}
