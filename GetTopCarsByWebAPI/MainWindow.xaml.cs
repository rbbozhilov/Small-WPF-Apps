using Newtonsoft.Json;

using System.Collections.Generic;

using System.Net.Http;

using System.Windows;
using System.Windows.Controls;
using TopCars.Models;

namespace TopCars
{

    public partial class MainWindow : Window
    {

        private Label name;
        private Label mark;
        private Label price;


        public MainWindow()
        {
            InitializeComponent();

            
        }

        private async void  GetCars_Click(object sender, RoutedEventArgs e)
        {

            this.name = new Label();
            this.name.Content = "Name:";

            this.mark = new Label();
            this.mark.Content = "Mark:";

            this.price = new Label();
            this.price.Content = "Price:";

            HttpClient client = new HttpClient();
            string url = "https://localhost:44353/api/topcars";

           string responseString = await client.GetStringAsync(url);


           List<Car> cars =  JsonConvert.DeserializeObject<List<Car>>(responseString);

            int counter = 1;
            foreach(var car in cars)
            {
                Label newLabel = new Label();
                newLabel.Content = $"{counter} Car";
                counter++;
                stackPan.Children.Add(newLabel);

                TextBox carName = new TextBox();
                carName.Text = car.Name;
                stackPan.Children.Add(carName);

                TextBox carModel = new TextBox();
                carModel.Text = car.Mark;
                stackPan.Children.Add(carModel);

                TextBox carPrice = new TextBox();
                carPrice.Text = car.Price.ToString();
                stackPan.Children.Add(carPrice);


            }

        }
    }
}
