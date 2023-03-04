using Core.Entities;
using Core.Helper;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class AdminService
    {
        private readonly AdminRepository _adminRepository;

        public AdminService() 
        {
            _adminRepository= new AdminRepository();
        }

        public Admin Authorize()
        {
            LoginDesc: ConsoleHelper.WriteWithColor("--- Login in ---", ConsoleColor.Blue);

            ConsoleHelper.WriteWithColor("Enter username", ConsoleColor.Cyan);
            string username = Console.ReadLine();
            ConsoleHelper.WriteWithColor("Enter password", ConsoleColor.Cyan);
            string password = Console.ReadLine();

            var admin = _adminRepository.GetByUsernameAndPassword(username, password);
            if (admin is null)
            {
                ConsoleHelper.WriteWithColor("Username or Pasword incorrect",ConsoleColor.Red);

                goto LoginDesc;
            }

            return admin;
        }
    }
}
