using CommonClass;
using CommonClass.MateWsAndHouse;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static WsOfWebClient.ConnectInfo;

namespace WsOfWebClient
{
    public class CommonF
    {
        public static void SendData(string sendMsg, ConnectInfo.ConnectInfoDetail detail, int outTime)
        {
            try
            {
                lock (detail.LockObj)
                {
                    // detail.datas.Add(sendMsg);
                    var sendData = Encoding.UTF8.GetBytes(sendMsg);
                    CancellationToken timeOut;
                    if (outTime < 60000)
                        timeOut = new CancellationTokenSource(60000).Token;
                    else
                        timeOut = new CancellationTokenSource(outTime).Token;
                    var t = detail.ws.SendAsync(new ArraySegment<byte>(sendData, 0, sendData.Length), WebSocketMessageType.Text, true, timeOut);
                    t.GetAwaiter().GetResult();
                }
                //Thread th = new Thread(() => SendMsgF(detail, outTime));
                //th.Start();
                //lock (detail.LockObj)
                //detail.ws.SendAsync

                {



                }
                //while (!t.IsCompleted && !timeOut.IsCancellationRequested)
                //{
                //    //  await Task.Delay(100).ConfigureAwait(false);
                //}
            }
            catch { }
        }


    }
    internal partial class Room
    {
        internal static bool CheckSign(PlayerCheck playerCheck)
        {
            var roomUrl = roomUrls[playerCheck.RoomIndex];
            var check = CommonClass.Random.GetMD5HashFromStr(playerCheck.Key + roomUrl + CheckParameter);
            return playerCheck.Check == check;
        }
        internal static bool ExitF(ref State s, ConnectInfo.ConnectInfoDetail connectInfoDetail)
        {

            var exitObj = new ExitObj()
            {
                c = "ExitObj",
                Key = s.Key,
                GroupKey = s.GroupKey
            };
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(exitObj);
            var respon = Startup.sendInmationToUrlAndGetRes(Room.roomUrls[s.roomIndex], msg);
            ExitObj.ExitObjResult r = Newtonsoft.Json.JsonConvert.DeserializeObject<ExitObj.ExitObjResult>(respon);
            if (r.Success)
            {
                var obj = new
                {
                    c = "ClearSession",
                    Key = s.Key,
                };
                msg = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                CommonF.SendData(msg, connectInfoDetail, 0);
                var checkIsOk = CheckRespon(connectInfoDetail, "ClearSession");
                if (checkIsOk)
                {
                    s = Room.setState(s, connectInfoDetail, LoginState.empty);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
                return false;
        }

        public static State GetRoomThenStart(State s, ConnectInfo.ConnectInfoDetail connectInfoDetail, string playerName, string refererAddr, int groupMemberCount)
        {
            /*
             * 单人组队下
             */
            int roomIndex;
            var roomInfo = Room.getRoomNum(s.WebsocketID, playerName, refererAddr, groupMemberCount);

            roomIndex = roomInfo.RoomIndex;
            //  Console.WriteLine(roomInfo.RoomIndex);
            s.Key = roomInfo.Key;
            var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(roomInfo);
            var receivedMsg = Startup.sendInmationToUrlAndGetRes(Room.roomUrls[roomInfo.RoomIndex], sendMsg);
            if (receivedMsg == "ok")
            {
                //  Console.WriteLine(receivedMsg);
                WriteSession(roomInfo, connectInfoDetail);
                s.roomIndex = roomIndex;
                s.GroupKey = roomInfo.GroupKey;
                s = setOnLine(s, connectInfoDetail);

            }
            else
            {
                NotifyMsg(connectInfoDetail, "进入房间失败！");
            }
            return s;
        }


        internal static void Alert(ConnectInfo.ConnectInfoDetail connectInfoDetail, string alertMsg)
        {
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new { c = "Alert", msg = alertMsg });
            CommonF.SendData(msg, connectInfoDetail, 0);
        }

        internal static State ClearOffLineAfterCreateTeam(State s, ConnectInfo.ConnectInfoDetail connectInfoDetail, TeamResult team, string playerName, string refererAddr)
        {
            var receivedMsg = Team.SetToClear(team);
            // s = Room.setState(s, connectInfoDetail, LoginState.selectSingleTeamJoin);
            return s;
        }

        internal static State CancelAfterCreateTeam(State s, ConnectInfo.ConnectInfoDetail connectInfoDetail, TeamResult team, string playerName, string refererAddr)
        {
            var receivedMsg = Team.SetToExit(team);
            s = Room.setState(s, connectInfoDetail, LoginState.selectSingleTeamJoin);
            return s;
        }

        public static State GetRoomThenStartAfterCreateTeam(State s, ConnectInfo.ConnectInfoDetail connectInfoDetail, TeamResult team, string playerName, string refererAddr)
        {
            /*
             * 组队，队长状态下，队长点击了开始
             */
            int roomMember = Team.TeamMemberCountResult(team.TeamNumber);
            if (roomMember < 1)
            {
                roomMember = 1;
            }
            int roomIndex;


            var roomInfo = Room.getRoomNum(s.WebsocketID, playerName, refererAddr, roomMember);
            roomIndex = roomInfo.RoomIndex;
            s.Key = roomInfo.Key;
            s.GroupKey = roomInfo.GroupKey;
            var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(roomInfo);

            var receivedMsg = Startup.sendInmationToUrlAndGetRes(Room.roomUrls[roomInfo.RoomIndex], sendMsg);
            if (receivedMsg == "ok")
            {
                receivedMsg = Team.SetToBegain(team, roomInfo);
                WriteSession(roomInfo, connectInfoDetail);
                s.roomIndex = roomIndex;
                s = setOnLine(s, connectInfoDetail);

                //receivedMsg = receivedMsg.Substring(0, 2);
            }
            else
            {
                NotifyMsg(connectInfoDetail, "进入房间失败！");
            }
            return s;
        }

        private static System.Random rm = new System.Random(DateTime.Now.GetHashCode());
        internal static PlayerAdd_V2 getRoomNum(int websocketID, string playerName, string refererAddr, int groupMemberCount)
        {
            int roomIndex = 0;
            {
                var index1 = rm.Next(roomUrls.Count);
                var index2 = rm.Next(roomUrls.Count);
                if (index1 == index2)
                {
                    roomIndex = index1;
                }
                else
                {
                    var frequency1 = getFrequency(Room.roomUrls[index1]); ; //Startup.sendInmationToUrlAndGetRes(Room.roomUrls[roomInfo.RoomIndex], sendMsg);
                    var frequency2 = getFrequency(Room.roomUrls[index2]);
                    //100代表1/120hz,这里的2000，极值是1Hz(12000)。极限是2Hz(24000)
                    var value1 = frequency1 < 12000 ? frequency1 : Math.Max(1, 24000 - frequency1);
                    var value2 = frequency2 < 12000 ? frequency2 : Math.Max(1, 24000 - frequency2);
                    var sumV = value1 + value2;
                    var rIndex = rm.Next(sumV);
                    if (rIndex < value1)
                    {
                        roomIndex = index1;
                    }
                    else
                    {
                        roomIndex = index2;
                    }
                }
            }
            // var  
            var key = CommonClass.Random.GetMD5HashFromStr(ConnectInfo.HostIP + websocketID + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ConnectInfo.tcpServerPort + "_" + ConnectInfo.webSocketPort);
            //var mid = key.Substring(7, 24);
            //key = $"nyrq123{mid}2";//前7为是log，中间为GroupID，
            //key = "nyrq123" + key;
            //key = key.Substring(0, 31);
            // key=key
            var roomUrl = roomUrls[roomIndex];
            return new PlayerAdd_V2()
            {
                Key = key,
                GroupKey = key,
                c = "PlayerAdd_V2",
                FromUrl = $"{ConnectInfo.HostIP}:{ConnectInfo.tcpServerPort}",// ConnectInfo.ConnectedInfo + "/notify",
                RoomIndex = roomIndex,
                WebSocketID = websocketID,
                Check = CommonClass.Random.GetMD5HashFromStr(key + roomUrl + CheckParameter),
                PlayerName = playerName,
                RefererAddr = refererAddr,
                groupMemberCount = groupMemberCount
            };
            // throw new NotImplementedException();
        }
        private static int getFrequency(string roomUrl)
        {
            var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(new GetFrequency()
            {
                c = "GetFrequency",

            });
            var result = Startup.sendInmationToUrlAndGetRes(roomUrl, sendMsg);
            return int.Parse(result);
        }

        internal static string setOffLine(ref State s)
        {
            s = null;
#warning 这里要优化！！！
            return "";
        }

        internal static bool CheckSecretIsExit(string result, string key, out string refererAddr)
        {
            try
            {
                CommonClass.TeamNumWithSecret passObj = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.TeamNumWithSecret>(result);
                var roomNum = CommonClass.AES.AesDecrypt(passObj.Secret, key);
                var ss = roomNum.Split(':');
                //Consol.WriteLine($"sec:{ss}");
                if (ss[0] == "exitTeam")
                {
                    refererAddr = passObj.RefererAddr;
                    return true;
                }
                else
                {
                    refererAddr = "";
                    return false;
                }
            }
            catch
            {
                refererAddr = "";
                return false;
            }
        }

        internal static bool CheckSecret(string result, string key, out CommonClass.MateWsAndHouse.RoomInfo roomInfo, out string refererAddr)
        {
            try
            {
                // CommonClass.MateWsAndHouse.RoomInfo roomInfo;
                CommonClass.TeamNumWithSecret passObj = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.TeamNumWithSecret>(result);
                var roomNum = CommonClass.AES.AesDecrypt(passObj.Secret, key);
                var ss = roomNum.Split('-');
                //Consol.WriteLine($"sec:{ss}");
                if (ss[0] == "team")
                {
                    refererAddr = passObj.RefererAddr;
                    roomInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonClass.MateWsAndHouse.RoomInfo>(ss[1]);
                    //  roomIndex = int.Parse(ss[1]);
                    return true;
                }
                else
                {
                    roomInfo = null;
                    refererAddr = "";
                    return false;
                }
            }
            catch
            {
                roomInfo = null;
                refererAddr = "";
                return false;
            }
        }

        internal static void NotifyMsg(ConnectInfoDetail connectInfoDetail, string info)
        {
            var notifyMsg = info;
            var passObj = new
            {
                msg = notifyMsg,
                c = "ShowAgreementMsg"
            };
            var returnMsg = Newtonsoft.Json.JsonConvert.SerializeObject(passObj);
            CommonF.SendData(returnMsg, connectInfoDetail, 0);
        }

        internal static State setState(State s, ConnectInfoDetail connectInfoDetail, LoginState ls)
        {
            s.Ls = ls;
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new { c = "setState", state = Enum.GetName(typeof(LoginState), s.Ls) });
            CommonF.SendData(msg, connectInfoDetail, 0);
            return s;
            //   throw new NotImplementedException();
        }
        internal static State GetRoomThenStartAfterJoinTeam(State s, ConnectInfo.ConnectInfoDetail connectInfoDetail, CommonClass.MateWsAndHouse.RoomInfo roomInfoPass, string playerName, string refererAddr)
        {
            var roomInfo = Room.getRoomNumByRoom(s.WebsocketID, roomInfoPass, playerName, refererAddr);
            s.Key = roomInfo.Key;
            s.GroupKey = roomInfo.GroupKey;
            var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(roomInfo);
            var receivedMsg = Startup.sendInmationToUrlAndGetRes(Room.roomUrls[roomInfo.RoomIndex], sendMsg);
            //Consoe.WriteLine($"{receivedMsg},{s.Key},{s.WebsocketID}");
            if (receivedMsg == "ok")
            {
                WriteSession(roomInfo, connectInfoDetail);
                s.roomIndex = roomInfo.RoomIndex;
                s = setOnLine(s, connectInfoDetail);
            }

            else
            {
                NotifyMsg(connectInfoDetail, "进入房间失败！");
            }
            return s;
        }

        /// <summary>
        /// 起到一个承前启后的作用，好些功能需要在这个参数里加载。包括后台，包括前台！
        /// </summary>
        /// <param name="s"></param>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        public static State setOnLine(State s, ConnectInfo.ConnectInfoDetail connectInfoDetail)
        {
            State result;
            {

                coinIcon ci = new coinIcon();
                if (SetModelCopy(ci, connectInfoDetail)) { }
                else
                {
                    return s;
                }

                result = setState(s, connectInfoDetail, LoginState.OnLine);

                {
                    #region 校验响应
                    var checkIsOk = CheckRespon(connectInfoDetail, "SetOnLine");
                    if (checkIsOk)
                    {
                        // UpdateAfter3DCreate();
                    }
                    else
                    {
                        return s;
                    }
                    #endregion
                }
                initializeOperation(s);
            }
            result.JoinGameSingle_Success = true;
            return result;
        }
        /// <summary>
        /// 发送此命令，必在await setState(s, webSocket, LoginState.OnLine) 之后。两者是在前台是依托关系！
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static void initializeOperation(State s)
        {
            // var key = CommonClass.Random.GetMD5HashFromStr(ConnectInfo.ConnectedInfo + websocketID + DateTime.Now.ToString());
            // var roomUrl = roomUrls[s.roomIndex];
            var getPosition = new GetPosition()
            {
                c = "GetPosition",
                Key = s.Key,
                GroupKey = s.GroupKey
            };
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(getPosition);
            var result = Startup.sendInmationToUrlAndGetRes(Room.roomUrls[s.roomIndex], msg);

        }

        /// <summary>
        /// 校验网页的回应！
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private static bool CheckRespon(ConnectInfo.ConnectInfoDetail connectInfoDetail, string checkValue)
        {
            var timeOut = new CancellationTokenSource(1500000).Token;
            var resultAsync = Startup.ReceiveStringAsync(connectInfoDetail, timeOut);

            //  resultAsync 
            if (resultAsync.wr == null)
            {
                return false;
            }
            if (resultAsync.result == checkValue)
            {
                return true;
            }
            else
            {
                Console.WriteLine($"错误的回话--checkValue:{checkValue}!=resultAsync.result:{resultAsync.result}");
                var t2 = connectInfoDetail.ws.CloseAsync(WebSocketCloseStatus.PolicyViolation, "错误的回话", new CancellationToken());
                t2.GetAwaiter().GetResult();
                return false;
            }
        }

        private static PlayerAdd_V2 getRoomNumByRoom(int websocketID, CommonClass.MateWsAndHouse.RoomInfo roomInfo, string playerName, string refererAddr)
        {
            var key = CommonClass.Random.GetMD5HashFromStr(ConnectInfo.HostIP + websocketID + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ConnectInfo.tcpServerPort + "_" + ConnectInfo.webSocketPort);
            var roomUrl = roomUrls[roomInfo.RoomIndex];
            return new PlayerAdd_V2()
            {
                Key = key,
                c = "PlayerAdd_V2",
                FromUrl = $"{ConnectInfo.HostIP}:{ConnectInfo.tcpServerPort}",// ConnectInfo.ConnectedInfo + "/notify",
                RoomIndex = roomInfo.RoomIndex,
                WebSocketID = websocketID,
                Check = CommonClass.Random.GetMD5HashFromStr(key + roomUrl + CheckParameter),
                PlayerName = playerName,
                RefererAddr = refererAddr,
                GroupKey = roomInfo.GroupKey,
                groupMemberCount = roomInfo.MemberCount
            };
        }
        static List<string> debugItem = new List<string>();
        public static List<string> roomUrls
        {
            get
            {
                if (debugItem.Count == 0)
                {
                    var rootPath = System.IO.Directory.GetCurrentDirectory();
                    {
                        var text = File.ReadAllLines($"{rootPath}\\config\\rooms.txt");
                        for (int i = 0; i < text.Length; i++)
                        {
                            if (string.IsNullOrEmpty(text[i])) { }
                            else
                            {
                                debugItem.Add(text[i]);
                            }
                        }
                    }
                }
                return debugItem;
            }
        }
        static string CheckParameter = "_add_yrq";

        static void WriteSession(PlayerAdd_V2 roomInfo, ConnectInfo.ConnectInfoDetail connectInfoDetail)
        {
            // roomNumber
            /*
             * 在发送到前台以前，必须将PlayerAdd对象中的FromUrl属性擦除
             */
            roomInfo.FromUrl = "";
            //var session = Newtonsoft.Json.JsonConvert.SerializeObject(roomInfo);
            var session = $"{{\"Key\":\"{roomInfo.Key}\",\"GroupKey\":\"{roomInfo.GroupKey}\",\"FromUrl\":\"{roomInfo.FromUrl}\",\"RoomIndex\":{roomInfo.RoomIndex},\"Check\":\"{roomInfo.Check}\",\"WebSocketID\":{roomInfo.WebSocketID},\"PlayerName\":\"{roomInfo.PlayerName}\",\"RefererAddr\":\"{roomInfo.RefererAddr}\",\"groupMemberCount\":{roomInfo.groupMemberCount},\"c\":\"{roomInfo.c}\"}}";
            Regex reg = new Regex(BLL.CheckSessionBLL.RoomInfoRegexPattern);
            if (reg.IsMatch(session))
            {
                var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new { session = session, c = "setSession" });
                CommonF.SendData(msg, connectInfoDetail, 0);
            }
            else
            {
                throw new Exception("逻辑错误！");
            }
        }
    }
    public class Team
    {
        //  "http://127.0.0.1:11100" + "/notify"
        static string teamUrl = "127.0.0.1:11200";
        internal static TeamResult createTeam2(int websocketID, string playerName, string command_start)
        {
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new CommonClass.TeamCreate()
            {
                WebSocketID = websocketID,
                c = "TeamCreate",
                FromUrl = $"{ConnectInfo.HostIP}:{ConnectInfo.tcpServerPort}",//ConnectInfo.ConnectedInfo + "/notify",
                CommandStart = command_start,
                PlayerName = playerName
            });
            var result = Startup.sendInmationToUrlAndGetRes($"{teamUrl}", msg);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TeamResult>(result);

        }

        internal static string SetToBegain(TeamResult team, PlayerAdd_V2 roomInfo)
        {
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new CommonClass.TeamBegain()
            {
                c = "TeamBegain",
                TeamNum = team.TeamNumber,
                RoomIndex = roomInfo.RoomIndex,
                GroupKey = roomInfo.GroupKey
            });
            var result = Startup.sendInmationToUrlAndGetRes($"{teamUrl}", msg);
            return result;
        }

        internal static string findTeam2(int websocketID, string playerName, string command_start, string teamIndex, out string updateKey)
        {
            updateKey = CommonClass.Random.GetMD5HashFromStr(DateTime.Now.ToString());
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new CommonClass.TeamJoin()
            {
                WebSocketID = websocketID,
                c = "TeamJoin",
                FromUrl = $"{ConnectInfo.HostIP}:{ConnectInfo.tcpServerPort}",// ConnectInfo.ConnectedInfo + "/notify",
                CommandStart = command_start,
                PlayerName = playerName,
                TeamIndex = teamIndex,
                UpdateKey = updateKey
            });
            string resStr = Startup.sendInmationToUrlAndGetRes($"{teamUrl}", msg);

            return resStr;
            //return Newtonsoft.Json.JsonConvert.DeserializeObject<TeamFoundResult>(json);
        }

        internal static void Config()
        {
            var rootPath = System.IO.Directory.GetCurrentDirectory();
            //Consol.WriteLine($"path:{rootPath}");
            //Consoe.WriteLine($"IPPath:{rootPath}");
            if (File.Exists($"{rootPath}\\config\\teamIP.txt"))
            {
                var text = File.ReadAllText($"{rootPath}\\config\\teamIP.txt");
                teamUrl = text;
                Console.WriteLine($"读取了组队ip地址--{teamUrl},按任意键继续");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"没有组队服务IP，按任意键继续");
                Console.ReadLine();
                //Console.WriteLine($"请market输入IP即端口，如127.0.0.1:11200");
                //teamUrl = Console.ReadLine();
                //Console.WriteLine("请market输入端口");
                //this.port = int.Parse(Console.ReadLine());
                //var text = $"{this.IP}:{this.port}";
                //File.WriteAllText($"{rootPath}\\config\\MarketIP.txt", text);
            }
            //throw new NotImplementedException();
        }

        internal static bool leaveTeam(string teamID, int websocketID)
        {
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new CommonClass.LeaveTeam()
            {
                WebSocketID = websocketID,
                c = "LeaveTeam",
                FromUrl = $"{ConnectInfo.HostIP}:{ConnectInfo.tcpServerPort}",
                TeamIndex = teamID
            });
            string resStr = Startup.sendInmationToUrlAndGetRes($"{teamUrl}", msg);
            if (resStr == "success")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static string SetToExit(TeamResult team)
        {
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new CommonClass.TeamExit()
            {
                c = "TeamExit",
                TeamNum = team.TeamNumber,
            });
            var result = Startup.sendInmationToUrlAndGetRes($"{teamUrl}", msg);
            return result;
        }

        //SetToClear
        internal static string SetToClear(TeamResult team)
        {
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new CommonClass.CheckMembersIsAllOnLine()
            {
                c = "ClearOffLine",
                TeamNumber = team.TeamNumber
            });
            var result = Startup.sendInmationToUrlAndGetRes($"{teamUrl}", msg);
            return result;
        }


        internal static int TeamMemberCountResult(int TeamNum)
        {
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new CommonClass.TeamMemberCount()
            {
                c = "TeamMemberCount",
                TeamNum = TeamNum
            });
            var result = Startup.sendInmationToUrlAndGetRes($"{teamUrl}", msg);

            return int.Parse(result);
        }


        public static string TeamCaptainInfoRegexPattern = $"^\\{{\\\"TeamNumber\\\":{"[0-9]{1,9}"},\\\"UpdateKey\\\":\\\"{"[0-9a-f]{32}"}\\\",\\\"c\\\":\\\"{"TeamUpdate"}\\\",\\\"FromUrl\\\":\\\"\\\",\\\"WebSocketID\\\":{"0"},\\\"CommandStart\\\":\\\"\\\"\\}}$";//commandStart

        internal static void WriteSession(TeamResult team, ConnectInfo.ConnectInfoDetail connectInfoDetail)
        {
            //roomInfo.FromUrl = "";
            //var session = Newtonsoft.Json.JsonConvert.SerializeObject(roomInfo);
            var session = $"{{\"TeamNumber\":{team.TeamNumber},\"UpdateKey\":\"{team.UpdateKey}\",\"c\":\"TeamUpdate\",\"FromUrl\":\"\",\"WebSocketID\":{"0"},\"CommandStart\":\"\"}}";//CommandStart
            Regex reg = new Regex(TeamCaptainInfoRegexPattern);
            if (reg.IsMatch(session))
            {
                var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new { session = session, c = "setSession" });
                CommonF.SendData(msg, connectInfoDetail, 0);
            }
            else
            {
                throw new Exception("逻辑错误！");
            }
        }

        internal static string checkIsOK(CheckSession checkSession, State s, out string command_start, out string updateKey, out string teamID)
        {
            command_start = CommonClass.Random.GetMD5HashFromStr(s.WebsocketID.ToString() + s.WebsocketID);

            TeamUpdate teamUpdate = Newtonsoft.Json.JsonConvert.DeserializeObject<TeamUpdate>(checkSession.session);
            updateKey = teamUpdate.UpdateKey;
            teamID = teamUpdate.TeamNumber.ToString();
            teamUpdate.FromUrl = $"{ConnectInfo.HostIP}:{ConnectInfo.tcpServerPort}";
            teamUpdate.WebSocketID = s.WebsocketID;
            teamUpdate.CommandStart = command_start;
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(teamUpdate);
            var result = Startup.sendInmationToUrlAndGetRes($"{teamUrl}", msg);


            return result;
            // if (result == "captain") { }
        }

        internal static void UpdateTeammate(TeamResult team)
        {
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new CommonClass.UpdateTeammateOfCaptal()
            {
                c = "UpdateTeammateOfCaptal",
                TeamNumber = team.TeamNumber,
            });
            var result = Startup.sendInmationToUrlAndGetRes($"{teamUrl}", msg);
        }
        public static string TeamMemberInfoRegexPattern = $"^\\{{\\\"c\\\":\\\"{"TeamUpdate"}\\\",\\\"FromUrl\\\":\\\"\\\",\\\"TeamNumber\\\":{"[0-9]{1,9}"},\\\"UpdateKey\\\":\\\"{"[0-9a-f]{32}"}\\\",\\\"WebSocketID\\\":{"0"},\\\"CommandStart\\\":\\\"\\\"\\}}$";//commandStart

        /// <summary>
        /// 此方法，作为队员写入session
        /// </summary>
        /// <param name="teamID"></param>
        /// <param name="connectInfoDetail"></param>
        /// <exception cref="Exception"></exception>
        internal static void WriteSession(string teamID, string updateKey, ConnectInfoDetail connectInfoDetail)
        {
            var session = $"{{\"c\":\"TeamUpdate\",\"FromUrl\":\"\",\"TeamNumber\":{teamID},\"UpdateKey\":\"{updateKey}\",\"WebSocketID\":{"0"},\"CommandStart\":\"\"}}";//CommandStart
            Regex reg = new Regex(TeamMemberInfoRegexPattern);
            if (reg.IsMatch(session))
            {
                var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new { session = session, c = "setSession" });
                CommonF.SendData(msg, connectInfoDetail, 0);
            }
            else
            {
                throw new Exception("逻辑错误！");
            }
        }

        internal static bool IsAllOnLine(TeamResult team, ConnectInfoDetail connectInfoDetail)
        {
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new CommonClass.CheckMembersIsAllOnLine()
            {
                c = "CheckMembersIsAllOnLine",
                TeamNumber = team.TeamNumber,
            });
            var result = Startup.sendInmationToUrlAndGetRes($"{teamUrl}", msg);
            var listResult = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TeamJoin>>(result);
            if (listResult.Count > 0)
            {
                TeamStartFailed(connectInfoDetail);
                for (int i = 0; i < listResult.Count; i++)
                {
                    Room.NotifyMsg(connectInfoDetail, $"{i}-{listResult[i].PlayerName}现在处于离线！未能开始！");
                }
                return false;
            }
            else
            {
                return true;
            }
            // return false;
        }

        public static void TeamStartFailed(ConnectInfo.ConnectInfoDetail connectInfoDetail)
        {
            // var notifyMsg = info;
            var passObj = new
            {
                c = "TeamStartFailed"
            };
            var returnMsg = Newtonsoft.Json.JsonConvert.SerializeObject(passObj);
            CommonF.SendData(returnMsg, connectInfoDetail, 0);
        }
    }

    internal partial class Room
    {
        class coinIcon : interfaceTag.modelForCopy
        {
            public string Command { get { return "SetVehicle"; } }
        }

        private static bool SetModelCopy(interfaceTag.modelForCopy mp, ConnectInfo.ConnectInfoDetail connectInfoDetail)
        {

            {
                var msg = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    c = mp.Command, 
                });
                CommonF.SendData(msg, connectInfoDetail, 0);
                {
                    #region 校验响应
                    var checkIsOk = CheckRespon(connectInfoDetail, mp.Command);
                    if (checkIsOk)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    #endregion
                }
            }
        }
    }
}
