using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event.Models;

namespace Event.Models.Respone.Item
{
    public class ItemsResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public IEnumerable<Models.Item> Items { get; set; }
    }
}
