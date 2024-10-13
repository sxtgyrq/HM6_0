using CommonClass;
using ModelBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.RoomMainF
{
    public partial class RoomMain : interfaceOfHM.Collect
    {
        /// <summary>
        /// 获取收集金钱的状态
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private void CheckCollectState(string key, string groupKey)
        {
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                //  lock (group.PlayerLock_)
                {
                    group.CheckCollectState(key);
                }
            }
            //  throw new Exception();

            //List<string> sendMsgs = new List<string>();
            //lock (this.PlayerLock)
            //    if (this._Players.ContainsKey(key))
            //        for (var i = 0; i < 38; i++)
            //        {
            //            if (this._Players[key].CollectPosition[i] == this._collectPosition[i])
            //            { }
            //            else
            //            {
            //                if (this._Players[key].playerType == Player.PlayerType.player)
            //                {
            //                    var infomation = Program.rm.GetCollectInfomation(((Player)this._Players[key]).WebSocketID, i);
            //                    var url = ((Player)this._Players[key]).FromUrl;
            //                    var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(infomation);
            //                    sendMsgs.Add(url);
            //                    sendMsgs.Add(sendMsg);
            //                }
            //                this._Players[key].CollectPosition[i] = this._collectPosition[i];
            //            }
            //        }

            //Startup.sendSeveralMsgs(sendMsgs); 
        }

        public int GetCollectReWard(int collectIndex)
        {
            switch (collectIndex)
            {
                case 0:
                    {
                        return 1;
                    };
                case 1:
                case 2:
                    {
                        return 1;
                    }
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    {
                        return 1;
                    }
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                    {
                        return 1;
                    }
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                    { return 1; }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

        public void CheckAllPlayersCollectState(GroupClassF.GroupClass group)
        {
            // group.CheckAllPlayersCollectState();
            //throw new Exception("");

            //var all = getGetAllRoles();
            //for (var i = 0; i < all.Count; i++)
            //{
            //    CheckCollectState(all[i].Key);
            //}
        }
        public enum MileResultReason
        {
            /// <summary>
            /// 里程充足
            /// </summary>
            Abundant,
            /// <summary>
            /// 不能到达
            /// </summary>
            CanNotReach,
            /// <summary>
            /// 能到达但是不能返回
            /// </summary>
            CanNotReturn,
            MoneyIsNotEnougt,
            NearestIsMoneyWhenPromote,
            NearestIsMoneyWhenAttack,
            NotHasGroup
        }
        public string updateCollect(SetCollect sc, GetRandomPos grp)
        {
            //Action p = () =>
            //{
            //    // this.collectE.updateCollect(sc, grp);
            //};
            //WaitForAPeriodOfTime(p, 10 * 1000);
            return "";
        }

        private void WaitForAPeriodOfTime(Action p, int waitTime)
        {
            Thread th = new Thread(() =>
            {
                p();
                Thread.Sleep(waitTime);
            });
            th.Start();
        }


        private void UpdateGoldOjb(string key, string groupKey, GetRandomPos grp, ref List<string> notifyMsgs)
        {
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                if (group._PlayerInGroup.ContainsKey(key))
                {
                    var player = group._PlayerInGroup[key];
                    var targetFpIndex = player.getCar().targetFpIndex;
                    //  var target = getRandomPosObj.GetSelections(targetFpIndex);

                    var position = grp.GetGoldOjb(targetFpIndex);
                    var obj = GetItemGoldObj(player.WebSocketID, position);
                    obj.hasValue = group.HasGold(targetFpIndex);
                    if (player.BTCAddress == AdministratorAddr)
                    {
                        if (grp.GetFpByIndex(targetFpIndex).CanGetScore)
                            obj.hasValue = true;
                    }

                    var url = player.FromUrl;
                    var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    notifyMsgs.Add(url);
                    notifyMsgs.Add(sendMsg);
                    //if (group.HasGold(targetFpIndex))
                    //{

                    //}
                    //else
                    //{

                    //}
                }
            }
        }
        private BradCastGoldObj GetItemGoldObj(int webSocketID, CompassPosition position)
        {
            var obj = new BradCastGoldObj
            {
                c = "BradCastGoldObj",
                WebSocketID = webSocketID,
                position = position
            };
            return obj;
        }

        public string CollectF(CollectPassData cpd, GetRandomPos grp)
        {
            if (this._Groups.ContainsKey(cpd.GroupKey))
            {
                List<string> notifyMsgs = new List<string>();
                var group = this._Groups[cpd.GroupKey];
                var collectSuccess = group.CollectF(cpd.Key, grp, ref notifyMsgs);

                if (collectSuccess)
                {
                    foreach (var item in group._PlayerInGroup)
                    {
                        UpdateGoldOjb(item.Key, group.GroupKey, grp, ref notifyMsgs);
                    }
                }

                Startup.sendSeveralMsgs(notifyMsgs);
            }
            return "";
        }
    }
}
