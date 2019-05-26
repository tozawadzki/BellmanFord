using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellmanFord
{
   public class Edge
   {
       public int source, destination, weight;

       public Edge(int start, int end, int cost)
       {
           source = start;
           destination = end;
           weight = cost;
       }
   }
}
