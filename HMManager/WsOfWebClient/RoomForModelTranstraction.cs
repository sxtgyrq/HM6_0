using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommonClass.ModelTranstraction;

namespace WsOfWebClient
{
    internal partial class Room
    {
        internal static string GetAllBusinessAddr(ConnectInfo.ConnectInfoDetail connectInfoDetail, RewardSet rs)
        {
            string r = "";
            if (AdministratorBTCAddr._addresses.ContainsKey(rs.administratorAddr))
            {
                if (BitCoin.Sign.checkSign(rs.signOfAdministrator, DateTime.Now.ToString("yyyyMMdd"), rs.administratorAddr))
                {
                    var ti = new AllBuiisnessAddr()
                    {
                        c = "AllBuiisnessAddr"
                    };
                    r = r.GetHashCode().ToString();
                    var index = rm.Next(0, roomUrls.Count);
                    var msg = Newtonsoft.Json.JsonConvert.SerializeObject(ti);
                    var info = Startup.sendInmationToUrlAndGetRes(Room.roomUrls[index], msg);
                    var f = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(info);
                    for (int i = 0; i < f.Count; i++)
                    {
                        var passObj = new
                        {
                            id = "buidingAddrForAddReward",
                            c = "addOption",
                            value = f[i]
                        };
                        var returnMsg = Newtonsoft.Json.JsonConvert.SerializeObject(passObj);
                        CommonF.SendData(returnMsg, connectInfoDetail, 0);
                        //var sendData = Encoding.UTF8.GetBytes(returnMsg);

                        //await webSocket.SendAsync(new ArraySegment<byte>(sendData, 0, sendData.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                        r = r.GetHashCode().ToString() + f[i];
                    }
                }
            }
            return r;
        }

        internal static void GetAllStockAddr(ConnectInfo.ConnectInfoDetail connectInfoDetail, AllStockAddr asa)
        {
            if (AdministratorBTCAddr._addresses.ContainsKey(asa.administratorAddr))
            {
                if (BitCoin.Sign.checkSign(asa.signOfAdministrator, DateTime.Now.ToString("yyyyMMdd"), asa.administratorAddr))
                {
                    if (BitCoin.CheckAddress.CheckAddressIsUseful(asa.bAddr))
                    {
                        //    Console.WriteLine(asa.bAddr);
                        //var index = rm.Next(0, roomUrls.Count);
                        //var msg = Newtonsoft.Json.JsonConvert.SerializeObject(asa);
                        //var info = await Startup.sendInmationToUrlAndGetRes(Room.roomUrls[index], msg);
                        //var f = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(info);

                        var trDetail = getValueOfAddr(asa.bAddr);
                        foreach (var i in trDetail)
                        {
                            //       Console.WriteLine($"{i.Key},{i.Value}");
                            var passObj = new
                            {
                                id = "stockAddrForAddReward",
                                c = "addOption",
                                value = $"{i.Key}:{i.Value}"
                            };
                            var returnMsg = Newtonsoft.Json.JsonConvert.SerializeObject(passObj);
                            CommonF.SendData(returnMsg, connectInfoDetail, 0);
                            //var sendData = Encoding.UTF8.GetBytes(returnMsg);
                            //await webSocket.SendAsync(new ArraySegment<byte>(sendData, 0, sendData.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                }
            }
        }

        internal static Dictionary<string, long> getValueOfAddr(string addr)
        {
            // BitCoin.Transtraction.TradeInfo t = new BitCoin.Transtraction.TradeInfo(addr);
            bool getTradeDetailSuccess;
            var tradeDetail = ConsoleBitcoinChainApp.GetData.GetTradeInfomationFromChain(addr, out getTradeDetailSuccess);

            if (getTradeDetailSuccess)
            {
                List<string> list;
                {
                    var grn = new GetTransctionModelDetail()
                    {
                        c = "GetTransctionModelDetail",
                        bussinessAddr = addr,
                    };
                    var index = rm.Next(0, roomUrls.Count);
                    var msg = Newtonsoft.Json.JsonConvert.SerializeObject(grn);
                    var json = Startup.sendInmationToUrlAndGetRes(Room.roomUrls[index], msg);
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(json);
                }
                var r = ConsoleBitcoinChainApp.GetData.SetTrade(ref tradeDetail, list);
                return r;
            }
            else
            {
                return new Dictionary<string, long>();
            }
        }

        internal static void GenerateRewardAgreementF(ConnectInfo.ConnectInfoDetail connectInfoDetail, GenerateRewardAgreement ga)
        {
            if (AdministratorBTCAddr._addresses.ContainsKey(ga.administratorAddr))
            {
                if (BitCoin.Sign.checkSign(ga.signOfAdministrator, DateTime.Now.ToString("yyyyMMdd"), ga.administratorAddr))
                {
                    if (
      BitCoin.CheckAddress.CheckAddressIsUseful(ga.addrBussiness) &&
      BitCoin.CheckAddress.CheckAddressIsUseful(ga.addrFrom) &&
      ga.tranNum >= 0.00000001
      )
                    {
                        int indexNumber = 0;
                        indexNumber = GetIndexOfTrade(ga.addrBussiness, ga.addrFrom);
                        if (indexNumber >= 0)
                        {
                            //var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(new CommonClass.MapEditor.DrawRoad()
                            //{
                            //    c = "DrawRoad",
                            //    roadCode = roadCode
                            //});
                            //var json = await Startup.sendInmationToUrlAndGetRes(roomUrl, sendMsg);

                            var agreement = $"{indexNumber}@{ga.addrFrom}@{ga.addrBussiness}->SetAsReward:{ga.tranNum}satoshi";
                            var passObj = new
                            {
                                agreement = agreement,
                                c = "ShowRewardAgreement"
                            };
                            var returnMsg = Newtonsoft.Json.JsonConvert.SerializeObject(passObj);
                            CommonF.SendData(returnMsg, connectInfoDetail, 0);
                            //var sendData = Encoding.UTF8.GetBytes(returnMsg);
                            //await webSocket.SendAsync(new ArraySegment<byte>(sendData, 0, sendData.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                }
            }

        }

        private static int GetIndexOfTrade(string addrBussiness, string addrFrom)
        {
            var ti = new TradeIndex()
            {
                c = "TradeIndex",
                addrFrom = addrFrom,
                addrBussiness = addrBussiness
            };
            var index = rm.Next(0, roomUrls.Count);
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(ti);
            var info = Startup.sendInmationToUrlAndGetRes(Room.roomUrls[index], msg);
            return Convert.ToInt32(info);
        }

        internal static void PublicReward(State s, ConnectInfo.ConnectInfoDetail connectInfoDetail, RewardPublicSign rewardPub)
        {
            var parameter = rewardPub.msg.Split(new char[] { '@', '-', '>', ':' }, StringSplitOptions.RemoveEmptyEntries);
            var firstIndex = rewardPub.msg.IndexOf('@');
            var secondIndex = rewardPub.msg.IndexOf('@', firstIndex + 1);
            if (secondIndex > firstIndex)
            {
            }
            else
            {
                return;
            }

            if (parameter.Length == 5)
            {
                if (BitCoin.Sign.checkSign(rewardPub.signOfAddrReward, rewardPub.msg, parameter[1]))
                {
                    if (BitCoin.Sign.checkSign(rewardPub.signOfAddrBussiness, rewardPub.msg, parameter[2]))
                    {
                        var tradeIndex = int.Parse(parameter[0]);

                        var addrFrom = parameter[1];
                        var addrBussiness = parameter[2];
                        var addrTo = parameter[3];
                        if (addrTo == "SetAsReward")
                        {
                            var indexV = GetIndexOfTrade(addrBussiness, addrFrom);
                            if (indexV < 0)
                            {
                                NotifyMsg(connectInfoDetail, $"错误的addrBussiness:{addrBussiness}");
                            }
                            else if (tradeIndex == indexV)
                            {
                                var passCoinStr = parameter[4];

                                if (passCoinStr.Substring(passCoinStr.Length - 7, 7) == "Satoshi" || passCoinStr.Substring(passCoinStr.Length - 7, 7) == "satoshi")
                                {
                                    var passCoin = Convert.ToInt64(passCoinStr.Substring(0, passCoinStr.Length - 7));
                                    if (passCoin > 0)
                                    {
                                        var trDetail = getValueOfAddr(addrBussiness);
                                        if (trDetail.ContainsKey(addrFrom))
                                        {
                                            if (trDetail[addrFrom] >= passCoin)
                                            {
                                                var tc = new TradeSetAsReward()
                                                {
                                                    tradeIndex = tradeIndex,
                                                    addrBussiness = addrBussiness,
                                                    addrReward = addrFrom,
                                                    c = "TradeSetAsReward",
                                                    msg = rewardPub.msg,
                                                    passCoin = passCoin,
                                                    signOfaddrBussiness = rewardPub.signOfAddrBussiness,
                                                    signOfAddrReward = rewardPub.signOfAddrReward
                                                };
                                                var index = rm.Next(0, roomUrls.Count);
                                                var msg = Newtonsoft.Json.JsonConvert.SerializeObject(tc);
                                                var info = Startup.sendInmationToUrlAndGetRes(Room.roomUrls[index], msg);

                                                var resultObj = Newtonsoft.Json.JsonConvert.DeserializeObject<TradeSetAsReward.Result>(info);
                                                {
                                                    NotifyMsg(connectInfoDetail, resultObj.msg);
                                                }
                                            }
                                            else
                                            {
                                                var notifyMsg = $"{addrFrom}没有足够的余额。";
                                                NotifyMsg(connectInfoDetail, notifyMsg);
                                            }
                                        }
                                        else
                                        {
                                            var notifyMsg = $"{addrFrom}没有足够的余额。";
                                            NotifyMsg(connectInfoDetail, notifyMsg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                NotifyMsg(connectInfoDetail, $"错误的tradeIndex:{tradeIndex}");
                            } 
                        }
                    }
                } 
            }
        }
    }
}
