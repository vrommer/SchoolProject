﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronized.ViewModel.UsersViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public string Address { get; set; }
        public int Points { get; set; }
        public string Email { get; set; }
    }
}
