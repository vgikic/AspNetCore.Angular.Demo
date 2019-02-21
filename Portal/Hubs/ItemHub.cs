using Angular.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Portal.Identity;
using Portal.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Hubs
{
    // Authorization works the same way as Web API Authorization
    //[Authorize(Policy = RoleClaims.Admin)]
    public class ItemHub : Hub
    {
        private static List<ClientDto> ConnectedClients = new List<ClientDto>();

        // Clients call these methods
        public async Task GetUpodateForItem(int id)
        {

            // simulates DB call
            var item = ItemsApiController.items.FirstOrDefault(i => i.Id == id);
            // simulates processing time
            Thread.Sleep(3000);

            // Calls UserUpdate function on caller with updated item
            await Clients.Caller.SendAsync("ItemUpdate", item);
            //Call Finished function on the caller when there are no more updates
            await Clients.Caller.SendAsync("Finished");

        }

        // Called when new client connects
        public override async Task OnConnectedAsync()
        {
            // Unique client's Id
            var connectionId = Context.ConnectionId;
            var connectedClient = new ClientDto { Id = connectionId };
            ConnectedClients.Add(connectedClient);
            await Clients.Caller.SendAsync("GetAllClients", ConnectedClients);

            await Clients.AllExcept(connectionId).SendAsync("ClientConnected", connectedClient);

            // You can also add clients to groups
            await Groups.AddToGroupAsync(connectionId, "ItemsGroup");


            await base.OnConnectedAsync();
        }

        // Called when client disconnected
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            ConnectedClients = ConnectedClients.Where(c => c.Id != connectionId).ToList();
            await Groups.RemoveFromGroupAsync(connectionId, "ItemsGroup");
            await Clients.AllExcept(connectionId).SendAsync("ClientDisconnected", new ClientDto { Id = connectionId });
            await base.OnDisconnectedAsync(exception);
        }
    }
}
