﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event.Models.Respone.Item
{
    public class ItemExchangeViewModel
    {
        public int ItemId { get; set; }
        public string NewOwnerAccountUserName { get; set; }
        public double PointValue { get; set; }
    }
}
