using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DbInput
{
    public class CUDAGPU
    {
        internal static void Cal(int[] costTime, int[] lastFP, int[] resultForSave, int costTimeCount, int FPCount, int unitCount,int[] startDic, int[] endDic)
        {
            throw new NotImplementedException();
        }

        [DllImport(@"E:\Project\CudaObj\CudaRun\x64\Debug\PathCore.dll")]
        static extern IntPtr MCal_Create(int[] costTime, int[] lastFP, int[] resultForSave, int pathCount, int FPCount, int[] startDic, int[] endDic);

    }
}
