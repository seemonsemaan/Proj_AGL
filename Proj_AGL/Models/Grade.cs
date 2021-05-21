using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj_AGL.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public int Score { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual Course Course { get; set; }
    }
}
