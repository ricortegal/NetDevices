namespace Devices.Abstract.OnOff;

public class EstadoEventArgs : EventArgs
{
    private string _estado;


    public EstadoEventArgs(string estado)
    {
        _estado = estado;
    }

    public string Estado
    {
        get
        {
            return _estado;
        }
        private set
        {
            _estado = value;
        }
    }
}
