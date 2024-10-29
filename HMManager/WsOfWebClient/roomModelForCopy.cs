using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WsOfWebClient
{
    internal partial class Room
    {
        class SetVehicle : interfaceTag.modelForCopy
        {
            public string Command { get { return "SetVehicle"; } }
        }
        class SetCubeCore : interfaceTag.modelForCopy
        {
            public string Command { get { return "SetCubeCore"; } }
        }

        class SetGoldBaby : interfaceTag.modelForCopy
        {
            public string Command { get { return "SetGoldBaby"; } }
        }

        class SetCompass : interfaceTag.modelForCopy
        {
            public string Command { get { return "SetCompass"; } }
        }

        class SetTurbine : interfaceTag.modelForCopy
        {
            public string Command { get { return "SetTurbine"; } }
        }

        class SetSatelite : interfaceTag.modelForCopy
        {
            public string Command { get { return "SetSatelite"; } }
        }

        class SetBattery : interfaceTag.modelForCopy
        {
            public string Command { get { return "SetBattery"; } }
        }
        class SetDoubleRewardIcon : interfaceTag.modelForCopy
        {
            public string Command { get { return "SetDoubleRewardIcon"; } }
        }

        class SetSpeedIcon : interfaceTag.modelForCopy
        {
            public string Command { get { return "SetSpeedIcon"; } }
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
