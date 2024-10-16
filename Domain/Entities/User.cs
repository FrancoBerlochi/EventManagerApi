﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class User
    {
        protected string Name { get; set; }
        protected string Email { get; set; }
        protected string Password { get; set; }
        protected string Phone {  get; set; }
        public string Role {  get; set; }
        protected User(string name, string email, string password, string phone, string role)
        {
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            Role = role;
        }
    }
}
