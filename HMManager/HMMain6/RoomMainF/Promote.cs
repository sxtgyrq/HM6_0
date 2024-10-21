using CommonClass;
using ModelBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.RoomMainF
{
    public partial class RoomMain : interfaceOfHM.Promote
    {
        public void SetLookForPromote(GetRandomPos gp)
        {
            //this.promoteMilePosition = GetRandomPosition(true, gp);
            //this.promoteBusinessPosition = GetRandomPosition(true, gp);
            //this.promoteVolumePosition = GetRandomPosition(true, gp);
            //this.promoteSpeedPosition = GetRandomPosition(true, gp);
        }

        private void SendPromoteCountOfPlayer(string pType, Player player, ref List<string> notifyMsgs, bool asynSend)
        {
            if (!(pType == "mile" || pType == "volume" || pType == "speed"))
            {

            }
            else
            {
                var count = player.PromoteDiamondCount[pType];
                var obj = new BradCastPromoteDiamondCount
                {
                    c = "BradCastPromoteDiamondCount",
                    count = count,
                    WebSocketID = player.WebSocketID,
                    pType = pType,
                    AsynSend = asynSend,
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                notifyMsgs.Add(player.FromUrl);
                notifyMsgs.Add(json);
            }
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 获取一个玩家的 四个能力提升点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private void CheckAllPromoteState(string key, string groupKey)
        {
            CheckPromoteState(key, groupKey, "mile");
            //   CheckPromoteState(key, "business");
            CheckPromoteState(key, groupKey, "volume");
            CheckPromoteState(key, groupKey, "speed");
        }
        private void CheckPromoteState(string key, string groupKey, string promoteType)
        {
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                group.CheckPromoteState(key, promoteType);
            }
            //string url = "";
            //string sendMsg = "";
            //lock (this.PlayerLock)
            //    if (this._Players.ContainsKey(key))
            //        if (this._Players[key].playerType == Player.PlayerType.player)
            //            if (((Player)this._Players[key]).PromoteState[promoteType] == this.getPromoteState(promoteType))
            //            {
            //            }
            //            else
            //            {
            //                var infomation = Program.rm.GetPromoteInfomation(((Player)this._Players[key]).WebSocketID, promoteType);
            //                url = ((Player)this._Players[key]).FromUrl;
            //                sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(infomation);
            //                ((Player)this._Players[key]).PromoteState[promoteType] = this.getPromoteState(promoteType);
            //            }
            //if (!string.IsNullOrEmpty(url))
            //{
            //    Startup.sendSingleMsg(url, sendMsg);
            //}
        }


        //   UpdateBaseTurbine()；
        private void UpdateBaseTurbine(string key, string groupKey, GetRandomPos grp, ref List<string> notifyMsgs)
        {
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                if (group._PlayerInGroup.ContainsKey(key))
                {
                    var player = group._PlayerInGroup[key];
                    var targetFpIndex = player.getCar().targetFpIndex;
                    //  var target = getRandomPosObj.GetSelections(targetFpIndex);
                    // if (targetFpIndex == player.StartFPIndex)
                    {
                        var position = grp.GetTurbine(targetFpIndex);
                        var obj = GetItemTurbine(player.WebSocketID, position);
                        obj.hasValue = targetFpIndex == player.StartFPIndex;
                        if (player.BTCAddress == AdministratorAddr)
                        {
                            if (grp.GetFpByIndex(targetFpIndex).CanGetScore)
                                obj.hasValue = true;
                        }
                        var url = player.FromUrl;
                        var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                        notifyMsgs.Add(url);
                        notifyMsgs.Add(sendMsg);
                    }

                    //if (group.HasGold(targetFpIndex))
                    //{

                    //}
                    //else
                    //{

                    //}
                }
            }
        }

        private BradCastTurbine GetItemTurbine(int webSocketID, CompassPosition position)
        {
            var obj = new BradCastTurbine
            {
                c = "BradCastTurbine",
                WebSocketID = webSocketID,
                position = position
            };
            return obj;
        }

        private void UpdateRedDiamond(string key, string groupKey, GetRandomPos grp, ref List<string> notifyMsgs)
        {
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                if (group._PlayerInGroup.ContainsKey(key))
                {
                    var player = group._PlayerInGroup[key];
                    var targetFpIndex = player.getCar().targetFpIndex;
                    //  var target = getRandomPosObj.GetSelections(targetFpIndex);
                    // if (targetFpIndex == player.StartFPIndex)
                    {
                        var position = grp.GetRedDiamond(targetFpIndex);
                        var obj = GetItemBattery(player.WebSocketID, position);
                        obj.hasValue = targetFpIndex == group.promoteMilePosition;
                        if (player.BTCAddress == AdministratorAddr)
                        {
                            if (grp.GetFpByIndex(targetFpIndex).CanGetScore)
                                obj.hasValue = true;
                        }
                        var url = player.FromUrl;
                        var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                        notifyMsgs.Add(url);
                        notifyMsgs.Add(sendMsg);
                    }

                    //if (group.HasGold(targetFpIndex))
                    //{

                    //}
                    //else
                    //{

                    //}
                }
            }
        }

        private BradCastBattery GetItemBattery(int webSocketID, CompassPosition position)
        {
            var obj = new BradCastBattery
            {
                c = "BradCastBattery",
                WebSocketID = webSocketID,
                position = position
            };
            return obj;
        }

        private void UpdateBlueDiamond(string key, string groupKey, GetRandomPos grp, ref List<string> notifyMsgs)
        {
            return;
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                if (group._PlayerInGroup.ContainsKey(key))
                {
                    var player = group._PlayerInGroup[key];
                    var targetFpIndex = player.getCar().targetFpIndex;
                    //  var target = getRandomPosObj.GetSelections(targetFpIndex);
                    // if (targetFpIndex == player.StartFPIndex)
                    {
                        var position = grp.GetBlueDiamond(targetFpIndex);
                        var obj = GetItemBattery(player.WebSocketID, position);
                        obj.hasValue = targetFpIndex == group.promoteMilePosition;
                        if (player.BTCAddress == AdministratorAddr)
                        {
                            if (grp.GetFpByIndex(targetFpIndex).CanGetScore)
                                obj.hasValue = true;
                        }
                        var url = player.FromUrl;
                        var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                        notifyMsgs.Add(url);
                        notifyMsgs.Add(sendMsg);
                    }

                    //if (group.HasGold(targetFpIndex))
                    //{

                    //}
                    //else
                    //{

                    //}
                }
            }
        }

    }
}
