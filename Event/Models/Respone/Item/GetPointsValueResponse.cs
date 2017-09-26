using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Models.Respone.Item
{
    public class GetPointsValueResponse 
    {
        public IEnumerable<string> Errors { get; set; }
        public double Points { get; set; }
    }
}
