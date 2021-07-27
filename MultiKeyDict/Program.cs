using System;
using System.Collections.Generic;

namespace MultiKeyDict
{
    class Program
    {
        // initialize an empty dict of string keys and list vals.
        public static Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
        static int memCount = 1; // variable to print counts in the list.
        
        // funciton to check if the keyExists
        public static bool keyExists(string key)
        {
            return result.ContainsKey(key);
        }

        // function to check if a memberExists
        public static bool memberExists(string key, string val)
        {
            bool memEx = false;
            if (keyExists(key))
            {
                if (result[key].Contains(val)) memEx = true;
            }
            return memEx;
        }

        // prints all members for the key
        // if pair is set print key: val pairs.
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
            while (true)
            {
                // Get the operation key val
                Console.WriteLine("Enter the operation, key and val: ");
                var inputParams = (Console.ReadLine()).Split(" "); // params[0]: operation, params[1]: key params[2]: val
               
                if (inputParams[0] != null)
                {
                    switch (inputParams[0].ToUpper())
                    {
                        // print all items in the dictionary (key: val)
                        case "ITEMS":
                            if (result.Count > 0) // check if dictionary is not empty
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

                        // print all memebers of the dictionary for all keys
                        case "ALLMEMBERS":
                            if (result.Count > 0) // check if dictionary is not empty
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
                        
                        // check if the given key exists in the dict
                        case "KEYEXISTS":
                            if (inputParams[1] != null) // check if key is provided in cmd line
                            {
                                Console.WriteLine(keyExists(inputParams[1]));
                            }
                            else
                            {
                                Console.WriteLine("Invalid");
                            }
                            break;

                        // return true if the given memeber is found
                        case "MEMBEREXISTS":
                            // check if all params are provided
                            if (inputParams.Length == 3 && inputParams[1] != null && inputParams[2] != null)
                            {
                                Console.WriteLine(memberExists(inputParams[1], inputParams[2]));
                            }
                            else
                            {
                                Console.WriteLine("Invalid");
                            }
                            break;

                        // clear the dict
                        case "CLEAR":
                            result.Clear();
                            Console.WriteLine("Cleared");
                            break;

                        // get memebers
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

                        // remove the given member for the given key
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

                        // remove the key from the dict
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

                        // print all keys in the dict.
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

                        // add key val pair to dict.
                        // if key exists, append val to the list
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

                        // if operation not defined.
                        default:
                            Console.WriteLine("Invalid");
                            break;
                    }
                }
            }
        }
    }
}