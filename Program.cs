using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ShortColumnRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = "";

            do
            {
                Console.WriteLine("Input file name:");
                dir = Console.ReadLine();
            }
            while (!File.Exists(dir) && Path.GetExtension(dir) != ".txt");

            string[] input = File.ReadAllLines(dir);

            Console.WriteLine("Max length:");
            int length = int.Parse(Console.ReadLine());

            List<string>[] inputList = new List<string>[input[0].Split(new string[] { "\t" }, StringSplitOptions.None).Length];

            for (int i = 0; i < inputList.Length; i++)
                inputList[i] = new List<string>();

            string[] buf;

            foreach(string row in input)
            {
                buf = row.Split(new string[] { "\t" }, StringSplitOptions.None);
                for (int i = 0; i < buf.Length; i++)
                    if (buf[i] != "")
                        inputList[i].Add(buf[i]);
            }

            for (int i = 0; i < inputList.Length; i++)
                if(inputList[i].Count < length)
                {
                    inputList[i].Clear();
                    inputList[i] = null;
                }

            string[] output = new string[length];
            List<string> outputBuf = new List<string>();

            for(int i = 0; i < length; i++)
            {
                outputBuf = new List<string>();

                for (int j = 0; j < inputList.Length; j++)
                    if (inputList[j] != null)
                        outputBuf.Add(inputList[j][i]);
                    else
                        outputBuf.Add("");

                output[i] = string.Join("\t", outputBuf);
            }

            File.WriteAllLines(dir.Replace(".txt" , "_" + length + "length.txt"), output);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

    }
}
