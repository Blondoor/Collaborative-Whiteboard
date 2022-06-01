using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollaborativeWhiteboard.Models
{
    public class UserWhiteBoard
    {
        public Guid UserId { get; set; }
        public Guid WhiteboardId { get; set; }
    }
}
