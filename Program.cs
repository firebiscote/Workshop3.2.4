using System;
using System.Collections.Generic;
using System.Linq;

namespace Prosit3._2._4
{
    public class Prosit
    {
        static void Main(string[] args)
        {
            //string order = string.Join(" ", args);
            while (true)
            {
                Console.Write("order : ");
                string order = Console.ReadLine();
                Console.WriteLine();
                Factory.Instance.Assemble(order);
                Console.WriteLine();
            }
        }

        public class Factory
        {
            public static Factory Instance = new Factory();
            public AssemblyLine Line { get; }

            private Factory() 
            {
                Line = new AssemblyLine();
            }

            public void Assemble(string order)
            {
                using (Process process = new Process(order))
                {
                    Line.Process = process;
                }
                Line.Execute();
            }
        }

        public class AssemblyLine
        {
            public Process Process { get; set; }

            public AssemblyLine() { }

            public void Execute()
            {
                Stack<Unit> toExecute = Process.Units;
                Process.Dispose();
                while (toExecute.Count > 0)
                {
                    using Unit unit = toExecute.Pop();
                    unit.Work();
                }
            }
        }

        public class Process : IDisposable
        {
            public Stack<Unit> Units { get; }

            public Process(string order)
            {
                Units = Parse(order);
            }

            private Stack<Unit> Parse(string order)
            {
                Stack<Unit> units = new Stack<Unit>();
                List<string> unitNames = order.Split(" -> ").ToList();
                int totalUnitNames = unitNames.Count;
                for (int i = 0; i < totalUnitNames; i++)
                {
                    units.Push(new Unit(unitNames[^1]));
                    unitNames.RemoveAt(unitNames.Count - 1);
                }
                return units;
            }

            public void Dispose() 
            {
                foreach (Unit unit in Units)
                    unit.Dispose();
            }
        }

        public class Unit : IDisposable
        {
            public string Name;

            public Unit(string name)
            {
                if (int.Parse(name[1..]) > 359 || int.Parse(name[1..]) < 1)
                    throw new Exception();
                Name = name;
            }

            public void Work()
            {
                Console.WriteLine(Name + " work!");
            }

            public void Dispose() 
            {
                Name = null;
            }
        }

    }
}
