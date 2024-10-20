﻿using CommonClass;
using HMMain6.interfaceOfEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.GroupClassF
{
    public partial class GroupClass
    {


        internal bool UpdateCurrentPosition(Player player, GetRandomPos grp, WebSelectPassData wspd, ref List<string> notifyMsg)
        {
            grp.GetFpByIndex(player.getCar().targetFpIndex);
            var target = grp.GetSelections(player.getCar().targetFpIndex);

            if (player.getCar().targetFpIndex == -1)
            {
                this.that.WebNotify(player, "当前无人机处于坠毁状态，请点击复活。");
            }
            else
            {
                var targetFind = -1;
                int index = -1;
                for (int i = 0; i < target.Count; i++)
                {
                    if (grp.GetFpByIndex(target[i]).fPCode == wspd.code && grp.GetFpByIndex(target[i]).Height == wspd.height)
                    {
                        targetFind = target[i];
                        index = i;
                        break;
                    }
                }
                if (targetFind >= 0)
                {
                    var enegy = grp.GetEnegy(player.getCar().targetFpIndex);
                    var costEnegy = enegy[index];

                    var timeCostToWait = DateTime.Now - player.LastActionTime;
                    var sumNeed = costEnegy + Convert.ToInt32(timeCostToWait.TotalSeconds);
                    if (player.getCar().ability.leftMile > sumNeed)
                    {
                        player.getCar().ability.setCostMiles(
                            player.getCar().ability.costMiles + sumNeed,
                            player,
                            player.getCar(),
                            ref notifyMsg);
                        player.getCar().targetFpIndexSet(targetFind, ref notifyMsg);
                        var fpFound = grp.GetFpByIndex(targetFind);
                        if (fpFound.CanGetScore)
                            player.rm.WebNotify(player, $"到达了{fpFound.fPName}");
                        else
                            player.rm.WebNotify(player, $"到达了{fpFound.fPName}上方{fpFound.Height}米处。");
                        player.rm.WebNotify(player, $"等待消耗{Convert.ToInt32(timeCostToWait.TotalSeconds)}能量。飞行消耗{costEnegy}能量。一共");
                        // player.rm.WebNotify(player, $"");
                        // var  
                        player.LastActionTime = DateTime.Now;

                        return true;
                    }
                    else
                    {
                        if (player.StartFPIndex == player.getCar().targetFpIndex)
                        {
                            that.WebNotify(player, "点击旋转的风车，可以给无人机补充能量！");
                        }
                        else
                        {
                            if (player.Group.groupNumber == 1)
                            {
                                that.WebNotify(player, "能量不足！你可以点击卫星来回城。");
                            }
                            else
                            {
                                that.WebNotify(player, "能量不足！你可以点击卫星来回城，或者请求队友来救援！");
                            }
                        }
                    }
                }
            }
            return false;
            //throw new NotImplementedException();
        }
    }

    public partial class GroupClass
    {


        internal void AddOtherPlayer(string key, ref List<string> msgsWithUrl)
        {
            if (this._PlayerInGroup.ContainsKey(key))
            {
                var players = getGetAllRoles();
                for (var i = 0; i < players.Count; i++)
                {
                    if (players[i].Key == key)
                    {
                        /*
                         * 保证自己不会算作其他人
                         */
                    }
                    else
                    {
                        {
                            /*
                             * 告诉自己，场景中有哪些人！
                             * 告诉场景中的其他人，场景中有我！
                             */
                            {
                                var self = this._PlayerInGroup[key];
                                var other = players[i];
                                that.addPlayerRecord(self, other, ref msgsWithUrl);

                            }
                            {
                                var self = players[i];
                                var other = this._PlayerInGroup[key];
                                that.addPlayerRecord(self, other, ref msgsWithUrl);
                            }
                        }
                    }
                }
            }
        }

        public List<Player> getGetAllRoles()
        {
            return that.getGetAllRoles(this);
        }


    }
}
