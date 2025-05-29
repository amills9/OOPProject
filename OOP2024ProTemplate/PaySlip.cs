using System;
using System.Collections.Generic;
using System.IO;

namespace OOP2024ProTemplate
{
    public class PaySlip
    {
        public Employee employee { get; set; }
        public decimal hoursWorked { get; set; }

        public decimal grossPay => employee.hourlyRate * hoursWorked;
        public decimal tax => CalculateTax(grossPay, employee.taxthreshold);
        public decimal nettPay => grossPay - tax;
        public decimal super => grossPay * 0.1m; // 10% superannuation

        public PaySlip(Employee emp, decimal hours)
        {
            employee = emp;
            hoursWorked = hours;
        }

        public PaySlip() { }

        // Inner class for tax bracket information
        private class TaxBracket
        {
            public decimal LowerLimit { get; set; }
            public decimal UpperLimit { get; set; }
            public decimal Rate { get; set; }
            public decimal BaseTax { get; set; }
        }

        private decimal CalculateTax(decimal grossPay, string threshold)
        {
            var brackets = LoadTaxBrackets(threshold);
            foreach (var b in brackets)
            {
                if (grossPay >= b.LowerLimit && grossPay < b.UpperLimit)
                {
                    return (grossPay - b.LowerLimit) * b.Rate + b.BaseTax;
                }
            }
            return 0;
        }

        private List<TaxBracket> LoadTaxBrackets(string threshold)
        {
            string path = threshold == "Y" ? "taxrate-withthreshold.csv" : "taxrate-nothreshold.csv";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Tax file not found: {path}");
            }

            var brackets = new List<TaxBracket>();
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                if (parts.Length < 4) continue;

                brackets.Add(new TaxBracket
                {
                    LowerLimit = decimal.Parse(parts[0]),
                    UpperLimit = decimal.Parse(parts[1]),
                    Rate = decimal.Parse(parts[2]),
                    BaseTax = decimal.Parse(parts[3])
                });
            }

            return brackets;
        }
    }

    public class PaySlipExport
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string TaxThreshold { get; set; }
        public decimal GrossPay { get; set; }
        public decimal Tax { get; set; }
        public decimal NetPay { get; set; }
        public decimal Superannuation { get; set; }
    }
}


