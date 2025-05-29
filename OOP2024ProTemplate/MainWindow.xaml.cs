using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;

namespace OOP2024ProTemplate
{
   /// <summary>
   /// Main window class for WPF claculator
   /// </summary>
    public partial class MainWindow : Window
    {
        private List<Employee> employees = new();
        private Employee selectedEmployee;
        public MainWindow()
        {
            InitializeComponent();
            // Add code below to complete the implementation to populate the listBox
            // by reading the employee.csv file into a List of PaySlip objects, then binding this to the DatatGrid (OR ListBox.)
            // CSV file format: <employee ID>, <first name>, <last name>, <hourly rate>,<taxthreshold>
            //List<Employee> employees = new List<Employee>();
            //employees with some CSV import list?
            //empDataGrid.DataContext = employees;

            using var reader = new StreamReader("employee.csv");
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            { HasHeaderRecord = false };
            
            using var csv = new CsvReader(reader, config);
            employees = csv.GetRecords<Employee>().ToList();
            empDataGrid.ItemsSource = employees;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Add code below to complete the implementation to populate the
            // payment summary (textBox2) using the PaySlip and PayCalculatorNoThreshold
            // and PayCalculatorWithThresholds classes object and methods.

            if (selectedEmployee == null)
            {
                MessageBox.Show("Please select an employee.");
                return;
            }

            if (!decimal.TryParse(hrsWrkEntry.Text, out decimal hoursWorked))
            {
                MessageBox.Show("Enter valid number of hours worked.");
                return;
            }

            PaySlip payslip = new()
            {
                employee = selectedEmployee,
                hoursWorked = hoursWorked
            };

            PaySummary.Text = $"Employee ID: {selectedEmployee.employeeID}\n" +
                              $"Name: {selectedEmployee.firstName} {selectedEmployee.lastName}\n" +
                              $"Hours Worked: {hoursWorked}\n" +
                              $"Hourly Rate: ${selectedEmployee.hourlyRate}\n" +
                              $"Tax Threshold: {selectedEmployee.taxthreshold}\n" +
                              $"Gross Pay: ${payslip.grossPay:F2}\n" +
                              $"Tax: ${payslip.tax:F2}\n" +
                              $"Net Pay: ${payslip.nettPay:F2}\n" +
                              $"Superannuation: ${payslip.super:F2}";
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            // Add code below to complete the implementation for saving the
            // calculated payment data into a csv file.
            // File naming convention: Pay_<full name>_<datetimenow>.csv
            // Data fields expected - EmployeeId, Full Name, Hours Worked, Hourly Rate, Tax Threshold, Gross Pay, Tax, Net Pay, Superannuation

            if (selectedEmployee == null)
            {
                MessageBox.Show("Please select an employee and calculate the payslip first.");
                return;
            }

            if (!decimal.TryParse(hrsWrkEntry.Text, out decimal hoursWorked))
            {
                MessageBox.Show("Enter valid number of hours worked.");
                return;
            }

            PaySlip payslip = new()
            {
                employee = selectedEmployee,
                hoursWorked = hoursWorked
            };

            string fileName = $"Pay-{selectedEmployee.employeeID}-{selectedEmployee.firstName}{selectedEmployee.lastName}-{DateTime.Now:yyyyMMddHHmmss}.csv";
            using var writer = new StreamWriter(fileName);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteHeader<PaySlipExport>();
            csv.NextRecord();
            csv.WriteRecord(new PaySlipExport
            {
                EmployeeID = selectedEmployee.employeeID,
                FullName = $"{selectedEmployee.firstName} {selectedEmployee.lastName}",
                HoursWorked = payslip.hoursWorked,
                HourlyRate = selectedEmployee.hourlyRate,
                TaxThreshold = selectedEmployee.taxthreshold,
                GrossPay = payslip.grossPay,
                Tax = payslip.tax,
                NetPay = payslip.nettPay,
                Superannuation = payslip.super
            });

            MessageBox.Show($"Payslip saved to {fileName}");
        }

        private void empDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Add code below to select values off of a datagrid
            selectedEmployee = empDataGrid.SelectedItem as Employee;

        }
    }


}