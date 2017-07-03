using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.DataStructure
{
    /// <summary>
    /// Classe que representa um grafo.
    /// </summary>
    public class Graph
    {
        
        #region Atributos

        /// <summary>
        /// Lista de nós que compõe o grafo.
        /// </summary>
        private List<Node> nodes;
        public  List<Edge> edges = new List<Edge>();
        public int caminhos = 0;
        #endregion
        #region Propridades

        /// <summary>
        /// Mostra todos os nós do grafo.
        /// </summary>
        public Node[] Nodes
        {
            get { return this.nodes.ToArray(); }
        }

        #endregion
        #region Construtores

        /// <summary>
        /// Cria nova instância do grafo.
        /// </summary>
        public Graph()
        {
            this.nodes = new List<Node>();
        }
        public Graph(bool direcionado)
        {
            this.nodes = new List<Node>();
        }


        #endregion
        #region Métodos
        /// <summary>
        /// Encontra o nó através do seu nome.
        /// </summary>
        /// <param name="name">O nome do nó.</param>
        /// <returns>O nó encontrado ou nulo caso não encontre nada.</returns>
        public Node Find(string name)
        {
            return this.nodes.SingleOrDefault(e => e.Name == name);
        }
        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name)
        {
            AddNode(name, null);
        }
        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name, object info)
        {
            if (Find(name) != null)
            {
                throw new Exception("Um nó com o mesmo nome já foi adicionado a este grafo.");
            }
            this.nodes.Add(new Node(name, info));
        }
        public void AddNode(string name, int info)
        {
            if (Find(name) != null)
            {
                throw new Exception("Um nó com o mesmo nome já foi adicionado a este grafo.");
            }
            this.nodes.Add(new Node(name, info));
        }
        protected void AddNode(Node node)
        {
            this.nodes.Add(node);
        }
        /// <summary>
        /// Remove um nó do grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser removido.</param>
        public void RemoveNode(string name)
        {
            Node existingNode = Find(name);
            if (existingNode == null)
            {
                throw new Exception("Não foi possível encontrar o nó a ser removido.");
            }
            this.nodes.Remove(existingNode);

            for(int i =0; i<this.edges.Count();i++)
            {
                if (this.edges[i].To == existingNode)
                    this.edges.Remove(this.edges[i]);
            }
        }
        /// <summary>
        /// Adiciona o arco entre dois nós associando determinado custo.
        /// </summary>
        /// <param name="from">O nó de origem.</param>
        /// <param name="to">O nó de destino.</param>
        /// <param name="cost">O cust associado.</param>
        public void AddEdge(string from, string to, double cost)
        {
            Node start = Find(from);
            Node end = Find(to);
            // Verifica se os nós existem..
            if (start == null)
            {
                throw new Exception("Não foi possível encontrar o nó origem no grafo.");
            }
            if (end == null)
            {
                throw new Exception("Não foi possível encontrar o nó origem no grafo.");
            }

            this.edges.Add(new Edge(start, end));
            start.AddEdge(end, cost);
        }
        public void AddEdge(string from, string to)
        {
            Node start = Find(from);
            Node end = Find(to);
            // Verifica se os nós existem..
            if (start == null)
            {
                throw new Exception("Não foi possível encontrar o nó origem no grafo.");
            }
            if (end == null)
            {
                throw new Exception("Não foi possível encontrar o nó origem no grafo.");
            }
            

            this.edges.Add(new Edge(start, end));

            start.AddEdge(end, 1);
        }
        /// <summary>
        /// Obtem todos os nós vizinhos de determinado nó.
        /// </summary>
        /// <param name="node">O nó origem.</param>
        /// <returns></returns>
        public Node[] GetNeighbours(string from)
        {
            Node node = Find(from);
            // Verifica se os nós existem..
            if (node == null)
            {
                throw new Exception("Não foi possível encontrar o nó origem no grafo.");
            }
            return node.Edges.Select(e => e.To).ToArray();
        }
        /// <summary>
        /// Valida um caminho, retornando a lista de nós pelos quais ele passou.
        /// </summary>
        /// <param name="nodes">A lista de nós por onde passou.</param>
        /// <param name="path">O nome de cada nó na ordem que devem ser encontrados.</param>
        /// <returns></returns>
        public bool IsValidPath(ref Node[] nodes, params string[] path)
        {
            Node no;
            int existe = 0;
            List<Node> nos = new List<Node>();
            // Implementar
            for (int i = 0; i < path.Count() - 1; i++)
            {
                existe = 0;
                no = Find(path[i]);
                if (no != null)
                {
                    foreach (Edge e in no.Edges)
                    {
                        if (e.To == Find(path[i + 1]))
                        {
                            existe = 1;
                            nos.Add(no);
                        }
                    }
                }
                if (existe == 0)
                {
                    return false;
                }
            }
            nos.Add(Find(path[path.Count() - 1]));
            nodes = nos.ToArray();
            return true;
        }
        public bool Hamiltonian()
        {
            foreach (Node n in this.nodes)
            {
                bool ret = this.Hamiltonian(n);
                if (ret) return true;
            }
            return false;
        }
        private bool Hamiltonian(Node n)
        {
            // Cria lista para armazenar o resultado..
            Queue<Node> queue = new Queue<Node>();
            // Arvore
            Graph arvore = new Graph();
            int id = 0;
            id++;
            arvore.AddNode(id.ToString(),n.Name);
            queue.Enqueue(arvore.Find(id.ToString()));

            // Realiza a busca..
            while (queue.Count > 0)
            {
                Node np = queue.Dequeue();
                Node currentNode = this.Find(np.Info.ToString());
                if (this.nodes.Count == CountNodes(np))
                    return true;

                foreach (Edge edge in currentNode.Edges)
                {
                    if (!ExistNode(np, edge.To.Name))
                    {
                        id++;
                        arvore.AddNode(id.ToString(), edge.To.Name);
                        Node nf = arvore.Find(id.ToString());
                        queue.Enqueue(nf);
                        arvore.AddEdge(nf.Name, np.Name, 1);
                    }
                }
            }
            return false;
        }
        private bool ExistNode(Node np, string p)
        {
            if (np == null) return false;
            while (np.Edges.Count > 0) 
            {
                if (np.Info.ToString() == p) return true;
                np = np.Edges[0].To; 
            }
            return np.Info.ToString() == p;
        }
        private bool ExistEdge(Graph arvore, Edge eReal)
        {
            if (arvore.edges.Count == 0)
                return false;
            foreach(Edge eArvore in arvore.edges)
            {
                if (eArvore.From.Info == eReal.From.Name && eArvore.To.Info == eReal.To.Name)
                    return true;
            }
                
                return false;
        }
        private int CountNodes(Node np)
        {
            if (np == null) return 0;
            int count = 1;
            while (np.Edges.Count > 0)
            { count++; np = np.Edges[0].To; }
            return count;
        }


        public void RemoverArco(string de, string para)
        {
            Node from = Find(de);
            Node to = Find(para);
            for (int i = 0; i < from.Edges.Count(); i++)
            {
                if (from.Edges[i].To.Name == para)
                {
                    from.Edges.Remove(from.Edges[i]);
                }
            }

            
            
            for (int i = 0; i < this.edges.Count(); i++)
            {
                if(this.edges[i].To == to && this.edges[i].From == from)
                {
                  /*  List<Edge> aux = new List<Edge>();
                    for(int j=0;j<this.edges.Count();j++)
                    {
                        aux.Add(this.edges[i]);
                    }
                    */
                    this.edges.Remove(new Edge(from, to));
                }
            }
            


        }
        public bool Conexo()
        {
            foreach (Node n in this.nodes)
            {
                if (Conexo(n.Name))
                    return true;
            }
            return false;
        }
        public bool Conexo(string startNode)
        {

            List<Node> lista = passeioLargura(startNode);

            return ConexoLista(lista);
        }
        public bool ConexoLista(List<Node> lista)
        {
            Node inicio = lista[0];
            Node fim = lista[lista.Count - 1];
            int valido = 0;
            foreach (Edge e in fim.Edges)
            {
                if (e.To == inicio)
                    valido = 1;
            }

            if (valido == 1)
                    return true;
            return false;
        }
        public List<Node> passeioLargura(string startNodes)
        {
            this.limpaVisitados();

            Node startNode = Find(startNodes);
            
            List<Node> visitados = new List<Node>();
            Queue<Node> fila = new Queue<Node>();
            if (startNode == null)
                return null;

            startNode.visitado = true;
            
            
            fila.Enqueue(startNode);
         

            while (fila.Count > 0)
            {
                Node no = fila.Dequeue();
                visitados.Add(no);

                foreach (Edge e in no.Edges)
                {
                    if (e.To.visitado == false)
                    {
                        e.To.visitado = true;
                        
                        fila.Enqueue(e.To);
                    }
                }
            }
            return visitados;
        }
        public List<Node> passeioLarguraNivelado(string startNodes)
        {
            this.limpaVisitados();

            Node startNode = Find(startNodes);
            int nivel = 0;
            List<Node> visitados = new List<Node>();
            Queue<Node> fila = new Queue<Node>();
            if (startNode == null)
                return null;

            startNode.visitado = true;
            startNode.Info = nivel;
            fila.Enqueue(startNode);


            while (fila.Count > 0)
            {
                Node no = fila.Dequeue();
                visitados.Add(no);
                nivel++;
                foreach (Edge e in no.Edges)
                {
                    if (e.To.visitado == false)
                    {
                        e.To.visitado = true;
                        e.To.Info = nivel;
                        fila.Enqueue(e.To);
                    }
                }
            }
            return visitados;
        }
        public List<Node> passeioProfundidade(string startNodes)
        {
            this.limpaVisitados();
            Node startNode = Find(startNodes);
            List<Node> visitados = new List<Node>();
            Stack<Node> fila = new Stack<Node>();
            startNode.visitado = true;
            fila.Push(startNode);
            

            while (fila.Count > 0)
            {
                Node no = fila.Pop();
                visitados.Add(no);

                foreach (Edge e in no.Edges)
                {
                    if (e.To.visitado == false)
                    {
                        e.To.visitado = true;
                        fila.Push(e.To);
                    }
                }
            }
            return visitados;
        }
        public void limpaVisitados()
        {
            foreach (Node n in this.Nodes)
                n.visitado = false;
        }
        public List<Node> caminhoMinimo(string p1, string p2)
        {
            Node start = Find(p1);

            if (start == null)
                return null;

            bool hasChanged = true;
            Graph solution = new Graph();

            solution.AddNode(p1, 0);

            while (solution.Find(p2) == null && hasChanged)
            {
                double minDistance = double.MaxValue;
                Node parent = null;
                Edge minEdge = null;
                hasChanged = false;
                foreach (Node solutionNode in solution.Nodes)
                {
                    Node node = Find(solutionNode.Name);
                    foreach (Edge edge in node.Edges)
                    {
                        if (solution.Find(edge.To.Name) == null)
                        {
                            double distance = edge.Cost + Convert.ToDouble(solutionNode.Info);
                            if (distance < minDistance)
                            {
                                hasChanged = true;
                                minDistance = distance;
                                parent = solutionNode;
                                minEdge = edge;
                            }
                        }
                    }
                }
                if (hasChanged)
                {
                    solution.AddNode(minEdge.To.Name, minDistance);
                    solution.AddEdge(minEdge.To.Name, parent.Name, minEdge.Cost);
                }
            }
            return solution.passeioLargura(p2);
        }
        public object MenorCaminho(string p1, string p2)
        {

            string resposta = "";
            List<Node> resp = caminhoMinimo(p1, p2);

            if (resp == null)
                return "";

            for (int i = resp.Count - 1; i >= 0; i--)
            {
                resposta += (string)resp[i].Name + ",";
            }

            int j = 0;
            string respostaFinal = "";
            foreach (char c in resposta)
            {
                j++;
                if (j != resposta.Count())
                    respostaFinal += c;
            }

            return respostaFinal;
        }
        public object EhDirigido()
        {
    
            foreach (Node n in this.nodes)
            {
                foreach (Edge e in n.Edges)
                {
                    int existe = 0;
                    foreach (Edge ee in e.To.Edges)
                    {
                        if (ee.To == n)
                            existe = 1;
                    }
                    if (existe == 0)
                        return true;
                }
            }
            return false;
        }
        public object Nivel(string p1, string p2)
        {
            List<Node> l = caminhoMinimo(p1, p2);

            return Convert.ToInt32(l[0].Info);
        }
        public object NodesNivel(int p1, string p2)
        {

            List<Node> l = passeioLarguraNivelado(p2);
            
            l[l.Count-1].Info = Convert.ToInt32(l[l.Count-1].Info) - 1;
            List<Node> resposta = new List<Node>();

            for (int i = 0; i < l.Count; i++)
            {
                int a = Convert.ToInt32(l[i].Info);
                if(a==p1)
                {
                    resposta.Add(l[i]);
                }
            }

            resposta = ordemAlfabetica(resposta);
            
            string respostaFinal= montaRespostaFinal(resposta);

            return respostaFinal;
        }
        private string montaRespostaFinal(List<Node> resposta)
        {
            int j = 0;
            string respostaFinal = "";
            foreach (Node c in resposta)
            {
                j++;
                if (j == resposta.Count)
                    respostaFinal += c.Name;
                else
                    respostaFinal += c.Name + ",";
            }

            return respostaFinal;
        }
        private string montaRespostaFinalEuleriano(List<Node> resposta)
        {
            int j = 0;
            string respostaFinal = "";
            foreach (Node c in resposta)
            {
                j++;
                if (j == resposta.Count)
                    respostaFinal += c.Info;
                else
                    respostaFinal += c.Info + ",";
            }

            return respostaFinal;
        }
        private List<Node> ordemAlfabetica(List<Node> resposta)
        {

            int x = 0;
            int y = 0;
            Node aux;
            int TAM = resposta.Count;

            for (x = 0; x < TAM; x++)
            {
                for (y = x + 1; y < TAM; y++) // sempre 1 elemento à frente
                {
                    // se o (x > (x+1)) então o x passa pra frente (ordem crescente)
                    if (Convert.ToChar(resposta[x].Name) > Convert.ToChar(resposta[y].Name))
                    {
                        aux = resposta[x];
                        resposta[x] = resposta[y];
                        resposta[y] = aux;
                    }
                }
            }

            return resposta;
        }
        public void ConverteNaoDirigido()
        {
            foreach (Node n in this.nodes)
            {
                foreach (Edge e in n.Edges)
                {
                    int existe = 0;
                    foreach (Edge ee in e.To.Edges)
                    {
                        if (ee.To == n)
                            existe = 1;
                    }
                    if (existe == 0)
                    {
                        e.To.AddEdge(n);
                    }
                }
            }
        }
        public object RegioesConexas()
        {
            List<Node> lista = new List<Node>();
            string respostaFinal = "";
          //  this.ConverteNaoDirigido();

            lista.Add(this.nodes[0]);
            lista[0].AddEdge(lista[0]);
            foreach(Node n in this.nodes)
            {
                if (!lista.Contains(n))
                {
                    lista = passeioProfundidade(n.Name);
                    lista = ordemAlfabetica(lista);
                    if (this.ConexoLista(lista))
                    {
                        respostaFinal += "{";
                        //lista = passeioProfundidade(n.Name);
                        //lista = ordemAlfabetica(lista);
                        respostaFinal += montaRespostaFinal(lista) + "}";
                    }
                }
            }
            return respostaFinal;
        }
 
        public object EhVizinho(string p1, string p2)
        {
            Node from = Find(p1);
            Node to = Find(p2);
            int vizinhos = 0;

            foreach(Edge e in from.Edges)
            {
                if (e.To == to)
                    vizinhos = 1;
            }

            foreach (Edge e in to.Edges)
            {
                if (e.To == from)
                    vizinhos = 1;
            }

            if (vizinhos == 1)
                return true;

            return false;
        }
        public object EhVizinhoDirigido(string p1, string p2)
        {
            Node from = Find(p1);
            Node to = Find(p2);
            int vizinhos = 0;

            foreach (Edge e in from.Edges)
            {
                if (e.To == to)
                    vizinhos = 1;
            }

          
            if (vizinhos == 1)
                return true;

            return false;
        }
        public object ExisteCaminho(string p1, string p2)
        {
            List<Node> l = caminhoMinimo(p1, p2);
            if (l != null)
                return true;
            return false;
        }
        public object ExisteCiclo()
        {          
            return Conexo();
        }
        public object passeioLarguraString(string p)
        {
            string resposta;
            List<Node> lista = passeioLargura(p);

            resposta = montaRespostaFinal(lista);

            return resposta;
        }
        public bool VerificaConsistencia()
        {
            int qtdEdges = this.edges.Count;
            int somatoria = 0;
            foreach(Node n in this.nodes)
            {
                somatoria += grau(n);
            }
            if (somatoria == (qtdEdges * 2))
                return true;
            return false;
        }
        public int grau(Node n)
        {
            int i=0;
            foreach (Edge e in n.Edges)
                i++;

            return i;
        }
        public object GrafoCompleto()
        {
            int qtdNodes = this.nodes.Count-1;
            foreach(Node n in this.nodes)
            {
                if (grau(n) != qtdNodes)
                    return false;
            }

            return true;
        }
        public object Geodesica(string p1, string p2)
        {
            List<Node> lista = caminhoMinimo(p1, p2);

            return lista.Count - 1;
        }

        public object Excentricidade(string p)
        {
            int maior = 0;
            foreach (Node n in this.nodes)
            {
                int valor = Convert.ToInt32(Geodesica(p, n.Name));
                if ( valor > maior)
                    maior = valor;
            }
            return maior;
        }

        public object Raio()
        {
            return Convert.ToInt32(Diametro()) / 2;
        }

        public object Diametro()
        {
            int maior = 0;
            foreach (Node n in this.nodes)
            {
                int valor = Convert.ToInt32(Excentricidade(n.Name));
                if (valor > maior)
                    maior = valor;
            }
            return maior;
        }

        public object CaminhoMinimoNegativo(string p1, string p2)
        {

            Node start = Find(p1);

            if (start == null)
                return null;

            bool hasChanged = true;
            Graph solution = new Graph();

            solution.AddNode(p1, 0);

            while (naoEdgeVisitados() && hasChanged)
            {
                double minDistance = double.MaxValue;
                Node parent = null;
                Edge minEdge = null;
                hasChanged = false;
                foreach (Node solutionNode in solution.Nodes)
                {
                    Node node = Find(solutionNode.Name);
                   
                    foreach (Edge edge in node.Edges)
                    {
                        if (edge.Visitado == false)
                        {
                            double distance = edge.Cost + Convert.ToDouble(solutionNode.Info);
                            if (distance < minDistance)
                            {
                                hasChanged = true;
                                minDistance = distance;
                                parent = solutionNode;
                                minEdge = edge;
                                edge.Visitado = true;
                            }
                        }
                    }
                }
                if (hasChanged)
                {
                    if (solution.Find(minEdge.To.Name) == null)
                    {
                        solution.AddNode(minEdge.To.Name, minDistance);
                        solution.AddEdge(minEdge.To.Name, parent.Name, minEdge.Cost);
                    }
                    else
                    {
                        Node aux = solution.Find(minEdge.To.Name);
                        aux.Info = minDistance;
                        aux.Edges.Clear();
                        solution.AddEdge(minEdge.To.Name, parent.Name, minEdge.Cost);
                    } 
                }
            }
            return montaRespostaFinal(inverteList(solution.passeioLargura(p2)));

        }
        private List<Node> inverteList(List<Node> list)
        {
            List<Node> listaInvertida = new List<Node>();
            int tam = list.Count-1;
            for(int i=tam;i>=0;i--)
                listaInvertida.Add(list[i]);

            return listaInvertida;
        }
        private bool naoEdgeVisitados()
        {
            foreach (Edge n in this.edges)
            {
                if (n.Visitado == false)
                    return true;
            }
            return false;
        }
        private bool ExisteNodeNaoVisitado()
        {
            Node no = new Node();
            int v1 = 0;
            foreach (Node n in this.nodes)
            {
                if (n.visitado == false)
                {
                    no = n;
                    v1 = 1;
                }
            }

            if(v1==1)
                foreach(Edge e in this.edges)
                {
                    if (e.To == no)
                        return true;
                }

            return false;
        }
        
        public string profundidadeIterativa2(Node nO, int limite)
        {
            string resp = "";
            do
            {
                limpaVisitados();
                resp += profundidadeIterativa(nO, limite++);
            } while (ExisteNodeNaoVisitado());

            return resp;
        }

        public string profundidadeIterativa(Node nO, int limite)
        {
            Stack<Node> p = new Stack<Node>();
            List<Node> l = new List<Node>();
            p.Push(nO);
            nO.Nivel = 0;
         
            while (p.Count() > 0)
            {
                Node n = p.Pop();

                if (!l.Contains(n))
                    n.visitado = false;

                if (n.Nivel <= limite && n.visitado==false)
                {
                        l.Add(n);
                        
                        foreach (Edge e in n.Edges)
                        {
                            p.Push(e.To);

                            if (n.Nivel < limite )
                            {
                                e.To.Nivel = n.Nivel + 1;
                                n.visitado = true;
                            }
                        }
                }
            }
            return montaRespostaFinal(l)+"|";
        }


        #endregion


        public string EncontraEulerianoLargura()
        {
            throw new NotImplementedException();
        }
    }
}


/* 
 * 
 1)	a)
public class Node{
	private char carneiro;//E ou D
	private char barco;//E ou D
	private int fe, fd;
	}
	b) Ações: ‘C’, ‘F’,’N’
	public List<char> Acoes(Node n){
		List<char> acoes  = new List<char>();
		Acoes.Add(‘N’);
		If(barco==carneiro)
			Acoes.Add(‘C’);
		If(  (barco==’E’ && fe>=1) || (barco==’D’&&fd>=1))
			Acoes.Add(‘F’);
	}
	c)
	public Node Sucessor(Node n, char acao){
		Node novo = new Node();
		novo.Barco = (n.Barco==’E’?’D’:’E’);
		novo.Carneiro = n.Carneiro;
		novo.FE = n.FE;
		novo.FD = n.FD;
		if(acao=’C’)
			novo.Carneiro = (n.Carneiro==’E’?’D’:’E’);
		else if(acao=’F’)
		{
			if(n.Barco==’E’)
				novo.FE--; novo.FD++;
			else
				novo.FD--; novo.FE++;
		}
		return novo;
	}
2. a)
	A,[G,B,D],[H,C,E],I,J,K
1,	2, 3, 4, 5
b) D,E,C,B,A,G,H
    0, 1, 2, 3, 1, 2, 3
c) A
    A,G,B,D 
    A,G,H,B,C,D,E 
   A,G,H,I,B,C,D
   A,G,H,I,J,B,C,D,E
   A,G,H,I,J,K,B,C,D,E
d)
 
3)
public void Profundidade(Node nO, int limite)
{
	Stack<Node> p = new Stack<Node>();
	p.Push(nO);
	nO.Nivel = 0;
	while(P.Count() > 0)
	{
		Node n = P.Pop();
		if(n.Nivel <= limite)
			foreach(Edge e in n.Edges)
			{
				if(!e.To.Visited)
				{
					P.Push(e.To);
					e.To.Nivel = n.Nivel++;
					e.To.Visited = true;
				}
			}
	}
	Profundidade(nO, limite+1);
}


 
 * 
 * 
 
 */