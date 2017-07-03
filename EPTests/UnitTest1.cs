using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EP;
using ProjetoGrafos.DataStructure;
using System.Collections.Generic;

namespace EPTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int a = 1 + 1;
            Assert.AreEqual(2, a);
        }
        [TestMethod]
        public void TestaGrafo()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddEdge("A", "B", 1);
            g.AddEdge("B", "C", 1);
            g.AddEdge("C", "D", 1);
            g.AddEdge("D", "A", 1);

            string[] caminho1 = { "A", "B", "C" };
            Node[] n = new Node[100];
            Assert.AreEqual(true, g.IsValidPath(ref n, caminho1));

            string[] caminho2 = { "A", "B", "D" };
            Assert.AreEqual(false, g.IsValidPath(ref n, caminho2));
        }
        [TestMethod]
        public void TestaHamilton()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddEdge("A", "B", 1);
            g.AddEdge("B", "C", 1);
            g.AddEdge("C", "D", 1);
            g.AddEdge("D", "A", 1);
            Assert.AreEqual(true, g.Hamiltonian());

            g.AddNode("E");
            g.AddNode("F");
            g.AddEdge("C", "E", 1);
            g.AddEdge("B", "F", 1);
            Assert.AreEqual(false, g.Hamiltonian());
        }
        [TestMethod]
        public void TesteConexo()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddEdge("A", "B");
            g.AddEdge("B", "C");
            g.AddEdge("C", "D");
            g.AddEdge("D", "A");

            Assert.AreEqual(true, g.Conexo());

            g.AddEdge("A", "D");
            g.RemoverArco("D", "A");
            Assert.AreEqual(false, g.Conexo());
        }
        [TestMethod]
        public void TesteMenorCaminho()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddEdge("A", "D");
            g.AddEdge("A", "C");
            g.AddEdge("C", "D");
            g.AddEdge("D", "C");
            g.AddEdge("D", "B");

            Assert.AreEqual("A,D,B", g.MenorCaminho("A", "B"));

            g.RemoverArco("A", "D");

            Assert.AreEqual("A,C,D,B", g.MenorCaminho("A", "B"));

            g.RemoverArco("D", "B");
            Assert.AreEqual("", g.MenorCaminho("A", "B"));
        }
        [TestMethod]
        public void TesteNaoDirigido()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddEdge("A", "C");
            g.AddEdge("C", "A");
            g.AddEdge("B", "C");
            g.AddEdge("C", "B");

            Assert.AreEqual(false, g.EhDirigido());

            g.RemoverArco("C", "A");

            Assert.AreEqual(true, g.EhDirigido());
        }
        [TestMethod]
        public void TesteNivel()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddEdge("A", "D");
            g.AddEdge("A", "C");
            g.AddEdge("C", "D");
            g.AddEdge("D", "C");
            g.AddEdge("D", "B");

            Assert.AreEqual(2, g.Nivel("A", "B"));

            g.RemoverArco("A", "D");

            Assert.AreEqual(3, g.Nivel("A", "B"));
        }
        [TestMethod]
        public void TesteNivel_2()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddNode("E");
            g.AddNode("F");
            g.AddNode("G");
            g.AddNode("H");
            g.AddEdge("A", "D");
            g.AddEdge("A", "C");
            g.AddEdge("C", "E");
            g.AddEdge("D", "F");
            g.AddEdge("D", "B");
            g.AddEdge("D", "H");
            g.AddEdge("C", "A");
            g.AddEdge("H", "B");

            Assert.AreEqual("B,E,F,H", g.NodesNivel(2, "A"));
        }
        [TestMethod]
        public void TesteRemoveNo()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddEdge("A", "D");
            g.AddEdge("A", "C");
            g.AddEdge("C", "D");
            g.AddEdge("D", "C");
            g.AddEdge("D", "B");
            Assert.AreEqual("D", g.edges[0].To.Name);

            g.RemoveNode("D");
            Assert.AreEqual("C", g.edges[0].To.Name);
        }
        /*
      [TestMethod]
      public void TesteRegioes() //Só funciona se não houver arcos cruzados
      {
          Graph g = new Graph();
          g.AddNode("A");
          g.AddNode("B");
          g.AddNode("C");
          g.AddNode("D");
          g.AddEdge("A", "B");
          g.AddEdge("B", "C");
          g.AddEdge("C", "D");
          g.AddEdge("D", "A");
          g.AddEdge("A", "C");
          Assert.AreEqual(3, g.CalcRegiao());
            
          g.AddNode("E");
          g.AddNode("F");
          g.AddNode("G");
          g.AddNode("H");
          g.AddEdge("E", "F");
          g.AddEdge("F", "G");
          g.AddEdge("G", "H");
          g.AddEdge("H", "E");
          g.AddEdge("E", "G");
          Assert.AreEqual(5, g.CalcRegiao());

          g.AddEdge("E", "A");
          g.AddEdge("B", "F");
          Assert.AreEqual(6, g.CalcRegiao());

          g.AddEdge("E", "B");
          Assert.AreEqual(7, g.CalcRegiao());
      }*/
        [TestMethod]
        public void TesteNaoDirigido_2()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddEdge("A", "B");
            g.AddEdge("B", "A");
            g.AddEdge("B", "C");
            g.AddEdge("C", "D");
            g.AddEdge("D", "A");
            Assert.AreEqual(true, g.EhDirigido());

            g.ConverteNaoDirigido();
            Assert.AreEqual(false, g.EhDirigido());
        }
        [TestMethod]
        public void TesteRegioesConexas()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddNode("E");
            g.AddNode("F");
            g.AddNode("G");
            g.AddNode("H");
            g.AddEdge("A", "B");
            g.AddEdge("B", "C");
            g.AddEdge("C", "D");
            g.AddEdge("D", "A");
            g.AddEdge("A", "C");
            g.AddEdge("E", "F");
            g.AddEdge("F", "G");
            g.AddEdge("G", "H");
            g.AddEdge("H", "E");
            Assert.AreEqual("{A,B,C,D}{E,F,G,H}", g.RegioesConexas());

            g.RemoverArco("H", "E");
            Assert.AreEqual("{A,B,C,D}", g.RegioesConexas());

            g.AddEdge("G", "E");
            //    Assert.AreEqual("{A,B,C,D}{E,F,G,H}", g.RegioesConexas());
        }
        
              [TestMethod]
              public void TesteEuleriano()
              {
                  Graph g = new Graph();
                  g.AddNode("A");
                  g.AddNode("B");
                  g.AddNode("C");
                  g.AddNode("D");
                  g.AddEdge("A", "B");
                  g.AddEdge("B", "C");
                  g.AddEdge("C", "D");
                  g.AddEdge("D", "A");
                  g.AddEdge("A", "C");

                  string lll = g.EncontraEulerianoLargura();
            
                  Assert.AreEqual("C,B,A,D,C,A", g.EncontraEulerianoLargura());

                  g.RemoverArco("A", "C");
                  string ashu = g.EncontraEulerianoLargura();
                  Assert.AreEqual("A,B,C,D,A", g.EncontraEulerianoLargura());

                  g.AddEdge("A", "C");
                  g.AddEdge("B", "D");
                  Assert.AreEqual("", g.EncontraEulerianoLargura());
              }
                


        /// <summary>
        /// Criado para a prova. Cria um Graph usado por todas as questões. 
        /// </summary>
        private Graph CriaGraphProva()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddNode("E");
            g.AddNode("F");
            g.AddNode("G");
            g.AddNode("H");
            g.AddNode("I");
            g.AddEdge("A", "B");
            g.AddEdge("A", "D");
            g.AddEdge("A", "H");
            g.AddEdge("B", "F");
            g.AddEdge("C", "D");
            g.AddEdge("D", "E");
            g.AddEdge("D", "G");
            g.AddEdge("E", "A");
            g.AddEdge("E", "C");
            g.AddEdge("E", "I");
            g.AddEdge("F", "G");
            g.AddEdge("G", "A");
            g.AddEdge("G", "C");
            g.AddEdge("H", "B");
            g.AddEdge("H", "I");
            g.AddEdge("I", "B");
            return g;
        }
        /// <summary>
        /// Teste do método remover arco, capaz de remover um arco do Graph. 
        /// Vale 2 pontos. 
        /// </summary>
        [TestMethod]
        public void Prova_2P_TesteArcos()
        {
            Graph g = CriaGraphProva();
            Assert.AreEqual(true, g.EhVizinho("I", "H"));
            g.RemoverArco("H", "I");
            Assert.AreEqual(false, g.EhVizinho("I", "H"));
        }
        /// <summary>
        /// Teste do método MenorCaminho, capaz de retornar o menor caminho em um Graph SEM PESOS NOS ARCOS. 
        /// Vale 4 pontos. 
        /// </summary>
        /// 
        [TestMethod]
        public void Prova_4P_TesteExisteCaminho()
        {
            Graph g = CriaGraphProva();
            Assert.AreEqual(true, g.ExisteCaminho("A", "I"));
            Assert.AreEqual(true, g.ExisteCaminho("B", "E"));
            g.RemoverArco("E", "I");
            Assert.AreEqual(false, g.ExisteCaminho("A", "K"));
        }
        /// <summary>
        /// Teste do método ExisteCiclo, capaz de identificar se um Graph possui ou não algum ciclo.
        /// Vale 4 pontos. 
        /// </summary>
        [TestMethod]
        public void Prova_4P_TesteCiclico()
        {
            Graph g = CriaGraphProva();
            Assert.AreEqual(true, g.ExisteCiclo());
            g.RemoverArco("G", "A");
            g.RemoverArco("E", "A");
            g.RemoverArco("D", "G");
            g.RemoverArco("E", "C");
            g.RemoverArco("E", "I");
            Assert.AreEqual(false, g.ExisteCiclo());
        }
        private bool CompareVector(int[] v1, int[] v2)
        {
            if (v1.Length != v2.Length) return false;
            for (int i = 0; i < v1.Length; i++)
            {
                if (v1[i] != v2[i]) return false;
            }
            return true;
        }
        [TestMethod]
        public void TesteFacinho()
        {
            // teste 1
            EightPuzzle ComputerAgent = new EightPuzzle(new int[] { 1, 0, 2, 3, 4, 5, 6, 7, 8 },
                new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            int[] sol = ComputerAgent.GetSolution();
            Assert.AreEqual(true, CompareVector(new int[] { 1 }, sol));
        }
        [TestMethod]
        public void TesteMedio()
        {
            // teste 2
            EightPuzzle ComputerAgent = new EightPuzzle(new int[] { 1, 2, 5, 7, 0, 4, 3, 6, 8 },
                new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            int[] sol = ComputerAgent.GetSolution();
            Assert.AreEqual(true, resolveProblem(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, new int[] { 1, 2, 5, 7, 0, 4, 3, 6, 8 }, sol));
        }
        /*


        [TestMethod]
        public void TesteNinja()
        {

            EightPuzzle ComputerAgent = new EightPuzzle(new int[] { 8, 0, 6, 5, 4, 7, 2, 3, 1 },
                new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            int[] sol = ComputerAgent.GetSolution();
            Assert.AreEqual(true, resolveProblem(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, new int[] { 8, 0, 6, 5, 4, 7, 2, 3, 1 }, sol));
        }
        */
        private bool resolveProblem(int[] target, int[] initial, int[] solution)
        {
            int[] resolving = (int[])initial.Clone();
            for (int i = 0; i < solution.Length; i++)
            {
                int de = FindSpace(resolving);
                int para = FindNumber(resolving, solution[i]);

                resolving = GeraFilhos(resolving, de, para);
            }

            return CompareVector(target, resolving);
        }
        private int[] GeraFilhos(int[] estado, int antes, int depois)
        {
            int[] aux = (int[])estado.Clone();
            aux[antes] = aux[depois];
            aux[depois] = 0;
            return aux;
        }
        private int FindSpace(int[] state)
        {
            for (int i = 0; i < 9; i++)
            {
                if (state[i] == 0)
                    return i;
            }
            return -1;
        }
        private int FindNumber(int[] state, int number)
        {
            for (int i = 0; i < 9; i++)
            {
                if (state[i] == number)
                    return i;
            }
            return -1;
        }
        [TestMethod]
        public void TesteArco()
        {
            Edge a = new Edge();
            a.Cost = 3;
          //  a.From = new Node("A");
            Assert.AreEqual(3, a.Cost);
        }
        [TestMethod]
        public void TesteNo()
        {
            Node n1 = new Node("A");
            Node n2 = new Node("B");
            n1.AddEdge(n2);
            Assert.AreEqual("B", n1.Edges[0].To.Name);
        }
        [TestMethod]
        public void TesteGrafo()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddNode("E");
            g.AddEdge("A", "B");
            g.AddEdge("A", "E");
            g.AddEdge("B", "D");
            g.AddEdge("C", "A");
            g.AddEdge("C", "B");
            g.AddEdge("E", "C");
            g.AddEdge("E", "D");
            Assert.AreEqual(false, g.EhVizinhoDirigido("B", "A"));
            Assert.AreEqual(true, g.EhVizinhoDirigido("A", "B"));
            Assert.AreEqual(false, g.EhVizinhoDirigido("A", "C"));
        }
        [TestMethod]
        public void TesteCaminho()
        {
            Graph g = new Graph();
            g.AddNode("A");
            g.AddNode("B");
            g.AddNode("C");
            g.AddNode("D");
            g.AddNode("E");
            g.AddEdge("A", "B");
            g.AddEdge("A", "E");
            g.AddEdge("B", "D");
            g.AddEdge("C", "A");
            g.AddEdge("C", "B");
            g.AddEdge("E", "C");
            g.AddEdge("E", "D");
            g.AddEdge("D", "C");

            Node[] n = new Node[100];

            string[] caso1 = { "A", "B", "D", "C"};
            Assert.AreEqual(true, g.IsValidPath(ref n,caso1));
        
            string[] caso2 = { "A", "B", "E"};
            Assert.AreEqual(false, g.IsValidPath(ref n, caso2));

            string[] caso3 = { "A", "B", "D", "C", "A" };
            Assert.AreEqual(true, g.IsValidPath(ref n, caso3));
        }
        [TestMethod]
        public void TestePasseioLargura()
            {
                Graph g = new Graph();
                g.AddNode("A0");
                int cj = 0;
                for (int i = 0; i < 5; i++)
                {
                    string nome_i = "B" + i.ToString();
                    g.AddNode(nome_i);
                    g.AddEdge("A0", nome_i);

                    for (int j = 0; j < 5; j++)
                    {
                        string nome_j = "C" + cj.ToString();
                        g.AddNode(nome_j);
                        g.AddEdge(nome_i, nome_j);
                        cj++;
                    }
                }
                string final = "A0,";
                for (int i = 0; i < 5; i++)
                    final += "B" + i.ToString() + ",";
                for (int i = 0; i < 25; i++)
                    final += "C" + i.ToString() + ",";
                final += ",";
                final = final.Replace(",,", "");
                Assert.AreEqual(final, g.passeioLarguraString("A0"));
            }

        

         /// <summary>
         /// Criado para a prova. Cria um grafo usado por algumas questões. 
         /// </summary>
         private Graph CriaGrafoProva(bool direcionado)
         {
             Graph g = new Graph(direcionado);
             g.AddNode("A");
             g.AddNode("B");
             g.AddNode("C");
             g.AddNode("D");
             g.AddNode("E");
             g.AddNode("F");
             g.AddNode("G");
             g.AddEdge("A", "B");
             g.AddEdge("B", "F");
             g.AddEdge("C", "D");
             g.AddEdge("D", "E");
             g.AddEdge("E", "A");
             g.AddEdge("F", "G");
             g.AddEdge("G", "C");
             g.AddEdge("A", "C");
             g.AddEdge("D", "B");
             return g;
         }

         /// <summary>
         /// Criado para a prova. Cria um Graph usado por algumas questões. 
         /// </summary>
         private Graph CriaGrafoCompleto(bool direcionado)
         {
             Graph g = new Graph(direcionado);
             g.AddNode("A");
             g.AddNode("B");
             g.AddNode("C");
             g.AddNode("D");
             g.AddEdge("A", "B");
             g.AddEdge("A", "C");
             g.AddEdge("A", "D");
             g.AddEdge("B", "A");
             g.AddEdge("B", "C");
             g.AddEdge("B", "D");
             g.AddEdge("C", "A");
             g.AddEdge("C", "B");
             g.AddEdge("C", "D");
             g.AddEdge("D", "A");
             g.AddEdge("D", "B");
             return g;
         }
      
        /// <summary>
        /// Teste do construtor especial para criar grafos não direcionados (parâmetro direcionado = false). 
        /// Vale 2 pontos. 
        /// </summary>
        [TestMethod]
        public void Prova_2P_TesteNaoDirecionado()
        {
            Node[] n = new Node[100];
            Graph g = CriaGrafoProva(false);
            g.ConverteNaoDirigido();
            string[] caso1 = { "A", "B", "A", "E", "D" };
            Assert.AreEqual(true, g.IsValidPath(ref n, caso1));
            string[] caso2 = { "A", "B", "E", "C" };
            Assert.AreEqual(false, g.IsValidPath(ref n, caso2));
        }
        

      /// <summary>
      /// Em grafos não direcionados a soma dos graus de todos os vértices é igual ao dobro do número de arestas.
      /// A função VerificaConsistencia avalia se o grafo atende esse requisito. 
      /// Vale 2 pontos. 
      /// </summary>
      [TestMethod]
      public void Prova_3P_TesteConsistencia()
      {
          Graph g = CriaGrafoProva(false);
          g.ConverteNaoDirigido();
          bool lol = g.VerificaConsistencia();
          Assert.AreEqual(true, g.VerificaConsistencia());
          g.AddNode("Z");
          g.Find("Z").Edges.Add(new Edge());
          Assert.AreEqual(false, g.VerificaConsistencia());
      }
        
      /// <summary>
      /// Teste do método GrafoCompleto. Grafo completo é o grafo simples em que, para cada vértice do grafo, 
      /// existe uma aresta conectando este vértice a cada um dos demais. Ou seja, todos os vértices do grafo 
      /// possuem mesmo grau. 
      /// Vale 2 pontos. 
      /// </summary>
      [TestMethod]
      public void Prova_2P_TesteGrafoCompleto()
      {
          Graph g = CriaGrafoCompleto(true);
          Assert.AreEqual(false, g.GrafoCompleto());
          g.AddEdge("D", "C");
          Assert.AreEqual(true, g.GrafoCompleto());
      }
        
      /// <summary>
      /// Verifica quantos caminhos possíveis existem entre dois nós de um grafo.
      /// Vale 3 pontos. 
      /// </summary>
      /// 
   /*   [TestMethod]
      public void Prova_3P_CaminhosPossiveis()
      {
          Graph g = CriaGrafoProva(false);
          g.ConverteNaoDirigido();
          int a = g.CaminhosPossiveis("A", "F");
          Assert.AreEqual(6, g.CaminhosPossiveis("A", "F"));
          Assert.AreEqual(5, g.CaminhosPossiveis("A", "B"));
      }
*/


      
          // Criando o grafo
          private Graph CriaGrafo()
          {
              Graph grafo = new Graph();

              // Adicionando nós
              for (int i = 0; i < 10; i++)
              {
                  for (int j = 0; j < 10; j++)
                  {
                      if (i != j || i < 2)
                      {
                          string name = "N" + i.ToString() + "-" + j.ToString();
                          if (i % 2 == 0 || j % 2 == 0)
                              grafo.AddNode(name, "L");
                          else
                              grafo.AddNode(name, "");

                      }
                  }
              }

              // Adicionando arcos
              for (int i = 0; i < 10; i++)
              {
                  for (int j = 0; j < 10; j++)
                  {
                      if (i != j || i < 2)
                      {
                          string nameFrom = "N" + i.ToString() + "-" + j.ToString();
                          // Direções
                          Edge[] directions = new Edge[4];
                          directions[0] = new Edge(i - 1, j);
                          directions[1] = new Edge(i + 1, j);
                          directions[2] = new Edge(i, j - 1);
                          directions[3] = new Edge(i, j + 1);
                          foreach (Edge p in directions)
                          {
                              if (p.X >= 0 && p.Y >= 0 && p.X < 10 && p.Y < 10)
                              {
                                  try
                                  {
                                      string nameTo = "N" + (p.X.ToString() + "-" + p.Y.ToString());
                                      if (i % 2 == 0 || j % 2 == 0)
                                      {
                                          grafo.AddEdge(nameFrom, nameTo, 3);
                                      }
                                      else
                                      {
                                          grafo.AddEdge(nameFrom, nameTo, 1);
                                      }
                                  }
                                  catch { }
                              }
                          }
                      }
                  }
              }
              

              return grafo;

          }

          [TestMethod]
          // Distancia Geodésica entre dois nós é o tamanho do menor caminho entre eles
          public void P3_TesteDistanciaGeodesica()
          {
              Graph g = CriaGrafo();
              Assert.AreEqual(3, g.Geodesica("N0-0", "N0-3"));
              Assert.AreEqual(17, g.Geodesica("N0-0", "N9-8"));
              Assert.AreEqual(30, g.Geodesica("N8-9", "N9-8"));
          }
  
         [TestMethod]
         // Excentricidade de um nó é a maior distância geodésica entre o nó e qualquer outro do grafo
         public void P3_TesteExcentricidade()
         {
             Graph g = CriaGrafo();
             Assert.AreEqual(17, g.Excentricidade("N0-0"));
             Assert.AreEqual(28, g.Excentricidade("N7-8"));
             Assert.AreEqual(21, g.Excentricidade("N3-5"));
         }
      
         [TestMethod]
         // O diametro de um grafo é sua excentricidade máxima, e o raio é sua excentricidade mínima
         public void P4_TesteDiametroRaio()
         {
             Graph g = CriaGrafo();
             Assert.AreEqual(30, g.Diametro());
             Assert.AreEqual(15, g.Raio());
         }
        
    
      [TestMethod]
         // Implementar uma solução simples capaz de limpar todos os arcos de um nó 
         public void P2_LimparArcos()
         {
             Node nA = new Node("A", null);
             Node nB = new Node("B", null);
             nA.AddEdge(nB);
             nA.AddEdge(nA);
             Assert.AreEqual(2, nA.Edges.Count);
             nA.LimparArcos();
             Assert.AreEqual(0, nA.Edges.Count);
         }
       
   
         [TestMethod]
         // O método CaminhoMinimoNegativo encontra o menor caminho entre dois nós sendo que no grafo podem 
         // existir arcos com PESO NEGATIVO
         public void P3_MinimoNegativo()
         {
             Graph grafo = new Graph();
             grafo.AddNode("A");
             grafo.AddNode("B");
             grafo.AddNode("C");
             grafo.AddEdge("A", "B", 3);
             grafo.AddEdge("A", "C", 4);
             grafo.AddEdge("C", "B", -2);

             Assert.AreEqual("A,C,B", grafo.CaminhoMinimoNegativo("A", "B"));
         }


     [TestMethod]
     // O método CaminhoMinimoNegativo encontra o menor caminho entre dois nós sendo que no grafo podem 
     // existir arcos com PESO NEGATIVO
     public void P3_MinimoNegativoDois()
     {
         Graph grafo = new Graph();
         grafo.AddNode("A");
         grafo.AddNode("B");
         grafo.AddNode("C");
         grafo.AddNode("D");
         grafo.AddNode("E");
         grafo.AddEdge("A", "C", 4);
         grafo.AddEdge("C", "B", -2);
         grafo.AddEdge("B", "D", 8);
         grafo.AddEdge("D", "E", -12);
         grafo.AddEdge("E", "C", 3);

         Assert.AreEqual("A,C,B", grafo.CaminhoMinimoNegativo("A", "B"));
     }

    /*
    [TestMethod]
    // O método CaminhoMinimoNegativo encontra o menor caminho entre dois nós sendo que no grafo podem 
    // existir arcos com PESO NEGATIVO
    public void P2_MinimoNegativoTres()
    {
        Grafo grafo = new Grafo();
        grafo.AddNode("A");
        grafo.AddNode("B");
        grafo.AddNode("C");
        grafo.AddNode("D");
        grafo.AddNode("E");
        grafo.AddEdge("A", "C", 4);
        grafo.AddEdge("C", "B", -2);
        grafo.AddEdge("B", "D", 8);
        grafo.AddEdge("D", "E", -12);
        grafo.AddEdge("E", "C", 3);

        Assert.AreEqual("A;C;B;D;E;", grafo.CaminhoMinimoNegativo("A", "E"));
        } 
      */

     [TestMethod]
     public void TesteIterativo()
     {
         Graph g = new Graph();
         g.AddNode("A");
         g.AddNode("B");
         g.AddNode("C");
         g.AddNode("D");
         g.AddNode("E");
         g.AddNode("F");
         g.AddEdge("A", "B");
         g.AddEdge("A", "C");
         g.AddEdge("B", "D");
         g.AddEdge("B", "E");
         g.AddEdge("C", "F");
     //    g.ConverteNaoDirigido();

         Assert.AreEqual("A|A,C,B|A,C,F,B,E,D|", g.profundidadeIterativa2(g.Nodes[0], 0));
     }

     [TestMethod]
     public void TesteIterativo2()
     {
         Graph g = new Graph();
         g.AddNode("A");
         g.AddNode("B");
         g.AddNode("C");
         g.AddNode("D");
         g.AddNode("E");
         g.AddNode("F");
         g.AddNode("G");
         g.AddNode("H");
         g.AddNode("I");
         g.AddNode("J");
         g.AddNode("K");
         g.AddEdge("A", "D");
         g.AddEdge("A", "B");
         g.AddEdge("A", "G");
         g.AddEdge("B", "A");
         g.AddEdge("B", "C");
         g.AddEdge("C", "B");
         g.AddEdge("C", "D");
         g.AddEdge("D", "A");
         g.AddEdge("D", "C");
         g.AddEdge("D", "E");
         g.AddEdge("E", "C");
         g.AddEdge("F", "C");
         g.AddEdge("G", "H");
         g.AddEdge("H", "I");
         g.AddEdge("I", "J");
         g.AddEdge("J", "K");


         Assert.AreEqual("A|A,G,B,D|A,G,H,B,C,D,E|A,G,H,I,B,C,D,E|A,G,H,I,J,B,C,D,E|A,G,H,I,J,K,B,C,D,E|", g.profundidadeIterativa2(g.Nodes[0], 0));
     }

    }
}

