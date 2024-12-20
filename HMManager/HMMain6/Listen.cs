﻿using CommonClass;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6
{
    internal class Listen
    {
        internal static void IpAndPort(string hostIP, int tcpPort)
        {
            var dealWith = new TcpFunction.ResponseC.DealWith(DealWith);
            TcpFunction.ResponseC.f.ListenIpAndPort(hostIP, tcpPort, dealWith);
        }
        private static string DealWith(string notifyJson, int port)
        {
            // Console.WriteLine(notifyJson);
            CommonClass.Command c = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.Command>(notifyJson);
            return DealWithInterfaceAndObj(Program.rm, c, notifyJson);

            return "";
        }
        static string DealWithInterfaceAndObj(interfaceOfHM.ListenInterface objI, CommonClass.Command c, string notifyJson)
        {
            /*
        * 这些方法，中间禁止线程暂停，即Thread.sleep()
        */
            string outPut = "haveNothingToReturn";
            switch (c.c)
            {
                case "PlayerAdd_V2":
                    {
                        CommonClass.PlayerAdd_V2 addItem = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.PlayerAdd_V2>(notifyJson);
                        var result = objI.AddPlayer(addItem, Program.rm, Program.dt);
                        outPut = result;

                    }; break;
                case "GetPosition":
                    {
                        CommonClass.GetPosition getPosition = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.GetPosition>(notifyJson);
                        //string fromUrl; 
                        var GPResult = objI.GetPosition(getPosition);
                        if (GPResult.Success)
                        {
                            CommonClass.GetPositionNotify_v2 notify = new CommonClass.GetPositionNotify_v2()
                            {
                                c = "GetPositionNotify_v2",
                                fp = GPResult.Fp,
                                WebSocketID = GPResult.WebSocketID,
                                key = getPosition.Key,
                                PlayerName = GPResult.PlayerName,
                                positionInStation = GPResult.positionInStation,
                                fPIndex = GPResult.fPIndex,
                                AsynSend = false, //这里之所以要同步发送，是因为刷新的时候不报错！,
                                groupNumber = GPResult.groupNumber,
                            };

                            Startup.sendSingleMsg(GPResult.FromUrl, Newtonsoft.Json.JsonConvert.SerializeObject(notify));
                            var notifyMsgs = GPResult.NotifyMsgs;
                            Startup.sendSeveralMsgs(notifyMsgs);
                        }
                        outPut = "ok";
                    }; break;
                case "PlayerCheck":
                    {
                        CommonClass.PlayerCheck checkItem = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.PlayerCheck>(notifyJson);
                        var result = objI.UpdatePlayer(checkItem);
                        outPut = result;
                    }; break;
                case "WebSelectPassData":
                    {
                        CommonClass.WebSelectPassData wspd = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.WebSelectPassData>(notifyJson);
                        var result = objI.UpdateCurrentPosition(wspd, Program.dt);
                        outPut = result;
                    }; break;
                case "ExitObj":
                    {
                        ExitObj obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ExitObj>(notifyJson);
                        outPut = objI.ExitF(obj);
                    }; break;
                case "OrderToSubsidize":
                    {
                        /*
                         * 这里意味着登录与获取积分！
                         */
                        CommonClass.OrderToSubsidize ots = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.OrderToSubsidize>(notifyJson);
                        objI.OrderToSubsidize(ots);
                        outPut = "ok";
                    }; break;
                case "SaveMoney":
                    {
                        CommonClass.SaveMoney saveMoney = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.SaveMoney>(notifyJson);
                        objI.SaveMoney(saveMoney);
                        outPut = "ok";
                    }; break;
                case "Navigate":
                    {
                        CommonClass.Navigate n = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.Navigate>(notifyJson);
                        objI.NavigateF(n, Program.dt);
                        outPut = "ok";
                    }; break;
                case "SmallMapClick":
                    {
                        SmallMapClick smc = Newtonsoft.Json.JsonConvert.DeserializeObject<SmallMapClick>(notifyJson);
                        outPut = objI.SmallMapClickF(smc);
                    }; break;
                case "CollectPassData":
                    {
                        CollectPassData cpd = Newtonsoft.Json.JsonConvert.DeserializeObject<CollectPassData>(notifyJson);
                        outPut = objI.CollectF(cpd, Program.dt);
                        outPut = "ok";
                    }; break;
                case "ChargePassData":
                    {
                        ChargePassData cpd = Newtonsoft.Json.JsonConvert.DeserializeObject<ChargePassData>(notifyJson);
                        outPut = objI.ChargeF(cpd, Program.dt);
                        outPut = "ok";
                    }; break;
                case "ReturnHomePassData":
                    {
                        ReturnHomePassData rhpd = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnHomePassData>(notifyJson);
                        outPut = objI.ReturnHomeF(rhpd, Program.dt);
                    }; break;
                case "CheckIsAdministrator":
                    {
                        CheckIsAdministrator cisA = Newtonsoft.Json.JsonConvert.DeserializeObject<CheckIsAdministrator>(notifyJson);
                        outPut = objI.CheckIsAdministratorF(cisA, Program.dt);
                    }; break;
                case "SqlCommand":
                    {
                        SqlCommand sq = Newtonsoft.Json.JsonConvert.DeserializeObject<SqlCommand>(notifyJson);
                        Program.dt.DealWithSql(sq);
                    }; break;
                case "UploadPositionJson":
                    {
                        UploadPositionJson upj = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadPositionJson>(notifyJson);
                        Program.dt.DealWithUploadJson(upj);
                    }; break;
                case "GetFrequency":
                    {
                        outPut = Program.rm.GetFrequency().ToString();
                    }; break;
                case "AllBuiisnessAddr":
                    {
                        outPut = objI.GetAllBuiisnessAddr(Program.dt);
                    }; break;
                case "GetTransctionModelDetail":
                    {
                        CommonClass.ModelTranstraction.GetTransctionModelDetail gtmd = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.ModelTranstraction.GetTransctionModelDetail>(notifyJson);
                        outPut = objI.GetTransctionModelDetail(gtmd);
                    }; break;
                case "TradeIndex":
                    {
                        CommonClass.ModelTranstraction.TradeIndex tc = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.ModelTranstraction.TradeIndex>(notifyJson);
                        outPut = objI.TradeIndex(tc);
                    }; break;
                case "TradeSetAsReward":
                    {

                        CommonClass.ModelTranstraction.TradeSetAsReward tsar = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.ModelTranstraction.TradeSetAsReward>(notifyJson);
                        outPut = objI.TradeSetAsRewardF(tsar);
                    }; break;
                case "GetCurrentPlaceBitcoinAddr":
                    {
                        CommonClass.ModelTranstraction.GetCurrentPlaceBitcoinAddr gcpb = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.ModelTranstraction.GetCurrentPlaceBitcoinAddr>(notifyJson);
                        outPut = objI.GetCurrentPlaceBitcoinAddrF(gcpb, Program.dt);
                    }; break;
                case "GetTransctionFromChain":
                    {
                        /*
                         * 从区块链获取数据
                         */
                        CommonClass.ModelTranstraction.GetTransctionFromChain gtfc = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.ModelTranstraction.GetTransctionFromChain>(notifyJson);
                        outPut = objI.GetTransctionFromChainF(gtfc);
                    }; break;
                case "MarketPrice":
                    {
                        CommonClass.MarketPrice sa = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.MarketPrice>(notifyJson);
                        objI.MarketUpdate(sa);
                    }; break;
                case "ServerStatictis":
                    {
                        CommonClass.ServerStatictis ss = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.ServerStatictis>(notifyJson);
                        outPut = objI.Statictis(ss);
                    }; break;
                case "ModelStock":
                    {
                        CommonClass.ModelStock sa = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.ModelStock>(notifyJson);
                        objI.UpdateModelStock(sa);
                    }; break;
                case "SetPromote":
                    {
                        //有了Player.Ts,此方法在后台调用！
                        //后台调用需要有后台调用的放发。先设置Player.Ts，再调用！
                        CommonClass.SetPromote sp = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.SetPromote>(notifyJson);
                        var result = objI.updatePromote(sp, Program.dt);
                        outPut = "ok";
                        //await context.Response.WriteAsync("ok");
                    }; break;
            }
            return outPut;
        }
    }
}
