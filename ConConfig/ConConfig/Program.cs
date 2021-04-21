using System;
using System.IO;
using System.Xml;

namespace ConConfig
{
    class Program
    {
        static string getAttribute(XmlNode node, string attrName)
        {
            if (node.Attributes.GetNamedItem(attrName) == null)
            {
                Console.WriteLine("Warning: " + attrName + " is not set");
                return "";
            } 
            else return node.Attributes.GetNamedItem(attrName).Value;
        }

        static void Main(string[] args)
        {
            string stringPath = ".\\config.xml";

            if (args.Length == 1)
            {
                if (args[0] == "-h" || args[0] == "--help") 
                    Console.WriteLine("Pass the config file path as a first argument, otherwise the default config file is " + stringPath);
                else stringPath = args[0];
            } 
            else if (args.Length > 1)
            {
                Console.WriteLine("Wrong usage, use --help or -h");
            }

            XmlDocument xml = new XmlDocument();

            try
            {
                xml.Load(stringPath);
            }
            catch (Exception e) {
                Console.WriteLine("Error: {0}", e.Message);
                return;
            }

            XmlNode nodes = xml.SelectSingleNode("config");

            if (nodes == null)
            {
                Console.WriteLine("Wrong usage, check your config file");
                return;
            }

            foreach (XmlNode node in nodes.ChildNodes)
            {
                string source_path = getAttribute(node, "source_path");
                string destination_path = getAttribute(node, "destination_path");
                string file_name = getAttribute(node, "file_name");

                try
                {
                    File.Copy(source_path + "\\" + file_name, destination_path + "\\" + file_name);
                    Console.WriteLine("Copying the " + file_name + " file is completed");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                }
            }
        }
    }
}
