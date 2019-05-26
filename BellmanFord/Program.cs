using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellmanFord
{
    public class Program
    {
        private static StringBuilder resultsMatrix = new StringBuilder();
        private static StringBuilder resultsList = new StringBuilder();

        public void saveTime(String nMatrix, String nList)
        {
            nMatrix += ".txt";
            try
            {
                StreamWriter save = new StreamWriter(nMatrix);
                save.WriteLine(resultsMatrix.ToString());
                save.Close();
            }
            catch (FileNotFoundException e)
            {
                throw;
            }
            nList += ".txt";
            try
            {
                StreamWriter save = new StreamWriter(nList);
                save.WriteLine(resultsList.ToString());
                save.Close();

            }
            catch (FileNotFoundException e)
            {
                throw;
            }
        }
        static void Main(string[] args)
        {
            Graph graf = new Graph();
            Generator Graph_gen = new Generator();;
            String tmp;
            Console.WriteLine("Generujemy nowe?");
            tmp = Console.ReadLine();
            tmp.ToLower();
            if (tmp.Equals("tak") == true)
                Graph_gen.saveGenerator();
            try
            {
                Console.WriteLine("Testujemy?");
                tmp = Console.ReadLine();
                if (tmp.Equals("tak") == true)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        String j = Convert.ToString(i);
                        graf.Load(j);
                        Bellman bf_alg = new Bellman(graf);
                        int StartVertex = graf.getStartVertex();
                        //ms, then *10^3 to get us
                        var watchMatrix = System.Diagnostics.Stopwatch.StartNew();
                        bf_alg.Bf_Solve(StartVertex);
                        watchMatrix.Stop();
                        resultsMatrix.Append(watchMatrix);
                        Console.WriteLine("Done");
                        var watchList = System.Diagnostics.Stopwatch.StartNew();
                        bf_alg.Bf_Solve_List(StartVertex);
                        watchList.Stop();
                        resultsList.Append(watchList);
                    }
                }
                Console.WriteLine("Podaj nazwe pliku:");
                tmp = Console.ReadLine();
                graf.Load(tmp);
                Bellman bf_algorithm = new Bellman (graf);
                int StartVertexFromFile = graf.getStartVertex();
                bf_algorithm.typeEndVertex();
                bf_algorithm.Bf_Solve(StartVertexFromFile);
                bf_algorithm.Bf_Solve_List(StartVertexFromFile);
                graf.DisplayMatrix();
                graf.DisplayList();
            }
            catch (IOException e)
            {
                throw;
            }

            Console.WriteLine();


        }
    }
}