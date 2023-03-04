using Core.Constants;
using Core.Helper;
using Data;
using Presentation.Services;
using System;

namespace Presentation
{
    class Program
    {
        private readonly static GroupService _groupService;
        private readonly static StudentService _studentService;
        private readonly static AdminService _adminService;
        private readonly static TeacherService _teacherService;


        static Program()
        {
            _groupService = new GroupService();
            _studentService = new StudentService();
            _adminService = new AdminService();
            _teacherService = new TeacherService();
            DbInitalizer.SeedAdmins();
        }


        public static void Main()
        {
            ConsoleHelper.WriteWithColor("--- Welcome ---", ConsoleColor.Cyan);

            var admin = _adminService.Authorize();
            if (admin is not null)
            {
                while (true)
                {
                MainMenuDesc: ConsoleHelper.WriteWithColor("1-Group", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor("2-Student", ConsoleColor.Yellow);
                    ConsoleHelper.WriteWithColor("0-Exit", ConsoleColor.Yellow);
                    int number;
                    bool isSucceesed = int.TryParse(Console.ReadLine(), out number);
                    if (!isSucceesed)
                    {
                        ConsoleHelper.WriteWithColor("Input number is not correct", ConsoleColor.Red);
                        goto MainMenuDesc;
                    }
                    else
                    {
                        switch (number)
                        {
                            case
                                (int)MainMenuOptions.Groups:
                                while (true)
                                {
                                GroupDesc: ConsoleHelper.WriteWithColor("1- Create Group", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("2- Update Group", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("3- Delete Group", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("4- Get All Groups", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("5- Get Group By Id", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("6- Get Group By Name", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("0- BackToMainMenu", ConsoleColor.Magenta);

                                    ConsoleHelper.WriteWithColor("--- Sellect Option ---", ConsoleColor.Cyan);


                                    bool IsSucceesed = int.TryParse(Console.ReadLine(), out number);

                                    switch (number)
                                    {
                                        case (int)GroupOptions.CreateGroup:
                                            _groupService.Create();
                                            break;

                                        case (int)GroupOptions.UpdateGroup:
                                            _groupService.Update();
                                            break;

                                        case (int)GroupOptions.DeleteGroup:
                                            _groupService.Delete();
                                            break;

                                        case (int)GroupOptions.GetAllGroups:
                                            _groupService.GetAll();
                                            break;

                                        case (int)GroupOptions.GetGroupById:
                                            _groupService.GetGroupById();

                                            break;

                                        case (int)GroupOptions.GetGroupByName:
                                            _groupService.GetGroupByName();
                                            break;

                                        case (int)GroupOptions.BackToMainMenu:
                                            goto MainMenuDesc;
                                            break;

                                        default:
                                            ConsoleHelper.WriteWithColor("Input number is not correct", ConsoleColor.Red);
                                            goto GroupDesc;
                                            break;

                                    }
                                }
                            case (int)MainMenuOptions.Students:
                                while (true)
                                {
                                StudentDesc: ConsoleHelper.WriteWithColor("1- Create Student", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("2- Update Student", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("3- Delete Student", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("4- Get All Students", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("5- GetAllStudentByGroup", ConsoleColor.Magenta);
                                    ConsoleHelper.WriteWithColor("0- BackToMainMenu", ConsoleColor.Magenta);

                                    ConsoleHelper.WriteWithColor("--- Sellect Option ---", ConsoleColor.Cyan);


                                    bool IsSucceesed = int.TryParse(Console.ReadLine(), out number);

                                    switch (number)
                                    {
                                        case (int)StudentOptions.CreateStudent:
                                            _studentService.Create();
                                            break;

                                        case (int)StudentOptions.UpdateStudent:
                                            break;

                                        case (int)StudentOptions.DeleteStudent:
                                            _studentService.Delete();
                                            break;

                                        case (int)StudentOptions.GetAllStudents:
                                            _studentService.GetAll();
                                            break;

                                        case (int)StudentOptions.GetAllStudentByGroup:
                                            _studentService.GetAllByGroup();
                                            break;

                                        case (int)StudentOptions.BackToMainMenu:
                                            goto MainMenuDesc;
                                            break;

                                        default:
                                            ConsoleHelper.WriteWithColor("Input number is not correct", ConsoleColor.Red);
                                            goto StudentDesc;
                                            break;

                                    }
                                }
                            case (int)MainMenuOptions.Teacher:
                                while (true)
                                {
                                TeacherMenu: ConsoleHelper.WriteWithColor("1. Create Teacher", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("2. Update Teacher", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("3. Delete Teacher", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("4. Get All Teacher", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("5. Get Group By Name", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("0. Back to Main Menu", ConsoleColor.DarkYellow);

                                    ConsoleHelper.WriteWithColor("--- Select Option ---", ConsoleColor.DarkCyan);


                                    bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Inputed is not correct format");
                                        goto TeacherMenu;
                                    }
                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)TeacherOptions.CreateTeacher:
                                                _teacherService.Create();
                                                break;

                                            case (int)TeacherOptions.UpdateTeacher:
                                                _teacherService.Update();
                                                break;

                                            case (int)TeacherOptions.DeleteTeacher:
                                                _teacherService.Delete();
                                                break;

                                            case (int)TeacherOptions.GetAllTeacher:
                                                _teacherService.GetAll();
                                                break;

                                            case (int)TeacherOptions.BackToMainMenu:
                                                goto MainMenuDesc;

                                            default:
                                                ConsoleHelper.WriteWithColor("Inputed number is not exist", ConsoleColor.Red);
                                                goto TeacherMenu;
                                        }
                                    }
                                }
                                
                            case (int)MainMenuOptions.Exit:
                                return;
                            default:
                                ConsoleHelper.WriteWithColor("Input number is not correct", ConsoleColor.Red);
                                break;
                        }


                    }
                }

            }


        }
    }
}
