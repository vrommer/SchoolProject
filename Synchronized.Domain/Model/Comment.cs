﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Synchronized.Repository.Model
{
    public class Comment : Post
    {
        public string PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}