using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellmanFord
{
    public class Results
    {
        int amountOfVertex;

        private int[,] Paths;
        private int[,] Paths_List;
        private int[] costsTabs;
        private int[] pathBetweenChosenVertex;
        private int[] pathBetweenChosenVertexList;
        public Results(int Vertex)
        {
            amountOfVertex = Vertex;
            
            pathBetweenChosenVertex = new int[Vertex];
            pathBetweenChosenVertexList = new int[Vertex];
            for (int i = 0; i < pathBetweenChosenVertex.Length; i++)
            {
                pathBetweenChosenVertex[i] = -1;
            }
            for (int i = 0; i < pathBetweenChosenVertexList.Length; i++)
            {
                pathBetweenChosenVertexList[i] = -1;
            }
            Paths = new int[Vertex, Vertex];
            Paths_List = new int[Vertex, Vertex];
            for (int z = 0; z < Vertex; z++)
            {
                for (int x = 0; x < Vertex; x++)
                {
                    Paths[z, x] = -1;
                    Paths_List[z, x] = -1;
                }
            }
        }


        public int[,] getPaths()
        {
            return Paths;
        }

        public int[,] getPaths_List()
        {
            return Paths_List;
        }

        public void setCostsTabs(int[] costsTabs)
        {
            this.costsTabs = costsTabs;
        }

        public void setPathBetweenChosenVertex(int start, int end)
        {
            Console.WriteLine("Algorytmy przy pomocy tablicy sasiedztwa");
            Console.WriteLine("Sciezka pomiedzy wierzcholkiem " + start + " a wierzcholkiem " + end);
            for (int j = amountOfVertex - 1; j >= 0; j--)
            {
                if (Paths[end, j] != -1)
                {
                    Console.WriteLine(Paths[end,j] + " -> ");
                }
            }
            Console.WriteLine(end);
            Console.WriteLine("Calkowity koszt przejscia: " + costsTabs[end]);
            save_Matrix("ResultsOfMatrix");
        }

        public void setPathBetweenChosenVertexList(int start, int end)
        {
            Console.WriteLine("Algorytmy przy pomocy listy sasiedztwa");
            Console.WriteLine("Sciezka pomiedzy wierzcholkiem " + start + " a wierzcholkiem " + end);
            for (int j = amountOfVertex - 1; j >= 0; j--)
            {
                if (Paths_List[end, j] != -1)
                {
                    Console.WriteLine(Paths_List[end, j] + " -> ");
                }
            }
            Console.WriteLine(end);
            Console.WriteLine("Calkowity koszt przejscia: " + costsTabs[end]);
            save_List("ResultsOfList");
        }

        public void save_Matrix(String Name)
        {
            Name += ".txt";
            try
            {
                StreamWriter save = new StreamWriter(Name);
                save.WriteLine("\nSciezki:\n");
                for (int i = 0; i < amountOfVertex; i++)
                {
                    for (int j = amountOfVertex - 1; j >= 0; j--)
                    {
                        if (Paths[i, j] != -1)
                        {
                            save.WriteLine(Paths[i,j] + "  ->  ");
                        }
                    }
                    save.WriteLine(i + "   \n");
                    save.WriteLine("Calkowity koszt przejscia: " + costsTabs[i]);
                    save.WriteLine();
                }
                save.Close();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void save_List(String Name)
        {
            Name += ".txt";
            try
            {
                StreamWriter save = new StreamWriter(Name);
                save.WriteLine("\nSciezki:\n");
                for (int i = 0; i < amountOfVertex; i++)
                {
                    for (int j = amountOfVertex - 1; j >= 0; j--)
                    {
                        if (Paths_List[i,j] != -1)
                        {
                            save.WriteLine(Paths_List[i,j] + "  ->  ");
                        }
                    }
                    save.WriteLine(i + "   \n");
                    save.WriteLine("Calkowity koszt przejscia: " + costsTabs[i]);
                    save.WriteLine();
                }
                save.Close();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
    public static class CustomArray
    {
        public static void Fill(this Array x, object y)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x.SetValue(y, i);
            }
        }
    }
}
