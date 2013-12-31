using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListSort_Enterprise_Edition
{
    class Program
    {
        /*
         * First of all, let's try to break down the task:
         * 1. Read the numbers
         * 2. Sort them
         * 3. Output the sorted numbers.
         * As "Divide and conquer" is very important strategy when working with software problems, lets tackle them one at a time
         */

        static void Main(string[] args)
        {
            WriteIntro();
            IDoubleArrayReader reader = DoubleArrayInputOutputFactory.CreateDoubleArrayReader(DoubleArrayReaderType.Console);
            IDoubleArrayProcessor processor = DoubleArrayProcessorFactory.CreateDoubleArrayProcessor(DoubleArrayProcessorType.Sorter);
            IDoubleArrayWriter writer = DoubleArrayInputOutputFactory.CreateDoubleArrayWriter(DoubleArrayWriterType.Console);
            var doubles = reader.GetDoubles();
            doubles = processor.ProcessDoubles(doubles);
            writer.WriteDoublesArray(doubles);
            WaitForExit();
        }

        static void WriteIntro()
        {
            Console.WriteLine("ListSort Enterprise Edition");
            Console.WriteLine();
        }

        static void WaitForExit()
        {
            Console.WriteLine();
            Console.Write("Press <ENTER> to exit");
            Console.ReadLine();
        }
    }
}
