using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Triangle;

namespace TriangleTests
{
    [TestClass]
    public class TriangleTests
    {
        [TestMethod]
        public void TestingWithReadingFromFile()
        {
            string line; 
            try
            {
                StreamReader sr = new StreamReader("../../tests.txt");
                StreamWriter sw = new StreamWriter("../../tests_results.txt");
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Replace(Environment.NewLine, " ");
                    string[] args = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Triangle.Triangle.Main(args);
                    line = sr.ReadLine();
                    i++;
                    Triangle.Triangle.WriteTestsResultsInFile(line, Triangle.Triangle.resultString, i, sw);
                }
                sr.Close();
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
