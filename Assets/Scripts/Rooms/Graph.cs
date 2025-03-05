using UnityEngine;
using System.Collections.Generic;

public class Node
{
    public int ID;
    public Node[] nodes = new Node[4] { null, null, null, null };

    public Node(int id)
    {
        ID = id;
    }
}

public class Graph : MonoBehaviour
{
    public int VerticesNumber = 5;                    // 🆕 Numero di nodi, configurabile da Unity
    private int[,] BaseMatrix;                        // Matrice di adiacenza
    private List<Node> Grafo = new List<Node>();      // Lista di nodi
    public List<Node> ConnectedNodes = new List<Node>();  // 🆕 Lista pubblica dei nodi collegati

    public Node FirstNode;
    public Node LastNode;

    private System.Random random;

    void Start()
    {
        
    }

    // 🆕 Inizializza il grafo con un numero di nodi specificato
    public void InitializeGraph(int vertices)
    {
        VerticesNumber = vertices;
        BaseMatrix = new int[VerticesNumber, VerticesNumber];
        random = new System.Random();

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

        Debug.Log($"Grafo inizializzato con {vertices} nodi - " + BaseMatrix);
    }

    // 🆕 Ottiene i collegamenti di un nodo specifico
    public List<int> GetConnections(int nodeID)
    {
        Debug.Log($"Richiamato GetConnections");
        Debug.Log("NodeID: "+ nodeID);
        List<int> connections = new List<int>();

        Debug.Log("NAAAAA: " + BaseMatrix[0, 0]);
        for (int j = 0; j < VerticesNumber; j++)
        {
            
            if (BaseMatrix[nodeID, j] == 1)
            {
                connections.Add(j);
            }
        }
        return connections;
    }

    // Genera la matrice di adiacenza con collegamenti casuali
    public void GenerateMatrix()
    {
        int[] connections = new int[VerticesNumber];

        for (int i = 0; i < VerticesNumber; i++)
        {
            for (int j = 0; j < VerticesNumber; j++)
            {
                if (i == 0 && j == VerticesNumber - 1) continue;  // Evita collegamenti diretti tra inizio e fine
                if (i == j) continue;  // Evita autocollegamenti

                if (connections[i] < 4 && connections[j] < 4 && random.NextDouble() < 0.15)
                {
                    BaseMatrix[i, j] = 1;
                    BaseMatrix[j, i] = 1;
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

    // Conta i percorsi possibili tra due nodi
    public int CountPaths(int start, int end)
    {
        bool[] visited = new bool[VerticesNumber];
        int pathCount = 0;
        DFS(start, end, visited, ref pathCount);
        return pathCount;
    }

    // Ricerca in profondità per contare i percorsi
    private void DFS(int node, int end, bool[] visited, ref int pathCount)
    {
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
                DFS(i, end, visited, ref pathCount);
            }
        }

        visited[node] = false;
    }

    // Aggiunge collegamenti casuali
    public void AddRandomEdge(int count)
    {
        int edgesAdded = 0;

        while (edgesAdded < count)
        {
            int node1 = random.Next(1, VerticesNumber - 1);
            int node2 = random.Next(1, VerticesNumber - 1);

            while (node1 == node2)
            {
                node2 = random.Next(1, VerticesNumber - 1);
            }

            if (BaseMatrix[node1, node2] == 0)
            {
                AddEdge(node1, node2);
                edgesAdded++;
            }
        }
    }

    // Aggiunge un collegamento tra due nodi
    public void AddEdge(int Vertex1, int Vertex2)
    {
        if (isValidVertex(Vertex1) && isValidVertex(Vertex2))
        {
            BaseMatrix[Vertex1, Vertex2] = 1;
            BaseMatrix[Vertex2, Vertex1] = 1;
        }
    }

    // Valida un nodo
    private bool isValidVertex(int Vertex)
    {
        return (Vertex >= 0 && Vertex < VerticesNumber);
    }

    // 🆕 Popola la lista dei nodi collegati
    public void PopulateList()
    {
        ConnectedNodes.Clear();

        for (int i = 0; i < VerticesNumber; i++)
        {
            Node nodo = Grafo[i];
            int count = 0;

            for (int j = 0; j < VerticesNumber; j++)
            {
                if (BaseMatrix[i, j] != 0 && Grafo[j] != null)
                {
                    nodo.nodes[count] = Grafo[j];
                    count++;
                }
            }

            // Rimuove eventuali null rimanenti negli slot dell'array
            for (int k = count; k < nodo.nodes.Length; k++)
            {
                nodo.nodes[k] = null;
            }

            if (count > 0 && !ConnectedNodes.Contains(nodo))
            {
                ConnectedNodes.Add(nodo);
            }
        }
    }

}
