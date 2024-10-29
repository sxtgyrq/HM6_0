using CommonClass;
using HMMain6.interfaceOfEngine;
using HMMain6.RoomMainF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Aliyun.OSS.Model.CreateSelectObjectMetaInputFormatModel;
using static HMMain6.Car;

namespace HMMain6
{
    public class Manager_Driver : Manager, startNewCommandThread, manager
    {
        public Manager_Driver(RoomMain roomMain)
        {
            this.roomMain = roomMain;

        }

        internal void SelectDriver(SetSelectDriver dm)
        {
            //List<string> notifyMsg = new List<string>();
            //lock (that.PlayerLock)
            //{
            //    if (that._Players.ContainsKey(dm.Key))
            //    {
            //        var player = that._Players[dm.Key];
            //        if (player.Bust) { }
            //        else
            //        {

            //            var car = player.getCar();
            //            const int CostMoney = 5000;
            //            if (car.state == Car.CarState.waitAtBaseStation)
            //                if (car.ability.driver == null)
            //                {
            //                    this.setDriver(player, car, dm.Index, ref notifyMsg);
            //                }
            //                else if (car.ability.driver.Index == dm.Index)
            //                {
            //                    this.WebNotify(player, $"你现在的司机就是{car.ability.driver.Name}.");
            //                }
            //                else if (player.Money > CostMoney)
            //                {
            //                    player.MoneySet(player.Money - CostMoney, ref notifyMsg);
            //                    //var recruit = player.buildingReward[0];
            //                    if (that.rm.Next(100) < Manager_Driver.GetRecruit(player))
            //                    {
            //                        this.setDriver(player, car, dm.Index, ref notifyMsg);
            //                        this.WebNotify(player, "招聘成功！");
            //                    }
            //                    else
            //                    {
            //                        this.WebNotify(player, "招聘失败！到指定地点祈更多福可以提高成功率");
            //                    }
            //                }
            //                else
            //                {
            //                    this.WebNotify(player, "换司机最少要消耗50.00点积分！");
            //                }

            //        }
            //    }
            //}
            //this.sendSeveralMsgs(notifyMsg); 
        }





        //private void SetRecruit(int recruit, ref Player player)
        //{
        //    /*
        //     * 不会衰减，只有重新求福，才会衰减。
        //     */
        //    //player.buildingReward[0] -= 5;
        //    //player.buildingReward[0] = Math.Max(0, player.buildingReward[0]);
        //}





        public void newThreadDo(CommonClass.Command c, GetRandomPos grp)
        {
            // throw new NotImplementedException();
        }



        public class ImproveManager
        {
            long _speedValue = 0;
            public long SpeedValue { get { return this._speedValue; } }
            public bool HasValueToImproveSpeed { get { return this._speedValue >= 10; } }

            //   public DateTime LastTimeOfSpeedImproved { get; set; }

            public ImproveManager()
            {
                this._speedValue = 0;
                // LastTimeOfSpeedImproved = DateTime.Now.AddDays(10);
            }
            internal void addSpeed(Player role, int addValue, ref List<string> notifyMsg)
            {
                if (this.HasValueToImproveSpeed)
                {
                    this._speedValue -= 10;
                }
                this._speedValue += addValue;
                {
                    //   role.speedMagicChanged(role, ref notifyMsg);
                    // role.nitrogenValueChanged(role, ref notifyMsg);
                }
                if (this.HasValueToImproveSpeed)
                {
                    //  this.LastTimeOfSpeedImproved = DateTime.Now;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="role">操作的对象</param>
            /// <param name="changeValue">被减去的值</param>
            /// <param name="notifyMsg"></param>
            internal void reduceSpeed(Player role, ref List<string> notifyMsg)
            {
                if (this.HasValueToImproveSpeed)
                {
                    this._speedValue -= 10;
                    if (this._speedValue < 0)
                    {
                        this._speedValue = 0;
                    }
                    //    role.speedMagicChanged(role, ref notifyMsg);
                    //  role.nitrogenValueChanged(role, ref notifyMsg);
                }
            }



            bool _CollectIsDouble_p = false;
            public bool CollectIsDouble
            {
                get { return this._CollectIsDouble_p; }
                private set
                {
                    this._CollectIsDouble_p = value;
                }
            }

            /// <summary>
            /// 在选择时，有一定纪律增加。
            /// </summary>
            /// <param name="role"></param>
            /// <param name="notifyMsg"></param>
            internal void addAttack(Player role, ref List<string> notifyMsg)
            {
                CollectIsDouble = true;
                // if (CollectIsDouble)
                {
                    role.attackMagicChanged(role, ref notifyMsg);
                }
            }

            /// <summary>
            /// 到达收集地点，收集数量翻倍
            /// </summary>
            /// <param name="role"></param>
            /// <param name="notifyMsg"></param>
            internal void reduceAttack(Player role, ref List<string> notifyMsg)
            {
                CollectIsDouble = false;
                // if (CollectIsDouble)
                {
                    //  role.attackMagicChanged(role, ref notifyMsg);
                }
            }
        }

        internal bool controlledByMagic(Player victim, Car car, GetRandomPos grp, ref List<string> notifyMsg)
        {
            //if (victim.confuseRecord.IsBeingControlled())
            //{
            //    return true;
            //}
            //else
            //{
            //    victim.confuseRecord.ControllSelf(victim, that, grp, ref notifyMsg, this);
            //    return victim.confuseRecord.IsBeingControlled();
            //}
            return false;
        }
        internal enum AmbushMagicType
        {
            waterMagic,
            electicMagic,
            fireMagic,
            attack
        }
    }
}
