using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Roadhackers_finalproject
{
    public class WeatherData // Sandra: hands off this is my class 
    {
        private const string url = "http://api.openweathermap.org/data/2.5/weather?q=antwerp,be&units=metric&apikey=23c1237f8e3a4a8eca581f9c2c647d85"; // decided to put in as a constant, we will always use the same url... and if it needs changing, will be easir.

        public async static Task<RootObject> GetWeather() // this is an async method, the task of this method is to return a RootObject at the end of the method, this is why I used task<>
        {

            var http = new HttpClient(); // to connect to the website
            var response = await http.GetAsync(url); // a cup of tea and wait 
            var result = await response.Content.ReadAsStringAsync(); // where there is an async there is always a wait. time for a cuppa
            var serializer = new DataContractJsonSerializer(typeof(RootObject));               // serializer... take the json string and transform into something usefull
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));// a memory stream, it allows to collect data which comes in at different speeds from the stream, Encoding, tells which type encoding is used to encode to Json, 
            var data = (RootObject)serializer.ReadObject(ms);  // this is a variable which collects the data, here we use our serializer and we cast the data to our RootObject

            return data;
        }


    }
    [DataContract] // it needs the namespace System.Runtime.Serialization. This attribute tells the serializer treat this as a class 
    public class Coord
    {
        [DataMember] // this tells the serializer treat this like a property.. both attributes help the serializer to pair the data received with the classes and properties
        public double lon { get; set; }
        [DataMember]
        public double lat { get; set; }
    }

    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }

    [DataContract]
    public class Main
    {
        [DataMember]
        public double temp { get; set; }
        [DataMember]
        public int pressure { get; set; }
        [DataMember]
        public int humidity { get; set; }
        [DataMember]
        public int temp_min { get; set; }
        [DataMember]
        public int temp_max { get; set; }
    }

    [DataContract]
    public class Wind
    {
        [DataMember]
        public double speed { get; set; }
        [DataMember]
        public int deg { get; set; }
    }

    [DataContract]
    public class Clouds
    {
        public int all { get; set; }
    }

    [DataContract]
    public class Sys
    {
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public double message { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public int sunrise { get; set; }
        [DataMember]
        public int sunset { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public string @base { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public int visibility { get; set; }
        [DataMember]
        public Wind wind { get; set; }
        [DataMember]
        public Clouds clouds { get; set; }
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Sys sys { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int cod { get; set; }
    }


}
