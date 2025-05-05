using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SuperAdminService
    {
        private readonly ISuperAdminRepository _superAdminRepository;

        public SuperAdminService(ISuperAdminRepository superAdminRepository) 
        {
            _superAdminRepository = superAdminRepository;
        }

        public bool Update(int id, SuperAdminUpdateRequest updateRequest) 
        { 
            var superAdmin = new SuperAdmin(updateRequest.Name, updateRequest.Email, updateRequest.Password, updateRequest.Phone);
            var update = _superAdminRepository.Update(id, superAdmin);

            if (update)
            {
                return true;   
            }
            return false;
        }
    }
}
