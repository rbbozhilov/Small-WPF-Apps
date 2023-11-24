using System;
using System.Collections.Generic;
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

namespace Controls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            label.Content = Result();

        }

        private bool ValidateTextBoxes(string firstTextBox, string secondTextBox, string thirdTextBox)
        {

            bool isValide = false;

            if (String.IsNullOrEmpty(firstTextBox) || String.IsNullOrEmpty(secondTextBox) || String.IsNullOrEmpty(thirdTextBox))
            {
                return isValide;
            }

            if (thirdTextBox == "+" || thirdTextBox == "-" || thirdTextBox == "*" || thirdTextBox == "/")
            {
                isValide = true;
            }

            bool valideFirstTextBox = double.TryParse(firstTextBox, out double resultFirst);
            bool valideSecondTextBox = double.TryParse(firstTextBox, out double resultSecond);


            return isValide && valideFirstTextBox && valideSecondTextBox;

        }

        private double Result()
        {
            string firstTextBoxText = textBox.Text;
            string secondTextBoxText = textBox1.Text;
            string thirdTextBoxText = textBox2.Text;

            bool isValide = ValidateTextBoxes(firstTextBoxText, secondTextBoxText, thirdTextBoxText);

            if (!isValide)
            {
                MessageBox.Show("Something went wrong..");
                return -1;
            }

            double firstNumber = double.Parse(firstTextBoxText);
            double secondNumber = double.Parse(secondTextBoxText);

            if (thirdTextBoxText == "+")
            {
                return firstNumber + secondNumber;
            }
            else if (thirdTextBoxText == "-")
            {
                return firstNumber - secondNumber;

            }
            else if (thirdTextBoxText == "*")
            {
                return firstNumber * secondNumber;

            }
            else
            {
                return firstNumber / secondNumber;
            }

        }
    }
}
