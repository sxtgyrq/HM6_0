using CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.GroupClassF
{
    public partial class GroupClass
    {
      

        internal bool ReturnHomeF(string key, GetRandomPos grp, ref List<string> notifyMsg)
        {
            var player = this._PlayerInGroup[key];
            player.getCar().targetFpIndexSet(player.StartFPIndex, ref notifyMsg);
            player.getCar().ability.Refresh(player, player.getCar(), ref notifyMsg);

            return true;
        }

       
    }
}
