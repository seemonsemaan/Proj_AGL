using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj_AGL.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public int Count { get; set; }
        public string UserId { get; set; }
        public int StatusId { get; set; }
        public int LocationId { get; set; }
        public virtual StatusType Status { get; set; }
        public virtual Location Location { get; set; }
    }
}
