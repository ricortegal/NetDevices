using Devices.Abstract.OnOff;
using System.Device.Gpio;

namespace Devices.Factory.Devices;


public class OnOffRpiGPIO : IOnOffDevice
{

    GpioController _controller;

    private const int PIN = 5;
    private const string ON = "on";
    private const string OFF = "off";

    private string _state = OFF;

    public event EventHandler<EstadoEventArgs>? StateChangeEvent;

    public OnOffRpiGPIO()
    {
        _controller = new GpioController();
        _controller.OpenPin(PIN, PinMode.Output);
    }

    public string State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            OnStateChange();
        }
    }

    private void OnStateChange()
    {
        StateChangeEvent?.Invoke(this, new EstadoEventArgs(_state));
    }

    public async Task SetOff()
    {
        _controller.Write(PIN, PinValue.Low);
        State = ON;
    }

    public async Task SetOn()
    {
        _controller.Write(PIN, PinValue.High);
        State = OFF;
    }

}
