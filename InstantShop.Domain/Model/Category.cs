﻿using InstantShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantShop.Domain.Model
{
    public class Category : BaseModel
    {
        [Required]
        public string Name { get; set; }

    }
}
