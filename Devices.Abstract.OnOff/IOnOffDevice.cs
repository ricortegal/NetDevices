using Devices.Abstract.OnOff;

namespace Devices.Abstract.OnOff;

public interface IOnOffDevice 
{
    #region eventos
    public event EventHandler<EstadoEventArgs>? StateChangeEvent;
    #endregion

    #region propiedades
    public string State { get; }
    #endregion

    #region métodos
    public Task SetOn();
    public Task SetOff();
    #endregion
}
