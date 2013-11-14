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

        public RouteFinder(MapData MD, int numberOfNodes)
        {
            _MD = MD;
            N = numberOfNodes;
            _included = new bool[N];
            _distance = new int[N];
            _path = new int[N];
        }

        //--------------------------------------------------

        public void FindShortestRoute(int Start, int End)
        {
            int totalDistance = 0;

            InitializeArrays(Start);
            totalDistance = SearchForPath(Start, End);
            ReportAnswers(totalDistance);

        }

        //---------------------------------------------------
        /// <summary>
        /// Initialize the arrays
        /// </summary>
        /// <param name="Start">Location of where the user wants to start</param>
        private void InitializeArrays(int Start)
        {
            int distance;
            for(int i = 0; i < N; i++)
            {
                _included[i] = false;
                _distance[i] = distance = _MD.GetRoadDistance(Start, i);

                if (distance != int.MaxValue)
                    _path[i] = Start;
                else
                    _path[i] = -1;

            }
            _included[Start] = true;
        }

        //---------------------------------------------------------
        private int SearchForPath(int Start, int End)
        {
            while (_included[End] == false)
            {
                for(int i =0; i < N; i++) //****Consider N-1
                {
                    if(_MD.GetRoadDistance())
                }
            }

            return 0;
        }

        //----------------------------------------------------------
        /// <summary>
        /// Reports the shortest path between the two destinations
        /// </summary>
        /// <param name="totalDistance"></param>
        private void ReportAnswers(int totalDistance)
        {

        }


    }
}
