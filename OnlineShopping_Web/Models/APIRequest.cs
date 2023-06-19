﻿using OnlineShopping_Utility;
using static OnlineShopping_Utility.SD;

namespace OnlineShopping_Web.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
