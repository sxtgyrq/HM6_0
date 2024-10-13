using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HMMain6.AbilityAndState;

namespace HMMain6.GroupClassF
{
    public partial class GroupClass
    {
        long _valueOfEnegy = 0;

        public AbilityChangedF EnegyChanged;
        public long costEnegy
        {
            get
            { return this._valueOfEnegy; }
            set
            {
                this._valueOfEnegy = value;
                List<string> notifyMsg = new List<string>();
                foreach (var item in this._PlayerInGroup)
                {
                    var player = item.Value;
                    var car = player.getCar();
                    EnegyChanged(player, car, ref notifyMsg, "enegy");
                }
                Startup.sendSeveralMsgs(notifyMsg);
            }
        }
    }
}
