using Core.Constants;
using Core.Entities;
using Core.Helper;
using Data.Repositories.Concrete;
using Presentation.Services;
using System;
using System.Globalization;

namespace Presentation
{
    class Program
    {
        private readonly static GroupService _groupService;
        static Program()
        {
            _groupService = new GroupService();
        }


        public static void Main()
        {
            GroupRepository _groupRepositery = new GroupRepository();

            ConsoleHelper.WriteWithColor("--- Welcome ---", ConsoleColor.Cyan);
            while (true)
            {

                ConsoleHelper.WriteWithColor("1- Create Group", ConsoleColor.Magenta);
                ConsoleHelper.WriteWithColor("2- Update Group", ConsoleColor.Magenta);
                ConsoleHelper.WriteWithColor("3- Delete Group", ConsoleColor.Magenta);
                ConsoleHelper.WriteWithColor("4- Get All Groups", ConsoleColor.Magenta);
                ConsoleHelper.WriteWithColor("5- Get Group By Id", ConsoleColor.Magenta);
                ConsoleHelper.WriteWithColor("6- Get Group By Name", ConsoleColor.Magenta);
                ConsoleHelper.WriteWithColor("0- exit", ConsoleColor.Magenta);

                ConsoleHelper.WriteWithColor("--- Sellect Option ---", ConsoleColor.Cyan);

                int number;
                bool IsSucceesed = int.TryParse(Console.ReadLine(), out number);
                if (!IsSucceesed)
                {
                    ConsoleHelper.WriteWithColor("Input number is not correct", ConsoleColor.Red);
                }
                else
                {
                    if (!(number >= 0 && number <= 6))
                    {
                        ConsoleHelper.WriteWithColor(" The entered number is not available\n Enter 0-6 digits", ConsoleColor.Red);
                    }
                    else
                    {

                        switch (number)
                        {
                            case (int)GroupOptions.CreateGroup:
                                _groupService.Create();
                               break;
                                ///////
                            case (int)GroupOptions.UpdateGroup:
                                _groupService.Update();
                                break;
                                //////////

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
                                break;


                            case (int)GroupOptions.Exit:
                                if(_groupService.Exit()==true)
                                {
                                    return;
                                }
                                break;

                                
                               
                            default:
                                break;

                        }
                    }
                }
            }
        }
    }
}
