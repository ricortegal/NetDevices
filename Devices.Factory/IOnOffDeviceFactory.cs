using Devices.Abstract.OnOff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Factory
{
    public interface IOnOffDeviceFactory
    {
        IOnOffDevice Create();
    }
}
