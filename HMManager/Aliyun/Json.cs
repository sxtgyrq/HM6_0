﻿using Aliyun.Acs.Core.Auth;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Aliyun
{
    public class Json
    {
        public static bool Add(string path, string json)
        {
            if (!AliyunOSSHelper.loadSuccess)
                AliyunOSSHelper.LoadKey();

            return AliyunOSSHelper.PutString("yrqmodeldata", path, json);
        }
        public delegate bool IsSame(string json1, string json2);
        public static bool AddAndCheck(string path, string json, IsSame isSameF)
        {
            if (!AliyunOSSHelper.loadSuccess)
                AliyunOSSHelper.LoadKey();
            if (AliyunOSSHelper.ExistsObject("yrqmodeldata", path))
            {
                var jsonSaving = AliyunOSSHelper.GetString("yrqmodeldata", path);
                if (isSameF(jsonSaving, json))
                {
                    return true;
                }
                else
                {
                    return AliyunOSSHelper.PutString("yrqmodeldata", path, json);
                }
            }
            else
            {
                return AliyunOSSHelper.PutString("yrqmodeldata", path, json);
            }
        }

        public static bool Delete(string path)
        {
            if (!AliyunOSSHelper.loadSuccess)
                AliyunOSSHelper.LoadKey();
            return AliyunOSSHelper.DeleteObject("yrqmodeldata", path);
        }

        public static bool Existed(string path)
        {
            if (!AliyunOSSHelper.loadSuccess)
                AliyunOSSHelper.LoadKey();
            return AliyunOSSHelper.ExistsObject("yrqmodeldata", path);
        }

        public static string Read(string path)
        {
            if (!AliyunOSSHelper.loadSuccess)
                AliyunOSSHelper.LoadKey();
            return AliyunOSSHelper.GetString("yrqmodeldata", path);
        }
    }
    public class ByteData
    {
        public static void Add(string path, byte[] data)
        {
            if (!AliyunOSSHelper.loadSuccess)
                AliyunOSSHelper.LoadKey();

            var success = AliyunOSSHelper.PutByte("yrqmodeldata", path, data);
            if (success)
            {
                Console.WriteLine($"{path}存储成功！");
            }
            // AliyunOSSHelper.
            //AliyunOSSHelper.ExistsObject()
        }
    }
}
