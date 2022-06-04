using CollaborativeWhiteboard.HubConfig;
using CollaborativeWhiteboard.Resource.Whiteboard;
using CollaborativeWhiteboard.TimerFeatures;
using CollaborativeWhiteboard.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CollaborativeWhiteboard.Controllers
{
    [Route("Canvas")]
    [ApiController]
    public class WhiteboardController: ControllerBase
    {
        private readonly IWhiteboardResource whiteboardResource;
        private readonly IHubContext<CanvasHub> _hub;

        public WhiteboardController(IWhiteboardResource whiteboardResource, IHubContext<CanvasHub> hub)
        {
            this.whiteboardResource = whiteboardResource;
            _hub = hub;
        }

        [HttpGet]
        [Route("get/{CanvasId: Guid}")]
        public ActionResult<Models.Whiteboard> Get(Guid CanvasId)
        {
            var result = whiteboardResource.GetWhiteboard(CanvasId);
            return Ok(result);
        }

        [HttpGet]
        [Route("{UserId: Guid}")]
        public ActionResult<ViewCanvasForList[]> getListOfWhiteboards(Guid UserId)
        {
            var whiteboards = whiteboardResource.GetListOfWhiteBoards(UserId);
            return whiteboards;
        }

        [HttpPost]
        [Route("createWhiteboard")]
        public ActionResult<Models.Whiteboard> createWhiteboard([FromBody] CreateWhiteboard whiteboard)
        {
            var result = whiteboardResource.CreateWhiteboard(whiteboard);
            return Ok(result);
        }
    }
}
