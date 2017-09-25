using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Models.Categories
{      
    public class ItemCategoriesResponse 
    {
        public IEnumerable<string> Errors { get; set; }
        public IEnumerable<ItemCategory> ItemCategories { get; set; }
    }   
}
