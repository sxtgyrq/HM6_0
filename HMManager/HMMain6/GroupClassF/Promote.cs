﻿using CommonClass;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HMMain6.Car;

namespace HMMain6.GroupClassF
{
    public partial class GroupClass : interfaceOfHM.Promote
    {
        public void SetLookForPromote(GetRandomPos gp)
        {
            {
                int k = 0;
                do
                {
                    this.promoteMilePosition = GetRandomPosition(true, gp);
                    k++;
                    if (k < 10) { }
                    else
                    {
                        break;
                    }
                }
                while (IsOutTheAbility(gp));
                // var from = this.GetFromWhenUpdateCollect(player, sc.cType, car);
                //var to = getCollectPositionTo(sc.collectIndex, player.Group);//  this.promoteMilePosition;
                //var fp1 = grp.GetFpByIndex(from);

                //this.MaxMile
            }
            //   this.promoteBusinessPosition = GetRandomPosition(true, gp);
            this.promoteVolumePosition = GetRandomPosition(true, gp);
            // this.promoteSpeedPosition = GetRandomPosition(true, gp);
        }

        private bool IsOutTheAbility(GetRandomPos grp)
        {
            //  throw new Exception();
            List<string> notifyMsg = new List<string>();
            List<int> goFpIndex, returnFpIndex;
            var goPath = grp.GetAFromB(this.StartFPIndex, this.promoteMilePosition, out goFpIndex);
            var returnPath = grp.GetAFromB(this.promoteMilePosition, this.StartFPIndex, out returnFpIndex);

            int goEnergy = 0, returnEnergy = 0;

            for (int i = 0; i < goFpIndex.Count - 1; i++)
            {
                var selections = grp.GetSelections(goFpIndex[i]);
                var selectionIndex = selections.FindIndex(item => item == goFpIndex[i + 1]);
                //   Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(selections));
                goEnergy += grp.GetEnegy(goFpIndex[i])[selectionIndex];
            }

            for (int i = 0; i < returnFpIndex.Count - 1; i++)
            {
                var selections = grp.GetSelections(returnFpIndex[i]);
                var selectionIndex = selections.FindIndex(item => item == returnFpIndex[i + 1]);
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(selections));
                returnEnergy += grp.GetEnegy(returnFpIndex[i])[selectionIndex];
            }


            //  Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(goPath));
            //  Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(returnPath));
            // throw (new Exception("暂时展厅"));
            //var goPathEnegy = grp.GetEnegy(this.StartFPIndex)[this.promoteMilePosition];// that.GetAFromB_v2(this.StartFPIndex, this.promoteMilePosition, grp, ref notifyMsg);
            //var returnPathEnegy = grp.GetEnegy(this.promoteMilePosition)[this.StartFPIndex];//, grp, ref notifyMsg);

            //var goMile = that.GetMile(goPath);
            //var returnMile = that.GetMile(returnPath);

            if (goEnergy + returnEnergy > this.MaxMile * 90 / 100)
            {
                return true;
            }
            else
                return false;
        }

        int _promoteMilePosition = -1;
        //int _promoteBusinessPosition = -1;
        int _promoteVolumePosition = -1;
        int _promoteSpeedPosition = -1;

        public int promoteMilePosition
        {
            get
            {
                return this._promoteMilePosition;
            }
            set
            {
                this._promoteMilePosition = value;
            }
        }
        public int promoteVolumePosition
        {
            get { return this._promoteVolumePosition; }
            set
            {
                this._promoteVolumePosition = value;
            }
        }
        public int promoteSpeedPosition
        {
            get { return this._promoteSpeedPosition; }
            set
            {
                this._promoteSpeedPosition = value;
            }
        }

        public long MaxMile
        {
            get
            {
                // throw new Exception("");
                long maxValue = 20;
                foreach (var item in this._PlayerInGroup)
                {
                    if (item.Value.playerType == Player.PlayerType.player)
                    {
                        if (item.Value.getCar().ability.mile > maxValue)
                        {
                            maxValue = item.Value.getCar().ability.mile;
                        }
                    }
                }
                return maxValue;
            }
        }

        internal void CheckPromoteState(string key, string promoteType)
        {
            string url = "";
            string sendMsg = "";

            if (this._PlayerInGroup.ContainsKey(key))
                if (this._PlayerInGroup[key].playerType == Player.PlayerType.player)
                    if (((Player)this._PlayerInGroup[key]).PromoteState[promoteType] == this.getPromoteState(promoteType))
                    {
                    }
                    else
                    {
                        //  var infomation = this.GetPromoteInfomation(((Player)this._PlayerInGroup[key]).WebSocketID, promoteType);
                        url = ((Player)this._PlayerInGroup[key]).FromUrl;
                        //sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(infomation);
                        ((Player)this._PlayerInGroup[key]).PromoteState[promoteType] = this.getPromoteState(promoteType);
                    }
            if (!string.IsNullOrEmpty(url))
            {
                Startup.sendSingleMsg(url, sendMsg);
            }
        }

        int getPromoteState(string pType)
        {
            switch (pType)
            {
                case "mile":
                    {
                        return this.promoteMilePosition;
                    }

                case "volume":
                    {
                        return this.promoteVolumePosition;
                    };
                case "speed":
                    {
                        return this.promoteSpeedPosition;
                    };
                default:
                    {
                        throw new Exception($"{pType}是什么类型");
                    };
            }
        }

        //internal BradCastPromoteInfoDetail GetPromoteInfomation(int webSocketID, string resultType)
        //{
        //    //  throw new Exception();
        //    switch (resultType)
        //    {
        //        case "mile":
        //            {
        //                var obj = new BradCastPromoteInfoDetail
        //                {
        //                    c = "BradCastPromoteInfoDetail",
        //                    WebSocketID = webSocketID,
        //                    resultType = resultType,
        //                    // Fp = Program.dt.GetFpByIndex(this.promoteMilePosition),
        //                    Price = this.promotePrice[resultType]
        //                };
        //                return obj;
        //            };
        //        //case "business":
        //        //    {
        //        //        var obj = new BradCastPromoteInfoDetail
        //        //        {
        //        //            c = "BradCastPromoteInfoDetail",
        //        //            WebSocketID = webSocketID,
        //        //            resultType = resultType,
        //        //            Fp = Program.dt.GetFpByIndex(this.promoteBusinessPosition),
        //        //            Price = this.promotePrice[resultType]
        //        //        };
        //        //        return obj;
        //        //    };
        //        case "volume":
        //            {
        //                var obj = new BradCastPromoteInfoDetail
        //                {
        //                    c = "BradCastPromoteInfoDetail",
        //                    WebSocketID = webSocketID,
        //                    resultType = resultType,
        //                    //  Fp = Program.dt.GetFpByIndex(this.promoteVolumePosition),
        //                    Price = this.promotePrice[resultType]
        //                };
        //                return obj;
        //            };
        //        case "speed":
        //            {
        //                var obj = new BradCastPromoteInfoDetail
        //                {
        //                    c = "BradCastPromoteInfoDetail",
        //                    WebSocketID = webSocketID,
        //                    resultType = resultType,
        //                    // Fp = Program.dt.GetFpByIndex(this.promoteSpeedPosition),
        //                    Price = this.promotePrice[resultType]
        //                };
        //                return obj;
        //            };
        //        default:
        //            {
        //                var obj = new BradCastPromoteInfoDetail
        //                {
        //                    c = "BradCastPromoteInfoDetail",
        //                    WebSocketID = webSocketID,
        //                    resultType = "mile",
        //                    //    Fp = Program.dt.GetFpByIndex(this.promoteMilePosition),
        //                    Price = this.promotePrice["mile"]
        //                };
        //                return obj;
        //            };
        //    }
        //}

        void PromoteClickFunctionWhenAuto(Player player, string pType, GetRandomPos gp, string Uid)
        {
            //int promotePosition;
            ////  string diamondName;
            //HouseManager5_0.TargetForSelect.TargetForSelectType tsType;
            //switch (pType)
            //{
            //    case "mile":
            //        {
            //            promotePosition = this.promoteMilePosition;
            //            //   diamondName = "红宝石";
            //            tsType = TargetForSelect.TargetForSelectType.mile;
            //        }; break;
            //    case "volume":
            //        {
            //            promotePosition = this.promoteVolumePosition;
            //            // diamondName = "蓝宝石";
            //            tsType = TargetForSelect.TargetForSelectType.volume;
            //        }; break;
            //    case "speed":
            //        {
            //            promotePosition = this.promoteSpeedPosition;
            //            //diamondName = "黑宝石";
            //            tsType = TargetForSelect.TargetForSelectType.speed;
            //        }; break;
            //    default:
            //        {
            //            throw new Exception($"wrong parameter \"{pType}\"");
            //        }
            //};
            //OssModel.FastonPosition fpResult;
            //var car = player.getCar();
            //var distanceIsEnoughToStart = that.theNearestToDiamondIsCarNotMoney(player, car, pType, Program.dt, out fpResult);
            //if (distanceIsEnoughToStart)
            //{
            //    player.Ts = new TargetForSelect(promotePosition, tsType, 0, player.improvementRecord.HasValueToImproveSpeed);
            //    that.updatePromote(new SetPromote()
            //    {
            //        c = "SetPromote",
            //        GroupKey = this.GroupKey,
            //        Key = player.Key,
            //        pType = pType,
            //        Uid = Uid
            //    }, gp);
            //}
            //else if (player.improvementRecord.HasValueToImproveSpeed && player.getCar().state == CarState.waitOnRoad)
            //{
            //    List<string> sendMsgs = new List<string>();
            //    //var Fp = fps[0];
            //    var lengthToDiamond = this.getLength(gp.GetFpByIndex(promotePosition), gp.GetFpByIndex(player.getCar().targetFpIndex));
            //    var rank = (from item in this._collectPosition
            //                where this.getLength(gp.GetFpByIndex(item.Value), gp.GetFpByIndex(player.getCar().targetFpIndex)) < lengthToDiamond
            //                select gp.GetFpByIndex(item.Value)).ToList();
            //    var rankNum = rank.Count;
            //    player.Ts = new TargetForSelect(promotePosition, tsType, rankNum, player.improvementRecord.HasValueToImproveSpeed);
            //    that.updatePromote(new SetPromote()
            //    {
            //        c = "SetPromote",
            //        GroupKey = this.GroupKey,
            //        Key = player.Key,
            //        pType = pType,
            //        Uid = Uid
            //    }, gp);
            //}
        }
        private void PromoteClickFunction(Player player, List<ModelBase.Data.FPPosition> fps, string pType, GetRandomPos gp, ref List<string> notifyMsgs)
        {
            //int promotePosition;
            //string diamondName;
            //HouseManager5_0.TargetForSelect.TargetForSelectType tsType;
            switch (pType)
            {
                case "mile":
                    {
                        //promotePosition = this.promoteMilePosition;
                        //diamondName = "红宝石";
                        //tsType = TargetForSelect.TargetForSelectType.mile;
                        var fp = gp.GetFpByIndex(this.promoteMilePosition);
                        player.rm.WebNotify(player, $"{fp.fPName}处有电池装置可以提升续航能力。");
                    }; break;
                case "volume":
                    {
                        //promotePosition = this.promoteVolumePosition;
                        //diamondName = "蓝宝石";
                        //tsType = TargetForSelect.TargetForSelectType.volume;
                        var fp = gp.GetFpByIndex(this.promoteVolumePosition);
                        player.rm.WebNotify(player, $"{fp.fPName}处有此轮可以提升装载能力。");
                    }; break;
                case "speed":
                    {
                        //promotePosition = this.promoteSpeedPosition;
                        //diamondName = "黑宝石";
                        //tsType = TargetForSelect.TargetForSelectType.speed;
                    }; break;
                default:
                    {
                        //throw new Exception($"wrong parameter \"{pType}\"");
                    }; break;
            };
            //OssModel.FastonPosition fpResult;
            //var car = player.getCar();
            //var distanceIsEnoughToStart = that.theNearestToDiamondIsCarNotMoney(player, car, pType, Program.dt, out fpResult);
            //if (distanceIsEnoughToStart)
            //{
            //    player.Ts = new TargetForSelect(promotePosition, tsType, 0, player.improvementRecord.HasValueToImproveSpeed);
            //    that.updatePromote(new SetPromote()
            //    {
            //        c = "SetPromote",
            //        GroupKey = this.GroupKey,
            //        Key = player.Key,
            //        pType = pType,
            //        Uid = ""
            //    }, gp);
            //}
            //else if (player.improvementRecord.HasValueToImproveSpeed && player.getCar().state == CarState.waitOnRoad)
            //{
            //    List<string> sendMsgs = new List<string>();
            //    var Fp = fps[0];
            //    var lengthToDiamond = this.getLength(gp.GetFpByIndex(promotePosition), gp.GetFpByIndex(player.getCar().targetFpIndex));
            //    var rank = (from item in this._collectPosition
            //                where this.getLength(gp.GetFpByIndex(item.Value), gp.GetFpByIndex(player.getCar().targetFpIndex)) < lengthToDiamond
            //                select gp.GetFpByIndex(item.Value)).ToList();
            //    var rankNum = rank.Count;
            //    player.Ts = new TargetForSelect(promotePosition, tsType, rankNum, player.improvementRecord.HasValueToImproveSpeed);



            //    var msg = "";
            //    {
            //        var priceStr = player.Ts.costPriceStr;
            //        if (string.IsNullOrEmpty(Fp.region))
            //        {
            //            msg = $"<b>是否掏<span style=\"color:blue;text-shadow:1px 1px green;\">{priceStr}</span>路费到【{Fp.FastenPositionName}】找收集{diamondName}？</b>";
            //        }
            //        else
            //        {
            //            msg = $"<b>是否掏<span style=\"color:blue;text-shadow:1px 1px green;\">{priceStr}</span>路费到[{Fp.region}]【{Fp.FastenPositionName}】收集{diamondName}？</b>";
            //        }
            //    }
            //    var obj = GetConfirmInfomation(player.WebSocketID, Fp, msg, player.Ts, gp);
            //    var url = player.FromUrl;
            //    var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            //    notifyMsgs.Add(url);
            //    notifyMsgs.Add(sendMsg);
            //}
            //else
            //{
            //    /*
            //     * promote distanceIsEnoughToStart=false当执行 promote方法之时，该方法回告诉前台，离宝石最近的选择
            //     */
            //    player.Ts = new TargetForSelect(promotePosition, tsType, 100, player.improvementRecord.HasValueToImproveSpeed);
            //    that.updatePromote(new SetPromote()
            //    {
            //        c = "SetPromote",
            //        GroupKey = this.GroupKey,
            //        Key = player.Key,
            //        pType = pType,
            //        Uid = ""
            //    }, gp);
            //}
        }



        public Dictionary<string, long> promotePrice = new Dictionary<string, long>()
        {
            { "mile",1000},
            { "business",1000},
            { "volume",1000},
            { "speed",1000},
        };
        //internal commandWithTime.ReturningOjb promote(Player player, Car car, SetPromote sp, GetRandomPos grp, ref List<string> notifyMsg, out MileResultReason mrr)
        //{
        //    //if(sp.pType=="mi")
        //    switch (sp.pType)
        //    {
        //        case "mile":
        //        // case "business":
        //        case "volume":
        //        case "speed":
        //            {
        //                switch (car.state)
        //                {
        //                    case CarState.waitAtBaseStation:
        //                        {
        //                            // if(player.Money<)
        //                            OssModel.FastonPosition fpResult;
        //                            var distanceIsEnoughToStart = that.theNearestToDiamondIsCarNotMoney(player, car, sp.pType, Program.dt, out fpResult);
        //                            if (distanceIsEnoughToStart)
        //                            {
        //                                var from = that.promoteE.getFromWhenAction(player, car);
        //                                var to = that.GetPromotePositionTo(sp.pType, player.Group);//  this.promoteMilePosition;

        //                                var fp1 = Program.dt.GetFpByIndex(from);
        //                                var fp2 = Program.dt.GetFpByIndex(to);
        //                                var baseFp = Program.dt.GetFpByIndex(player.StartFPIndex);
        //                                var goPath = that.GetAFromB_v2(from, to, player, grp, ref notifyMsg);
        //                                // var returnPath = Program.dt.GetAFromB(to, player.StartFPIndex);
        //                                var returnPath = that.GetAFromB_v2(to, player.StartFPIndex, player, grp, ref notifyMsg);

        //                                var goMile = that.GetMile(goPath);
        //                                var returnMile = that.GetMile(returnPath);


        //                                //第一步，计算去程和回程。
        //                                if (car.ability.leftMile >= goMile + returnMile)
        //                                {
        //                                    int startT;
        //                                    that.promoteE.EditCarStateWhenActionStartOK(player, ref car, to, fp1, goPath, grp, ref notifyMsg, out startT);

        //                                    RoomMainF.RoomMain.commandWithTime.ReturningOjb ro = commandWithTime.ReturningOjb.ojbWithoutBoss(returnPath);
        //                                    that.diamondOwnerE.StartDiamondOwnerThread(startT, 0, player, car, sp, ro, goMile, goPath, grp);
        //                                    //  getAllCarInfomations(sp.Key, ref notifyMsg);
        //                                    mrr = MileResultReason.Abundant;
        //                                    return ro;
        //                                }

        //                                else if (car.ability.leftMile >= goMile)
        //                                {
        //                                    that.WebNotify(player, $"去程{goMile}km，回程{returnMile}km,去了目的地回不基地。");
        //                                    mrr = MileResultReason.CanNotReturn;
        //                                    this.askWhetherGoToPositon(player.Key, grp);
        //                                    return player.returningOjb;
        //                                }
        //                                else
        //                                {
        //                                    that.WebNotify(player, $"去程{goMile}km，回程{returnMile}km,去不了目的地。");
        //                                    // printState(player, car, $"去程{goMile}，回程{returnMile},去不了");
        //                                    mrr = MileResultReason.CanNotReach;
        //                                    this.askWhetherGoToPositon(player.Key, grp);
        //                                    return player.returningOjb;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                mrr = MileResultReason.NearestIsMoneyWhenPromote;
        //                                that.WebNotify(player, $"离宝石最近的是[{fpResult.FastenPositionName}]处的钱，不是你的车。请离宝石再近点儿！");
        //                                this.askWhetherGoToPositon(player.Key, grp);
        //                                return player.returningOjb;
        //                            }
        //                        };
        //                    case CarState.waitOnRoad:
        //                        {
        //                            OssModel.FastonPosition fpResult;
        //                            var distanceIsEnoughToStart = that.theNearestToDiamondIsCarNotMoney(player, car, sp.pType, Program.dt, out fpResult);
        //                            if (distanceIsEnoughToStart)
        //                            {
        //                                var from = that.promoteE.getFromWhenAction(player, car);
        //                                var to = that.GetPromotePositionTo(sp.pType, player.Group);//  this.promoteMilePosition;

        //                                var fp1 = Program.dt.GetFpByIndex(from);
        //                                var fp2 = Program.dt.GetFpByIndex(to);
        //                                var baseFp = Program.dt.GetFpByIndex(player.StartFPIndex);
        //                                var goPath = that.GetAFromB_v2(from, to, player, grp, ref notifyMsg);
        //                                var returnPath = that.GetAFromB_v2(to, player.StartFPIndex, player, grp, ref notifyMsg);

        //                                var goMile = that.GetMile(goPath);
        //                                var returnMile = that.GetMile(returnPath);


        //                                //第一步，计算去程和回程。
        //                                if (car.ability.leftMile >= goMile + returnMile)
        //                                {
        //                                    int startT;
        //                                    that.promoteE.EditCarStateWhenActionStartOK(player, ref car, to, fp1, goPath, grp, ref notifyMsg, out startT);

        //                                    RoomMainF.RoomMain.commandWithTime.ReturningOjb ro = commandWithTime.ReturningOjb.ojbWithoutBoss(returnPath);
        //                                    that.diamondOwnerE.StartDiamondOwnerThread(startT, 0, player, car, sp, ro, goMile, goPath, grp);
        //                                    mrr = MileResultReason.Abundant;
        //                                    return ro;
        //                                }

        //                                else if (car.ability.leftMile >= goMile)
        //                                {
        //                                    that.WebNotify(player, $"去程{goMile}km，回程{returnMile}km,去了回不来");
        //                                    mrr = MileResultReason.CanNotReturn;
        //                                    this.askWhetherGoToPositon(player.Key, grp);
        //                                    return player.returningOjb;
        //                                }
        //                                else
        //                                {
        //                                    that.WebNotify(player, $"去程{goMile}km，回程{returnMile}km,去不了");
        //                                    mrr = MileResultReason.CanNotReach;
        //                                    this.askWhetherGoToPositon(player.Key, grp);
        //                                    return player.returningOjb;
        //                                }
        //                            }
        //                            else if (player.improvementRecord.HasValueToImproveSpeed)
        //                            {
        //                                if (player.Money - player.Ts.costPrice > 0)
        //                                {
        //                                    player.MoneySet(player.Money - player.Ts.costPrice, ref notifyMsg);
        //                                }
        //                                else
        //                                {
        //                                    player.MoneySet(0, ref notifyMsg);
        //                                }

        //                                var from = that.promoteE.getFromWhenAction(player, car);
        //                                var to = that.GetPromotePositionTo(sp.pType, player.Group);//  this.promoteMilePosition;

        //                                var fp1 = Program.dt.GetFpByIndex(from);
        //                                //  var fp2 = Program.dt.GetFpByIndex(to);
        //                                //var baseFp = Program.dt.GetFpByIndex(player.StartFPIndex);
        //                                var goPath = that.GetAFromB_v2(from, to, player, grp, ref notifyMsg);
        //                                var returnPath = that.GetAFromB_v2(to, player.StartFPIndex, player, grp, ref notifyMsg);

        //                                var goMile = that.GetMile(goPath);
        //                                var returnMile = that.GetMile(returnPath);


        //                                //第一步，计算去程和回程。
        //                                if (car.ability.leftMile >= goMile + returnMile)
        //                                {
        //                                    int startT;
        //                                    that.promoteE.EditCarStateWhenActionStartOK(player, ref car, to, fp1, goPath, grp, ref notifyMsg, out startT);

        //                                    RoomMainF.RoomMain.commandWithTime.ReturningOjb ro = commandWithTime.ReturningOjb.ojbWithoutBoss(returnPath);
        //                                    that.diamondOwnerE.StartDiamondOwnerThread(startT, 0, player, car, sp, ro, goMile, goPath, grp);
        //                                    mrr = MileResultReason.Abundant;
        //                                    return ro;
        //                                }

        //                                else if (car.ability.leftMile >= goMile)
        //                                {
        //                                    that.WebNotify(player, $"去程{goMile}km，回程{returnMile}km,去了回不来");
        //                                    mrr = MileResultReason.CanNotReturn;
        //                                    this.askWhetherGoToPositon(player.Key, grp);
        //                                    return player.returningOjb;
        //                                }
        //                                else
        //                                {
        //                                    that.WebNotify(player, $"去程{goMile}km，回程{returnMile}km,去不了");
        //                                    mrr = MileResultReason.CanNotReach;
        //                                    this.askWhetherGoToPositon(player.Key, grp);
        //                                    return player.returningOjb;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                mrr = MileResultReason.NearestIsMoneyWhenPromote;
        //                                that.WebNotify(player, $"离宝石最近的是[{fpResult.FastenPositionName}]处的钱，不是你的车。请离宝石再近点儿！");
        //                                this.askWhetherGoToPositon(player.Key, grp);
        //                                return player.returningOjb;
        //                            }
        //                        };
        //                    default:
        //                        {
        //                            throw new Exception($"{Enum.GetName(typeof(CarState), car.state)}不是注册的类型！");
        //                        }
        //                }

        //            };
        //        default:
        //            {
        //                throw new Exception($"{sp.pType}-不是规定的输入！");
        //            };
        //    }
        //}

        internal void setPromtePosition(string changeType, GetRandomPos grp)
        {
            //if (changeType == "mile")
            //{
            //    do
            //    {
            //        this.promoteMilePosition = GetRandomPositionFromAnotherRegion(true, Program.dt, this.promoteMilePosition);
            //    }
            //    while (this.IsOutTheAbility(grp));
            //}
            //else if (changeType == "volume")
            //    this.promoteVolumePosition = GetRandomPositionFromAnotherRegion(true, Program.dt, this.promoteVolumePosition);
            //else if (changeType == "speed")
            //    this.promoteSpeedPosition = GetRandomPositionFromAnotherRegion(true, Program.dt, this.promoteSpeedPosition);
            //else
            //{
            //    throw new Exception($"{changeType}是什么类型？");
            //}
            //  this.promotePrice[changeType] = this.GetPriceOfPromotePosition(changeType);
        }

        internal bool updateRedDiamond(SetPromote sp, GetRandomPos grp, ref List<string> notifyMsgs)
        {
            bool success = false;
            if (this._PlayerInGroup.ContainsKey(sp.Key))
            {
                var player = this._PlayerInGroup[sp.Key];
                if (this.promoteMilePosition == player.getCar().targetFpIndex)
                {
                    // if (player.getCar().ability.costVolume == 0)
                    {
                        if (string.IsNullOrEmpty(player.getCar().ability.diamondInCar))
                        {
                            player.getCar().ability.setDiamondInCar("mile", player, player.getCar(), ref notifyMsgs);
                            success = true;
                        }
                        else
                        {
                            this.that.WebNotify(player, "已经装载宝石了，请回基地复命。");
                        }
                    }
                    // else
                    {
                        //  this.that.WebNotify(player, "宝石");
                    }
                }
            }
            if (success)
            {
                {
                    int k = 0;
                    do
                    {
                        this.promoteMilePosition = GetRandomPosition(true, grp);
                        k++;
                        if (k < 10) { }
                        else
                        {
                            break;
                        }
                    }
                    while (IsOutTheAbility(grp));
                    // var from = this.GetFromWhenUpdateCollect(player, sc.cType, car);
                    //var to = getCollectPositionTo(sc.collectIndex, player.Group);//  this.promoteMilePosition;
                    //var fp1 = grp.GetFpByIndex(from);

                    //this.MaxMile
                }
            }
            return success;
            //  throw new NotImplementedException();
        }

        internal bool updateBlueDiamond(SetPromote sp, GetRandomPos grp, ref List<string> notifyMsgs)
        {
            bool success = false;
            if (this._PlayerInGroup.ContainsKey(sp.Key))
            {
                var player = this._PlayerInGroup[sp.Key];
                if (this.promoteVolumePosition == player.getCar().targetFpIndex)
                {
                    // if (player.getCar().ability.costVolume == 0)
                    {
                        if (string.IsNullOrEmpty(player.getCar().ability.diamondInCar))
                        {
                            player.getCar().ability.setDiamondInCar("volume", player, player.getCar(), ref notifyMsgs);
                            success = true;
                        }
                        else
                        {
                            this.that.WebNotify(player, "已经装载宝石了，请回基地复命。");
                        }
                    }
                    // else
                    {
                        //  this.that.WebNotify(player, "宝石");
                    }
                }
            }
            if (success)
            {
                {
                    int k = 0;
                    do
                    {
                        this.promoteMilePosition = GetRandomPosition(true, grp);
                        k++;
                        if (k < 10) { }
                        else
                        {
                            break;
                        }
                    }
                    while (IsOutTheAbility(grp));
                    // var from = this.GetFromWhenUpdateCollect(player, sc.cType, car);
                    //var to = getCollectPositionTo(sc.collectIndex, player.Group);//  this.promoteMilePosition;
                    //var fp1 = grp.GetFpByIndex(from);

                    //this.MaxMile
                }
            }
            return success;
            //  throw new NotImplementedException();
        }

        //private long GetPriceOfPromotePosition(string changeType)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 依据频率，获取价格。这个是随机获取地址的时候，就会获得。
        ///// </summary>
        ///// <param name="resultType">mile，business，volume，speed</param>
        ///// <returns>返回结果为分，即1/100元</returns>
        //protected long GetPriceOfPromotePosition(string resultType)
        //{
        //    if (resultType == "mile" || resultType == "business" || resultType == "volume" || resultType == "speed")
        //    {
        //        this.recordOfPromote[resultType].Add(DateTime.Now);
        //    }
        //    else
        //    {
        //        throw new Exception($"错误地调用{resultType}");
        //    }
        //    if (this.recordOfPromote[resultType].Count < 10)
        //    {
        //        //  this.recordOfPromote[resultType].Add(DateTime.Now);
        //        return 10 * 100;
        //    }
        //    else
        //    {
        //        if (this.recordOfPromote[resultType].Count > 10)
        //        {
        //            this.recordOfPromote[resultType].RemoveAt(0);
        //        }
        //        double sumHz = 0;
        //        for (var i = 1; i < this.recordOfPromote[resultType].Count; i++)
        //        {
        //            var timeS = (this.recordOfPromote[resultType][i] - this.recordOfPromote[resultType][i - 1]).TotalSeconds;
        //            timeS = Math.Max(1, timeS);
        //            var itemHz = 1 / timeS;
        //            sumHz += itemHz;
        //        }
        //        var averageValue = sumHz / (this.recordOfPromote[resultType].Count - 1);
        //        return Convert.ToInt32(50 * 100 * 60 * averageValue); //确保1分钟 的价格是50元
        //        //var calResult = Math.Round(Convert.ToDecimal(Math.Round(50 * 60 * averageValue, 2)), 2);
        //        //return Math.Max(0.01m, calResult);
        //    }
        //}
    }
}
