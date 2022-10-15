using Devices.Abstract.OnOff;
using System.Net.WebSockets;
using System.Text;

namespace IoTOnOff.WebApi.Servicios
{
    public class WebSocketOnOffService
    {

        List<WebSocket> _webSockets;
        IOnOffDevice _device;
  

        public WebSocketOnOffService(IOnOffDevice device)
        {
            _device = device;
            _device.StateChangeEvent += Device_StateChangeEvent;
            _webSockets = new List<WebSocket>();
        }


        private void Device_StateChangeEvent(object? sender, EstadoEventArgs e)
        {
            foreach (WebSocket ws in _webSockets)
            {
                if(ws.State == WebSocketState.Open && e.Estado == "on")
                    ws.SendAsync( new ArraySegment<byte>(Encoding.ASCII.GetBytes("on"), 0, 2), WebSocketMessageType.Text, true, CancellationToken.None);
                if(ws.State == WebSocketState.Open && e.Estado == "off")
                    ws.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes("off"), 0, 3), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }


        public void AddWebSocket(WebSocket websocket)
        {
            _webSockets.Add(websocket);
        }





    }
}
