using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using ProjetoGrafos.DataStructure;
using System.Windows.Forms;

namespace EP
{
 	

	/// <summary>
	/// EPAgent - searchs solution for the eight puzzle problem
	/// </summary>
	public class EightPuzzle: Graph
	{
		private int [] initState;
		private int [] target;
		
		/// <summary>
		/// Creating the agent and setting the initialstate plus target
		/// </summary>
		/// <param name="InitialState"></param>
		public EightPuzzle(int [] InitialState, int [] Target)
		{
			initState = InitialState;
			target = Target;
		}

		/// <summary>
		/// Accessor for the solution
		/// </summary>
        
		public int [] GetSolution()
		{
			return FindSolution();
		}


        private List<Node> CreateChildren(Node node)
        {

            int [] state = (int[]) node.Info;
            int spaceIndex = FindSpace(state);
            int row = spaceIndex / 3;
            int col = spaceIndex % 3;

            List<Node> nodes = new List<Node>();

            Node nUp = CreateChild(state, row - 1, col, spaceIndex);
            Node nDown = CreateChild(state, row + 1, col, spaceIndex);
            Node nLeft = CreateChild(state, row, col - 1, spaceIndex);
            Node nRight = CreateChild(state, row, col + 1, spaceIndex);

            if (nUp != null) nodes.Add(nUp);
            if (nDown != null) nodes.Add(nDown);
            if (nLeft != null) nodes.Add(nLeft);
            if (nRight != null) nodes.Add(nRight);

            return nodes;
        }

        private Node CreateChild(int[] state, int row, int col, int space)
        {
            if(row>=0 && row<3 && col>=0 && col<3)
            {
                int[] child = state.Clone() as int[];
                child[row * 3 + col] = 0;
                child[space] = state[row * 3 + col];
                return new Node(GenerateID(child), child);
            }
            return null;
        }


        private int FindSpace(int[] array)
        {
            return Array.IndexOf<int>(array,0);
        }


        private bool IsTarget(Node node)
        {
            return GenerateID(target) == node.Name;
        }

        private int[] BuildAnswer(Node initNode, Node finalNode)
        {
            Stack<int> stack = new Stack<int>();
            int spaceIndex;

            if(initNode==finalNode)
            {
                int[] info =  finalNode.Info as int[];
                spaceIndex = FindSpace(info);
                stack.Push(info[spaceIndex]);
            }
            else
            {
                do
                {
                    Node parent = finalNode.Edges[0].To;
                    int[] currentInfo = finalNode.Info as int[];
                    int[] parentInfo = parent.Info as int[];
                    spaceIndex = FindSpace(parentInfo);
                    stack.Push(currentInfo[spaceIndex]);
                    finalNode = parent;
                } while (initNode != finalNode);
            }
            return stack.ToArray();
        }
		/// <summary>
		/// Função principal de busca
		/// </summary>
		/// <returns></returns>
		private int[] FindSolution()
		{
            Node initNode = new Node(GenerateID(initState), initState);
            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(initNode);
            this.AddNode(initNode);

            //Executar passeio em largura
            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                if (IsTarget(node))
                {
                    return BuildAnswer(initNode, node);
                }
                List<Node> childrenNodes = CreateChildren(node);
                foreach (Node childNode in childrenNodes)
                {
                    if (this.Find(childNode.Name) == null)
                    {
                        this.AddNode(childNode);
                        this.AddEdge(childNode.Name, node.Name, 0);
                        queue.Enqueue(childNode);
                    }
                }
            }
            return null;
		}

        /// <summary>
        /// Gera o ID de um nó
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private string GenerateID(int[] v)
        {
            string id = "";
            foreach (int i in v)
                id += i.ToString();
            return id;
        }
	}
}

