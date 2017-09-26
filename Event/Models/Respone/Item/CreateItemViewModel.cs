using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Models.Respone.Item
{
    public class CreateItemViewModel
    {   
        public string UserId { get; set; }
        [Required]
        public string Username { get; set; }


        public string Error { get; set; }
        public  IEnumerable<SelectListItem> ItemCategories { get; set; }

        public double PointValue { get; set; }

        public string SelectedValue { get; set; }
    }
}
