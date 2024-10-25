using CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.RoomMainF
{
    public partial class RoomMain
    {
        internal void ConfigMagic(Player role)
        {
            //role.confuseRecord = new Manager_Driver.ConfuseManger();
            //role.improvementRecord = new Manager_Driver.ImproveManager();
            //role.speedMagicChanged = this.speedMagicChanged;
            //role.nitrogenValueChanged = this.nitrogenValueChanged;
            //role.attackMagicChanged = this.attackMagicChanged;
            role.collectMagicChanged = this.collectMoneyCountMagicChanged;
            //role.defenceMagicChanged = this.defenceMagicChanged;
            //role.confusePrepareMagicChanged = this.confusePrepareMagicChanged;
            //role.lostPrepareMagicChanged = this.lostPrepareMagicChanged;
            //role.ambushPrepareMagicChanged = this.ambushPrepareMagicChanged;
            //role.controlPrepareMagicChanged = this.controlPrepareMagicChanged;

            //role.confuseMagicChanged = this.confuseMagicChanged;
            //role.loseMagicChanged = this.loseMagicChanged;

            //role.fireMagicChanged = this.fireMagicChanged;
            //role.waterMagicChanged = this.waterMagicChanged;
            //role.electricMagicChanged = this.electricMagicChanged;
        }

        internal void collectMoneyCountMagicChanged(Player role, ref List<string> notifyMsgs)
        {
            //  throw new Exception("");
            var group = role.Group;
            foreach (var item in group._PlayerInGroup)
            {
                if (item.Value.playerType == Player.PlayerType.player)
                {
                    var player = (Player)item.Value;
                    if (player.Key == role.Key)
                    {
                        /*
                         * 如果是自己，有自己的更新方法！
                         */
                    }
                    else
                    {
                        var url = player.FromUrl;
                        CollectCountNotify an = new CollectCountNotify()
                        {
                            c = "CollectCountNotify",
                            WebSocketID = player.WebSocketID,
                            Key = role.Key,
                            Count = role.getCar().ability.costVolume / 100
                        };
                        var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(an);
                        notifyMsgs.Add(url);
                        notifyMsgs.Add(sendMsg);
                    }
                }
            }
        }
    }
}
