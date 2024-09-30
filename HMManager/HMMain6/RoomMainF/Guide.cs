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
        public void NoNeedToLogin(Player player)
        {
            SetParameterIsLogin spil = new SetParameterIsLogin()
            {
                c = "SetParameterIsLogin",
                WebSocketID = player.WebSocketID
                //  TimeOut
            };
            Startup.sendSingleMsg(player.FromUrl, Newtonsoft.Json.JsonConvert.SerializeObject(spil));
        }
    }
}
