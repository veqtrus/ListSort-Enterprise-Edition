using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ListSort_Enterprise_Edition
{
    /*
     * 1. Reading
     * Another important issue in software is versatility. Since it's not specified how the user will input the 
     * numbers, that can happen via the console, via a file, via a web service, etc. Maybe even some method 
     * that we can't think of at the moment. So, it's important that our solution will be able to accommodate 
     * various types of input. The easiest way to achieve that will be to extract the important part to an 
     * interface, let's say
     */

    public interface IDoubleArrayReader
    {
        IEnumerable<double> GetDoubles();

        DoubleArrayReaderType Type { get; }
    }

    public enum DoubleArrayReaderType
    {
        Console,
        File,
        Database,
        Internet,
        Cloud,
        MockService
    }

    public class MockServiceDoubleArrayReader : IDoubleArrayReader // It's also important to make the software testable from the ground up, so an implementation of the interface will be
    {
        IEnumerable<double> IDoubleArrayReader.GetDoubles()
        {
            Random r = new Random();
            for (int i = 0; i <= 10; i++)
            {
                yield return r.NextDouble();
            }
        }

        DoubleArrayReaderType IDoubleArrayReader.Type
        {
            get
            {
                return DoubleArrayReaderType.MockService;
            }
        }
    }

    public class ConsoleDoubleArrayReader : IDoubleArrayReader
    {
        IEnumerable<double> IDoubleArrayReader.GetDoubles()
        {
            Console.WriteLine("Enter a list of doubles, one per line. Leave blank to quit input.");
            double result = 0.0;
            string input = "";
            do
            {
                input = Console.ReadLine();
                if (input != "")
                {
                    if (!double.TryParse(input, out result))
                    {
                        throw new FormatException("Invalid input");
                    }
                    yield return result;
                }
            } while (input != "");
        }

        DoubleArrayReaderType IDoubleArrayReader.Type
        {
            get
            {
                return DoubleArrayReaderType.Console;
            }
        }
    }
}
