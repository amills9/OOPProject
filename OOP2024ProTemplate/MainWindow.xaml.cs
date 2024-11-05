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

namespace OOP2024ProTemplate
{
   /// <summary>
   /// Main window class for WPF claculator
   /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Add code below to complete the implementation to populate the listBox
            // by reading the employee.csv file into a List of PaySlip objects, then binding this to the DatatGrid (OR ListBox.)
            // CSV file format: <employee ID>, <first name>, <last name>, <hourly rate>,<taxthreshold>
            List<Employee> employees = new List<Employee>();

            //employees with some CSV import list?

            empDataGrid.DataContext = employees;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Add code below to complete the implementation to populate the
            // payment summary (textBox2) using the PaySlip and PayCalculatorNoThreshold
            // and PayCalculatorWithThresholds classes object and methods.
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            // Add code below to complete the implementation for saving the
            // calculated payment data into a csv file.
            // File naming convention: Pay_<full name>_<datetimenow>.csv
            // Data fields expected - EmployeeId, Full Name, Hours Worked, Hourly Rate, Tax Threshold, Gross Pay, Tax, Net Pay, Superannuation
        }

        private void empDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Add code below to select values off of a datagrid

        }
    }


}