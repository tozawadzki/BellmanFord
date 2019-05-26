using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellmanFord
{
    public class Bellman
    {
        Graph graph;
        private int[] costTab;
        private int[] prevTab;
        private int endVertex;
        int amountOfVertex;
        int[,] matrix;

    public Bellman(Graph graph)
        {
            this.graph = graph;
            costTab = new int[graph.getAmountOfVertexes()];
            prevTab = new int[graph.getAmountOfVertexes()];
            matrix = graph.getMatrix();
            amountOfVertex = graph.getAmountOfVertexes();
        }

        public Results Bf_Solve(int Start)
        {
            Results results = new Results(amountOfVertex);
            results.setCostsTabs(costTab);
            for (int i = 0; i < amountOfVertex; i++)
            {
                costTab[i] = Graph.MAX_INT;
                prevTab[i] = -1;
            }
            bool change;
            costTab[Start] = 0;
            prevTab[Start] = Start;

            for (int k = 0; k < amountOfVertex - 1; k++)
            {
                change = false;
                for (int i = 0; i < amountOfVertex; i++)
                {
                    for (int j = 0; j < amountOfVertex; j++)
                    {
                        if (matrix[i,j] != Graph.MAX_INT)
                        {
                            if (costTab[j] > costTab[i] + matrix[i,j])
                            {
                                costTab[j] = costTab[i] + matrix[i,j];
                                prevTab[j] = i;
                                Console.WriteLine(prevTab[j]);
                                change = true;
                            }
                        }
                    }
                }
                if (!change) break;
            }
            if (!isNegativeCycle())
                Shortest_Path(Start, results);

            return results;
        }

        public Results Bf_Solve_List(int Start)
        {
            Results results = new Results(amountOfVertex);
            results.setCostsTabs(costTab);
            for (int i = 0; i < amountOfVertex; i++)
            {
                costTab[i] = Graph.MAX_INT;
                prevTab[i] = -1;
            }
            bool change;
            costTab[Start] = 0;
            prevTab[Start] = Start;
            for (int k = 0; k < amountOfVertex - 1; k++)
            {
                change = false;
                for (int i = 0; i < amountOfVertex; i++)
                {
                    for (int j = 0; j != graph.vertexes[i].edgeList.Length; j++)
                    {
                        if (costTab[graph.vertexes[i].edgeList[j].destination] > costTab[i] + graph.vertexes[i].edgeList[j].weight)
                        {
                            costTab[graph.vertexes[i].edgeList[j].destination] = costTab[i] + graph.vertexes[i].edgeList[j].weight;
                            prevTab[graph.vertexes[i].edgeList[j].destination] = i;
                            change = true;
                        }
                    }
                }
                if (!change) break;
            }
            if (!isNegativeCycleList())
                Shortest_Path_List(Start, results);
            return results;
        }

        public bool isNegativeCycle()
        {
            for (int i = 0; i < amountOfVertex; i++)
            {
                for (int j = 0; j < amountOfVertex; j++)
                {
                    if (matrix[i,j] != Graph.MAX_INT)
                    {
                        if (costTab[j] > (costTab[i] + matrix[i,j]))
                        {
                            Console.WriteLine("CYKL UJEMNY!");
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool isNegativeCycleList()
        {
            for (int i = 0; i < amountOfVertex; i++)
            {
                for (int j = 0; j < graph.vertexes[i].edgeList.Length; j++)
                {
                    if (costTab[graph.vertexes[i].edgeList[j].destination] > (costTab[i] + graph.vertexes[i].edgeList[j].weight))
                    {
                        Console.WriteLine("CYKL UJEMNY!");
                        return true;
                    }
                }
            }
            return false;
        }

        public void Shortest_Path(int Start, Results results)
        {
            int[,] Path = results.getPaths();
            int k = 0;
            int i = 0;
            for (int j = 0; j < amountOfVertex; j++)
            {
                k = j;
                i = 0;
                do
                {
                    Path[j,i++] = prevTab[k];
                    k = prevTab[k];
                } while (Start != Path[j, i-1]);
            }
            results.setPathBetweenChosenVertex(graph.getStartVertex(), endVertex);
            Console.WriteLine();
            Console.WriteLine("\nSciezki:\n");
            for (int s = 0; s < amountOfVertex; s++)
            {
                for (int h = amountOfVertex - 1; h >= 0; h--)
                {
                    if (Path[s,h] != -1)
                    {
                        Console.WriteLine(Path[s,h] + "  ->  ");
                    }
                }
                Console.WriteLine(s + "   \n");
                Console.WriteLine("Calkowity koszt przejscia: " + costTab[s]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void Shortest_Path_List(int Start, Results results)
        {
            int[,] Path_List = results.getPaths_List();
            int k = 0;
            int i = 0;
            for (int j = 0; j < amountOfVertex; j++)
            {
                k = j;
                i = 0;
                do
                {
                    Path_List[j,i++] = prevTab[k];
                    k = prevTab[k];
                } while (Path_List[j,i - 1] != Start);
            }
            results.setPathBetweenChosenVertexList(graph.getStartVertex(), endVertex);
            Console.WriteLine();
            Console.WriteLine("\nSciezki:\n");
            for (int s = 0; s < amountOfVertex; s++)
            {
                for (int h = graph.vertexes[s].edgeList.Length; h >= 0; h--)
                {
                    if (Path_List[s,h] != -1)
                    {
                        Console.WriteLine(Path_List[s,h] + "  ->  ");
                    }
                }
                Console.WriteLine(s + "   \n");
                Console.WriteLine("Calkowity koszt przejscia: " + costTab[s]);
                Console.WriteLine();
            }
            Console.WriteLine();

        }

        public void typeEndVertex()
        {      
            Console.WriteLine("Podaj wierzcholek koncowy:");
            endVertex = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
        }
    }
}
