using System;
using System.Collections.Generic;
using System.Linq;

namespace New
{
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
    }
    class vmEmployeeShortInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class ToDoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<vmEmployeeShortInfo> EmployeeInfo { get; set; }
    }

    class Participant
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ToDoTaskId { get; set; }
    }
    
    internal class New
    {
        public void Assign(ToDoTask toDoTasks)
        {
            //Data Source
            List<Employee> employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Arif",
                    UserId = "arif123"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Rajib",
                    UserId = "raj123"
                },
                new Employee
                {
                    Id = 3,
                    Name = "Akash",
                    UserId = "akash123"
                },
                new Employee
                {
                    Id = 4,
                    Name = "Rimon",
                    UserId = "rim123"
                }
            };
            
            //Data Source
            List<Participant> participants = new List<Participant>
            {
                new Participant{Id = 1, UserId = "arif123", ToDoTaskId = 1},
                /*new Participant{Id = 2, UserId = "raj123", ToDoTaskId = 1},
                new Participant{Id = 3, UserId = "akash123", ToDoTaskId = 1},*/
            };

            //get all Employees
            var employeeList = employees.ToList();
            
            //get all Participants
            var participantList = participants.ToList();
            
            //get all vmEmployeeShortInfo
            var shortInfo = toDoTasks.EmployeeInfo.ToList();
            
            int i = 0;
            
            //Check the already existed elements with "to be" updated elements (old vs new)
            foreach (var item in participants)
            {
                //check if the old elements exists in the new elements. If not exists then remove
                var model = shortInfo.FirstOrDefault(x => x.Id == item.Id);
                if (model == null)
                {
                    participantList.RemoveAt(i);
                    i--;
                }

                i++;
            }

            i = 0;
            foreach (var item in shortInfo)
            {
                //check if the new elements exists in the old elements. If not exists then insert
                var participantEmployee = employeeList.FirstOrDefault(x => x.Id == item.Id);
                
                //check if the participant already exists or not
                var signal = participants.FirstOrDefault(x => x.UserId == participantEmployee.UserId);
                if (signal == null)
                {
                    //Gather information and insert into the Participant
                    var model = new Participant
                    {
                        Id = 123,
                        UserId = participantEmployee.UserId,
                        ToDoTaskId = toDoTasks.Id
                    };
                    participantList.Add(model);
                }

                i++;
            }

            foreach (var item in participantList)
            {
                Console.WriteLine("Id: "+ item.Id +", UserId: " + item.UserId + ", ToDoTaskId: " +item.ToDoTaskId);
            }
        }
        public static void Main(string[] args)
        {
            List<vmEmployeeShortInfo> employeeShortInfos = new List<vmEmployeeShortInfo>
            {
                new vmEmployeeShortInfo{Id = 2},
                new vmEmployeeShortInfo{Id = 1},
                new vmEmployeeShortInfo{Id = 3},
                new vmEmployeeShortInfo{Id = 4}
            };
            ToDoTask toDoTasks = new ToDoTask
            {
                Id = 1,
                Name = "Task123",
                EmployeeInfo = employeeShortInfos
            };
            new New().Assign(toDoTasks);
        }
    }
}