using Devices.Abstract.OnOff;


namespace Devices.Factory.Devices
{
    /// <summary>
    /// Implementa un dispositivo virtual o nulo no está conectado a nigún dispositivo real
    /// </summary>
    public class OnOffNull : IOnOffDevice
    {

        private string _estado = "off";

        internal OnOffNull()
        {
           
        }


        public string State
        {
            get
            {
                return _estado;
            }
            private set
            {
                if (value.ToLower() == "on" || value.ToLower() == "off")
                    _estado = value;
                OnChangeState();
            }
        }


        public event EventHandler<EstadoEventArgs>? StateChangeEvent;

        public async Task SetOff()
        {
            State = "off";
        }


        public async Task SetOn()
        {
            State = "on";
        }


        private void OnChangeState()
        {
            StateChangeEvent?.Invoke(this, new EstadoEventArgs(_estado));
        }


    }
}
