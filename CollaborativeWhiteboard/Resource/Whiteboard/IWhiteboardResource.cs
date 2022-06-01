using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollaborativeWhiteboard.Resource.Whiteboard
{
    public interface IWhiteboardResource
    {
        ViewModel.ViewCanvasForList[] GetListOfWhiteBoards(Guid UserId);
        Models.Whiteboard CreateWhiteboard(ViewModel.CreateWhiteboard createWhiteboard);
        Models.Whiteboard UpdateWhiteboard(Models.Whiteboard whiteboard);
        Models.Whiteboard GetWhiteboard(Guid Id);
    }

}
