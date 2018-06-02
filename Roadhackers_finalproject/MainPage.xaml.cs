using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
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
            Traffic_Load();
            getLiveboardStation();
            GetWeather();
      

        }


        private async void GetWeather() // Sandra: this is my code... don't you dare touche it...
        {     
             RootObject myWeather =
                await WeatherData.GetWeather(); // 
                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);// accessing my own pics (roayalty free)
            ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
            ResultTextBlock.Text = "windspeed: " + myWeather.wind.speed + " km/u" + " \n " + ((int)myWeather.main.temp).ToString() + " °C " +" \n " + myWeather.weather[0].description;
        }

        private void update_Click(object sender, RoutedEventArgs e) // this is Lennerts code
        {
            try
            {
                float distance = Convert.ToSingle(dist.Text);
                float velocity = Convert.ToSingle(veloc.Value);
                float sol = distance / velocity * 60;
                res.Text = String.Format("Het zal {0} minuten duren.", sol.ToString("0.0"));
            }
            catch (FormatException)
            {
                res.Text = String.Format("Vul aub in hoe lang uw fietsroute is.");
            }
        }

        /// Vincent's Code !! 
        /// string url = Link Json API irail -> new http client 
        /// Output in Textblock -> Liveboard results in minutes

        async void getLiveboardStation()
        {
            string url = "https://api.irail.be/liveboard/?station=Antwerpen-Centraal&format=json&arrdep=ARR";
            HttpClient client = new HttpClient();

            string LiveboardStation = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<RootObjectStation>(LiveboardStation);

            
            string vertraging =  data.arrivals.arrival[9].delay;

            if(vertraging.Contains("0") == true)
            {
                txtBlockLiveboardResult.Text ="Op dit moment zijn er geen vertragingen";

                //  Sandra: trying to give GUI a personal touch
                string image = "0";
                string smile = String.Format("ms-appx:///Assets/Smiley/{0}.png", image);// accessing my own pics (roayalty free)
                Smiley.Source = new BitmapImage(new Uri(smile, UriKind.Absolute));


            }
            else
            {
                txtBlockLiveboardResult.Text = "Opgelet ! Op dit moment zijn er" + vertraging + "minuten vertragingen";

                //  Sandra: trying to give GUI a personal touch
                string image = "1";
                string smile = String.Format("ms-appx:///Assets/Smiley/{0}.png", image);// accessing my own pics (roayalty free)
                Smiley.Source = new BitmapImage(new Uri(smile, UriKind.Absolute));

            }
        }


        //Sandra: THIS IS THE FEED I WANT TO SHOW THE TREND WITH AN ARROW UP OR DOWN OR LEVEL, 

        //Sandra: in order to that, I had to change Andres code a bit (which was perfectly working and a very cleverly written code) 


       public void Traffic_Load() //Andres' code, Traffic_Click is my button click
        {
            // Andres: Load the RSS file from the RSS URL
            XmlDocument.LoadFromUriAsync(new Uri("http://www.verkeerscentrum.be/rss/1-LOS.xml"))
                .Completed = XmlLoadedAsync;
        }

        public async Task UpdateTrafficInfo(string data, int trend) //Andres' code 
        {
            //SANDRA: old code from Andres (it worked perfectly) but caused troubles when trying to add my arrow codes: 
            // Andres code: await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => txtTraffic.Text = data);
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () => 
            {
               

                // Andres: txtTraffic is my textblock
                txtTraffic.Text = data;  
                
                // Sandra: Tadaaaaa, here is my arrow code

                //Sandra: show trend as arrows
                switch (trend)
                {
                    case 2:
                        FileTrend.Source = new BitmapImage(new Uri("ms-appx:///Assets/Arrow/arrow_up.png", UriKind.Absolute));
                    break;
                    case 1:
                        FileTrend.Source = new BitmapImage(new Uri("ms-appx:///Assets/Arrow/arrow_right.png", UriKind.Absolute));
                    break;
                    case 0:
                        FileTrend.Source = new BitmapImage(new Uri("ms-appx:///Assets/Arrow/arrow_down.png", UriKind.Absolute));
                    break;
                    default:
                        FileTrend.Visibility = 0; // Sandra... out of nothing comes nothing... 
                    break;
                }
                               });

        }

       public async void XmlLoadedAsync(IAsyncOperation<XmlDocument> asyncInfo, AsyncStatus asyncStatus) //still Andres' code
        {
            var rssXDoc = asyncInfo.GetResults();

            //Andres: Parse the Items in the RSS file
            var rssNodes = rssXDoc.SelectNodes("rss/channel/item");
            
            //Sandra: This is new... read out data
            for (int i = 0; i < rssNodes.Length; i++)
            {
                var rssSubNode1 = rssNodes[i].SelectSingleNode("title");
                System.Diagnostics.Debug.WriteLine(rssSubNode1.InnerText);
            }
            //Andres: select the right nodes, I only need "title"
            var rssSubNode = rssNodes.First().SelectSingleNode("title");
            var title = rssSubNode != null ? rssSubNode.InnerText : "";

            int trend = GetTrafficTrend(title);  // Sandra: Add trend number coming out of parsed text in order to be displayed next the text box in Update traffic method 


            // Andres :Return the string that contain the RSS items
            await UpdateTrafficInfo(title, trend);
            


         }

        //SANDRA:
        /// <summary>
        /// Find trends in text
        /// </summary>
        /// <param name="textToParse"></param>
        /// <returns>2 = stijgend; 1 = stabiel; 0 = dalend; -1 not defined</returns>
        private int GetTrafficTrend(string textToParse)
        {
            if (textToParse.ToLower().Contains("stijgend"))
                return 2;
            if (textToParse.ToLower().Contains("stabiel"))
                return 1;
            if (textToParse.ToLower().Contains("dalend"))
                return 0;
            return -1;
        }

    }
}

