using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.RoomMainF
{
    public partial class RoomMain
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msgsWithUrl"></param>
        private void AddOtherPlayer(string key, string groupKey, ref List<string> msgsWithUrl)
        {
            // throw new Exception("");
            if (this._Groups.ContainsKey(groupKey))
            {
                var group = this._Groups[groupKey];
                group.AddOtherPlayer(key, ref msgsWithUrl);
            }

        }

        public void addPlayerRecord(Player self, Player other, ref List<string> msgsWithUrl)
        {
            if (self.Key == other.Key)
            {
                return;
            }
            else if (self.othersContainsKey(other.Key))
            {
            }
            else
            {
                var otherPlayer = new OtherPlayers(self.Key, other.Key);
                //   otherPlayer.brokenParameterT1RecordChangedF = self.brokenParameterT1RecordChanged;
                self.othersAdd(other.Key, otherPlayer);
                //otherPlayer.setBrokenParameterT1Record(other.brokenParameterT1, ref msgsWithUrl);

                var fp = Program.dt.GetFpByIndex(other.StartFPIndex);
                // fromUrl = this._Players[getPosition.Key].FromUrl;
                if (self.playerType == Player.PlayerType.player)
                {
                    var webSocketID = ((Player)self).WebSocketID;
                    //var carsNames = other.ca;

                    //  var fp=  players[i].StartFPIndex
                    CommonClass.GetOthersPositionNotify_v2 notify = new CommonClass.GetOthersPositionNotify_v2()
                    {
                        c = "GetOthersPositionNotify_v2",
                        fp = fp,
                        WebSocketID = webSocketID,
                        key = other.Key,
                        PlayerName = other.PlayerName,
                        fPIndex = other.StartFPIndex,
                        positionInStation = other.positionInStation,
                        isNPC = other.playerType == Player.PlayerType.NPC,
                        isPlayer = other.playerType == Player.PlayerType.player,
                        Level = other.Level,
                        AsynSend = false
                        // var xx=  getPosition.Key
                    };
                    msgsWithUrl.Add(((Player)self).FromUrl);
                    msgsWithUrl.Add(Newtonsoft.Json.JsonConvert.SerializeObject(notify));
                }
            }


        }
    }
}
