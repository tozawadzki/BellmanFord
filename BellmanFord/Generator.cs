using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellmanFord
{
    public class Generator
    {
        private Random r = new Random();
        public int[,] generator(int Vertexes, int Edges)
        {
            int[,] graph = new int[Vertexes, Vertexes];

            for (int i = 0; i < Vertexes; i++)
            {
                for (int j = 0; j < Vertexes; j++)
                {
                    graph[i,j] = -1;
                }
            }

            for (int i = 0; i < Vertexes - 1; i++)
            {
                graph[i + 1, i] = (int)(r.NextDouble() * 100) + 1;
            }

            for (int i = 0; i < Edges; i++)
            {
                int startVertex;
                int endVertex;
                int value;
                bool flagEnd = true;
                // tu sie robi nieskonczona petla
                do
                {
                    flagEnd = true;
                    startVertex = (int)(r.NextDouble() * Vertexes);
                    endVertex = (int)(r.NextDouble() * Vertexes);
                    value = (int)(r.NextDouble() * 100) + 1;
                    if (startVertex == endVertex)
                    {
                        flagEnd = false;
                    }
                    if (graph[startVertex,endVertex] != -1)
                    {
                        flagEnd = false;
                    }
                } while (flagEnd == false);

                graph[startVertex,endVertex] = value;
            }
            return graph;
            //display(graph);
        }

        public void saveGenerator()
        {
            Console.WriteLine("Podaj ilosc wierzcholkow: ");
            int Vertexes = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Podaj gestosc grafu:");
            int Density = Convert.ToInt32(Console.ReadLine());
            int Edges = (Vertexes * (Vertexes - 2) * Density) / 100 + 1;
            Console.WriteLine();
            String Name;
            int[,] Graph = new int[Vertexes,Vertexes];
            for (int i = 0; i < 100; i++)
            {

                int RandomStartVertex = (int)(r.Next(0,1) * Vertexes);
                Graph = generator(Vertexes, Edges);
                Name = "src/resources/" + i + ".txt";
                StreamWriter save = null;
                try
                {
                    save = new StreamWriter(Name);
                }
                catch (FileNotFoundException e)
                {
                    throw;
                }
                save.WriteLine(Edges + "\t" + Vertexes + "\t" + RandomStartVertex);
                for (int k = 0; k < Vertexes; k++)
                {
                    for (int j = 0; j < Vertexes; j++)
                    {
                        if (Graph[k,j] != -1 && Graph[k,j] != 0)
                        {
                            save.WriteLine(k + "\t" + j + "\t" + Graph[k,j]);
                        }
                    }
                }
                save.Close();
            }
        }
    }
}
