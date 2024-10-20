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
            //if (this._Groups.ContainsKey(groupKey))
            //{
            //    var group = this._Groups[groupKey];
            //    if (group._PlayerInGroup.ContainsKey(key))
            //    {
            //        var player = group._PlayerInGroup[key];
            //        var targetFpIndex = player.getCar().targetFpIndex;
            //        //  var target = getRandomPosObj.GetSelections(targetFpIndex);


            //        // var obj = GetItemGoldObj(player.WebSocketID, position);
            //        // obj.hasValue = group.HasGold(targetFpIndex);
            //        //if (player.BTCAddress == AdministratorAddr)
            //        {
            //            if (string.IsNullOrEmpty(grp.GetFpByIndex(targetFpIndex).BitcoinAddr))
            //            {
            //                var position = grp.GetBtcPosition(targetFpIndex);
            //                //  var fs = Program.dt.GetFpByIndex(ti);
            //                var infomation = Program.rm.GetBackgroundInfomation(player.WebSocketID, fs);
            //                var url = player.FromUrl;
            //                var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(infomation);
            //                notifyMsgs.Add(url);
            //                notifyMsgs.Add(sendMsg);
            //            }
            //            // obj.hasValue = true;
            //        }

            //        //var url = player.FromUrl;
            //        //var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            //        //notifyMsgs.Add(url);
            //        //notifyMsgs.Add(sendMsg);
            //        //if (group.HasGold(targetFpIndex))
            //        //{

            //        //}
            //        //else
            //        //{

            //        //}
            //    }
            //}
        }
    }
}
