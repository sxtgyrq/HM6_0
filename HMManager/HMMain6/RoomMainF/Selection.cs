using CommonClass;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.RoomMainF
{
    public partial class RoomMain
    {
        private void UpdateSelection(string key, string groupKey, GetRandomPos getRandomPosObj, ref List<string> notifyMsgs)
        {
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                if (group._PlayerInGroup.ContainsKey(key))
                {
                    var player = group._PlayerInGroup[key];
                    var targetFpIndex = player.getCar().targetFpIndex;
                    var target = getRandomPosObj.GetSelections(targetFpIndex);
                    var obj = GetItemSelections(player.WebSocketID, getRandomPosObj.GetFpByIndex(targetFpIndex));
                    for (var i = 0; i < target.Count; i++)
                    {
                        var item = getRandomPosObj.GetFpByIndex(target[i]);
                        var baseFp = getRandomPosObj.GetFpByIndex(targetFpIndex);
                        var x = Math.Sin((item.lon - baseFp.lon) / 180 * Math.PI) * CommonClass.Geography.getLengthOfTwoPoint.EARTH_RADIUS * Math.Cos(baseFp.lat / 180 * Math.PI);
                        var y = Math.Sin((item.lat - baseFp.lat) / 180 * Math.PI) * CommonClass.Geography.getLengthOfTwoPoint.EARTH_RADIUS;
                        var z = (item.Height + item.baseHeight) - (baseFp.Height + baseFp.baseHeight);
                        obj.selections.Add(new BradCastSelections.FPItem()
                        {
                            x = Convert.ToInt32(x),
                            y = Convert.ToInt32(y),
                            z = z,
                            c = getRandomPosObj.GetFpByIndex(target[i]).fPCode,
                            h = getRandomPosObj.GetFpByIndex(target[i]).Height
                        });
                    }

                    var url = player.FromUrl;
                    var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    notifyMsgs.Add(url);
                    notifyMsgs.Add(sendMsg);  
                }
            }

        }
        private BradCastSelections GetItemSelections(int webSocketID, ModelBase.Data.FPPosition fp)
        {
            var obj = new BradCastSelections
            {
                c = "BradCastSelections",
                WebSocketID = webSocketID,
                fp = fp,
                selections = new List<BradCastSelections.FPItem>()
            };
            return obj;
        }
    }
}
