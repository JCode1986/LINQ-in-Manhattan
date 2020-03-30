using System;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using LinqInManhattan.Classes;

namespace LinqInManhattan
{
    class Program
    {
        public static string path = "../../../../data.json";
        public static JObject CSharpObj = JObject.Parse(File.ReadAllText(@path));
        static void Main(string[] args)
        {
            //OutputAllNeighborhoods(NeighborhoodList());
            //OutputAllNeighborhoodsThatHasNames(NeighborhoodList());
            RemoveDuplicateNeighborhoods(NeighborhoodList());
            SingleQueryFromPreviousMethods(NeighborhoodList());
        }

        public static List<Properties> NeighborhoodList()
        {
            List<Properties> neighborhoodList = new List<Properties>();
            foreach (var props in CSharpObj["features"])
            {
                Properties neighborhood = new Properties()
                {
                    Neighborhood = (string)props["properties"]["neighborhood"],
                };
                neighborhoodList.Add(neighborhood);
            }
            return neighborhoodList;
        }

    /// <summary>
    /// Output all of the neighborhoods in data list (Final Total: 147 neighborhoods)
    /// </summary>
    /// <returns></returns>
    public static void OutputAllNeighborhoods(List<Properties> neighborhoodList)
        {
            Console.WriteLine($"There are {neighborhoodList.Count} total neighborhoods which are:\n");

            foreach (var property in neighborhoodList)
            {
                Console.WriteLine(property.Neighborhood);
            }
        }

        /// <summary>
        /// Filter out all the neighborhoods that do not have any names (Final Total: 143)
        /// </summary>
        public static void OutputAllNeighborhoodsThatHasNames(List<Properties> neighborhoodList)
        {
          var queryWithNames =
                        from p in neighborhoodList
                        where p.Neighborhood != ""
                        select p.Neighborhood;

            Console.WriteLine($"There are {queryWithNames.Count()} total neighborhoods with names are:\n");

            foreach (var neighborhood in queryWithNames)
            {
                 Console.WriteLine(neighborhood);
            }
        }

        /// <summary>
        /// Remove the duplicates (Final Total: 39 neighborhoods)
        /// </summary>
        public static void RemoveDuplicateNeighborhoods(List<Properties> neighborhoodList)
        {
            var queryWithNames =
                from p in neighborhoodList
                where p.Neighborhood != ""
                select p.Neighborhood;

            var NoDupes = queryWithNames.GroupBy(n => n).Select(p => p.First());

            Console.WriteLine($"There are {NoDupes.Count()} total neighborhoods that are unqiqe which are:\n");

            foreach (var neighborhood in NoDupes)
            {
                Console.WriteLine(neighborhood);
            }
        }

        /// <summary>
        /// Filter out neighborhoods with no names and remove duplicates
        /// </summary>
        /// <param name="neighborhoodList"></param>
        public static void SingleQueryFromPreviousMethods(List<Properties> neighborhoodList)
        {
            
        }
    }
}

