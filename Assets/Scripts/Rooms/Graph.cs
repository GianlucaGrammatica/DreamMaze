using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEditor.Experimental.GraphView;
public class Node
    {
        public int ID;
        public Node[] nodes = new Node[4] { null, null, null, null };

        public Node(int id)
        {
            ID = id;

        }
    }

    public class Graph
    {
        public int VerticesNumber { get; set; }
        private int[,] BaseMatrix;
        private List<Node> Grafo = new List<Node>();

        public Node FirstNode;
        public Node LastNode;

        private System.Random random;


    // Costruttore
    public Graph(int vertices)
        {

            VerticesNumber = vertices;
            BaseMatrix = new int[VerticesNumber, VerticesNumber];


            for (int i = 0; i < VerticesNumber; i++)
            {
                Node nodo = new Node(i);
                Grafo.Add(nodo);

                for (int j = 0; j < VerticesNumber; j++)
                {
                    BaseMatrix[i, j] = 0;
                }
            }

            FirstNode = Grafo[0];
            LastNode = Grafo[VerticesNumber - 1];

            random = new System.Random();
        }

        // Matrice Casuale
        public void GenerateMatrix()
        {
            int[] connections = new int[VerticesNumber];

            for (int i = 0; i < VerticesNumber; i++)
            {
                for (int j = 0; j < VerticesNumber; j++)
                {
                    // Salta collegamento tra inizio e fine
                    if (i == 0 && j == VerticesNumber - 1)
                    {
                        continue;
                    }

                    // Salta collegamento a se stesso
                    if (i == j)
                    {
                        continue;
                    }

                    // Controlla se non ci sono troppe connessioni e genera casualmente 0 o 1 -> se 1 inserisci
                    if (connections[i] < 4 && connections[j] < 4 && random.NextDouble() < 0.15)
                    {
                        BaseMatrix[i, j] = 1;
                        BaseMatrix[j, i] = 1;

                        // Aumento counter connessioni
                        connections[i]++;
                        connections[j]++;
                    }
                }
            }

            if (CountPaths(0, VerticesNumber - 1) < 2)
            {
                AddRandomEdge(random.Next(1, 3));
            }
        }

        // Calcolo numero strade possibili
        public int CountPaths(int start, int end)
        {
            bool[] visited = new bool[VerticesNumber];
            int pathCount = 0;

            DFS(start, end, visited, ref pathCount);
            return pathCount;
        }

        // DFS
        private void DFS(int node, int end, bool[] visited, ref int pathCount)
        {
            // Ha trovato una strada
            if (node == end)
            {
                pathCount++;
                return;
            }

            visited[node] = true;

            for (int i = 0; i < VerticesNumber; i++)
            {
                if (BaseMatrix[node, i] == 1 && !visited[i])
                {
                    // Nodo successivo
                    DFS(i, end, visited, ref pathCount);
                }
            }

            // Mette a false per altre strade
            visited[node] = false;
        }

        // Aggiunta Coollegamneti casuali
        public void AddRandomEdge(int count)
        {
            int edgesAdded = 0;

            while (edgesAdded < count)
            {
                int node1 = random.Next(1, VerticesNumber - 1);
                int node2 = random.Next(1, VerticesNumber - 1);

                // Evita auto-collegamenti
                while (node1 == node2)
                {
                    node2 = random.Next(1, VerticesNumber - 1);
                }

                // Se i nodi non sono già collegati, aggiungi il collegamento
                if (BaseMatrix[node1, node2] == 0)
                {
                    AddEdge(node1, node2);
                    edgesAdded++;
                }
            }
        }


        // Aggiunta
        public void AddEdge(int Vertex1, int Vertex2)
        {
            if (isValidVertex(Vertex1) && isValidVertex(Vertex2))
            {
                this.BaseMatrix[Vertex1, Vertex2] = 1;
                this.BaseMatrix[Vertex2, Vertex1] = 1;
            }
        }

        // Rimozione
        public void RemoveEdge(int Vertex1, int Vertex2)
        {
            if (isValidVertex(Vertex1) && isValidVertex(Vertex2))
            {
                this.BaseMatrix[Vertex1, Vertex2] = 0;
                this.BaseMatrix[Vertex2, Vertex1] = 0;
            }
        }

    public List<(int, int)> GetEdges()
    {
        List<(int, int)> edges = new List<(int, int)>();

        for (int i = 0; i < VerticesNumber; i++)
        {
            for (int j = i + 1; j < VerticesNumber; j++) // Evita duplicati
            {
                if (BaseMatrix[i, j] == 1)
                {
                    edges.Add((i, j));
                }
            }
        }
        return edges;
    }



    // Validazione
    private bool isValidVertex(int Vertex)
        {
            return (Vertex >= 0 && Vertex < this.VerticesNumber);
        }

        // Stampa della matrice
        public void Stampa()
        {
            for (int i = 0; i < VerticesNumber; i++)
            {
                for (int j = 0; j < VerticesNumber; j++)
                {
                    //Console.Write(BaseMatrix[i, j] + " ");
                }
                //Console.WriteLine();
            }
        }

        private void PopulateList()
        {
            for (int i = 0; i < VerticesNumber; i++)
            {
                Node nodo = new Node(i);
                int count = 0;
                for (int j = 0; j < VerticesNumber; j++)
                {
                    if (BaseMatrix[i, j] != 0)
                    {
                        nodo.nodes[count] = Grafo[j];
                        count++;
                    }
                }
            }
        }
        // nella matrice segnero che 00 e nn sono i finali e iniziali quindi 
        // ci saranno n-2 nodi che creero

        /*
        inizio e fine non possono essere direttamente collegati
        non posso avere due porte che portano allo stesso posto, gli archi detti N12 dzevono essere univoci, quindi nell’inserimento guardo metà della matrice di adiacenza  perché speculare sulla diagonale
        dev’esserci più di un percorso possibile che porti allo stesso posto
        la differenza tra i vari percorsi dev’essere minore di 3, la differenza maggiore e il minore deve essere maggiore di 3
        per il maggiore tolgo i noti
        creare i e b, che non si colleghino, fai il dijkstra
        la lista dei nodi, sarà una list, sarà all

        NodiChiusi, lista dei nodi, al''inizio tutti sono chiusi
        NodiScoperti, lista di nodi che hanno dei collegamenti
        per generare il percorso che porta dall'inizio alla fine random
        dall'inizio, va avanti e trova tutti quelli che sono adiacenti al percorso e quando cercando trova che il nodo è presente dentro nodi scoperti 
        va e lo ferma, cosi che c'è un vicolo cieco
        quando inizia a cercare 
        dopo che trova i percorsi arancioni cerca, i successivi a quelli che non portano da nessuna parte 
        è una funzione ricorsiva affinche non ci sono piu nodi da scoprire

        seconda opzione:
        ogni nodo è una stanza che è uguale
        piu di 12 13 nodi se nodi 
        con almeno 
        */
    }
