using CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HMMain6.RoomMainF
{
    public partial class RoomMain
    {
        const string AdministratorAddr = "1NyrqneGRxTpCohjJdwKruM88JyARB2Ljr";
        private void UpdateAdministrator(Player player, ref List<string> notifyMsg)
        {
            if (player.BTCAddress == AdministratorAddr)
            {
                var url = player.FromUrl;
                IsAdministrator lmdb = new IsAdministrator()
                {
                    c = "IsAdministrator",
                    WebSocketID = player.WebSocketID,
                };
                var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(lmdb);
                notifyMsg.Add(url);
                notifyMsg.Add(sendMsg);
            }
        }

        public string CheckIsAdministratorF(CheckIsAdministrator cisA, GetRandomPos grp)
        {
            if (this._Groups.ContainsKey(cisA.GroupKey))
            {
                var group = this._Groups[cisA.GroupKey];
                if (group._PlayerInGroup.ContainsKey(cisA.Key))
                {
                    var player = group._PlayerInGroup[cisA.Key];
                    if (player.BTCAddress == AdministratorAddr)
                    {
                        return "ok";
                    }
                }
            }
            return "ng";
            // throw new NotImplementedException();
        }




        public string TradeCoinF(ModelTranstraction.TradeCoin tc, bool bySystem)
        {
            // throw new Exception();

            var parameter = tc.msg.Split(new char[] { '@', '-', '>', ':' }, StringSplitOptions.RemoveEmptyEntries);
            //  var agreement = $"{indexNumber}@{ga.addrFrom}@{ga.addrBussiness}->{ga.addrTo}:{ga.tranNum * 100000000}Satoshi";
            var regex = new Regex("^[0-9]{1,8}@[A-HJ-NP-Za-km-z1-9]{1,50}@[A-HJ-NP-Za-km-z1-9]{1,50}->[A-HJ-NP-Za-km-z1-9]{1,50}:[0-9]{1,13}[Ss]{1}atoshi$");
            if (regex.IsMatch(tc.msg))
            {
                if (parameter.Length == 5)
                {
                    if (BitCoin.Sign.checkSign(tc.sign, tc.msg, parameter[1]))
                    {
                        var tradeIndex = int.Parse(parameter[0]);
                        var addrFrom = parameter[1];
                        var addrBussiness = parameter[2];
                        var addrTo = parameter[3];
                        var passCoinStr = parameter[4];
                        if ((passCoinStr.Substring(passCoinStr.Length - 7, 7) == "Satoshi" || passCoinStr.Substring(passCoinStr.Length - 7, 7) == "satoshi") &&
                            tradeIndex == tc.tradeIndex &&
                            addrFrom == tc.addrFrom &&
                            addrTo == tc.addrTo &&
                            addrBussiness == tc.addrBussiness)
                        {
                            bool success;
                            var trDetail = getValueOfAddr(addrBussiness, out success);
                            if (success)
                            {
                                var passCoin = Convert.ToInt64(passCoinStr.Substring(0, passCoinStr.Length - 7));
                                if (passCoin > 0 && tc.passCoin == passCoin)
                                {
                                    if (trDetail.ContainsKey(addrFrom))
                                    {
                                        if (trDetail[addrFrom] >= passCoin)
                                        {
                                            string notifyMsg;
                                            if (tc.addrTo.Trim() != tc.addrBussiness.Trim())
                                            {
                                                bool r;
                                                if (bySystem)
                                                    r = DalOfAddress.HMSever.TradeRecord.AddBySystem(tc.tradeIndex, tc.addrFrom, tc.addrBussiness, tc.sign, tc.msg, tc.passCoin, out notifyMsg);
                                                else if (tc.addrTo.Trim() == this.Market.TradingCenterAddr.Trim())
                                                {
                                                    r = DalOfAddress.HMSever.TradeRecord.AddToTradeCenterBySystem(tc.tradeIndex, tc.addrFrom, tc.addrBussiness, tc.sign, tc.msg, tc.passCoin, out notifyMsg);
                                                }
                                                else
                                                {
                                                    r = DalOfAddress.HMSever.TradeRecord.Add(tc.tradeIndex, tc.addrFrom, tc.addrBussiness, tc.sign, tc.msg, tc.passCoin, out notifyMsg);
                                                    {
                                                        //List<Player> playerOperatings = new List<Player>();
                                                        //foreach (var item in this._Players)
                                                        //{
                                                        //    if (item.Value.playerType == Player.PlayerType.player)
                                                        //    {
                                                        //        var player = (Player)item.Value;
                                                        //        if (player.BTCAddress == addrFrom)
                                                        //        {
                                                        //            playerOperatings.Add(player);
                                                        //        }
                                                        //    }
                                                        //}
                                                        {
                                                            /*
                                                             * 由于这里没有采用传输Key,WsOfWebClient传输的对象不固定，
                                                             * 
                                                             */
                                                            //var data = DalOfAddress.TaskCopy.GetALLItem(addrFrom);
                                                            // this.taskM.TradeCoinF(playerOperatings.ToArray(), tc.addrBussiness, tc.addrTo, tc.passCoin, data);
                                                        }
                                                    }
                                                }
                                                var objR = new ModelTranstraction.TradeCoin.Result()
                                                {
                                                    msg = notifyMsg,
                                                    success = r
                                                };
                                                return Newtonsoft.Json.JsonConvert.SerializeObject(objR);
                                            }
                                            else
                                            {
                                                var r = DalOfAddress.HMSever.TradeRecord.AddWithBTCExtracted(tc.tradeIndex, tc.addrFrom, tc.addrBussiness, tc.sign, tc.msg, tc.passCoin, out notifyMsg);
                                                var objR = new ModelTranstraction.TradeCoin.Result()
                                                {
                                                    msg = notifyMsg,
                                                    success = r
                                                };
                                                return Newtonsoft.Json.JsonConvert.SerializeObject(objR);
                                            }
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
            {
                var objR = new ModelTranstraction.TradeCoin.Result()
                {
                    msg = "传输错误，校验数据为无效！",
                    success = false
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(objR);
            }
        }

        static Dictionary<string, long> getValueOfAddr(string addr, out bool tradeDetailSuccess)
        {
            var tradeDetail = ConsoleBitcoinChainApp.GetData.GetTradeInfomationFromChain(addr, out tradeDetailSuccess);
            //t1.Wait();
            //var tradeDetail = t1.GetAwaiter().GetResult();
            //var tradeDetail = await ConsoleBitcoinChainApp.GetData.GetTradeInfomationFromChain(addr);
            if (tradeDetailSuccess)
            {
                var list = DalOfAddress.HMSever.TradeRecord.GetAll(addr);
                var r = ConsoleBitcoinChainApp.GetData.SetTrade(ref tradeDetail, list);
                return r;
            }
            else
            {
                return new Dictionary<string, long>();
            }
        }

        public void UpdateModelStock(ModelStock sa)
        {
            Program.dt.LoadStock(sa);
            //grp.LoadStock(sa);
            // throw new NotImplementedException();
        }
    }

    public partial class RoomMain : interfaceOfHM.ModelTranstractionI
    {
        public string GetTransctionFromChainF(ModelTranstraction.GetTransctionFromChain gtfc)
        {
            if (this.Market.mile_Price == null)
                return Newtonsoft.Json.JsonConvert.SerializeObject(new Dictionary<string, long>());
            else
                return Market.Send(gtfc);
            //    throw new NotImplementedException();
        }

        public string GetAllBuiisnessAddr(GetRandomPos grp)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < grp.GetFpCount(); i++)
            {
                if (string.IsNullOrEmpty(grp.GetFpByIndex(i).BitcoinAddr)) { }
                else
                {
                    list.Add(grp.GetFpByIndex(i).BitcoinAddr);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
            //   throw new NotImplementedException();
        }

        //public string GetAllBuiisnessAddr(Data dt)
        //{
        //    throw new NotImplementedException();
        //}
        public string GetTransctionModelDetail(ModelTranstraction.GetTransctionModelDetail gtmd)
        {
            var r = DalOfAddress.HMSever.TradeRecord.GetAll(gtmd.bussinessAddr);
            return Newtonsoft.Json.JsonConvert.SerializeObject(r);
        }
        public string TradeIndex(ModelTranstraction.TradeIndex tc)
        {
            var Index = DalOfAddress.HMSever.TradeRecord.GetCount(tc.addrBussiness, tc.addrFrom);
            return Index.ToString();
        }

        public string TradeSetAsRewardF(ModelTranstraction.TradeSetAsReward tsar)
        {
            string dateStrng;
            var dateTime = DateTime.Now;
            for (int i = 0; i < tsar.afterWeek; i++)
            {
                dateTime = dateTime.AddDays(7);
            }
            while (dateTime.DayOfWeek != DayOfWeek.Monday)
            {
                dateTime = dateTime.AddDays(1);
            }
            dateStrng = dateTime.ToString("yyyyMMdd");
            int dataInt = int.Parse(dateStrng);

            var regex = new Regex("^[0-9]{1,8}@[A-HJ-NP-Za-km-z1-9]{1,50}@[A-HJ-NP-Za-km-z1-9]{1,50}->SetAsReward:[0-9]{1,13}[Ss]{1}atoshi$");

            if (regex.IsMatch(tsar.msg))
            {
                var parameter = tsar.msg.Split(new char[] { '@', '-', '>', ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (BitCoin.Sign.checkSign(tsar.signOfAddrReward, tsar.msg, parameter[1]))
                {
                    if (BitCoin.Sign.checkSign(tsar.signOfaddrBussiness, tsar.msg, parameter[2]))
                    {
                        var tradeIndex = int.Parse(parameter[0]);

                        var addrFrom = parameter[1];
                        var addrBussiness = parameter[2];
                        var addrTo = parameter[3];
                        if (addrTo == "SetAsReward" &&
                            tradeIndex == tsar.tradeIndex &&
                            addrFrom == tsar.addrReward &&
                            addrBussiness == tsar.addrBussiness
                            )
                        {
                            if (tradeIndex == GetIndexOfTrade(addrBussiness, addrFrom))
                            {
                                var passCoinStr = parameter[4];

                                if (passCoinStr.Substring(passCoinStr.Length - 7, 7) == "Satoshi" || passCoinStr.Substring(passCoinStr.Length - 7, 7) == "satoshi")
                                {
                                    var passCoin = Convert.ToInt64(passCoinStr.Substring(0, passCoinStr.Length - 7));
                                    if (passCoin > 0 && passCoin == tsar.passCoin)
                                    {
                                        bool getTradeDetailSuccess;
                                        var trDetail = getValueOfAddr(addrBussiness, out getTradeDetailSuccess);
                                        if (getTradeDetailSuccess)
                                            if (trDetail.ContainsKey(addrFrom))
                                            {
                                                if (trDetail[addrFrom] >= passCoin)
                                                {
                                                    bool success;
                                                    var msg = DalOfAddress.HMSever.TradeReward.Update(out success, dataInt, tsar.tradeIndex, tsar.addrReward, tsar.addrBussiness, tsar.passCoin, tsar.signOfAddrReward, tsar.signOfaddrBussiness, tsar.msg);
                                                    return Newtonsoft.Json.JsonConvert.SerializeObject(new ModelTranstraction.TradeSetAsReward.Result()
                                                    {
                                                        msg = msg,
                                                        success = success
                                                    });
                                                }
                                                else
                                                {

                                                }
                                            }
                                            else
                                            {
                                            }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(new ModelTranstraction.TradeSetAsReward.Result()
            {
                msg = "数据传输错误",
                success = false
            });
        }

        static int GetIndexOfTrade(string addrBussiness, string addrFrom)
        {
            var Index = DalOfAddress.HMSever.TradeRecord.GetCount(addrBussiness, addrFrom);
            return Index;
        }

        public string GetCurrentPlaceBitcoinAddrF(ModelTranstraction.GetCurrentPlaceBitcoinAddr gcpb, GetRandomPos grp)
        {
            if (this._Groups.ContainsKey(gcpb.GroupKey))
            {
                var group = this._Groups[gcpb.GroupKey];
                if (group._PlayerInGroup.ContainsKey(gcpb.Key))
                {
                    var player = group._PlayerInGroup[gcpb.Key];
                    return grp.GetFpByIndex(player.getCar().targetFpIndex).BitcoinAddr;

                }
            }
            return "";
        }

        public string GetRewardFromBuildingF(GetRewardFromBuildingM m, GetRandomPos grp)
        {
            return this.modelM.GetRewardFromBuildingF(m, grp);
        }

    }
}
