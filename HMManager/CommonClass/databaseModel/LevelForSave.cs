﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.databaseModel
{
    public class LevelForSave
    {
        public string BtcAddr { get; set; }
        public string Signature { get; set; }

        /// <summary>
        /// 如果此字符串为空字符串，要执行Insert Sql语句。不为空要执行Update Sql语句
        /// </summary>
        public string TimeStampStr { get; set; }
        public int Level { get; set; }
    }
}
