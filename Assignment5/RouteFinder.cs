using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    class RouteFinder
    {
        private bool[] _included;
        private int[] _distance;
        private int[] _path;
        private int N;
        private MapData _MD;

        public RouteFinder(MapData MD)
        {
            _MD = MD;
            N   = MD.GetNumberOfNodes();
            _included = new bool[N];
            _distance = new int[N];
            _path = new int[N];
        }

        //--------------------------------------------------

        public void FindShortestRoute(int Start, int End)
        {
            InitializeArrays(Start);
            SearchForPath(End);
            ReportAnswers();
        }

        //---------------------------------------------------
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
                    _path[i] = Start;
                else
                    _path[i] = -1;

            }
            _included[Start] = true;
        }

        //--------------------------------------------------------------
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
                targetSub = GetShortestDistance();
                _included[targetSub] = true;

                for(int i =0; i < N-1; i++) //****Consider N-1
                {
                    if(_included[i] == false)
                    {
                        targetToDestDistance = _MD.GetRoadDistance(targetSub, i);

                        if (targetToDestDistance != 0 && targetToDestDistance != int.MaxValue)
                       {
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

        //----------------------------------------------------------
        /// <summary>
        /// Reports the shortest path between the two destinations
        /// </summary>
        private void ReportAnswers()
        {
            string cityName;

            foreach(int n in _path)
            {
                if (n != -1)
                    Console.WriteLine(n);
            }
        }

        //-------------------------------------------------------------
        /// <summary>
        /// Returns the shortest path stored. That is not yet 'included'
        /// </summary>
        /// <returns>Subscript of shortest distance</returns>

        private int GetShortestDistance()
        {
            int lowestValue = _distance[0];
            int lowestSubScript = -1;
            for(int i = 0; i < N; i++)
            {
                if (_included[i] == false)
                {
                    if (_distance[i] <= lowestValue)
                    {
                        lowestValue = _distance[i];
                        lowestSubScript = i;
                    }
                }

            }

            return lowestSubScript;
        }


    }
}
