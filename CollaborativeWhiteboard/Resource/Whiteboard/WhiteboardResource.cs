using CollaborativeWhiteboard.Models;
using CollaborativeWhiteboard.ViewModel;
using Runtime.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollaborativeWhiteboard.Resource.Whiteboard
{
    public class WhiteboardResource : IWhiteboardResource
    {
        private readonly AppDBContext appDBContext;

        public WhiteboardResource(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public Models.Whiteboard CreateWhiteboard(ViewModel.CreateWhiteboard createWhiteboard)
        {
            var whiteboard = new Models.Whiteboard
            {
                Id = Guid.NewGuid(),
                Name = createWhiteboard.Name,
                Canvas = ""
            };

            var userWhiteboard = new Models.UserWhiteBoard
            {
                UserId = createWhiteboard.UserID,
                WhiteboardId = whiteboard.Id,
            };

            appDBContext.UserWhiteBoard.Add(userWhiteboard);
            appDBContext.Whiteboard.Add(whiteboard);
            appDBContext.SaveChanges();

            return whiteboard;
        }

        public ViewCanvasForList[] GetListOfWhiteBoards(Guid UserId)
        {
            var resultQuery = from uw in appDBContext.UserWhiteBoard
                              join w in appDBContext.Whiteboard on uw.WhiteboardId equals w.Id
                              where uw.UserId == UserId
                              select new ViewCanvasForList
                              {
                                  Id = w.Id,
                                  Name = w.Name
                              };
            return resultQuery.ToArray();
        }

        public Models.Whiteboard UpdateWhiteboard(Models.Whiteboard whiteboard)
        {
            var whiteBoard = appDBContext.Whiteboard.Where(w => w.Id == whiteboard.Id).DeepCopyTo<Models.Whiteboard>();
            return whiteBoard;
        }

        public Models.Whiteboard GetWhiteboard(Guid Id)
        {
            var whiteBoard = appDBContext.Whiteboard.Where(w => w.Id == Id).DeepCopyTo<Models.Whiteboard>();
            return whiteBoard;
        }
    }
}
