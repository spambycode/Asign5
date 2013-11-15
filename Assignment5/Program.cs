using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    class Program
    {
        static void Main(string[] args)
        {
            MapData MD = new MapData();
            MD.LoadMapData();

            RouteFinder RF = new RouteFinder(MD);
            int CityA = MD.GetCityNumber(MD.GetCityName(7));
            int CityB = MD.GetCityNumber(MD.GetCityName(4));

            RF.FindShortestRoute(CityA, CityB);


        }
    }
}
