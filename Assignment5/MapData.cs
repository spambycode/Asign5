/*Author: George Karaszi
 * Date:11-14-2013
 * 
 * Files Accessed: (in) MichiganMap.txt
 * 
 * Description:Class organizes all the data coming from the Michigan map file. 
 *             Stores all the distances between given cities in a two denominational 
 *             array. This class has methods designed to give easy access to the 
 *             data that can be parsed to find the quickest way from Point A to Point B.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment5
{
    class MapData
    {
        private int N;               //Number of nodes in map memory
        private const short MaxN = 100;
        private int[,] _roadDistance;  //2d Array of the map matrix
        private string[] _cityNameList; //LP name list associated with subscript x
        private string[] _upCityList;   //UP name list associated with subscript x.
        private StreamReader _mapDataReader; //File for reading in the map information
        private StreamWriter _logFile;


        public MapData(StreamWriter logFile)
        {
            _roadDistance = new int[MaxN, MaxN];
            _cityNameList = new string[1];
            _upCityList = new string[1];
            _mapDataReader = new StreamReader("MichiganRoads.txt");
            _logFile = logFile;

            for(int i = 0; i < MaxN; i++)
            {
                for(int j = 0; j < MaxN; j++)
                {
                    _roadDistance[i,j] = int.MaxValue;
                }

                _roadDistance[i, i] = 0;
            }
        }

        //-------------------------------------------------------------
        /// <summary>
        /// Loads the file and initialize all the variables from it.
        /// </summary>
        public void LoadMapData()
        {
            string lineRead;
            int cityANum, cityBNum;
            int distance;

            while(_mapDataReader.EndOfStream != true)
            {
                lineRead = _mapDataReader.ReadLine();

                if (lineRead.Contains('%'))
                    continue;

                var lineSplit = lineRead.Split(new char[]{' ', ',', '\n','\r'}, 
                                StringSplitOptions.RemoveEmptyEntries);

                if (lineSplit.Length <= 2)
                    continue;

                if(lineSplit[0].ToUpper().Contains("UP"))
                {
                    //lineS[0] = "UP**" and lineS[Len-1] = Bad Data
                    for(int i = 1; i < lineSplit.Length-1; i++)
                    {
                        StoreCityName(lineSplit[i], ref _upCityList);
                    }
                }
                else if (lineSplit[0].ToUpper().Contains("DIST"))
                {
                    lineSplit[0] = lineSplit[0].Replace("dist(", "");
                    StoreCityName(lineSplit[0], ref _cityNameList);
                    StoreCityName(lineSplit[1], ref _cityNameList);
                    distance = Convert.ToInt32(lineSplit[2].Replace(").", ""));

                    N = _cityNameList.Length - 1;

                    cityANum = GetCityNumber(lineSplit[0]);
                    cityBNum = GetCityNumber(lineSplit[1]);

                    if(cityANum != cityBNum)
                    {
                        _roadDistance[cityANum, cityBNum] = distance;
                        _roadDistance[cityBNum, cityANum] = distance;
                    }

                }
            }
        }

        //--------------------------------------------------------------
        /// <summary>
        /// Gets the city's number
        /// </summary>
        /// <param name="cityName">Name of city</param>
        /// <returns>Number of city</returns>
        public int GetCityNumber(string cityName)
        {
            for(int i = 0; i < N; i++)
            {
                if (_cityNameList[i].ToUpper().CompareTo(cityName.ToUpper()) == 0)
                    return i;
            }

            return -1;
        }

        //-------------------------------------------------------------
        /// <summary>
        /// Gets the location of where the city is located
        /// </summary>
        /// <param name="cityName">Name of city</param>
        /// <returns>True if the city is in the UP</returns>
        public bool GetCityPeninsula(string  cityName)
        {
            for (int i = 0; i < _upCityList.Length; i++ )
            {
                if (_upCityList[i].ToUpper().CompareTo(cityName.ToUpper()) == 0)
                    return true;

            }
            return false;
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Gets the number of the associated number stored in the city name index.
        /// </summary>
        /// <param name="cityNumber">Number of city</param>
        /// <returns>City Name</returns>
        public string GetCityName(int cityNumber)
        {
            return _cityNameList[cityNumber];
        }

        //--------------------------------------------------------------------
        /// <summary>
        /// Returns the distance of the given values
        /// </summary>
        /// <param name="Start">Where the starting point is</param>
        /// <param name="Dest">Where the ending point is</param>
        /// <returns>Distance between the two values</returns>
        public int GetRoadDistance(int Start, int Dest)
        {
            return _roadDistance[Start,Dest];
        }

        public int GetNumberOfNodes()
        {
            return N;
        }

        //------------------------Private--------------------------------------
        /// <summary>
        /// Stores the cities name in the array
        /// </summary>
        /// <param name="name">Name of city</param>
        private void StoreCityName(string name, ref string[] cityArray)
        {
            bool NameFound = false;

            if (cityArray != null)
            {
                foreach (string n in cityArray)
                {
                    if (n != null)
                    {
                        if (n.ToUpper().CompareTo(name.ToUpper()) == 0)
                        {
                            NameFound = true;
                            break;
                        }
                    }
                }
            }

            if (NameFound == false)
            {

                Array.Resize(ref cityArray, cityArray.Length+1);
                cityArray[cityArray.Length - 2] = name;
            }


        }


    }
}
