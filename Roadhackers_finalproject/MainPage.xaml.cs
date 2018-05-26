using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Roadhackers_finalproject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private async void Button_Click(object sender, RoutedEventArgs e) // Sandra: this is my code... don't you dare touche it...
        {     
             RootObject myWeather =
                await WeatherData.GetWeather(); // 
                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);// accessing my own pics (roayalty free)
            ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
            ResultTextBlock.Text = "windspeed: " + myWeather.wind.speed + " km/u" + " \n " + ((int)myWeather.main.temp).ToString() + " °C " +" \n " + myWeather.weather[0].description;
        }

        private void update_Click(object sender, RoutedEventArgs e) // this is Lennerts code
        {
            float distance = Convert.ToSingle(dist.Text); // Lennert you will need to throw an exception here, so that when the user does not enter anything the programm does not stop debugging. 
            float velocity = Convert.ToSingle(veloc.Value);
            float sol = distance / velocity * 60;
            res.Text = String.Format("Het zal {0} minuten duren.", sol.ToString("0.0"));

        }
    }
}
