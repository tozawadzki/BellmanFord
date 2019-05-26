using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellmanFord
{
     public class Vertex
     {
         public Edge[] edgeList;

         public Vertex()
         {
             edgeList= new Edge[0];
         }

         public Edge[] GetEdges()
         {
             return edgeList;
         }

         public void addEdge(Edge edge)
         {
             Edge[] edgeArr = new Edge[edgeList.Length+1];
             for(int i=0; i < edgeArr.Length-1; i++)
             {
                 edgeArr[i] = edgeList[i];
             }

             edgeArr[edgeList.Length] = edge;
             edgeList = edgeArr;
         }
     }
}
