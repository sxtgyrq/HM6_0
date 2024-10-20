using CommonClass;
using HMMain6.GroupClassF;
using HMMain6.interfaceOfHM;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using ModelBase.Data;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static CommonClass.ExitObj;

namespace HMMain6.RoomMainF
{


    public partial class RoomMain : interfaceOfHM.Position
    {


        public GetRandomPos GetRandomPosObj { get { return Program.dt; } }

        //public int GetRandomPosition(bool withWeight) 
        //{
        //    return GetRandomPosition(withWeight, Program.dt);
        //}
        public int GetRandomPosition(bool withWeight, GetRandomPos gp, GroupClass group, out bool isFull)
        {
            throw new Exception();
            //int fpCount = gp.GetFpCount();
            //List<int> material = new List<int>(fpCount);
            //for (int i = 0; i < fpCount; i++)
            //{
            //    material.Add(i);
            //}
            //if (material.Count(item => !this.FpIsUsing(item, group)) < 3)
            //{
            //    isFull = true;
            //    return -1;
            //}
            //else
            //{
            //    isFull = false;
            //    return GetRandomPosition(withWeight, gp, group);
            //}
            //for(int i=0;)

        }

        public int GetRandomPosition(bool withWeight, GetRandomPos gp, GroupClass group)
        {
            throw new Exception();
            //int index;
            //do
            //{
            //    index = rm.Next(0, gp.GetFpCount());
            //    if (withWeight)
            //        if (gp.GetFpByIndex(index).Weight + 1 < rm.Next(100))
            //        {
            //            continue;
            //        }
            //}
            //while (this.FpIsUsing(index, group));
            //return index;
        }



        /// <summary>
        /// 此段代码debug时用。
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="searchName"></param>
        /// <returns></returns>
        int FindIndexByFpName(GetRandomPos gp, string searchName)
        {
            throw new Exception();
            //for (int i = 0; i < gp.GetFpCount(); i++)
            //{
            //    var fp = gp.GetFpByIndex(i);
            //    if (fp.FastenPositionName.Contains(searchName))
            //    {
            //        return i;
            //    }
            //}
            //return -1;
        }

        //public GetPositionResult GetPosition(GetPosition getPosition)
        //{
        //    throw new Exception();
        //    //// throw new Exception("");

        //    //GetPositionResult result;

        //    //int OpenMore = -1;//第一次打开？
        //    //var notifyMsgs = new List<string>();
        //    //GroupClassF.GroupClass gc = null;
        //    ////   lock (this.PlayerLock)
        //    //{
        //    //    if (string.IsNullOrEmpty(getPosition.GroupKey))
        //    //    {
        //    //        gc = null;
        //    //    }
        //    //    else if (this._Groups.ContainsKey(getPosition.GroupKey))
        //    //    {
        //    //        gc = this._Groups[getPosition.GroupKey];
        //    //    }
        //    //}
        //    //if (gc != null)
        //    //{
        //    //    // lock (gc.PlayerLock)
        //    //    {
        //    //        if (gc._PlayerInGroup.ContainsKey(getPosition.Key))
        //    //        {
        //    //            if (gc._PlayerInGroup[getPosition.Key].playerType == Player.PlayerType.player)
        //    //            {
        //    //                var player = gc._PlayerInGroup[getPosition.Key];
        //    //                var fp = Program.dt.GetFpByIndex(gc._PlayerInGroup[getPosition.Key].StartFPIndex);
        //    //                var fromUrl = player.FromUrl;
        //    //                var webSocketID = player.WebSocketID;
        //    //                //var carsNames = this._Players[getPosition.Key].CarsNames;
        //    //                var playerName = gc._PlayerInGroup[getPosition.Key].PlayerName;
        //    //                /*
        //    //                 * 这已经走查过，在AddNewPlayer、UpdatePlayer时，others都进行了初始化
        //    //                 */
        //    //                AddOtherPlayer(getPosition.Key, getPosition.GroupKey, ref notifyMsgs);
        //    //                //   this.brokenParameterT1RecordChanged(getPosition.Key, getPosition.Key, this._Players[getPosition.Key].brokenParameterT1, ref notifyMsgs);
        //    //                GetAllCarInfomationsWhenInitialize(getPosition.Key, getPosition.GroupKey, ref notifyMsgs);
        //    //                //getAllCarInfomations(getPosition.Key, ref notifyMsgs);
        //    //                OpenMore = gc._PlayerInGroup[getPosition.Key].OpenMore;

        //    //                // var player = this._Players[getPosition.Key];
        //    //                //var m2 = player.GetMoneyCanSave();

        //    //                //    MoneyCanSaveChanged(player, m2, ref notifyMsgs);

        //    //                SendPromoteCountOfPlayer("mile", player, ref notifyMsgs, false);
        //    //                //  SendPromoteCountOfPlayer("business", player, ref notifyMsgs);
        //    //                SendPromoteCountOfPlayer("volume", player, ref notifyMsgs, false);
        //    //                SendPromoteCountOfPlayer("speed", player, ref notifyMsgs, false);

        //    //                //   BroadCoastFrequency(player, ref notifyMsgs);
        //    //                player.SetMoneyCanSave(player, ref notifyMsgs);

        //    //                // player.RunSupportChangedF(ref notifyMsgs);
        //    //                //player.this._Players[addItem.Key].SetMoneyCanSave = RoomMain.SetMoneyCanSave;
        //    //                //MoneyCanSaveChanged(player, player.MoneyForSave, ref notifyMsgs);

        //    //                SendMaxHolderInfoMation(player, ref notifyMsgs);

        //    //                //var players = this._Players;
        //    //                //foreach (var item in players)
        //    //                //{
        //    //                //    if (item.Value.TheLargestHolderKey == player.Key)
        //    //                //    {
        //    //                //        //  player.TheLargestHolderKeyChanged(item.Key, item.Value.TheLargestHolderKey, item.Key, ref notifyMsgs);
        //    //                //    }
        //    //                //}
        //    //                var list = player.usedRoadsList;
        //    //                for (var i = 0; i < list.Count; i++)
        //    //                {
        //    //                    this.DrawSingleRoadF(player, list[i], ref notifyMsgs);
        //    //                }

        //    //                //this._Players[getPosition.Key];

        //    //                gc._PlayerInGroup[getPosition.Key].MoneyChanged(player, gc._PlayerInGroup[getPosition.Key].Money, ref notifyMsgs);
        //    //                gc._PlayerInGroup[getPosition.Key].ShowLevelOfPlayerDetail(ref notifyMsgs);
        //    //                // this.DriverSelected(this._Players[getPosition.Key], this._Players[getPosition.Key].getCar(), ref notifyMsgs);
        //    //                result = new GetPositionResult()
        //    //                {
        //    //                    Success = true,
        //    //                    //CarsNames = carsNames,
        //    //                    Fp = fp,
        //    //                    FromUrl = fromUrl,
        //    //                    NotifyMsgs = notifyMsgs,
        //    //                    WebSocketID = webSocketID,
        //    //                    PlayerName = playerName,
        //    //                    positionInStation = gc._PlayerInGroup[getPosition.Key].positionInStation,
        //    //                    fPIndex = gc._PlayerInGroup[getPosition.Key].StartFPIndex,
        //    //                    groupNumber = gc.groupNumber
        //    //                };

        //    //                if (OpenMore == 0)
        //    //                {
        //    //                    CheckAllPromoteState(getPosition.Key, getPosition.GroupKey);
        //    //                    CheckCollectState(getPosition.Key, getPosition.GroupKey);
        //    //                    sendCarAbilityState(getPosition.Key, getPosition.GroupKey);
        //    //                    sendCarStateAndPurpose(getPosition.Key, getPosition.GroupKey);
        //    //                    askWhetherGoToPositon(getPosition.Key, getPosition.GroupKey, this.GetRandomPosObj);
        //    //                }
        //    //                else if (OpenMore > 0)
        //    //                {
        //    //                    CheckAllPromoteState(getPosition.Key, getPosition.GroupKey);
        //    //                    CheckCollectState(getPosition.Key, getPosition.GroupKey);

        //    //                    sendCarAbilityState(getPosition.Key, getPosition.GroupKey);
        //    //                    sendCarStateAndPurpose(getPosition.Key, getPosition.GroupKey);

        //    //                    gc._PlayerInGroup[getPosition.Key].getCar().UpdateSelection();//保证前台的3D建立
        //    //                    gc._PlayerInGroup[getPosition.Key].nntlF();
        //    //                    askWhetherGoToPositon(getPosition.Key, getPosition.GroupKey, this.GetRandomPosObj);


        //    //                    List<string> notifyMsgs2 = new List<string>();

        //    //                    {
        //    //                        var usedRoadsList = player.usedRoadsList;
        //    //                        for (int i = 0; i < usedRoadsList.Count; i++)
        //    //                        {
        //    //                            var roadCode = usedRoadsList[i];
        //    //                            player.DrawSingleRoadF(player, roadCode, ref notifyMsgs2);

        //    //                        }
        //    //                        var models = Program.dt.models;
        //    //                        var modelHasShowed_old = player.modelHasShowed;
        //    //                        player.modelHasShowed = new Dictionary<string, bool>();
        //    //                        for (int i = 0; i < models.Count; i++)
        //    //                        {
        //    //                            Data.detailmodel modelNeedToShow = models[i];
        //    //                            if (modelHasShowed_old.ContainsKey(modelNeedToShow.modelID))
        //    //                            {
        //    //                                player.rm.modelM.setModel(player, modelNeedToShow, ref notifyMsgs2);
        //    //                            }
        //    //                        }
        //    //                    }

        //    //                    for (int i = 0; i < notifyMsgs2.Count; i++)
        //    //                    {
        //    //                        result.NotifyMsgs.Add(notifyMsgs2[i]);
        //    //                    }
        //    //                    //Startup.sendSeveralMsgs(notifyMsgs2);
        //    //                }
        //    //            }
        //    //            else
        //    //                result = new GetPositionResult()
        //    //                {
        //    //                    Success = false
        //    //                };
        //    //        }
        //    //        else
        //    //        {
        //    //            result = new GetPositionResult()
        //    //            {
        //    //                Success = false
        //    //            };
        //    //        }
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    result = new GetPositionResult()
        //    //    {
        //    //        Success = false
        //    //    };
        //    //}

        //    //return result;
        //}

        private void askWhetherGoToPositon(string key, string groupKey, GetRandomPos gp)
        {
            //  throw new Exception();
            //groupKey = groupKey.Trim();
            //key = key.Trim();
            //if (string.IsNullOrEmpty(groupKey)) { }
            //else if (this._Groups.ContainsKey(groupKey))
            //{
            //    var group = this._Groups[groupKey];
            //    group.askWhetherGoToPositon(key, gp);


            //}
            ////  throw new NotImplementedException();
        }

        private void SendMaxHolderInfoMation(Player player, ref List<string> notifyMsgs)
        {
            //foreach (var item in this._Players)
            //{
            //    //  if (player.Key == item.Key) { }
            //    //else 
            //    {
            //        if (item.Value.TheLargestHolderKey == item.Key)
            //        {
            //            this.TheLargestHolderKeyChanged(item.Key, player.Key, player.Key, ref notifyMsgs);
            //        }
            //    }
            //}
        }

        public class GetPositionResult
        {
            public bool Success { get; set; }
            public string FromUrl { get; set; }
            public int WebSocketID { get; set; }
            //  public Model.FastonPosition Fp { get; set; }
            public int fPIndex { get; set; }
            //public string[] CarsNames { get; set; }
            public List<string> NotifyMsgs { get; set; }
            public string PlayerName { get; set; }
            public int positionInStation { get; set; }
            public int groupNumber { get; set; }
            public FPPosition Fp { get; internal set; }
        }
    }

    public partial class RoomMain : interfaceOfHM.ListenInterface
    {
        string interfaceOfHM.ListenInterface.ask(Ask v)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.ModelTranstractionI.ChargingMax()
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.ListenInterface.CheckCarStateF(CheckCarState ccs)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.ListenInterface.ConfirmPanelSelectResultF(ConfirmPanelSelectResult nwtgntb)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.Marketing.DouyinLogContentF(DouyinLogContent douyinLog)
        {
            throw new NotImplementedException();
        }

        public string ExitF(ExitObj obj)
        {
            {
                if (this._Groups.ContainsKey(obj.GroupKey))
                {
                    var group = this._Groups[obj.GroupKey];
                    if (group._PlayerInGroup.ContainsKey(obj.Key))
                    {
                        var role = group._PlayerInGroup[obj.Key];
                        if (role.playerType == Player.PlayerType.player)
                        {
                            var player = (Player)role;
                            var car = player.getCar();
                            if (string.IsNullOrEmpty(player.BTCAddress))
                            {
                                ExitObjResult r = new ExitObjResult()
                                {
                                    Success = false,
                                };
                                this.WebNotify(player, "您还没有登录！");
                                return Newtonsoft.Json.JsonConvert.SerializeObject(r);
                            }
                            else if (player.MoneyForSave > 0)
                            {
                                ExitObjResult r = new ExitObjResult()
                                {
                                    Success = false,
                                };
                                this.WebNotify(player, "你的积分还没有存储！");
                                return Newtonsoft.Json.JsonConvert.SerializeObject(r);
                            }
                            //else if (car.ability.HasDiamond())
                            //{
                            //    ExitObjResult r = new ExitObjResult()
                            //    {
                            //        Success = false,
                            //    };
                            //    this.WebNotify(player, "还有宝石没有释放！");
                            //    return Newtonsoft.Json.JsonConvert.SerializeObject(r);
                            //}
                            else
                            {
                                ExitObjResult r = new ExitObjResult()
                                {
                                    Success = true,
                                };
                                return Newtonsoft.Json.JsonConvert.SerializeObject(r);
                            }
                        }
                    }
                }

            }
            {
                ExitObjResult r = new ExitObjResult()
                {
                    Success = false,
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(r);
            }
        }

        string interfaceOfHM.ModelTranstractionI.GetAddrFromAndToWhenGenerateAgreementBetweenTwo(GAFATWGABT gaobj)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.MapEditor.GetBG(SetCrossBG ss)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.MapEditor.GetFirstRoad()
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.MapEditor.GetFPBGF(GetFPBG ss)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.ListenInterface.GetOnLineStateF(GetOnLineState obj)
        {
            throw new NotImplementedException();
        }

        public GetPositionResult GetPosition(GetPosition getPosition)
        {
            // throw new Exception("");

            GetPositionResult result;

            int OpenMore = -1;//第一次打开？
            var notifyMsgs = new List<string>();
            GroupClassF.GroupClass gc = null;
            //   lock (this.PlayerLock)
            {
                if (string.IsNullOrEmpty(getPosition.GroupKey))
                {
                    gc = null;
                }
                else if (this._Groups.ContainsKey(getPosition.GroupKey))
                {
                    gc = this._Groups[getPosition.GroupKey];
                }
            }
            if (gc != null)
            {
                // lock (gc.PlayerLock)
                {
                    if (gc._PlayerInGroup.ContainsKey(getPosition.Key))
                    {
                        if (gc._PlayerInGroup[getPosition.Key].playerType == Player.PlayerType.player)
                        {
                            var player = gc._PlayerInGroup[getPosition.Key];
                            var fp = Program.dt.GetFpByIndex(gc._PlayerInGroup[getPosition.Key].StartFPIndex);
                            var fromUrl = player.FromUrl;
                            var webSocketID = player.WebSocketID;
                            //var carsNames = this._Players[getPosition.Key].CarsNames;
                            var playerName = gc._PlayerInGroup[getPosition.Key].PlayerName;
                            /*
                             * 这已经走查过，在AddNewPlayer、UpdatePlayer时，others都进行了初始化
                             */
                            AddOtherPlayer(getPosition.Key, getPosition.GroupKey, ref notifyMsgs);
                            //   this.brokenParameterT1RecordChanged(getPosition.Key, getPosition.Key, this._Players[getPosition.Key].brokenParameterT1, ref notifyMsgs);
                            GetAllCarInfomationsWhenInitialize(getPosition.Key, getPosition.GroupKey, ref notifyMsgs);
                            //getAllCarInfomations(getPosition.Key, ref notifyMsgs);
                            OpenMore = gc._PlayerInGroup[getPosition.Key].OpenMore;

                            // var player = this._Players[getPosition.Key];
                            //var m2 = player.GetMoneyCanSave();

                            //    MoneyCanSaveChanged(player, m2, ref notifyMsgs);

                            SendPromoteCountOfPlayer("mile", player, ref notifyMsgs, false);
                            //  SendPromoteCountOfPlayer("business", player, ref notifyMsgs);
                            SendPromoteCountOfPlayer("volume", player, ref notifyMsgs, false);
                            SendPromoteCountOfPlayer("speed", player, ref notifyMsgs, false);

                            //   BroadCoastFrequency(player, ref notifyMsgs);
                            player.SetMoneyCanSave(player, ref notifyMsgs);

                            // player.RunSupportChangedF(ref notifyMsgs);
                            //player.this._Players[addItem.Key].SetMoneyCanSave = RoomMain.SetMoneyCanSave;
                            //MoneyCanSaveChanged(player, player.MoneyForSave, ref notifyMsgs);

                            SendMaxHolderInfoMation(player, ref notifyMsgs);

                            //var players = this._Players;
                            //foreach (var item in players)
                            //{
                            //    if (item.Value.TheLargestHolderKey == player.Key)
                            //    {
                            //        //  player.TheLargestHolderKeyChanged(item.Key, item.Value.TheLargestHolderKey, item.Key, ref notifyMsgs);
                            //    }
                            //}
                            //var list = player.usedRoadsList;
                            //for (var i = 0; i < list.Count; i++)
                            //{
                            //    this.DrawSingleRoadF(player, list[i], ref notifyMsgs);
                            //}

                            //this._Players[getPosition.Key];

                            gc._PlayerInGroup[getPosition.Key].MoneyChanged(player, gc._PlayerInGroup[getPosition.Key].Money, ref notifyMsgs);
                            gc._PlayerInGroup[getPosition.Key].ShowLevelOfPlayerDetail(ref notifyMsgs);
                            // this.DriverSelected(this._Players[getPosition.Key], this._Players[getPosition.Key].getCar(), ref notifyMsgs);
                            result = new GetPositionResult()
                            {
                                Success = true,
                                //CarsNames = carsNames,
                                Fp = fp,
                                FromUrl = fromUrl,
                                NotifyMsgs = notifyMsgs,
                                WebSocketID = webSocketID,
                                PlayerName = playerName,
                                positionInStation = gc._PlayerInGroup[getPosition.Key].positionInStation,
                                fPIndex = gc._PlayerInGroup[getPosition.Key].StartFPIndex,
                                groupNumber = gc.groupNumber
                            };

                            if (OpenMore == 0)
                            {
                                CheckAllPromoteState(getPosition.Key, getPosition.GroupKey);
                                CheckCollectState(getPosition.Key, getPosition.GroupKey);
                                sendCarAbilityState(getPosition.Key, getPosition.GroupKey);
                                sendCarStateAndPurpose(getPosition.Key, getPosition.GroupKey);
                                askWhetherGoToPositon(getPosition.Key, getPosition.GroupKey, this.GetRandomPosObj);
                                UpdateScean(getPosition.Key, getPosition.GroupKey, this.GetRandomPosObj);
                            }
                            else if (OpenMore > 0)
                            {
                                CheckAllPromoteState(getPosition.Key, getPosition.GroupKey);
                                CheckCollectState(getPosition.Key, getPosition.GroupKey);

                                sendCarAbilityState(getPosition.Key, getPosition.GroupKey);
                                sendCarStateAndPurpose(getPosition.Key, getPosition.GroupKey);

                                gc._PlayerInGroup[getPosition.Key].getCar().UpdateSelection();//保证前台的3D建立
                                gc._PlayerInGroup[getPosition.Key].nntlF();
                                askWhetherGoToPositon(getPosition.Key, getPosition.GroupKey, this.GetRandomPosObj);

                                UpdateScean(getPosition.Key, getPosition.GroupKey, this.GetRandomPosObj);
                            }
                        }
                        else
                            result = new GetPositionResult()
                            {
                                Success = false
                            };
                    }
                    else
                    {
                        result = new GetPositionResult()
                        {
                            Success = false
                        };
                    }
                }
            }
            else
            {
                result = new GetPositionResult()
                {
                    Success = false
                };
            }

            return result;
        }

        private void UpdateScean(string key, string groupKey, GetRandomPos getRandomPosObj)
        {
            List<string> notifyMsgs = new List<string>();

            UpdateBackground(key, groupKey, getRandomPosObj, ref notifyMsgs);
            UpdateSelection(key, groupKey, getRandomPosObj, ref notifyMsgs);
            UpdateCompass(key, groupKey, getRandomPosObj, ref notifyMsgs);
            UpdateGoldOjb(key, groupKey, getRandomPosObj, ref notifyMsgs);
            UpdateBaseTurbine(key, groupKey, getRandomPosObj, ref notifyMsgs);
            UpdataSatelite(key, groupKey, getRandomPosObj, ref notifyMsgs);
<<<<<<< HEAD
            UpdateBitcoinAddr(key, groupKey, getRandomPosObj, ref notifyMsgs);
=======
>>>>>>> 5bbb0cdf3f891fa27c4db97f97ae3529c7f58980

            Startup.sendSeveralMsgs(notifyMsgs);
            //   GetBackground()
            //  throw new NotImplementedException();


        }

        private void UpdateBitcoinAddr(string key, string groupKey, GetRandomPos grp, ref List<string> notifyMsgs)
        {
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                if (group._PlayerInGroup.ContainsKey(key))
                {
                    var player = group._PlayerInGroup[key];
                    var targetFpIndex = player.getCar().targetFpIndex;
                    //  var target = getRandomPosObj.GetSelections(targetFpIndex);


                    // var obj = GetItemGoldObj(player.WebSocketID, position);
                    // obj.hasValue = group.HasGold(targetFpIndex);
                    //if (player.BTCAddress == AdministratorAddr)
                    {
                        if (string.IsNullOrEmpty(grp.GetFpByIndex(targetFpIndex).BitcoinAddr))
                        {
                            var position = grp.GetBtcPosition(targetFpIndex);
                            //  var fs = Program.dt.GetFpByIndex(ti);
                            var infomation = Program.rm.GetBackgroundInfomation(player.WebSocketID, fs);
                            var url = player.FromUrl;
                            var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(infomation);
                            notifyMsg.Add(url);
                            notifyMsg.Add(sendMsg);
                        }
                        // obj.hasValue = true;
                    }

                    //var url = player.FromUrl;
                    //var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    //notifyMsgs.Add(url);
                    //notifyMsgs.Add(sendMsg);
                    //if (group.HasGold(targetFpIndex))
                    //{

                    //}
                    //else
                    //{

                    //}
                }
            }
        }

        string interfaceOfHM.Resistance.GetResistance(GetResistanceObj r)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.ListenInterface.GetRoadMeshF(GetRoadMesh tbm)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.Marketing.GetStockTradeCenterDetailF(GetStockTradeCenterDetail gstcd)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.ListenInterface.OrderToReturn(OrderToReturn otr, GetRandomPos grp)
        {
            throw new NotImplementedException();
        }



        void interfaceOfHM.ListenInterface.SaveInFileF(SaveInFile sif)
        {
            throw new NotImplementedException();
        }



        void interfaceOfHM.ListenInterface.SendMsg(DialogMsg dm)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.ListenInterface.SetAbility(SetAbility sa)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.Marketing.SetGroupIsLive(SetGroupLive sgl)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.Marketing.SetNextPlaceF(SetNextPlace snp)
        {
            throw new NotImplementedException();
        }

        public string SmallMapClickF(SmallMapClick smc)
        {
            GroupClass group = null;
            //  lock (this.PlayerLock)
            {
                if (this._Groups.ContainsKey(smc.GroupKey))
                {
                    group = this._Groups[smc.GroupKey];
                }
            }
            if (group != null)
            {
                group.SmallMapClickF(smc, this.GetRandomPosObj);
            }
            return "";
        }

        string interfaceOfHM.ListenInterface.TakeApartF(TakeApart t)
        {
            throw new NotImplementedException();
        }

        void interfaceOfHM.ListenInterface.TurnOnBeginnerModeF(TurnOnBeginnerMode tbm)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.ListenInterface.updateCollect(SetCollect sc, GetRandomPos grp)
        {
            throw new NotImplementedException();
        }



        string interfaceOfHM.ListenInterface.updatePromote(SetPromote sp, GetRandomPos grp)
        {
            throw new NotImplementedException();
        }

        string interfaceOfHM.ListenInterface.updateView(View v)
        {
            throw new NotImplementedException();
        }

        public string UpdateCurrentPosition(WebSelectPassData wspd, GetRandomPos grp)
        {
            if (this._Groups.ContainsKey(wspd.GroupKey))
            {
                List<string> notifyMsg = new List<string>();
                var group = this._Groups[wspd.GroupKey];
                if (group._PlayerInGroup.ContainsKey(wspd.Key))
                {
                    var player = group._PlayerInGroup[wspd.Key];
                    player.ActiveTime = DateTime.Now;
                    var success = group.UpdateCurrentPosition(player, grp, wspd, ref notifyMsg);
                    if (success)
                    {
                        UpdateBackground(wspd.Key, wspd.GroupKey, grp, ref notifyMsg);
                        UpdateSelection(wspd.Key, wspd.GroupKey, grp, ref notifyMsg);
                        UpdateCompass(wspd.Key, wspd.GroupKey, grp, ref notifyMsg);
                        UpdateGoldOjb(wspd.Key, wspd.GroupKey, grp, ref notifyMsg);
                        UpdateBaseTurbine(wspd.Key, wspd.GroupKey, grp, ref notifyMsg);
                        UpdataSatelite(wspd.Key, wspd.GroupKey, grp, ref notifyMsg);
<<<<<<< HEAD
                        UpdateBitcoinAddr(wspd.Key, wspd.GroupKey, grp, ref notifyMsg);
=======
>>>>>>> 5bbb0cdf3f891fa27c4db97f97ae3529c7f58980

                    }
                    GetBackground(player, ref notifyMsg);

                    this.frequencyM.addFrequencyRecord();
                }
                Startup.sendSeveralMsgs(notifyMsg);
            }
            return "";
            // throw new NotImplementedException();
        }



        public void NavigateF(Navigate nObj, GetRandomPos grp)
        {
            if (this._Groups.ContainsKey(nObj.GroupKey))
            {
                var group = this._Groups[nObj.GroupKey];
                group.NavigateF(nObj.Key, grp);
            }
            // player.Group.askWhetherGoToPositon(player.Key, grp);
            //  throw new NotImplementedException();
        }

        public string ReturnHomeF(ReturnHomePassData rhpd, GetRandomPos grp)
        {
            //   throw new NotImplementedException();
            if (this._Groups.ContainsKey(rhpd.GroupKey))
            {
                List<string> notifyMsgs = new List<string>();
                var group = this._Groups[rhpd.GroupKey];
                var success = group.ReturnHomeF(rhpd.Key, grp, ref notifyMsgs);
                if (success)
                {
                    UpdateBackground(rhpd.Key, rhpd.GroupKey, grp, ref notifyMsgs);
                    UpdateSelection(rhpd.Key, rhpd.GroupKey, grp, ref notifyMsgs);
                    UpdateCompass(rhpd.Key, rhpd.GroupKey, grp, ref notifyMsgs);
                    UpdateGoldOjb(rhpd.Key, rhpd.GroupKey, grp, ref notifyMsgs);
                    UpdateBaseTurbine(rhpd.Key, rhpd.GroupKey, grp, ref notifyMsgs);
                    UpdataSatelite(rhpd.Key, rhpd.GroupKey, grp, ref notifyMsgs);
<<<<<<< HEAD
                    UpdateBitcoinAddr(rhpd.Key, rhpd.GroupKey, grp, ref notifyMsgs);
=======
>>>>>>> 5bbb0cdf3f891fa27c4db97f97ae3529c7f58980
                }

                Startup.sendSeveralMsgs(notifyMsgs);
            }
            return "";
        }


    }

    public partial class RoomMain
    {

    }
}
