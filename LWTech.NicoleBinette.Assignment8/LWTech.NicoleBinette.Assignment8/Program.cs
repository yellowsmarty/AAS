using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Data;

/* Airplane Histogram */

namespace LWTech.NicoleBinette.Assignment8
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient webClient = new WebClient();
            try
            {
                Stream stream = webClient.OpenRead("https://public-api.adsbexchange.com/VirtualRadar/AircraftList.json?fTypQN=");
                var airplaneTypes = new Dictionary<string,int>();
                var airplanes = new List<string>();
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();
                        //Console.WriteLine(line);
                        string[] words = line.Split("\"");
                        for (int i = 0; i < words.Length; i++)
                        {
                            if (words[i] == "Type")
                            {
                                string type = words[i];
                                string colon = words[++i];
                                string data = words[++i];
                                if (data[0] == 'B' && data[1] == '7')
                                {
                                    char[] b = data.ToCharArray();
                                    b[b.Length - 1] = '7';
                                    data = new string(b);
                                    if (airplaneTypes.ContainsKey(data))
                                    {
                                        airplaneTypes[data]++;
                                    }
                                    else
                                    {
                                        airplaneTypes.Add(data, 1);
                                    }
                                    airplanes.Add(data);
                                }
                                if (data[0] == 'A' && data[1] == '3')
                                {
                                    char[] a = data.ToCharArray();
                                    a[a.Length - 1] = '0';
                                    data = new string(a);
                                    if (airplaneTypes.ContainsKey(data))
                                    {
                                        airplaneTypes[data]++;
                                    }
                                    else
                                    {
                                        airplaneTypes.Add(data, 1);
                                    }
                                    airplanes.Add(data);

                                }
                                //Console.WriteLine(type + colon + " " + data);

                            }
                        }
                    }
                    airplanes.Sort();
                    Histogram airplaneHistogram = new Histogram(airplanes);
                    Console.WriteLine(airplaneHistogram);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("There was a network error : {0}", ex.Message);
                return;
            }
        }
    }

    class Histogram
    {
        private int width;
        private int maxBarWidth;
        private int maxLabelWidth;
        private int minValue;
        private List<KeyValuePair<string, int>> bars;

        public Histogram(List<string> data, int width = 80, int maxLabelWidth = 10, int minValue = 0)
        {
            this.width = width;
            this.maxLabelWidth = maxLabelWidth;
            this.minValue = minValue;
            this.maxBarWidth = width - maxLabelWidth - 2;   // -2 for the space and pipe separator

            var barCounts = new Dictionary<string, int>();

            foreach (string item in data)
            {
                if (barCounts.ContainsKey(item))
                    barCounts[item]++;
                else
                    barCounts.Add(item, 1);
            }

            this.bars = new List<KeyValuePair<string, int>>(barCounts);

        }

        public void Sort(Comparison<KeyValuePair<string, int>> f)
        {
            bars.Sort(f);
        }

        public override string ToString()
        {
            string s = "";
            string blankLabel = "".PadRight(maxLabelWidth);

            int maxValue = 0;
            foreach (KeyValuePair<string, int> bar in bars)
            {
                if (bar.Value > maxValue)
                    maxValue = bar.Value;
            }

            foreach (KeyValuePair<string, int> bar in bars)
            {
                string key = bar.Key;
                int value = bar.Value;

                if (value >= minValue)
                {
                    string label;
                    if (key.Length < maxLabelWidth)
                        label = key.PadLeft(maxLabelWidth);
                    else
                        label = key.Substring(0, maxLabelWidth);

                    int barSize = (int)(((double)value / maxValue) * maxBarWidth);
                    string barStars = "".PadRight(barSize, '*');

                    s += label + " |" + barStars + " " + value + "\n";
                }
            }

            string axis = blankLabel + " +".PadRight(maxBarWidth + 2, '-') + "\n";    //TODO: Why is +2 is needed?
            s += axis;

            return s;
        }
    }
}
