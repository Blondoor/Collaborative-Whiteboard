using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollaborativeWhiteboard.HubConfig
{
    public class CanvasHub: Hub
    {
        public async Task UpdateCanvas(Models.Whiteboard whiteboard)
        {
            var newWhiteboard = whiteboard;
            await Clients.All.SendAsync("broadcastCanvasData", newWhiteboard);
        }
    }
}
