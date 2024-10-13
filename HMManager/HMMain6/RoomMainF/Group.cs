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
        ////   const string AddSuffix = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        ////  enum CostOrSum { Cost, Sum }
        ///// <summary>
        ///// 这里要通知前台，值发生了变化。
        ///// </summary>
        ///// <param name="player"></param>
        ///// <param name="car"></param>
        ///// <param name="notifyMsgs"></param>
        ///// <param name="pType"></param>
        public void GroupValueChanged2_0(Player player, Car car, ref List<string> notifyMsgs, string pType)
        {
            //var carIndexStr = car.IndexString;
            //long costValue = 0;

            player.getCar().ability.SpeedChanged(player, car, ref notifyMsgs, "speed");
            long showValue = 0;
            switch (pType)
            {
                case "enegy":
                    {
                        showValue = player.Group.costEnegy;
                    }; break;
            }
            var obj = new BradCastGroupAbility
            {
                c = "BradCastGroupAbility",
                WebSocketID = player.WebSocketID,
                pType = pType,
                showValue = showValue,
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            notifyMsgs.Add(player.FromUrl);
            notifyMsgs.Add(json);
        }
    }
}
