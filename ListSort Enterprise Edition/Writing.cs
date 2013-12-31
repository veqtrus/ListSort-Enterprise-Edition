using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ListSort_Enterprise_Edition
{
    /*
     * 3. Writing the output
     * Nothing much to say here, as this is a process that mirror the input.
     */

    public interface IDoubleArrayWriter
    {
        void WriteDoublesArray(IEnumerable<double> doubles);

        DoubleArrayWriterType Type { get; }
    }

    public enum DoubleArrayWriterType
    {
        Console,
        File,
        Internet,
        Cloud,
        MockService,
        Database
    }

    public class ConsoleDoubleArrayWriter : IDoubleArrayWriter
    {
        void IDoubleArrayWriter.WriteDoublesArray(IEnumerable<double> doubles)
        {
            Console.WriteLine("Sorted:");
            foreach (double @double in doubles)
            {
                Console.WriteLine(@double);
            }
        }

        DoubleArrayWriterType IDoubleArrayWriter.Type
        {
            get
            {
                return DoubleArrayWriterType.Console;
            }
        }
    }


    public static class DoubleArrayInputOutputFactory
    {
        private static Dictionary<DoubleArrayReaderType, IDoubleArrayReader> readers;
        private static Dictionary<DoubleArrayWriterType, IDoubleArrayWriter> writers;

        static DoubleArrayInputOutputFactory()
        {
            readers = new Dictionary<DoubleArrayReaderType, IDoubleArrayReader>();
            writers = new Dictionary<DoubleArrayWriterType, IDoubleArrayWriter>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                try
                {
                    var instance = Activator.CreateInstance(type);
                    if (instance is IDoubleArrayReader)
                    {
                        readers.Add((instance as IDoubleArrayReader).Type, (instance as IDoubleArrayReader));
                    }
                }
                catch
                {
                    continue;
                }
            }

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                try
                {
                    var instance = Activator.CreateInstance(type);
                    if (instance is IDoubleArrayWriter)
                    {
                        writers.Add((instance as IDoubleArrayWriter).Type, (instance as IDoubleArrayWriter));
                    }
                }
                catch
                {
                    continue;
                }
            }

        }

        public static IDoubleArrayReader CreateDoubleArrayReader(DoubleArrayReaderType type)
        {
            return readers[type];
        }

        public static IDoubleArrayWriter CreateDoubleArrayWriter(DoubleArrayWriterType type)
        {
            return writers[type];
        }

    }
}
