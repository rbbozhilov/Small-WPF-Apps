using Gym.Data;
using Gym.Data.Models;
using Gym.DI;
using Gym.Services.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gym.WPF
{
    public partial class MainWindow : Window
    {

        private ServiceProvider provider;
        private IClientService clientService;


        public MainWindow()
        {
            InitializeComponent();
            this.provider = Container.ConfigureServices();
            this.clientService = this.provider.GetRequiredService<IClientService>();
        }

        private void SearchByNames(object sender, RoutedEventArgs e)
        {
            var textBoxFirstName = firstNameSearch.Text;
            var textBoxLastName = lastNameSearch.Text;

            var clientInformation = this.clientService.SearchClientByName(textBoxFirstName, textBoxLastName);

            if (clientInformation == null)
            {
                MessageBox.Show("Not Found");
                this.CleanTextBoxes();
                return;
            }


            fullName.Content = clientInformation.FullName;
            startDate.Content = clientInformation.StartDate.Date;
            endDate.Content = clientInformation.EndDate.Date;
            informationNumber.Content = clientInformation.Number;

            this.CleanTextBoxes();
        }

        private void SearchByNumber(object sender, RoutedEventArgs e)
        {
            var isNumber = int.TryParse(number.Text, out int clientNumber);

            if (!isNumber)
            {
                MessageBox.Show("Something went wrong!");
                this.CleanTextBoxes();
                return;
            }

            var clientInformation = this.clientService.SearchClientByNumber(clientNumber);

            if (clientInformation == null)
            {
                MessageBox.Show("Not Found");
                this.CleanTextBoxes();
                return;
            }


            fullName.Content = clientInformation.FullName;
            startDate.Content = clientInformation.StartDate.Date;
            endDate.Content = clientInformation.EndDate.Date;
            informationNumber.Content = clientInformation.Number;

            this.CleanTextBoxes();
        }

        private void CleanTextBoxes()
        {
            number.Text = string.Empty;
            firstNameSearch.Text = string.Empty;
            lastNameSearch.Text = string.Empty;
        }

        private void CleanAddTextBoxes()
        {
            addFirstName.Text = string.Empty;
            addLastName.Text = string.Empty;
            addStartDate.Text = string.Empty;
            addEndDate.Text = string.Empty;
        }

        private async void AddClient(object sender, RoutedEventArgs e)
        {
            var firstName = addFirstName.Text;
            var lastName = addLastName.Text;
            var startDate = addStartDate.Text;
            var endDate = addEndDate.Text;

            if (String.IsNullOrEmpty(firstName) || String.IsNullOrWhiteSpace(firstName) ||
                String.IsNullOrEmpty(lastName) || String.IsNullOrWhiteSpace(lastName) ||
                String.IsNullOrEmpty(startDate) || String.IsNullOrWhiteSpace(startDate) ||
                String.IsNullOrEmpty(endDate) || String.IsNullOrWhiteSpace(endDate))
            {
                MessageBox.Show("Something went wrong!");
                return;
            }

            string format = "dd-MM-yyyy";
            DateTime parsedStartDate;
            DateTime parsedEndDate;
            IFormatProvider provider = CultureInfo.InvariantCulture;
            DateTimeStyles styles = DateTimeStyles.None;

            bool isValidStartDate = DateTime.TryParseExact(startDate, format, provider, styles, out parsedStartDate);
            bool isValidEndDate = DateTime.TryParseExact(endDate, format, provider, styles, out parsedEndDate);

            if (!isValidStartDate || !isValidEndDate || (parsedStartDate >= parsedEndDate))
            {
                MessageBox.Show("Invalid start date or end date");
                return;
            }

            await this.clientService.AddClient(firstName, lastName, parsedStartDate, parsedEndDate);

            MessageBox.Show("Added succesfull");
            this.CleanAddTextBoxes();
        }

        private async void EditClient(object sender, RoutedEventArgs e)
        {
            var clientNumber = editNumber.Text;
            var clientFirstName = editFirstName.Text;
            var clientLastName = editLastName.Text;
            var clientStartDate = editStartDate.Text;
            var clientEndDate = editEndDate.Text;

            var isValidNumber = int.TryParse(clientNumber, out int resultNumber);

            if (!isValidNumber)
            {
                MessageBox.Show("Invalid number");
                return;
            }

            string format = "dd-MM-yyyy";
            DateTime parsedStartDate;
            DateTime parsedEndDate;
            IFormatProvider provider = CultureInfo.InvariantCulture;
            DateTimeStyles styles = DateTimeStyles.None;

            bool isValidStartDate = DateTime.TryParseExact(clientStartDate, format, provider, styles, out parsedStartDate);
            bool isValidEndDate = DateTime.TryParseExact(clientEndDate, format, provider, styles, out parsedEndDate);

            if (!isValidStartDate || !isValidEndDate || (parsedStartDate >= parsedEndDate))
            {
                MessageBox.Show("Invalid start date or end date");
                return;
            }

            if (clientFirstName == string.Empty && clientLastName == string.Empty)
            {
                bool isDone = await this.clientService.EditClient(resultNumber, parsedStartDate, parsedEndDate);

                if (!isDone)
                {
                    MessageBox.Show("Cannot find client");
                    return;
                }

                MessageBox.Show("Editted succesfull");
                this.CleanEditTextBoxes();
                return;
            }

            if (String.IsNullOrEmpty(clientFirstName) || String.IsNullOrWhiteSpace(clientFirstName) ||
              String.IsNullOrEmpty(clientLastName) || String.IsNullOrWhiteSpace(clientLastName))
            {
                MessageBox.Show("Please input first name and last name");
                return;
            }

            bool isEdit = await this.clientService.EditClient(resultNumber,clientFirstName,clientLastName, parsedStartDate, parsedEndDate);

            if (!isEdit)
            {
                MessageBox.Show("Cannot find client");
                return;
            }

            MessageBox.Show("Editted succesfull");
            this.CleanEditTextBoxes();

        }

        private void CleanEditTextBoxes()
        {
            editFirstName.Text = string.Empty;
            editLastName.Text = string.Empty;
            editStartDate.Text = string.Empty;
            editEndDate.Text = string.Empty;
            editNumber.Text = string.Empty;
        }
    }
}
