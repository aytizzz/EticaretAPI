﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Domain.Entities.Common
{
   public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate{ get; set; }
        virtual  public DateTime UpdateDate { get; set; }
    }
}
