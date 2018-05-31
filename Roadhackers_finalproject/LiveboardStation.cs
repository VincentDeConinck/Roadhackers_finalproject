using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roadhackers_finalproject

//// First thing to do: import Json link by Edit -> Paste Special -> choose as Json classes. 
///  C# generate automatically public classes 
///  Rename to LiveboardStation 

{

    public class Stationinfo
    {
        public string id { get; set; }
        public string locationX { get; set; }
        public string locationY { get; set; }
        public string name { get; set; }
        public string standardname { get; set; }
    }

    public class Stationinfo2
    {
        public string id { get; set; }
        public string locationX { get; set; }
        public string locationY { get; set; }
        public string standardname { get; set; }
        public string name { get; set; }
    }

    public class Vehicleinfo
    {
        public string name { get; set; }
    }

    public class Platforminfo
    {
        public string name { get; set; }
        public string normal { get; set; }
    }

    public class Occupancy
    {
        public string name { get; set; }
    }

    public class Arrival
    {
        public string id { get; set; }
        public string delay { get; set; }
        public string station { get; set; }
        public Stationinfo2 stationinfo { get; set; }
        public string time { get; set; }
        public string vehicle { get; set; }
        public Vehicleinfo vehicleinfo { get; set; }
        public string platform { get; set; }
        public Platforminfo platforminfo { get; set; }
        public string canceled { get; set; }
        public string left { get; set; }
        public string departureConnection { get; set; }
        public Occupancy occupancy { get; set; }
    }

    public class Arrivals
    {
        public string number { get; set; }
        public List<Arrival> arrival { get; set; }
    }

    public class RootObjectStation
    {
        public string version { get; set; }
        public string timestamp { get; set; }
        public string station { get; set; }
        public Stationinfo stationinfo { get; set; }
        public Arrivals arrivals { get; set; }
    }
}
