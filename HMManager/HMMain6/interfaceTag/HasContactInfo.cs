using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.interfaceTag
{
    public interface HasContactInfo
    {
        void GetUrlAndWebsocket(out string url, out int websocketID);
    }
}
