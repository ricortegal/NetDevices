using Devices.Abstract.OnOff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Factory.Devices
{
    internal class OnOffTcpIp : IOnOffDevice
    {
        public const string ON = "on";
        public const string OFF = "off";

        private string _estado = OFF;
        private TcpClient _client;
        private NetworkStream? _stream;


        internal OnOffTcpIp()
        {
            _client = new TcpClient();
            Thread thread = new Thread(new ThreadStart(EscuchaTcp));
            while(!_client.Connected)
            try
            {
                _client.Connect(new System.Net.IPAddress(new byte[] { 127, 0, 0, 1 }), 53998);
                _stream = _client.GetStream();
                thread.Start();
                Task.Delay(5000).Wait();
            }
            catch(Exception ex)
            {

            }
        }


        public string State
        {
            get
            {
                return _estado;
            }
            private set
            {
                if (value.ToLower() == ON || value.ToLower() == OFF)
                    _estado = value;
                OnChangeState();
            }
        }


        public event EventHandler<EstadoEventArgs>? StateChangeEvent;


        public async Task SetOff()
        {
            if (_client.Connected)
            {
                await EnviaTrama(OFF);
                State = OFF;
            }
        }


        public async Task SetOn()
        {
            if (_client.Connected)
            {
                await EnviaTrama(ON);
                State = ON;
            }
        }


        private void EscuchaTcp()
        {
            byte[] data = new byte[256];
            while (_stream != null)
            {
                _stream.Read(data);
                if (Encoding.ASCII.GetString(data).Contains(ON) && State == OFF)
                    State = ON;
                if (Encoding.ASCII.GetString(data).Contains(OFF) && State == ON)
                    State = OFF;
            }
        }


        private void OnChangeState()
        {
            StateChangeEvent?.Invoke(this, new EstadoEventArgs(_estado));
        }


        private async Task EnviaTrama(string trama)
        {
            if (_client.Connected && _stream != null)
            {
                await _stream.WriteAsync(Encoding.ASCII.GetBytes(trama));
            }
        }
    }
}
