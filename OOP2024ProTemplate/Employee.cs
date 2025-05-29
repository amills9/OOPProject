using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace OOP2024ProTemplate
{
    public class Employee
    {
        //Id, firstName, lastName, typeEmployee, hourlyRate, taxthreshold
        public int employeeID { get; set; }
        public string? firstName { get; set; }

        public string? lastName { get; set; }

        public string? typeEmployee { get; set; }

        public decimal hourlyRate { get; set; }

        public string taxthreshold { get; set; }


    }
}
