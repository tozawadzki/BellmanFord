using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BellmanFord
{
    public class Graph
    {
        private int vertices, edges;
        public Vertex[] vertexes;
        private int[,] matrix;

        private int startVertex;
        public static int MAX_INT = 20000000;

        public void AddEdge(int Start, int End, int Cost)
        {
            matrix[Start, End] = Cost;
            if (vertexes[Start] == null)
                vertexes[Start] = new Vertex();
            vertexes[Start].addEdge(new Edge(Start, End, Cost));
        }
        /*
         * LISTA SĄSIEDZTW
         */
        public void DisplayList()
        {
            Console.WriteLine("Graf wazony skierowany w postaci listy sasiedztwa:");
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertexes[i].GetEdges().Length; j++)
                {
                    if (j == 0)
                    {
                        Console.WriteLine(vertexes[i].GetEdges()[j].source);
                        Console.WriteLine(" -> " + "(" + vertexes[i].GetEdges()[j].destination + "," +
                                          vertexes[i].GetEdges()[j].weight + ")");
                    }
                    else
                        Console.WriteLine(" , (" + vertexes[i].GetEdges()[j].destination + "," +
                                          vertexes[i].GetEdges()[j].weight + ")");
                }

                Console.WriteLine();
            }
        }

        /*
         * MACIERZ SĄSIEDZTW
         */
        public void DisplayMatrix()
        {
            Console.WriteLine("Graf wazony skierowany w postaci macierzy sasiedztwa :");
            Console.WriteLine("    ");
            for (int k = 0; k < vertices; k++)
            {
                Console.WriteLine("%5d", k);
            }

            Console.WriteLine("_\n");
            for (int i = 0; i < vertices; i++)
            {
                Console.WriteLine("%5d", i);
                Console.WriteLine("%2c", '|');
                for (int j = 0; j < vertices; j++)
                {
                    if (matrix[i, j] == MAX_INT)
                    {
                        Console.WriteLine("%5c", '*');
                    }
                    else
                    {
                        Console.WriteLine("%5s", matrix[i,j]);
                    }
                }

                Console.WriteLine("\n");
            }
        }

        public void Load(String name)
        {
            String path = "src/resources/" + name + ".txt";
            using (StreamReader sr = File.OpenText(path))
            {
                string s = null;

                s = sr.ReadLine();
                String[] mainData = s.Split('\t');
                getGraphData(mainData);

                matrix = new int[vertices, vertices];
                for (int i = 0; i < vertices; i++)
                {
                    for (int j = 0; j < vertices; j++)
                    {
                        matrix[i, j] = MAX_INT;
                        if (i == j) matrix[i,j] = 0;
                    }
                }

                vertexes = new Vertex[vertices];

                while ((s = sr.ReadLine()) != null)
                {
                    String[] edgeData = s.Split('\t');
                    AddEdge(Int32.Parse(edgeData[0]), Int32.Parse(edgeData[1]), Int32.Parse(edgeData[2]));
                }
            }
        }

        public void getGraphData(String[] graphData)
        {
            edges = Int32.Parse(graphData[0]);
            vertices = Int32.Parse(graphData[1]);
            startVertex = Int32.Parse(graphData[2]);
        }

        public int[,] getMatrix()
        {
            return matrix;
        }

        public int getAmountOfVertexes()
        {
            return vertices;
        }

        public int getStartVertex()
        {
            return startVertex;
        }
    }
}
