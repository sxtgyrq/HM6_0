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
        private void UpdateBitcoinAddr(string key, string groupKey, GetRandomPos grp, ref List<string> notifyMsgs)
        {
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                if (group._PlayerInGroup.ContainsKey(key))
                {
                    var player = group._PlayerInGroup[key];
                    var targetFpIndex = player.getCar().targetFpIndex;
                    {
                        if (!string.IsNullOrEmpty(grp.GetFpByIndex(targetFpIndex).BitcoinAddr))
                        {
                            // var position = grp.GetBtcPosition(targetFpIndex);
                            //  var fs = Program.dt.GetFpByIndex(ti);
                            var infomation = Program.rm.GetBitcoinAddr(player.WebSocketID, grp.GetFpByIndex(targetFpIndex).BitcoinAddr);
                            infomation.position = grp.GetBtcPosition(targetFpIndex);
                            infomation.hasValue = true;
                            var url = player.FromUrl;
                            var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(infomation);
                            notifyMsgs.Add(url);
                            notifyMsgs.Add(sendMsg);
                        }
                        else
                        {
                            var infomation = Program.rm.GetBitcoinAddr(player.WebSocketID, grp.GetFpByIndex(targetFpIndex).BitcoinAddr);
                            infomation.position = grp.GetBtcPosition(targetFpIndex);
                            infomation.hasValue = false;
                            var url = player.FromUrl;
                            var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(infomation);
                            notifyMsgs.Add(url);
                            notifyMsgs.Add(sendMsg);
                        }
                        // obj.hasValue = true;
                    }

                    //var url = player.FromUrl;
                    //var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    //notifyMsgs.Add(url);
                    //notifyMsgs.Add(sendMsg);
                    //if (group.HasGold(targetFpIndex))
                    //{

                    //}
                    //else
                    //{

                    //}
                }
            }
        }

        private BradCastBitcoinAddr GetBitcoinAddr(int webSocketID, string addr)
        {
            var obj = new BradCastBitcoinAddr
            {
                c = "BradCastBitcoinAddr",
                WebSocketID = webSocketID,
                bitcoinAddr = addr
            };
            return obj;
        }
    }
}
