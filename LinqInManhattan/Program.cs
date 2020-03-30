using System;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace LinqInManhattan
{
    class Program
    {
        public static string path = "../../../../../data.json";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            JObject CSharpObj = JObject.Parse(File.ReadAllText(@"../../../../data.json"));
            Console.WriteLine(CSharpObj);
        }
        
    }
}

