using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Models
{
    public class ItemCategory
    {
        [JsonProperty("ItemCategoryId")]
        public int Id { get; set; }
        [JsonProperty("CategoryName")]
        public string Name { get; set; }
    }
}
