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
<<<<<<< HEAD
                            group.publishAchievement(player, ref notifyMsgs);
=======
                            group.publishAchievement(ref notifyMsgs);
>>>>>>> 5bbb0cdf3f891fa27c4db97f97ae3529c7f58980
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


<<<<<<< HEAD
      
=======
        private void publishAchievement(ref List<string> notifyMsg)
        {
            //item.Value.CollectMoney
            #region step1 OrderByDescending by item.Value.CollectMoney
            var playersOrderByCollectCount = this._PlayerInGroup.ToArray().OrderByDescending(item => item.Value.CollectMoney).ToList();
            #endregion

            #region step2 add raceTime by count of Askroad
            if (this.countOfAskRoad > 0)
            {
                if (this._groupNumber == 1)
                {
                    this.taskFineshedTime[true] = DateTime.Now.AddMinutes(this.countOfAskRoad);
                }
                else if (this._groupNumber == 2)
                {
                    this.taskFineshedTime[true] = DateTime.Now.AddMinutes(this.countOfAskRoad / 2.0);
                }
                else if (this._groupNumber == 3)
                {
                    this.taskFineshedTime[true] = DateTime.Now.AddMinutes(this.countOfAskRoad / 3.0);
                }
                else if (this._groupNumber == 4)
                {
                    this.taskFineshedTime[true] = DateTime.Now.AddMinutes(this.countOfAskRoad / 4.0);
                }
                else if (this._groupNumber == 5)
                {
                    this.taskFineshedTime[true] = DateTime.Now.AddMinutes(this.countOfAskRoad / 5.0);
                }

                for (int i = 0; i < playersOrderByCollectCount.Count; i++)
                {
                    var player = playersOrderByCollectCount[i].Value;
                    if (this.countOfAskRoad > 0)
                    {
                        if (this._groupNumber == 1)
                        {
                            that.WebNotify(player, $"您在完成任务中，额外进行了{this.countOfAskRoad}次问道，成绩多记{this.countOfAskRoad}分钟。");
                        }
                        else if (this._groupNumber == 2)
                        {
                            that.WebNotify(player, $"您与队友在完成任务中，额外进行了{this.countOfAskRoad}次问道，成绩多记{(this.countOfAskRoad / 2.0).ToString("f2")}分钟。");
                        }
                        else if (this._groupNumber == 3)
                        {
                            that.WebNotify(player, $"您与队友在完成任务中，额外进行了{this.countOfAskRoad}次问道，成绩多记{(this.countOfAskRoad / 3.0).ToString("f2")}分钟。");
                        }
                        else if (this._groupNumber == 4)
                        {
                            that.WebNotify(player, $"您与队友在完成任务中，额外进行了{this.countOfAskRoad}次问道，成绩多记{(this.countOfAskRoad / 4.0).ToString("f2")}分钟。");
                        }
                        else if (this._groupNumber == 5)
                        {
                            that.WebNotify(player, $"您与队友在完成任务中，额外进行了{this.countOfAskRoad}次问道，成绩多记{(this.countOfAskRoad / 5.0).ToString("f2")}分钟。");
                        }
                    }
                }
            }
            #endregion

            #region step3 record in DB
            List<string> playerKeys = new List<string>();
            for (int i = 0; i < playersOrderByCollectCount.Count; i++)
            {
                var player = playersOrderByCollectCount[i].Value;
                playerKeys.Add(player.Key);
            }
            this.recordRaceTime(playerKeys);
            #endregion

            #region step4 reward by count of diamond 
            if (this.countOfAskRoad <= 10)
            {
                for (int i = 0; i < playerKeys.Count; i++)
                {
                    var player = this._PlayerInGroup[playerKeys[i]];

                    long addMoney = 0;
                    /*宝石额外奖励*/
                    if (!string.IsNullOrEmpty(player.BTCAddress))
                    {
                        int rewardOfBindWords = 3000;
                        var bindWords = DalOfAddress.BindWordInfo.GetWordByAddr(player.BTCAddress);
                        if (!string.IsNullOrEmpty(bindWords))
                        {
                            that.WebNotify(player, $"绑定了“{bindWords}”奖励额外{rewardOfBindWords / 100}.{(rewardOfBindWords / 10) % 10}{(rewardOfBindWords / 1) % 10}积分");
                            addMoney += rewardOfBindWords;
                        }
                        else
                        {
                            that.WebNotify(player, $"未关联任何绑定词，未能获得额外{rewardOfBindWords / 100}.{(rewardOfBindWords / 10) % 10}{(rewardOfBindWords / 1) % 10}的积分奖励");
                        }
                    }
                    var diamondCollectCount = player.getCar().ability.DiamondCount();
                    if (diamondCollectCount > 0)
                    {
                        if (diamondCollectCount > 10)
                        {
                            /*
                             * 这里的目的，是为了防止人们恶意刷宝石！
                             */
                            int rewardOfDiamondCollect = 10 * 200;
                            addMoney += rewardOfDiamondCollect;
                            that.WebNotify(player, $"您收集了{diamondCollectCount}颗宝石，获得了额外{rewardOfDiamondCollect / 100}.{(rewardOfDiamondCollect / 10) % 10}{(rewardOfDiamondCollect / 1) % 10}的积分奖励");
                            addMoney += rewardOfDiamondCollect;
                        }
                        else
                        {
                            int rewardOfDiamondCollect = diamondCollectCount * 200;
                            addMoney += rewardOfDiamondCollect;
                            that.WebNotify(player, $"您收集了{diamondCollectCount}颗宝石，获得了额外{rewardOfDiamondCollect / 100}.{(rewardOfDiamondCollect / 10) % 10}{(rewardOfDiamondCollect / 1) % 10}的积分奖励");
                            addMoney += rewardOfDiamondCollect;
                        }
                    }
                    if (addMoney > 0)
                        player.MoneySet(player.Money + addMoney, ref notifyMsg);
                }

            }
            else
            {
                for (int i = 0; i < playerKeys.Count; i++)
                {
                    var player = this._PlayerInGroup[playerKeys[i]];
                    if (!string.IsNullOrEmpty(player.BTCAddress))
                    {
                        that.WebNotify(player, $"问道次数≥10，未能获得绑定词奖励");

                    }
                    var diamondCollectCount = player.getCar().ability.DiamondCount();
                    if (diamondCollectCount > 0)
                    {
                        that.WebNotify(player, $"问道次数≥10，未能获得宝石收集额外奖励");
                    }
                }
                /*宝石额外奖励*/

            }
            #endregion



            //foreach (var item in this._PlayerInGroup)
            //{

            //    this.recordRaceTime(player.Key);
            //    if (this.countOfAskRoad <= 10)
            //    {
            //        long addMoney = 0;
            //        /*宝石额外奖励*/
            //        if (!string.IsNullOrEmpty(player.BTCAddress))
            //        {
            //            int rewardOfBindWords = 3000;
            //            var bindWords = DalOfAddress.BindWordInfo.GetWordByAddr(player.BTCAddress);
            //            if (!string.IsNullOrEmpty(bindWords))
            //            {
            //                that.WebNotify(player, $"绑定了“{bindWords}”奖励额外{rewardOfBindWords / 100}.{(rewardOfBindWords / 10) % 10}{(rewardOfBindWords / 1) % 10}积分");
            //                addMoney += rewardOfBindWords;
            //            }
            //            else
            //            {
            //                that.WebNotify(player, $"未关联任何绑定词，未能获得额外{rewardOfBindWords / 100}.{(rewardOfBindWords / 10) % 10}{(rewardOfBindWords / 1) % 10}的积分奖励");
            //            }
            //        }
            //        var diamondCollectCount = player.getCar().ability.DiamondCount();
            //        if (diamondCollectCount > 0)
            //        {
            //            if (diamondCollectCount > 10)
            //            {
            //                /*
            //                 * 这里的目的，是为了防止人们恶意刷宝石！
            //                 */
            //                int rewardOfDiamondCollect = 10 * 200;
            //                addMoney += rewardOfDiamondCollect;
            //                that.WebNotify(player, $"您收集了{diamondCollectCount}颗宝石，获得了额外{rewardOfDiamondCollect / 100}.{(rewardOfDiamondCollect / 10) % 10}{(rewardOfDiamondCollect / 1) % 10}的积分奖励");
            //                addMoney += rewardOfDiamondCollect;
            //            }
            //            else
            //            {
            //                int rewardOfDiamondCollect = diamondCollectCount * 200;
            //                addMoney += rewardOfDiamondCollect;
            //                that.WebNotify(player, $"您收集了{diamondCollectCount}颗宝石，获得了额外{rewardOfDiamondCollect / 100}.{(rewardOfDiamondCollect / 10) % 10}{(rewardOfDiamondCollect / 1) % 10}的积分奖励");
            //                addMoney += rewardOfDiamondCollect;
            //            }
            //        }
            //        if (addMoney > 0)
            //            player.MoneySet(player.Money + addMoney, ref notifyMsg);
            //    }
            //    else
            //    {
            //        /*宝石额外奖励*/
            //        if (!string.IsNullOrEmpty(player.BTCAddress))
            //        {
            //            that.WebNotify(player, $"问道次数≥10，未能获得绑定词奖励");

            //        }
            //        var diamondCollectCount = player.getCar().ability.DiamondCount();
            //        if (diamondCollectCount > 0)
            //        {
            //            that.WebNotify(player, $"问道次数≥10，未能获得宝石收集额外奖励");
            //        }
            //    }
            //}
            //{

            //}
        }
>>>>>>> 5bbb0cdf3f891fa27c4db97f97ae3529c7f58980
    }
}
