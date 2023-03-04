using Core.Entities;
using Core.Extensions;
using Core.Helper;
using Data.Repositories.Concrete;
using System;
using System.Globalization;

namespace Presentation.Services
{
    public class StudentService
    {
        private readonly GroupService _groupService;
        private readonly GroupRepository _groupRepository;
        private readonly StudentRepository _studentRepository;
        public StudentService()
        {
            _groupService = new GroupService();
            _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();
        }
        public void GetAll()
        {
            var students = _studentRepository.GetAll();
            ConsoleHelper.WriteWithColor("---All Students---", ConsoleColor.Cyan);
            foreach (var student in students)
            {
                ConsoleHelper.WriteWithColor($"Id: {student.Id}\n Name: {student.Name}\n Surname: {student.Surname}\n Group: {student.Group?.Name}", ConsoleColor.Green);
            }
        }

        public void GetAllByGroup()
        {
            _groupService.GetAll();
        GroupDesc: ConsoleHelper.WriteWithColor("Enter group id");
            int groupId;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Group id is not correct format", ConsoleColor.Red);
                goto GroupDesc;
            }

            var group = _groupRepository.Get(groupId);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("there is no any group in this id", ConsoleColor.Red);
            }
            else
            {
                foreach (var student in group.Students)
                {
                    ConsoleHelper.WriteWithColor($"Id: {student.Id}\n Name: {student.Name}\n Surname: {student.Surname}", ConsoleColor.Green);
                }

            }


        }

        public void Create()
        {
            if (_groupRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("you must create a group first", ConsoleColor.Red);
                return;
            }
            ConsoleHelper.WriteWithColor("Enter student name", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("Enter student surname", ConsoleColor.Cyan);
            string surname = Console.ReadLine();
        EmailDesc: ConsoleHelper.WriteWithColor("Enter student email", ConsoleColor.Cyan);
            string email = Console.ReadLine();
            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Email is not corect format", ConsoleColor.Red);
                goto EmailDesc;
            }
            if (_studentRepository.IsDublicatedEmail(email))
            {
                ConsoleHelper.WriteWithColor("This Email is already used", ConsoleColor.Red);
                goto EmailDesc;
            }
        BirthDateTimeDesc: ConsoleHelper.WriteWithColor("---Enter birth Date---", ConsoleColor.DarkBlue);
            DateTime birthdate;
            bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" birth Date is not correct format\n Example:dd.mm.yyyy", ConsoleColor.Red);
                goto BirthDateTimeDesc;
            }

        GroupDescription: _groupService.GetAll();
            ConsoleHelper.WriteWithColor("Enter Group Id", ConsoleColor.Red);


            int groupId;
            isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Group Id is not correct format", ConsoleColor.Red);
                goto GroupDescription;
            }

            var group = _groupRepository.Get(groupId);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("group is not exsist", ConsoleColor.Red);
                goto GroupDescription;
            }

            if (group.MaxSize <= group.Students.Count)
            {
                ConsoleHelper.WriteWithColor("this group is full", ConsoleColor.Red);
                goto GroupDescription;

            }

            var student = new Student
            {
                Name = name,
                Surname = surname,
                Birthdate = birthdate,
                Email = email,
                Group = group,
                GroupId = group.Id
            };

            group.Students.Add(student);
            _studentRepository.Add(student);
            ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname} is successfully creqated", ConsoleColor.Green);
        }

        public void Delete()
        {
            GetAll();


        IdDescription: ConsoleHelper.WriteWithColor("--- Which group do you want to delete?  ---", ConsoleColor.Cyan);


            int id;
            bool isSucceded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceded)
            {
                ConsoleHelper.WriteWithColor("Uncorrect Format ! ", ConsoleColor.Red);
                goto IdDescription;
            }

            var dbStudent = _studentRepository.Get(id);
            if (dbStudent is null)
            {
                ConsoleHelper.WriteWithColor("There is no any group in this id! ", ConsoleColor.Red);


            }
            else
            {
                _studentRepository.Delete(dbStudent);
                ConsoleHelper.WriteWithColor("Group successfuly deleted ", ConsoleColor.Green);

            }
        }

    }
}
