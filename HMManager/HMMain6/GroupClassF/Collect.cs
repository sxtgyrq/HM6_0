﻿using CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.GroupClassF
{
    public partial class GroupClass
    {
        internal void CheckCollectState(string key)
        {
            List<string> sendMsgs = new List<string>();

            if (this._PlayerInGroup.ContainsKey(key))
                for (var i = 0; i < 38; i++)
                {
                    if (this._PlayerInGroup[key].CollectPosition[i] == this._collectPosition[i])
                    { }
                    else
                    {
                        if (this._PlayerInGroup[key].playerType == Player.PlayerType.player)
                        {
                            var infomation = this.GetCollectInfomation(((Player)this._PlayerInGroup[key]).WebSocketID, i);
                            var url = ((Player)this._PlayerInGroup[key]).FromUrl;
                            var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(infomation);
                            sendMsgs.Add(url);
                            sendMsgs.Add(sendMsg);
                        }
                        this._PlayerInGroup[key].CollectPosition[i] = this._collectPosition[i];
                    }
                }

            Startup.sendSeveralMsgs(sendMsgs);
        }
        public BradCastCollectInfoDetail_v2 GetCollectInfomation(int webSocketID, int collectIndex)
        {
            var obj = new BradCastCollectInfoDetail_v2
            {
                c = "BradCastCollectInfoDetail_v2",
                WebSocketID = webSocketID,
                Fp = Program.dt.GetFpByIndex(this._collectPosition[collectIndex]),
                collectMoney = this.GetCollectReWard(collectIndex),
                collectIndex = collectIndex
            };
            return obj;
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


        internal bool HasGold(int targetFpIndex, out int collectIndex)
        {
            collectIndex = -1;
            foreach (var item in this._collectPosition)
            {
                if (item.Value == targetFpIndex)
                {
                    collectIndex = item.Key; break;
                }
            }
            return collectIndex != -1;

            // return this._collectPosition.ContainsValue(targetFpIndex);
            //   throw new NotImplementedException();
        }
        internal bool HasGold(int targetFpIndex)
        {
            int collectIndex;
            return HasGold(targetFpIndex, out collectIndex);
            // return this._collectPosition.ContainsValue(targetFpIndex);
            //   throw new NotImplementedException();
        }


        internal bool CollectF(string key, GetRandomPos grp, ref List<string> notifyMsg)
        {
            if (this._PlayerInGroup.ContainsKey(key))
            {
                var player = this._PlayerInGroup[key];
                int collectIndex;
                if (HasGold(player.getCar().targetFpIndex, out collectIndex))
                {
                    if (string.IsNullOrEmpty(player.getCar().ability.diamondInCar))
                    {
                        if (player.getCar().ability.leftVolume >= 100)
                        {
                            this._collectPosition[collectIndex] = this.GetRandomPosition(false, grp);
                            player.getCar().ability.setCostVolume(player.getCar().ability.costVolume + (player.improvementRecord.CollectIsDouble ? 200 : 100), player, player.getCar(), ref notifyMsg);

                            player.collectMagicChanged(player, ref notifyMsg);
                            if (player.improvementRecord.CollectIsDouble)
                            {
                                player.improvementRecord.reduceAttack(player, ref notifyMsg);
                            }

                            int k = 0;
                            if (IsOutTheAbility(grp))
                            {
                                while (IsOutTheAbility(grp))
                                {
                                    k++;
                                    this.promoteMilePosition = GetRandomPosition(true, grp);
                                    if (k < 10)
                                    {
                                        continue;
                                    }
                                    else { break; }
                                }
                                foreach (var item in this._PlayerInGroup)
                                {
                                    item.Value.rm.UpdateRedDiamond(item.Key, this.GroupKey, grp, ref notifyMsg);
                                }
                            }
                            return true;
                        }
                        else
                        {
                            this.that.WebNotify(player, "无人已满载，回基地点击分车复命！");
                        }
                    }
                    else
                    {
                        this.that.WebNotify(player, "无人机栽有其他设备或物品，不能执行收集任务！");
                    }
                }
            }
            return false;
            //   throw new NotImplementedException();
        }
    }
}
