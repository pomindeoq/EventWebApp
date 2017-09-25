using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Models.Respone.Item
{
    public class CreateItemViewModel
    {   
        public string UserId { get; set; }

        public  IEnumerable<SelectListItem> ItemCategories { get; set; }

        public int PointValue { get; set; }

        public string SelectedValue { get; set; }
    }
}
