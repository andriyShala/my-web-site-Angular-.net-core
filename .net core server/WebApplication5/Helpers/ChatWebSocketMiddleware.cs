using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication5.Services;

namespace WebApplication5.Helpers
{
    public class Rom
    {
        public string LastMessage { get; set; }
        public string LastTime { get; set; }
        public string Romid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string type { get; set; }


    }
    public class MessagesType
    {
        private MessagesType(string value) { Value = value; }
        public string Value { get; set; }
        public static MessagesType RomCreate { get { return new MessagesType("RomCreate"); } }
        public static MessagesType Message { get { return new MessagesType("Message"); } }
    }
   
    public class Message
    {
        public string type { get; set; }
        public string author { get; set; }
        public string recipient { get; set; }
        public string message { get; set; }
        public override string ToString()
        {
            return String.Format("author:{0}; recipient:{1}; message:{2}; type:{3}", author, recipient, message,type);
        }
    }
    public class socket
    {
        public string username { get; set; }
        public WebSocket Websocket { get; set; }
        public CancellationToken cancellationToken { get; set; }
    }
    public class ChatWebSocketMiddleware
    {
        public static socket GetSocketByUsername(string usernmae)
        {
            foreach (var item in _sockets)
            {
                if(item.Value.username==usernmae)
                {
                    return item.Value;
                }
            }
            return null;
        }
        public static ConcurrentDictionary<string, socket> _sockets = new ConcurrentDictionary<string, socket>();
        
        private readonly RequestDelegate _next;
        private MessageService messageService;
            public ChatWebSocketMiddleware(RequestDelegate next)
        {
            _next = next;
            messageService = new MessageService(new DataContext());
            
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }

            CancellationToken ct = context.RequestAborted;
            WebSocket currentSocket = await context.WebSockets.AcceptWebSocketAsync();
            Debug.WriteLine("userConnected");
            var socketId = Guid.NewGuid().ToString();
            string username = "";
            _sockets.TryAdd(socketId,new socket() { username = "", Websocket = currentSocket,cancellationToken=ct });
            string response = "";
            Message mess = null;
            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    break;
                }
                try
                {
                    response = await ReceiveStringAsync(currentSocket, ct);
                }
                catch
                {
                    response = null;
                }
              
               
                try
                {
                    mess = JsonConvert.DeserializeObject<Message>(response);
                    Debug.WriteLine(mess.ToString());
                    if (mess.message == "firstCon" && mess.recipient == "")
                    {
                        _sockets[socketId].username = mess.author;
                    }
                    else
                    {
                        messageService.Create(mess.author, mess.recipient, mess.message);
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                
               
                if (string.IsNullOrEmpty(response))
                {
                    if (currentSocket.State != WebSocketState.Open)
                    {
                        break;
                    }

                    continue;
                }

                foreach (var socket in _sockets)
                {
                    if (socket.Value.Websocket.State != WebSocketState.Open)
                    {
                        continue;
                    }
                    if(socket.Value.username==mess.recipient)
                    {
                        await SendStringAsync(socket.Value.Websocket, response, ct);
                        break;
                    }
                }
            }

            socket socketw = new socket() { username = username, Websocket = null,cancellationToken=new CancellationToken() };

            _sockets.TryRemove(socketId, out socketw);
            Debug.WriteLine("user DIS Connected");

            try
            {
                await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);

            }
            catch
            {

            }
            currentSocket.Dispose();
        }

        public static Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
        {
            
            var buffer = Encoding.UTF8.GetBytes(data);
           
            var segment = new ArraySegment<byte>(buffer);
            return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        }

        private static async Task<string> ReceiveStringAsync(WebSocket socket, CancellationToken ct = default(CancellationToken))
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);
            using (var ms = new MemoryStream())
            {
                WebSocketReceiveResult result;
                do
                {
                    ct.ThrowIfCancellationRequested();

                    result = await socket.ReceiveAsync(buffer, ct);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                }
                while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);
                if (result.MessageType != WebSocketMessageType.Text)
                {
                    return null;
                }

                // Encoding UTF8: https://tools.ietf.org/html/rfc6455#section-5.6
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
