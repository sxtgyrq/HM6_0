using CommonClass;
using HMMain6.interfaceOfHM;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.RoomMainF
{
    public partial class RoomMain
    {
        public string ChargeF(ChargePassData cpd, GetRandomPos grp)
        {
            if (this._Groups.ContainsKey(cpd.GroupKey))
            {
                List<string> notifyMsgs = new List<string>();
                var group = this._Groups[cpd.GroupKey];
                var chargeSuccess = group.Charge(cpd.Key, grp, ref notifyMsgs);

                if (chargeSuccess)
                {
                    var player = group._PlayerInGroup[cpd.Key];
                    if (player.getCar().ability.leftBusiness <= 0)
                    {
                        if (group.taskFineshedTime.ContainsKey(cpd.Key))
                        {
                            this.WebNotify(player, "任务完成后，收集不会记录入个人收入中！");
                        }
                        else
                        {
                            group.taskFineshedTime.Add(cpd.Key, group.costEnegy);
                            group.publishAchievement(player, ref notifyMsgs);
                        }
                    }

                    //foreach (var item in group._PlayerInGroup)
                    //{
                    //    UpdateGoldOjb(item.Key, group.GroupKey, grp, ref notifyMsgs);
                    //}

                }

                Startup.sendSeveralMsgs(notifyMsgs);
            }
            return "";
            //   throw new NotImplementedException();
        }
    }
}
