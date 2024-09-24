using HMMain6.interfaceOfEngine;
using System.Threading;
using static HMMain6.RoomMainF.RoomMain;

namespace HMMain6
{
    public abstract class Manager : EngineAndManger
    {
        public void startNewCommandThread(int startT, CommonClass.Command command, interfaceOfEngine.startNewCommandThread objNeedToStartNewCommandThread, GetRandomPos grp)
        {
            Thread th = new Thread(() => newThreadDoBefore(startT, command, objNeedToStartNewCommandThread, grp));
            th.Start();
        }
        void newThreadDoBefore(int startT, CommonClass.Command command, interfaceOfEngine.startNewCommandThread objNeedToStartNewThread, GetRandomPos grp)
        {
            Thread.Sleep(startT);
            objNeedToStartNewThread.newThreadDo(command, grp);
        }
    }
}
