using System;
using System.Collections.Generic;
using System.Text;

namespace HMMain6.interfaceOfHM
{
    interface CarAndRoomInterface
    {
        void SetAnimateChanged(HMMain6.Player player, HMMain6.Car car, ref List<string> notifyMsg);
        void AbilityChanged2_0(HMMain6.Player player, HMMain6.Car car, ref List<string> notifyMsgs, string pType);
        void DiamondInCarChanged(HMMain6.Player player, HMMain6.Car car, ref List<string> notifyMsgs, string value);
        void DriverSelected(HMMain6.Player player, HMMain6.Car car, ref List<string> notifyMsgs);
    }
}
