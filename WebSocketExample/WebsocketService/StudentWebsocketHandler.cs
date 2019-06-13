using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using WebSocketExample.Context;
using WebSocketExample.Model;

namespace WebSocketExample.WebsocketService
{
    public class StudentWebsocketHandler : WebSocketHandler
    {
        
        private readonly IServiceScopeFactory _service;
        public StudentWebsocketHandler(WebSocketConnectionManager webSocketConnectionManager, IServiceScopeFactory service) : base(webSocketConnectionManager)
        {

            _service = service;

        }
        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            Student data = JsonConvert.DeserializeObject<Student>(Encoding.UTF8.GetString(buffer, 0, result.Count));
            

            var responseObject = await GetResponseObjectAsync(data);

            await SendMessageToAllAsync(
                responseObject
            );
        }
        private async Task<JObject> GetResponseObjectAsync(Student payload)
        {
            using (var scope = _service.CreateScope())
            {

                var _Context = scope.ServiceProvider.GetService<SocketExampleContext>();
               await _Context.Student.AddAsync(payload);
                var list = await _Context.Student.ToListAsync();


                return JObject.FromObject(list);
            }
           

        }

    }

}


