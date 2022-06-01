using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollaborativeWhiteboard.Models
{
    public class Whiteboard
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Canvas { get; set; }
    }
}
