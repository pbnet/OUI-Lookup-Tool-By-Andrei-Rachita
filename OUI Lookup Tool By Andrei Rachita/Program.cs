// OUI Lookup By Andrei Rachita - andrei(at)rachita(dot)net

using System;
using System.IO;
using System.Net;

namespace OuiLookup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OUI Lookup tool by Andrei Rachita");
            Console.WriteLine("\n");
            Console.WriteLine("Enter MAC address:");
            string macAddress = Console.ReadLine();

            // Remove any non-hex characters from the MAC address
            macAddress = System.Text.RegularExpressions.Regex.Replace(macAddress, "[^a-fA-F0-9]", "");

            if (macAddress.Length != 12)
            {
                Console.WriteLine("Invalid MAC address.");
                return;
            }

            // Get the first 6 characters of the MAC address (the OUI)
            string oui = macAddress.Substring(0, 6).ToUpper();

            try
            {
                // Download the IEEE OUI database
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("http://standards-oui.ieee.org/oui/oui.txt");
                StreamReader reader = new StreamReader(stream);

                // Search for the OUI in the database
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith(oui + "-") || line.StartsWith(oui + " "))
                    {
                        // Found the OUI, print the manufacturer name
                        Console.WriteLine(line.Substring(oui.Length + 1).Trim());
                        return;
                    }
                }

                // OUI not found
                Console.WriteLine("Manufacturer not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
