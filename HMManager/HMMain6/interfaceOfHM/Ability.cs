﻿using CommonClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMMain6.interfaceOfHM
{
    interface Ability
    {
        /// <summary>
        /// 这里要通知前台，值发生了变化。
        /// </summary>
        /// <param name="player"></param>
        /// <param name="car"></param>
        /// <param name="notifyMsgs"></param>
        /// <param name="pType"></param>
        void AbilityChanged2_0(interfaceTag.HasContactInfo player, HMMain6.Car car, ref List<string> notifyMsgs, string pType);

        string SetAbility(SetAbility sa);


    }
}
