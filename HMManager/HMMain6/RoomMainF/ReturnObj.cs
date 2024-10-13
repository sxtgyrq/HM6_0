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
        private void UpdataSatelite(string key, string groupKey, GetRandomPos grp, ref List<string> notifyMsg)
        {
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                if (group._PlayerInGroup.ContainsKey(key))
                {
                    var player = group._PlayerInGroup[key];
                    GetSatelite(player, grp, ref notifyMsg);
                }
            }
        }
        public void GetSatelite(Player player, GetRandomPos grp, ref List<string> notifyMsg)
        {
            var ti = player.getCar().targetFpIndex;
            if (ti >= 0)
            {
                var fs = grp.GetFpByIndex(ti);
                var infomation = GetBradCastSateliteInfomation(player.WebSocketID, fs);
                infomation.hasValue = true;
                infomation.position = grp.Satelite(ti);
                var url = player.FromUrl;
                var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(infomation);
                notifyMsg.Add(url);
                notifyMsg.Add(sendMsg);
            }
            else
                Console.WriteLine($"GetBackground,出现了意料之外的情况！ti={ti}");

        }

        private BradCastSatelite GetBradCastSateliteInfomation(int webSocketID, ModelBase.Data.FPPosition fp)
        {
            var obj = new BradCastSatelite
            {
                c = "BradCastSatelite",
                WebSocketID = webSocketID,
            };
            return obj;
        }
    }
}
