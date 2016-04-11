using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Semantic_Network
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding enc = Encoding.GetEncoding(1251);
            StreamReader file = new StreamReader("data.txt",enc);
            Semantic_Network network = new Semantic_Network(file);
            char answer='n';
            Console.Write("Do you want ask the question?(Y,N) ");
            answer = (char)Console.Read();
            while (Char.ToLower(answer) !='n')
            {
                Console.ReadLine();
                Console.Write("Write request: ");
                String request = Console.ReadLine();
                network.PrintRequest(request);
                Console.Write("Do you want ask the question?(Y,N) ");
                answer = (char)Console.Read();
            }

        }
    }
}
