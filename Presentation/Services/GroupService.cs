using Core.Entities;
using Core.Helper;
using Data.Repositories.Concrete;
using System;
using System.Globalization;
using System.Xml.Linq;

namespace Presentation.Services
{
    public class GroupService
    {
        private readonly GroupRepository _groupRepositery;
        private readonly StudentRepository _studentRepository;
        public GroupService()
        {
            _groupRepositery = new GroupRepository();
            _studentRepository = new StudentRepository();
        }
        public void Create()
        {
           NameDesc: ConsoleHelper.WriteWithColor("---Enter Group Name---", ConsoleColor.DarkBlue);
            string name = Console.ReadLine();
            var group = _groupRepositery.GetByName(name);
            if (group is not null)
            {
                ConsoleHelper.WriteWithColor("this group is already added",ConsoleColor.Red);
                goto NameDesc;
            }

            int maxSize;
        MaxSizeDesc: ConsoleHelper.WriteWithColor("---Enter Group Maxsize---", ConsoleColor.DarkBlue);
            bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Max number is not in correct format", ConsoleColor.Red);
                goto MaxSizeDesc;
            }        
            if (maxSize > 18)
            {
                ConsoleHelper.WriteWithColor("max number cannot be greater than 18", ConsoleColor.Red);
                goto MaxSizeDesc;
            }

        StartDateTimeDesc: ConsoleHelper.WriteWithColor("---Enter Start Date---", ConsoleColor.DarkBlue);
            DateTime startDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor(" Start Date is not correct format\n Example:dd.mm.yyyy", ConsoleColor.Red);
                goto StartDateTimeDesc;
            }
            DateTime boundryDate = new DateTime(2015, 1, 1);
            if (startDate < boundryDate)
            {
                ConsoleHelper.WriteWithColor(" Start date is not choosen right\n Startig 01.01.2015", ConsoleColor.Red);
                goto StartDateTimeDesc;
            }

        EndDateTimeDesc: ConsoleHelper.WriteWithColor("---Enter End Date---", ConsoleColor.DarkBlue);
            DateTime endDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            if (!(isSucceeded))
            {
                ConsoleHelper.WriteWithColor("*End Date is not correct format\n*Example:dd.mm.yyyy", ConsoleColor.Red);
                goto EndDateTimeDesc;
            }
            if (endDate < startDate)
            {
                ConsoleHelper.WriteWithColor("The end date cannot be greater than the start date", ConsoleColor.Red);
                goto EndDateTimeDesc;
            }

            group = new Group
            {
                Name = name,
                MaxSize = maxSize,
                StartDate = startDate,
                EndDate = endDate,
            };

            _groupRepositery.Add(group);
            ConsoleHelper.WriteWithColor($"Group succesfuly created\nName: {group.Name}\nMaxSize: {group.MaxSize}\nStart Date: {group.StartDate.ToShortDateString()}\nEnd Date: {group.EndDate.ToShortDateString()}", ConsoleColor.Yellow);

        }
        public void GetAll()
        {
            ConsoleHelper.WriteWithColor("--- ALL GROUPS---", ConsoleColor.DarkCyan);
            var groups = _groupRepositery.GetAll();
            foreach (var groups_ in groups)
            {
                ConsoleHelper.WriteWithColor($"Id: {groups_.Id}Name: {groups_.Name}\nMax Size: {groups_.MaxSize}\nStart Date: {groups_.StartDate}\nEnd Date: {groups_.EndDate}", ConsoleColor.Yellow);
            }
        }
        public void Delete()
        {
            GetAll();
        IdDesc: ConsoleHelper.WriteWithColor("---ENTER ID---", ConsoleColor.DarkBlue);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("ID is not correct variant", ConsoleColor.DarkBlue);
                goto IdDesc;
            }
            var dbGroup = _groupRepositery.Get(id);
            if (dbGroup is null)
            {
                ConsoleHelper.WriteWithColor("There is no any group this iD", ConsoleColor.Red);
            }
            else
            {
                foreach (var student in dbGroup.Students)
                {
                    student.Group = null;
                    _studentRepository.Update(student);
                }
                _groupRepositery.Delete(dbGroup);
                ConsoleHelper.WriteWithColor("Group succesfuly deleted", ConsoleColor.DarkGreen);
            }
        }
        public bool Exit()
        {

        AreYouSureDesc: ConsoleHelper.WriteWithColor("Are you sure -- y or n --", ConsoleColor.Red);
            char decision;
            bool IsSucceesed = char.TryParse(Console.ReadLine(), out decision);
            if (!IsSucceesed)
            {
                ConsoleHelper.WriteWithColor("your chois is not correct format", ConsoleColor.Red);
            }
            if (!(decision == 'y' || decision == 'n'))
            {
                ConsoleHelper.WriteWithColor("your chois is not correct", ConsoleColor.Red);
                goto AreYouSureDesc;
            }

            if (decision == 'y')
            {
            return true;
            }
            else
            {
                return false;
            }
           
        }
        public void GetGroupById()
        {
            var groupCount = _groupRepositery.GetAll();

            if (groupCount.Count == 0)
            {

                Areyousuredesc: ConsoleHelper.WriteWithColor("there is no any group do you want to creatre new group", ConsoleColor.Red); 
                char decision;
                bool isSucceedResult = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceedResult)
                {
                    ConsoleHelper.WriteWithColor("your chis is not correct format", ConsoleColor.Red);
                    goto Areyousuredesc;
                }
                if (!(decision == 'y' || decision == 'n'))
                {
                    ConsoleHelper.WriteWithColor("your chis is not correct", ConsoleColor.Red);
                    goto Areyousuredesc;
                }

                if (decision == 'y')
                {
                    Create();
                }
                else
                {
                    return;
                }
            }
            else
            {
                GetAll();

            EnterIdDesc: ConsoleHelper.WriteWithColor(" --- EnterId --- ", ConsoleColor.DarkBlue);

                int id;
                bool isSucceed = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceed)
                {
                    ConsoleHelper.WriteWithColor(" inputed Id is not correct format ", ConsoleColor.Red);
                    goto EnterIdDesc;
                }

                var group = _groupRepositery.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor(" there is no eny group in this Id ", ConsoleColor.Red);
                    goto EnterIdDesc;
                }

                ConsoleHelper.WriteWithColor($"Id: {group.Id}Name: {group.Name}\n Max Size: {group.MaxSize}\n Start Date: {group.StartDate}\n End Date: {group.EndDate}", ConsoleColor.Yellow);

            }


        }
        public void GetGroupByName()
        {


        StartName: ConsoleHelper.WriteWithColor("Enter group name ", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            var group = _groupRepositery.GetGroupByName(name);



            if (group is null)
            {
                ConsoleHelper.WriteWithColor("There is no any group in this name", ConsoleColor.Red);
                goto StartName;
            }
            ConsoleHelper.WriteWithColor($"\nId : {group.Id} \nName : {group.Name} \nMax size : {group.MaxSize}  \nStartDate : {group.StartDate.ToShortDateString()} \nEnd date : {group.EndDate.ToShortDateString()}", ConsoleColor.Blue);


        }
        public void Update()
        {
            GetAll();


        EnterGroupDescription: ConsoleHelper.WriteWithColor("Enter group \n1. id \n2. name", ConsoleColor.Cyan);


            int number;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Entered number is not correct format ", ConsoleColor.Red);
                goto EnterGroupDescription;
            }

            if (!(number == 1 || number == 2))
            {
                ConsoleHelper.WriteWithColor("Entered number is not correct", ConsoleColor.Red);
                goto EnterGroupDescription;
            }

            if (number == 1)
            {
            EnterGroupIdDescription: ConsoleHelper.WriteWithColor("Enter group id :", ConsoleColor.Cyan);
                int id;
                isSucceeded = int.TryParse(Console.ReadLine(), out id);

                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("inputed id is not correct format ", ConsoleColor.Red);
                    goto EnterGroupIdDescription;
                }
                var group = _groupRepositery.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("there is no any group in this id");
                }
                InternalUpdate(group);
            }
            else
            {
            EnterGroupNameDescription: ConsoleHelper.WriteWithColor("Enter group name", ConsoleColor.Cyan);
                string name = Console.ReadLine();
                var group = _groupRepositery.GetGroupByName(name);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("there is no any group in this name");
                }
                InternalUpdate((Group)group);
            }
        }
        public void InternalUpdate(Core.Entities.Group group)
        {
            ConsoleHelper.WriteWithColor("Enter new name", ConsoleColor.Cyan);
            string name = Console.ReadLine();
        MaxSizeDescription: ConsoleHelper.WriteWithColor("Enter new Max Size :", ConsoleColor.Cyan);
            int maxSize;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Max size is not correct format ", ConsoleColor.Red);
                goto MaxSizeDescription;
            }
        startDate: ConsoleHelper.WriteWithColor("Enter new start Date : ", ConsoleColor.Cyan);
            DateTime startDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Start Date is not correct format  ", ConsoleColor.Red);
                goto startDate;
            }
        endDate: ConsoleHelper.WriteWithColor("Enter new end Date : ", ConsoleColor.Cyan);
            DateTime endDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("End Date is not correct format ", ConsoleColor.Red);
                goto endDate;
            }
            group.Name = name;
            group.MaxSize = maxSize;
            group.StartDate = startDate;
            group.EndDate = endDate;
            _groupRepositery.Update(group);
        }
        



    }
}
