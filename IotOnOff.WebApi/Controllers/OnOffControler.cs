using Devices.Abstract.OnOff;
using IoTOnOff.WebApi.Servicios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.WebSockets;
using System.Text;

namespace IotOnOff.WebApi.Controllers;

[ApiController]
[Route("onoff")]
public class OnOffControler : ControllerBase
{

    private const string ON = "on";
    private const string OFF = "off";

    private readonly ILogger<OnOffControler> _logger;
    private readonly IOnOffDevice _device;
    private readonly WebSocketOnOffService _webSocketOnOffService;

    public OnOffControler(ILogger<OnOffControler> logger, IOnOffDevice device, WebSocketOnOffService webSocketOnOffService)
    {
        _logger = logger;
        _device = device;
        _webSocketOnOffService = webSocketOnOffService;
    }


  
    [Route("ws")]
    [HttpGet, ActionName("ws")]
    public async Task GetWS()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            var buffer = new byte[1024];
            _webSocketOnOffService.AddWebSocket(webSocket);
            bool continuar = true;
            while(webSocket.State.HasFlag(WebSocketState.Open) && continuar)
            {
                try
                {
                    await webSocket.ReceiveAsync(buffer,CancellationToken.None);
                    var estado = Encoding.ASCII.GetString(buffer);
                    if (estado.Contains(ON))
                        await _device.SetOn();
                    if (estado.Contains(OFF))
                        await _device.SetOff();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    continuar = false;
                }
            }
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }



    [HttpGet, ActionName("set")]
    public async Task<IActionResult> Get([FromQuery] string estado)
    {
        if(estado == null)
            return NotFound();

        estado = estado.ToLower();

        if (estado == ON)
        {
            await _device.SetOn();
            _logger.LogInformation("El dispositivo está encendido. petición de GET");
            return Ok(ON);
        }
        else if (estado == OFF)
        {
            await _device.SetOff();
            _logger.LogInformation("El dispositivo está apagado. petición de GET");
            return Ok(OFF);
        }
        else
            return new BadRequestObjectResult("estado solamente puede tomar valor on o off");

    }




}
