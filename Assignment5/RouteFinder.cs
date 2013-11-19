/*Author:George Karaszi
 * Date:11-18-13
 * 
 * Files Accessed (out) logFile.txt
 *                      -Handles output
 *                      
 * Description:Takes the mapdata class and uses its values to devise 
 *             the shortest path between two given cities based on 
 *             their weign between edges.
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Assignment5
{
    class RouteFinder
    {
        private bool[] _included;
        private int[] _distance;
        private int[] _path;
        private int N;
        private MapData _MD;
        private StreamWriter _logFile;
        private string TraceOfRoutes;

        public RouteFinder(MapData MD, StreamWriter logFile)
        {
            _logFile = logFile;
            _MD = MD;
            N   = MD.GetNumberOfNodes();
            _included = new bool[N];
            _distance = new int[N];
            _path = new int[N];
            
        }


        //-------------------------------------------------------------------------------
        /// <summary>
        /// Finds the shortest route from two points stored in the graph
        /// </summary>
        /// <param name="Start">Starting location</param>
        /// <param name="End">Ending location</param>
        public void FindShortestRoute(int Start, int End)
        {
            InitializeArrays(Start);
            SearchForPath(End);
            ReportAnswers(End);
        }

        //-------------------------------------------------------------------------------
        /// <summary>
        /// Initialize the arrays
        /// </summary>
        /// <param name="Start">Starting location subscript</param>
        private void InitializeArrays(int Start)
        {
            for(int i = 0; i < N; i++)
            {
                _included[i] = false;
                _distance[i] = _MD.GetRoadDistance(Start, i);

                if (_distance[i] != int.MaxValue && _distance[i] != 0)
                {
                    _path[i] = Start;
                }
                else
                    _path[i] = -1;

            }
            _included[Start] = true;

            TraceOfRoutes = _MD.GetCityName(Start);
        }

        //-------------------------------------------------------------------------------
        /// <summary>
        /// Search for the shortest path with the given graph variables
        /// </summary>
        /// <param name="End">Destination subscript</param>
        private void SearchForPath(int End)
        {
            int targetSub;
            int targetToDestDistance;
            int distanceCompare;

            while (_included[End] == false)
            {
                //Get the shortest distance that hasn't been 'Included'
                targetSub = GetShortestDistance(End);
                TraceOfRoutes += " " + _MD.GetCityName(targetSub);
                _included[targetSub] = true;
                

                //Loop through all structures and find shortest distances from that point
                for(int i =0; i < N; i++)
                {
                    if(_included[i] == false)
                    {
                        targetToDestDistance = _MD.GetRoadDistance(targetSub, i);

                        //Ignore zero(Self implied) and Infinity(No connection between vertex's)
                        if (targetToDestDistance != 0 && targetToDestDistance != int.MaxValue)
                       {
                            //Check if any of the combined points are less than what has been stored before
                           distanceCompare = _distance[targetSub] + targetToDestDistance;
                           if (distanceCompare < _distance[i])
                           {
                               _distance[i] = distanceCompare;
                               _path[i] = targetSub;
                           }
                       }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------
        /// <summary>
        /// Reports the shortest path between the two destinations
        /// </summary>
        private void ReportAnswers(int End)
        {
            _logFile.WriteLine();
            _logFile.WriteLine("TRACE OF TARGETS:  {0}", TraceOfRoutes);
            _logFile.WriteLine();
            _logFile.WriteLine("Total Distance:  {0} miles", _distance[End]);
            _logFile.WriteLine("Shortest Route:  {0}", TraversePath(End));
            
        }

        //-------------------------------------------------------------------------------
        /// <summary>
        /// Traverses the path index by using recursion to get paths in the correct order
        /// </summary>
        /// <param name="index">Starting index point</param>
        /// <returns>Path from point A to point B</returns>
        private string TraversePath(int index)
        {
            string path;

            if(_path[index] == -1)
            {
                return string.Format("{0}", _MD.GetCityName(index));
            }
            
            path = TraversePath(_path[index]);
            return path += string.Format(" > {0}", _MD.GetCityName(index));
        }

        //-------------------------------------------------------------------------------
        /// <summary>
        /// Returns the shortest path stored. That is not yet 'included'
        /// </summary>
        /// <returns>Subscript of shortest distance</returns>

        private int GetShortestDistance(int End)
        {
            int lowestValue = int.MaxValue;
            int lowestSubScript = -1;
            for(int i = 0; i < N; i++)
            {
                if (_included[i] == false)
                {
                    if (_distance[i] < lowestValue)
                    {
                        lowestValue = _distance[i];
                        lowestSubScript = i;
                    }
                }

            }

            if (lowestSubScript == -1)
                lowestSubScript = End;

            return lowestSubScript;
        }


    }
}
