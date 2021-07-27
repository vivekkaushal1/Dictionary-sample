using System;
using System.Collections.Generic;

namespace MultiKeyDict
{
    class Program
    {
        public static Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
        static int memCount = 1;
        
        public static bool keyExists(string key)
        {
            return result.ContainsKey(key);
        }

        public static bool memberExists(string key, string val)
        {
            bool memEx = false;
            if (keyExists(key))
            {
                if (result[key].Contains(val)) memEx = true;
            }
            return memEx;
        }

        public static void printMembers(string key, bool reset, bool pair = false)
        {
            if (reset)
            {
                memCount = 1;
            }
            foreach (string val in result[key])
            {
                if (pair)
                {
                    Console.WriteLine($"{memCount}) {key}: {val}");
                    memCount++;
                }
                else
                {
                    Console.WriteLine($"{memCount}) {val}");
                    memCount++;
                }
            }
        }

        public static void Main(string[] args)
        {
            //Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            
            while (true)
            {
                // Get the key: val pair
                Console.WriteLine("Enter the operation, key and val: ");
                var inputParams = (Console.ReadLine()).Split(" ");

                //Console.WriteLine($"Input: {String.Join(",", inputParams)}");

                string reqOperation = inputParams[0];
               
                if (reqOperation != null)
                {
                    switch (reqOperation.ToUpper())
                    {

                        case "ITEMS":
                            if (result.Count > 0)
                            {
                                bool resetNeeded = true;
                                foreach (string key in result.Keys)
                                {
                                    printMembers(key, resetNeeded, true);
                                    if (resetNeeded) resetNeeded = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("{}");
                            }
                            break;


                        case "ALLMEMBERS":
                            if (result.Count > 0)
                            {
                                bool resetNeeded = true;
                                foreach (string key in result.Keys)
                                {
                                    printMembers(key, resetNeeded);
                                    if (resetNeeded) resetNeeded = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("{}");
                            }
                            break;

                        case "KEYEXISTS":
                            if (inputParams[1] != null)
                            {
                                Console.WriteLine(keyExists(inputParams[1]));
                            }
                            else
                            {
                                Console.WriteLine("Invalid");
                            }
                            break;

                        case "MEMBEREXISTS":
                            if (inputParams.Length == 3 && inputParams[1] != null && inputParams[2] != null)
                            {
                                Console.WriteLine(memberExists(inputParams[1], inputParams[2]));
                            }
                            else
                            {
                                Console.WriteLine("Invalid");
                            }
                            break;

                        case "CLEAR":
                            result.Clear();
                            Console.WriteLine("Cleared");
                            break;

                        case "MEMBERS":
                            if (inputParams[1] != null && keyExists(inputParams[1]))
                            {
                                printMembers(inputParams[1], true);
                            }
                            else
                            {
                                Console.WriteLine("Key not found.");
                            }
                            break;

                        case "REMOVE":
                            if (inputParams[1] != null && inputParams[2] != null)
                            {
                                if (keyExists(inputParams[1]))
                                {
                                    if (memberExists(inputParams[1], inputParams[2]))
                                    {
                                        result[inputParams[1]].Remove(inputParams[2]);
                                        Console.WriteLine($"Removed {inputParams[2]}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Member {inputParams[2]} does not exists.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Key does not exists.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid params.");
                            }
                            break;

                        case "REMOVEALL":
                            if (inputParams[1] != null)
                            {
                                if (keyExists(inputParams[1]))
                                {
                                    result.Remove(inputParams[1]);
                                    Console.WriteLine("Removed");
                                }
                                else
                                {
                                    Console.WriteLine("Error, key does not exists.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid params.");
                            }
                                
                            break;

                        case "KEYS":
                            if (result.Count > 0)
                            {
                                int count = 1;
                                foreach (string key in result.Keys)
                                {
                                    Console.WriteLine($"{count}) {key}");
                                    count++;
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine("Dictionary Empty");
                            }
                            break;


                        case "ADD":
                            // Add to dict
                            if (inputParams[1] != null && inputParams[2] != null)
                            {
                                if (keyExists(inputParams[1]))
                                {
                                    if (memberExists(inputParams[1], inputParams[2]))
                                    {
                                        Console.WriteLine($"Member {inputParams[2]} already exists");
                                    }
                                    else
                                    {
                                        result[inputParams[1]].Add(inputParams[2]);
                                        Console.WriteLine("Added");
                                    }
                                }
                                else
                                {
                                    result.Add(inputParams[1], new List<string>() { inputParams[2] });
                                    Console.WriteLine("Added");
                                }
                            }
                            else
                            {
                                Console.Write("No Key/Val inserted");
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid");
                            break;
                    }
                }
            }
        }
    }
}