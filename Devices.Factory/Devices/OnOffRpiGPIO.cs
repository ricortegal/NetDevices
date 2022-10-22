using Devices.Abstract.OnOff;
using System.Device.Gpio;

namespace Devices.Factory.Devices;


public class OnOffRpiGPIO : IOnOffDevice
{

    GpioController _controller;

    private const int PIN_SALIDA = 5;
    private const int PIN_ENTRADA = 21;

    private const string ON = "on";
    private const string OFF = "off";

    private string _state = OFF;

    public event EventHandler<EstadoEventArgs>? StateChangeEvent;

    public OnOffRpiGPIO()
    {
        _controller = new GpioController();
        _controller.OpenPin(PIN_SALIDA, PinMode.Output);
        _controller.OpenPin(PIN_ENTRADA, PinMode.InputPullDown);
        _controller.RegisterCallbackForPinValueChangedEvent(PIN_ENTRADA, PinEventTypes.Rising, PulsadoFisico);
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

    public PinChangeEventHandler PulsadoFisico { get; private set; } = (s, pin) =>
        {
            if(pin.PinNumber == PIN_ENTRADA)
            {

            }
        };

    private void OnStateChange()
    {
        StateChangeEvent?.Invoke(this, new EstadoEventArgs(_state));
    }

    public async Task SetOff()
    {
        _controller.Write(PIN_SALIDA, PinValue.Low);
        State = ON;
    }

    public async Task SetOn()
    {
        _controller.Write(PIN_SALIDA, PinValue.High);
        State = OFF;
    }

}
