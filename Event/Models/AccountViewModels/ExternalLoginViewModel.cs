using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        
        public string Username { get; set; }
    }
}
