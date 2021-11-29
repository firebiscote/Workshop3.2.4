using System;

namespace Prosit3._2._4
{
    public class Prosit
    {
        static void Main(string[] args)
        {
            Unit[] units = new Unit[360];
            for (int i = 1; i < 360; i++)
                units[i] = new Unit(i.ToString());
            ProductionLine line = new ProductionLine(new Unit[]
            {
                units[128],
                units[2],
                units[128],
                units[128],
                units[67],
                units[359]
            });

            line.ExecuteLine();

        }

        public class Unit : IDisposable
        {
            public string Name;

            public Unit(string name)
            {
                Name = name;
            }

            public void Process()
            {
                Console.WriteLine("I'm " + Name);
            }

            public void Dispose() { }
        }

        public class ProductionLine
        {
            public Unit[] Line;

            public ProductionLine(Unit[] line)
            {
                Line = line;
            }

            public void ExecuteLine()
            {
                for (int i = 0; i < Line.Length; i++)
                {
                    Line[i].Process();
                    Line[i].Dispose();
                }
            }
        }
    }
}
