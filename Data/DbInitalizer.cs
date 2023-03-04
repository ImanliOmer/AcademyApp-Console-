using Core.Entities;
using Core.Helper;
using Data.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DbInitalizer
    {
        static int id;
        public static void SeedAdmins()
        {
            var admins = new List<Admin>
            {
                new Admin
                {
                    Id = ++id,
                    Username= "Omer",
                    Pasword= PaswordHasher.Encrypt( "12345678")
                },
                new Admin
                {
                    Id = ++id,
                    Username = "Metleb",
                    Pasword= PaswordHasher.Encrypt("12345678")
                }
            };
            DbContext.Admins.AddRange(admins);
        }
    }
}
