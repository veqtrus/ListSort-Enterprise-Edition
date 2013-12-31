using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ListSort_Enterprise_Edition
{
    /*
     * 2. Processing (sorting)
     * Now we need to process, i.e. sort the numbers we have acquired. Note that the steps are completely 
     * independent of each other, so to the sorting subsystem, it does not matter how the numbers were 
     * inputed. Additionally, the sorting behavior is also something that is subject to change, e.g. we might need 
     * to input a more efficient sorting algorithm in place. So, naturally, we'll extract the requested processing 
     * behaviour in an interface:
     */

    public interface IDoubleArrayProcessor
    {
        IEnumerable<double> ProcessDoubles(IEnumerable<double> input);

        DoubleArrayProcessorType Type { get; }
    }

    public enum DoubleArrayProcessorType
    {
        Sorter,
        Doubler,
        Tripler,
        Quadrupler,
        Squarer
    }

    public class SorterDoubleArrayProcessor : IDoubleArrayProcessor // And the sorting behaviour will just implement the interface
    {
        IEnumerable<double> IDoubleArrayProcessor.ProcessDoubles(IEnumerable<double> input)
        {
            var output = input.ToArray();
            Array.Sort(output);
            return output;
        }

        DoubleArrayProcessorType IDoubleArrayProcessor.Type
        {
            get
            {
                return DoubleArrayProcessorType.Sorter;
            }
        }
    }

    public static class DoubleArrayProcessorFactory // Of course, we will need a factory to load and manage the processing instances.
    {
        private static Dictionary<DoubleArrayProcessorType, IDoubleArrayProcessor> processors;

        static DoubleArrayProcessorFactory()
        {
            processors = new Dictionary<DoubleArrayProcessorType, IDoubleArrayProcessor>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                try
                {
                    var instance = Activator.CreateInstance(type);
                    if (instance is IDoubleArrayProcessor)
                    {
                        processors.Add((instance as IDoubleArrayProcessor).Type, (instance as IDoubleArrayProcessor));
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        public static IDoubleArrayProcessor CreateDoubleArrayProcessor(DoubleArrayProcessorType type)
        {
            return processors[type];
        }

    }
}
