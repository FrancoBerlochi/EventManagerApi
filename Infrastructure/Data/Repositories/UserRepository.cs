﻿using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
        public User? GetUserByUserName(string userName)
        {
            return null;
           // return _context.Users.SingleOrDefault(p => p.UserName == userName); ; 
        }
    }
}