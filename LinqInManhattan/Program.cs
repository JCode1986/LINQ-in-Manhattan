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
        static void Main(string[] args)
        {
            Start();
        }

        /// <summary>
        /// Starts the app, and gives options to user
        /// </summary>
        public static void Start()
        {
            Console.WriteLine("\nManhattan Neighborhood List:\n");
            Console.WriteLine("'1' - Output all neighborhoods");
            Console.WriteLine("'2' - Output all neighborhoods with names");
            Console.WriteLine("'3' - Output all unique neighborhoods");
            Console.WriteLine("'4' - Output previous methods in single query");
            Console.WriteLine("'5' - Rewrote option '2' using the opposing method");
            Console.WriteLine("'x' - Exit");
            Console.Write("\nWhat would you like to do?: ");
            string input = Console.ReadLine();
            Console.Clear();

            switch (input)
            {
                case "1":
                    OutputAllNeighborhoods(NeighborhoodList());
                    break;
                case "2":
                    OutputAllNeighborhoodsThatHasNames(NeighborhoodList());
                    break;
                case "3":
                    RemoveDuplicateNeighborhoods(NeighborhoodList());
                    break;
                case "4":
                    SingleQueryFromPreviousMethods(NeighborhoodList());
                    break;
                case "5":
                    OutPutAllNeighborhoodWithNamesOpposingMethod(NeighborhoodList());
                    break;
                case "x":
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Start();
                    break;
            }
            Start();
        }

        /// <summary>
        /// Returns string of contents in list
        /// </summary>
        /// <param name="array"></param>
        public static void PrintList(IEnumerable<string> array)
        {
            string result = "";
            foreach (var property in array)
            {
                result += $"{property}, ";
            }
            Console.WriteLine(result);
        }

        /// <summary>
        /// stores all the neighborhoods from json into a list
        /// </summary>
        /// <returns></returns>
        public static List<Properties> NeighborhoodList()
        {
            string path = "../../../../data.json";

            JObject CSharpObj = JObject.Parse(File.ReadAllText(@path));

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

        //Question 1: Output all of the neighborhoods in this data list (Final Total: 147 neighborhoods)

        /// <summary>
        /// Returns all the neighborhoods
        /// </summary>
        /// <returns></returns>
        public static void OutputAllNeighborhoods(List<Properties> neighborhoodList)
        {
            Console.WriteLine($"\nThere are {neighborhoodList.Count} total neighborhoods which are:\n");

            string result = "";

            foreach (var property in neighborhoodList)
            {
                result += $"{property.Neighborhood}, ";
            }

            Console.WriteLine(result);

        }

        //Filter out all the neighborhoods that do not have any names (Final Total: 143)

        /// <summary>
        /// Returns neighborhoods with names
        /// </summary>
        public static void OutputAllNeighborhoodsThatHasNames(List<Properties> neighborhoodList)
        {
          var queryWithNames =
                        from p in neighborhoodList
                        where p.Neighborhood != ""
                        select p.Neighborhood;

            Console.WriteLine($"\nThere are {queryWithNames.Count()} total neighborhoods with names which are:\n");

            PrintList(queryWithNames);
        }

        //Question 3: Remove the duplicates (Final Total: 39 neighborhoods)

        /// <summary>
        /// Removes all duplicate neighborhoods
        /// </summary>
        public static void RemoveDuplicateNeighborhoods(List<Properties> neighborhoodList)
        {
            var queryWithNames =
                from p in neighborhoodList
                where p.Neighborhood != ""
                select p.Neighborhood;

            var NoDupes = queryWithNames.GroupBy(n => n).Select(p => p.First());

            Console.WriteLine($"\nThere are {NoDupes.Count()} total neighborhoods that are unique which are:\n");

            PrintList(NoDupes);
        }

        //Question 4: Rewrite the queries from above and consolidate all into one single query

        /// <summary>
        /// Filter out neighborhoods with no names and remove duplicates
        /// </summary>
        /// <param name="neighborhoodList"></param>
        public static void SingleQueryFromPreviousMethods(List<Properties> neighborhoodList)
        {
            var chainingQuery = neighborhoodList
                    .Where(neighbor => neighbor.Neighborhood != "")
                    .Select(neigbor => neigbor.Neighborhood)
                    .Distinct();

            Console.WriteLine($"\nThere are {chainingQuery.Count()} total neighborhoods that are unique which are:\n");

            PrintList(chainingQuery);
        }

        //Question 5: Rewrite at least one of these questions only using the opposing method (example: Use LINQ Query statements instead of LINQ method calls and vice versa.)
       
        /// <summary>
        /// Rewrite at least one of these questions only using the opposing method
        /// </summary>
        /// <param name="neighborhoodList"></param>
        public static void OutPutAllNeighborhoodWithNamesOpposingMethod(List<Properties> neighborhoodList)
        {
            var queryWithNames = neighborhoodList.Where(p => p.Neighborhood != "").Select(p => p.Neighborhood);

            Console.WriteLine($"\nThere are {queryWithNames.Count()} total neighborhoods that are unique which are:\n");

            PrintList(queryWithNames);
        }
    }
}

