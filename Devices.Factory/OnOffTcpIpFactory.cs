using Devices.Abstract.OnOff;
using Devices.Factory.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Factory
{
    public class OnOffTcpIpFactory : IOnOffDeviceFactory
    {
        public IOnOffDevice Create()
        {
            return new OnOffTcpIp();
        }
    }
}
