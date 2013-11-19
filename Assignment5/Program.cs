using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment5
{
    class Program
    {
        static StreamReader cityReader;
        static StreamWriter logFile;
        static MapData MD;
        static RouteFinder RF;
        static void Main(string[] args)
        {
            cityReader = new StreamReader("CityPairs.txt");
            logFile = new StreamWriter("LogFile.txt");
            MD = new MapData(logFile);
            MD.LoadMapData();
            RF = new RouteFinder(MD, logFile);


            ReadEventFile();

        }

        //-----------------------------------------------------------------------------------

        static void ReadEventFile()
        {
            string lineReader;
            string logSpacer = "%".PadRight(30, '%');
            int StartNumber = -1;
            int EndNumber   = -1;
            string StartName;
            string EndName;
            bool StartIsUP = false;
            bool EndIsUP = false;

            while (cityReader.EndOfStream != true)
            {
                lineReader = cityReader.ReadLine();
                var MapDataSplit = lineReader.Split(',');

                StartName   = MapDataSplit[0];
                StartNumber = MD.GetCityNumber(StartName);
                StartIsUP   = MD.GetCityPeninsula(StartName);

                EndName     = MapDataSplit[1];
                EndNumber   = MD.GetCityNumber(EndName);
                EndIsUP     = MD.GetCityPeninsula(EndName);

                logFile.WriteLine(logSpacer);
                logFile.WriteLine();

                if(CitiesExist(StartName, EndName) == true)
                {
                    if ((StartIsUP == true && EndIsUP == false) || 
                        (StartIsUP == false && EndIsUP == true))
                    {
                        logFile.WriteLine("[***** 2 different peninsulas, so 2 partial routes *****]");
                        RF.FindShortestRoute(StartNumber, MD.GetCityNumber("theBridge"));
                        RF.FindShortestRoute(MD.GetCityNumber("theBridge"), EndNumber);
                    }
                    else
                    {
                        RF.FindShortestRoute(StartNumber, EndNumber);
                    }
                }

                logFile.WriteLine(logSpacer);
                logFile.WriteLine();
              
            }
        }


        //------------------------------------------------------------------------------------
        /// <summary>
        /// Checks to see if both city A & B exist within the michigan map data
        /// </summary>
        /// <param name="CityA"></param>
        /// <param name="CityB"></param>
        /// <returns>True if and only if both exist</returns>
        private static bool CitiesExist(string Start, string End)
        {
            int StartNum = MD.GetCityNumber(Start);
            int EndNum = MD.GetCityNumber(End);

            if (StartNum == -1)
            {
                logFile.WriteLine("START:  {0}", Start);
                logFile.WriteLine("ERROR:  start city not in Michigan Map Data");
                return false;
            }
            else
            {
                if (MD.GetCityPeninsula(Start))
                    logFile.WriteLine("START:  {0} ({1}) UP", Start, StartNum);
                else
                    logFile.WriteLine("START:  {0} ({1}) LP", Start, StartNum);
            }

            if (EndNum == -1)
            {
                logFile.WriteLine("DESTINATION:  {0}", End);
                logFile.WriteLine("ERROR:  destination city not in Michigan Map Data");
                return false;
            }
            else
            {
                if (MD.GetCityPeninsula(End))
                    logFile.WriteLine("DESTINATION:  {0} ({1}) UP", End, EndNum);
                else
                    logFile.WriteLine("DESTINATION:  {0} ({1}) LP", End, EndNum);
            }

            return true;
        }
    }
}
