using System;
using System.Collections.Generic;

namespace isolutions.GrillMaster.Entities
{
    public class Griller
    {
        private static int _numRound;
        private TimeSpan TimeRound { get; set; }
        private readonly Queue<List<GrillItem>> _grillMenus = new Queue<List<GrillItem>>();

        public Griller()
        {
        }

        public void StartRound(TimeSpan time, List<List<GrillItem>> foodElements)
        {
            this.TimeRound = time;
            _numRound++;
            foreach (var round in foodElements)
            {
                _grillMenus.Enqueue(round);
            }

            this.StartGrilling();
        }

        private void StartGrilling()
        {
            while (_grillMenus.Count > 0)
            {
                Console.WriteLine("Starting round : " + _numRound);
                Console.WriteLine("Items in the grill:");
                List<GrillItem> grillItems = _grillMenus.Dequeue();
                foreach (var item in grillItems)
                {
                    Console.WriteLine(item.Name);
                }

                //System.Threading.Thread.Sleep(TimeRound);
                _numRound++;
            }

            _numRound = 0;
        }

        public int GrillArea1 { get; } = 20 * 30;

        public static int NumRound
        {
            get => _numRound;
            set => _numRound = value;
        }
    }
}