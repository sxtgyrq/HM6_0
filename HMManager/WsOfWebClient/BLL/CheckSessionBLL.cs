﻿using CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WsOfWebClient.BLL
{
    class CheckSessionBLL
    {
        internal class CheckIsOKResult
        {
            public bool CheckOK { get; set; }
            public int roomIndex { get; set; }

            /// <summary>
            /// 由AddPlayer 产生的key
            /// </summary>
            public string Key { get; set; }
            public string GroupKey { get; internal set; }
        }
        internal static CheckIsOKResult checkIsOK(CheckSession checkSession, State s)
        {
            //
            try
            {
                if (string.IsNullOrEmpty(checkSession.session))
                {
                    return new CheckIsOKResult()
                    {
                        CheckOK = false,
                        roomIndex = -1,
                        Key = "不存在"
                    };
                }
                else
                {
                    var playerCheck = Newtonsoft.Json.JsonConvert.DeserializeObject<PlayerCheck>(checkSession.session);
                    playerCheck.c = "PlayerCheck";
                    playerCheck.FromUrl = $"{ConnectInfo.HostIP}:{ConnectInfo.tcpServerPort}";//ConnectInfo.ConnectedInfo + "/notify";
                                                                                              // pl
                    playerCheck.WebSocketID = s.WebsocketID;

                    if (Room.CheckSign(playerCheck))
                    {
                        var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(playerCheck);
                        var reqResult = Startup.sendInmationToUrlAndGetRes(Room.roomUrls[playerCheck.RoomIndex], sendMsg);
                        if (reqResult.ToLower() == "ok")
                        {
                            s.roomIndex = playerCheck.RoomIndex;
                            return new CheckIsOKResult()
                            {
                                CheckOK = true,
                                roomIndex = playerCheck.RoomIndex,
                                Key = playerCheck.Key,
                                GroupKey = playerCheck.GroupKey,
                            };
                        }
                    }
                    return new CheckIsOKResult()
                    {
                        CheckOK = false,
                        roomIndex = -1,
                        Key = "不存在"
                    };
                }
            }
            catch
            {
                return new CheckIsOKResult()
                {
                    CheckOK = false,
                    roomIndex = -1,
                    Key = "不存在"
                };
            }

        }

        internal static string getSession()
        {
            return "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa_0";
        }
        public static string RoomInfoRegexPattern = $"^\\{{\\\"Key\\\":\\\"{"[0-9a-f]{32}"}\\\",\\\"GroupKey\\\":\\\"{"[0-9a-f]{32}"}\\\",\\\"FromUrl\\\":\\\"\\\",\\\"RoomIndex\\\":{"[0-9]{1,5}"},\\\"Check\\\":\\\"{"[0-9a-f]{32}"}\\\",\\\"WebSocketID\\\":{"[0-9]{1,10}"},\\\"PlayerName\\\":\\\"{"[\u4e00-\u9fa5]{1}[a-zA-Z0-9\u4e00-\u9fa5]{1,8}"}\\\",\\\"RefererAddr\\\":\\\"{"[0-9a-zA-z]{0,99}"}\\\",\\\"groupMemberCount\\\":{"[0-9]{1,10}"},\\\"c\\\":\\\"{"PlayerAdd_V2"}\\\"\\}}$";
    }
}
