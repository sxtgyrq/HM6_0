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
        const string AdministratorAddr = "1Hu87mNieKQBZnY89LpeMb5AMJUE8rfExk";
        private void UpdateAdministrator(Player player, ref List<string> notifyMsg)
        {
            if (player.BTCAddress == AdministratorAddr)
            {
                var url = player.FromUrl;
                IsAdministrator lmdb = new IsAdministrator()
                {
                    c = "IsAdministrator",
                    WebSocketID = player.WebSocketID,
                };
                var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(lmdb);
                notifyMsg.Add(url);
                notifyMsg.Add(sendMsg);
            }
        }

        public string CheckIsAdministratorF(CheckIsAdministrator cisA, GetRandomPos grp)
        {
            if (this._Groups.ContainsKey(cisA.GroupKey))
            {
                var group = this._Groups[cisA.GroupKey];
                if (group._PlayerInGroup.ContainsKey(cisA.Key))
                {
                    var player = group._PlayerInGroup[cisA.Key];
                    if (player.BTCAddress == AdministratorAddr)
                    {
                        return "ok";
                    }
                }
            }
            return "ng";
            // throw new NotImplementedException();
        }

       
    }
}
