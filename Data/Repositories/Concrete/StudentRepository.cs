using Core.Entities;
using Core.Helper;
using Data.Contexts;
using Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
    public class StudentRepository : IStudenRepository
    {
        private int id;

        public List<Student> GetAll()
        {
            return DbContext.Students;
        }

        public Student Get(int id)
        {
            return DbContext.Students.FirstOrDefault(g => g.Id == id);
        }


        public void Add(Student student)
        {
            id++;
            student.Id = id;
            student.CreatedAt = DateTime.Now;
            DbContext.Students.Add(student);
        }
        public void Update(Student student)
        {
            var DbStudent = DbContext.Students.FirstOrDefault(s => s.Id == id);
            if (DbStudent is not null)
            {
                DbStudent.Name = student.Name;
                DbStudent.Surname = student.Surname;
                DbStudent.Birthdate= student.Birthdate;
            }
        }

        public void Delete(Student student)
        {
            DbContext.Students.Remove(student);
        }
        public bool IsDublicatedEmail(string email)
        {
            return DbContext.Students.Any(s => s.Email == email);
        }

    }
}
