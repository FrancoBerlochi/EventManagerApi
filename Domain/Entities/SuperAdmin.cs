﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SuperAdmin : User
    {
        public SuperAdmin() { }
        public SuperAdmin(string name, string email, string password, string phone, string role) : base (name, email, password, phone, role)
        { }
    }
}
