﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Areas.Administration.Models
{
    public class ErrorPageModel
    {
        public decimal StatusCode { get; set; }
        public string Message { get; set; }
    }
}
