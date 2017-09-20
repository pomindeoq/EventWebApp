using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Event.Models
{
    public class Item
    {
        [JsonProperty("itemid")]       
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string OwnerUserName { get; set; }
        public double PointValue { get; set; }
    }
}
