using CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.RoomMainF
{
    public partial class RoomMain
    {
        public void GetBackground(Player player, ref List<string> notifyMsg)
        {
            var ti = player.getCar().targetFpIndex;
            if (ti >= 0)
            {
                var fs = Program.dt.GetFpByIndex(ti);
                var infomation = Program.rm.GetBackgroundInfomation(player.WebSocketID, fs);
                var url = player.FromUrl;
                var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(infomation);
                notifyMsg.Add(url);
                notifyMsg.Add(sendMsg);
            }
            else
                Console.WriteLine($"GetBackground,出现了意料之外的情况！ti={ti}");

        }

        private BradCastBackground GetBackgroundInfomation(int webSocketID, ModelBase.Data.FPPosition fp)
        {
            var obj = new BradCastBackground
            {
                c = "BradCastBackground",
                WebSocketID = webSocketID,
                fp = fp
            };
            return obj;
        }
        private void UpdateBackground(string key, string groupKey, GetRandomPos getRandomPosObj, ref List<string> notifyMsg)
        {
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                if (group._PlayerInGroup.ContainsKey(key))
                {
                    var player = group._PlayerInGroup[key];
                    GetBackground(player, ref notifyMsg);

                } 
            }
        }
    }
}
