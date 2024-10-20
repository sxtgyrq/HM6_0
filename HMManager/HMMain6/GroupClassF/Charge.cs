using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.GroupClassF
{
    public partial class GroupClass
    {
        internal bool Charge(string key, GetRandomPos grp, ref List<string> notifyMsgs)
        {
            // throw new NotImplementedException();
            if (this._PlayerInGroup.ContainsKey(key))
            {
                var player = this._PlayerInGroup[key];
                if (player.getCar().targetFpIndex == player.StartFPIndex)
                {
                    player.getCar().Charge(ref notifyMsgs);
                    return true;
                }
            }
            return false;
        }

        Dictionary<string, bool> records = new Dictionary<string, bool>();
        public void publishAchievement(Player playerOperate, ref List<string> notifyMsg)
        {
            //item.Value.CollectMoney
            #region step1 OrderByDescending by item.Value.CollectMoney 
            #endregion



            #region step3 record in DB 
            this.recordRaceTime(playerOperate);
            #endregion

            #region step4 reward by count of diamond 
            // if (this.countOfAskRoad <= 10)
            {
                //  for (int i = 0; i < playerKeys.Count; i++)
                {
                    var player = playerOperate;

                    long addMoney = 0;
                    /*宝石额外奖励*/
                    if (!string.IsNullOrEmpty(player.BTCAddress))
                    {
                        int rewardOfBindWords = 3000;
                        var bindWords = DalOfAddress.HMSever.BindWordInfo.GetWordByAddr(player.BTCAddress);
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
            #endregion 
        }

        public void recordRaceTime(Player playerOperate)
        {

            //  for (int i = 0; i < keys.Count; i++)
            {
                var key = playerOperate.Key;
                if (this.taskFineshedTime.ContainsKey(key))
                {
                    if (this.recordErrorMsgs.ContainsKey(key)) { }
                    else
                    {
                        this.recordErrorMsgs.Add(key, "您还未登录！");
                    }
                    var player = this._PlayerInGroup[key];

                    if (string.IsNullOrEmpty(player.BTCAddress))
                    {
                        this.recordErrorMsgs[key] = "挑战记录未能记录";
                    }
                    else
                    {
                        var item = DalOfAddress.HMSever.TradeReward.GetByStartDate(int.Parse(this.RewardDate));
                        if (item != null)
                        {
                            if (item.waitingForAddition == 0)
                            {
                                this.recordErrorMsgs[key] = $"未能记录于{this.RewardDate}期荣誉";
                            }
                            else
                            {
                                this.recordErrorMsgs[key] = $"记录于{this.RewardDate}期荣誉";
                            }
                        }
                        else
                        {
                            this.recordErrorMsgs[key] = $"不存在日期{this.RewardDate}期奖励，挑战记录未能记录";
                        }
                    }
                }
                else
                {

                }
            }
            List<CommonClass.databaseModel.traderewardtimerecord> traderewardtimerecordRecords = new List<CommonClass.databaseModel.traderewardtimerecord>();
            List<Player> playerList = new List<Player>();

            //  for (int i = 0; i < keys.Count; i++)
            {
                var key = playerOperate.Key;
                if (this.taskFineshedTime.ContainsKey(key))
                {
                    var player = this._PlayerInGroup[key];
                    if (string.IsNullOrEmpty(player.BTCAddress))
                    {
                    }
                    else
                    {
                        var item = DalOfAddress.HMSever.TradeReward.GetByStartDate(int.Parse(this.RewardDate));
                        if (item != null)
                        {
                            if (item.waitingForAddition == 0)
                            {
                            }
                            else
                            {
                                traderewardtimerecordRecords.Add(new CommonClass.databaseModel.traderewardtimerecord()
                                {
                                    applyAddr = player.BTCAddress,
                                    raceStartTime = this.startTime,
                                    raceEndTime = this.startTime.AddSeconds(this.taskFineshedTime[key]), //this.taskFineshedTime[key].AddSeconds(i / 10.0),
                                    raceMember = this.groupNumber,
                                    rewardGiven = 0,
                                    startDate = int.Parse(this.RewardDate),
                                });
                                playerList.Add(player);
                            }
                        }
                    }
                }

            }
            if (traderewardtimerecordRecords.Count == 1 && playerList.Count == 1)
            {
                int findResultCount;
                var r = DalOfAddress.HMSever.traderewardtimerecord.Add2(traderewardtimerecordRecords, out findResultCount);

                if (r)
                {
                    for (int i = 0; i < traderewardtimerecordRecords.Count; i++)
                    {
                        this.records.Add(playerList[i].Key, true);
                        var player = playerList[i];
                        if (findResultCount == 0)
                        {
#warning 这里暂停打破记录奖励！
                            if (false)
                            {
                                that.WebNotify(player, this.groupNumber > 1 ? "你刷新了记录。" : "你刷新了记录");
                                rewardAnother(player);
                            }
                        }
                    }

                }
            }

        } 
        private void rewardAnother(Player player)
        {
            that.breakRecordReward.reward(player); 
        }  
    }
}
