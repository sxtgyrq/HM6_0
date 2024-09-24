using System;
using System.Collections.Generic;
using System.Text;

namespace HMMain6.interfaceOfHM
{
    interface Money
    {
        void SetMoneyCanSave(HMMain6.Player player, ref List<string> notifyMsg);
        void MoneyChanged(HMMain6.Player player, long money, ref List<string> notifyMsg);
        //void LookFor(GetRandomPos gp);
        void SetLookForMoney(GetRandomPos gp);
    }
}
