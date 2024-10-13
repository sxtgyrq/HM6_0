using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.RoomMainF
{
    public partial class RoomMain// : interfaceOfHM.Market
    {
        public void priceChanged(string priceType, long value)
        {
            //  throw new Exception();

            //List<string> msgs = new List<string>();
            //lock (this.PlayerLock)
            //{
            //    foreach (var item in this._Players)
            //    {
            //        var role = item.Value;
            //        if (role.playerType == Player.PlayerType.player)
            //        {
            //            var player = (Player)role;
            //            var obj = new BradDiamondPrice
            //            {
            //                c = "BradDiamondPrice",
            //                WebSocketID = player.WebSocketID,
            //                priceType = priceType,
            //                price = value
            //            };
            //            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            //            msgs.Add(player.FromUrl);
            //            msgs.Add(json);
            //        }
            //    }
            //}
            //Startup.sendSeveralMsgs(msgs); 
        }
    }
}
