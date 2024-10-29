using CommonClass;
using HMMain6.RoomMainF;
using Microsoft.AspNetCore.Routing.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HMMain6.Car;

namespace HMMain6
{
    public class Manager_Model : Manager
    {
        public Manager_Model(RoomMain roomMain)
        {
            this.roomMain = roomMain;
        }
        internal string GetRewardFromBuildingF(GetRewardFromBuildingM m, GetRandomPos grp)
        {
            {
                List<string> notifyMsg = new List<string>();
                //  lock (that.PlayerLock)
                {
                    if (that._Groups.ContainsKey(m.GroupKey))
                    {
                        var group = that._Groups[m.GroupKey];
                        if (group._PlayerInGroup.ContainsKey(m.Key))
                        {
                            if (group._PlayerInGroup[m.Key].Bust) { }
                            else
                            {

                                var role = group._PlayerInGroup[m.Key];
                                if (role.playerType == Player.PlayerType.player)
                                {
                                    var player = (Player)role;
                                    if (player.canGetReward)
                                    {
                                        var car = group._PlayerInGroup[m.Key].getCar();

                                        if (car.targetFpIndex >= 0)
                                        {
                                            var fp = grp.GetFpByIndex(car.targetFpIndex);
                                            if (!string.IsNullOrEmpty(fp.BitcoinAddr)) 
                                            {

                                            }
                                        }

                                        //switch (car.state)
                                        //{
                                        //    case CarState.waitOnRoad:
                                        //        {
                                        //            var models = that.goodsM.GetConnectionModels(player.getCar().targetFpIndex, player);
                                        //            // if (models.Count(item => item.modelID == m.selectObjName) > 0)
                                        //            {
                                        //                int defendLevel = 1;
                                        //                string rewardLittleReason;
                                        //                if (string.IsNullOrEmpty(player.BTCAddress))
                                        //                {
                                        //                    rewardLittleReason = "你还没有登录，登录可获取更多加成。";
                                        //                    defendLevel = 1;
                                        //                }
                                        //                else
                                        //                {
                                        //                    //   defendLevel = 2;
                                        //                    long sumSatoshi = 0;
                                        //                    for (int i = 0; i < models.Count; i++)
                                        //                    {
                                        //                        if (Program.dt.modelsStocks.ContainsKey(models[i].modelID))
                                        //                        {
                                        //                            if (Program.dt.modelsStocks[models[i].modelID].stocks.ContainsKey(player.BTCAddress))
                                        //                            {
                                        //                                sumSatoshi += Program.dt.modelsStocks[models[i].modelID].stocks[player.BTCAddress];
                                        //                            }
                                        //                        }
                                        //                    }
                                        //                    if (sumSatoshi == 0)
                                        //                    {
                                        //                        rewardLittleReason = "你在此处没有获得支持，因为在就近的一些建筑中没有股份！";
                                        //                        defendLevel = 2;
                                        //                    }
                                        //                    else if (sumSatoshi > 0)
                                        //                    {
                                        //                        defendLevel = CommonClass.Random.GetNitrogen(sumSatoshi, ref Program.rm.rm);
                                        //                        rewardLittleReason = $"在此有{sumSatoshi}点股,";
                                        //                    }
                                        //                    else rewardLittleReason = "";
                                        //                }
                                        //                player.improvementRecord.addSpeed(player, defendLevel, ref notifyMsg);

                                        //                // rewardLittleReason = $"！;
                                        //                if (!player.Group.Live)
                                        //                    this.WebNotify(player, $"{rewardLittleReason}液氮+{defendLevel},现有{player.improvementRecord.SpeedValue}。");
                                        //            }
                                        //        }; break;
                                        //    default:
                                        //        {
                                        //            if (group.Live) { }
                                        //            else
                                        //                WebNotify(player, "当前状态，求福不顶用！");
                                        //        }; break;
                                        //}
                                    }
                                    else
                                    {
                                        this.WebNotify(player, "在一个地点不能重复祈福");
                                    }
                                }
                            }
                        }
                    }

                }
                this.sendSeveralMsgs(notifyMsg);
                //var msgL = this.sendSeveralMsgs(notifyMsg).Count;
                //msgL++;
                //for (var i = 0; i < notifyMsg.Count; i += 2)
                //{
                //    var url = notifyMsg[i];
                //    var sendMsg = notifyMsg[i + 1]; 
                //    if (!string.IsNullOrEmpty(url))
                //    {
                //        Startup.sendMsg(url, sendMsg);
                //    }
                //}
                return "";
                //return $"{msgL}".Length > 0 ? "" : "";
            }

        }
    }
}
