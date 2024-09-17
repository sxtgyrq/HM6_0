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
        internal static void Cal(int[] costTime, int[] lastFP, int costTimeCount, int FPCount, int unitCount, int[] startDic, int[] endDic)
        {
            var p = MCal_Create(costTime, lastFP, costTimeCount, FPCount, unitCount, startDic, endDic);

            int length = FPCount; // 数组的长度
            IntPtr ptr = MCal_LastFPResult(p);

            // 创建一个托管的int数组
            int[] managedArray = new int[length];

            // 将非托管内存中的数据复制到托管数组中
            System.Runtime.InteropServices.Marshal.Copy(ptr, managedArray, 0, length);

            Console.WriteLine("输出结果：");
            for (int i = 0; i < managedArray.Length; i++)
            {
                Console.Write($"{managedArray[i]} ");
            }
            Console.WriteLine("结果完毕：按回车继续");
            Console.ReadLine();
            MCal_Delete(p);
        }

        [DllImport(@"E:\Project\HM_6\CUDA\LibToCal\LibToCal.dll")]
        static extern IntPtr MCal_Create(int[] costTime, int[] lastFP, int pathCount, int FPCount, int unitCount, int[] startDic, int[] endDic);

        [DllImport(@"E:\Project\HM_6\CUDA\LibToCal\LibToCal.dll")]
        static extern IntPtr MCal_Delete(IntPtr map);

        [DllImport(@"E:\Project\HM_6\CUDA\LibToCal\LibToCal.dll")]
        static extern IntPtr MCal_LastFPResult(IntPtr map);

    }
}
