using RestSharp;
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
using Newtonsoft;
using Newtonsoft.Json;

namespace Weather_with_API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ApiKey = "5ccb83bf4f26321a5ac73968627e4f1f";
        private const string BaseUrl = "http://api.openweathermap.org/data/2.5/weather";
        public class Main
        {
            public double temp { get; set; }
        }
        public class Weather
        {
            public string description { get; set; }
           // public string main { get; set; }
        }
        public class WeatherInfo
        {
            public Main main { get; set; }
            public string name { get; set; }
            public List<Weather> weather { get; set; }
        }
            public MainWindow()
            {
                InitializeComponent();
            }

    private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
        string city = txtCity.Text;
        if (string.IsNullOrEmpty(city))
        {
            MessageBox.Show("Please enter a city name.");
            return;
        }

        string url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric";
        var client = new RestClient(url);
        var request = new RestRequest(url,Method.Get);
        var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(response.Content);
                lblShow.Content = $"Temperature in {weatherInfo.name}: {weatherInfo.main.temp}°C";
                lblShowDesc.Content = $"sky is: {weatherInfo.weather[0].description}";
        }
        else
        {
            MessageBox.Show("Error fetching weather data.");
        }
    }
  } 
}

