using CommonClass;
using HMMain6.RoomMainF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6
{
    public class Manager_Connection : Manager
    {
        public Manager_Connection(RoomMain roomMain)
        {
            this.roomMain = roomMain;
        }

        internal bool IsOnline(Player player)
        {
            try
            {
                var obj = new CommandNotify()
                {
                    c = "WhetherOnLine",
                    WebSocketID = player.WebSocketID,
                };
                var url = player.FromUrl;
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                var r = this.sendSingleMsg(url, json);
                if (r == "on")
                {
                    return true;
                }
                else if (r == "off")
                {
                    return false;
                }
                else
                {
                    throw new Exception("");
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
