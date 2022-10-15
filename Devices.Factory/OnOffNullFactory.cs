using Devices.Abstract.OnOff;
using Devices.Factory.Devices;


namespace Devices.Factory
{
    public class OnOffNullFactory : IOnOffDeviceFactory
    {
        public IOnOffDevice Create()
        {
            return new OnOffNull();
        }
    }
}
