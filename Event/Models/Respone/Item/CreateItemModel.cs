﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Models.Respone.Item
{
    public class CreateItemModel
    {
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public int PointValue { get; set; }
    }
}
