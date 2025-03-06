using UnityEngine;
using System.Collections.Generic;

public class Node
{
    public int ID; // Identificativo del nodo
    public Node[] nodes = new Node[4] { null, null, null, null }; // Nodi collegati (massimo 4)

    public Node(int id)
    {
        ID = id;
    }
}

public class Graph : MonoBehaviour
{
    // Numero di nodi nel grafo
    public int VerticesNumber = 5;

    // Matrice di adiacenza
    private int[,] BaseMatrix;

    // Lista di tutti i nodi nel grafo
    private List<Node> Grafo = new List<Node>();

    // Lista dei nodi con almeno una connessione
    public List<Node> ConnectedNodes = new List<Node>();

    // Primo nodo e ultimo nodo del grafo
    public Node FirstNode;
    public Node LastNode;

    private System.Random random;

    void Start()
    {
    }

    // Inizializza il grafo con il numero specificato di nodi
    public void InitializeGraph(int vertices)
    {
        Debug.Log($"Inizializzazione grafo con {vertices} nodi");
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
    }

    // Ottiene i collegamenti di un nodo specifico
    public List<int> GetConnections(int nodeID)
    {
        Debug.Log($"Ottengo connessioni per nodo {nodeID}");
        List<int> connections = new List<int>();

        for (int j = 0; j < VerticesNumber; j++)
        {
            if (BaseMatrix[nodeID, j] == 1)
            {
                connections.Add(j);
            }
        }
        Debug.Log($"Nodo {nodeID} ha {connections.Count} connessioni");
        return connections;
    }

    // Genera la matrice di adiacenza con collegamenti casuali
    public void GenerateMatrix()
    {
        Debug.Log("Generazione matrice di adiacenza");
        int[] connections = new int[VerticesNumber];

        for (int i = 0; i < VerticesNumber; i++)
        {
            for (int j = 0; j < VerticesNumber; j++)
            {
                if (i == 0 && j == VerticesNumber - 1) continue;
                if (i == j) continue;

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
            Debug.Log("Percorsi insufficienti, aggiunta di collegamenti casuali");
            AddRandomEdge(random.Next(1, 3));
        }
    }

    // Conta i percorsi possibili tra due nodi
    public int CountPaths(int start, int end)
    {
        Debug.Log($"Conteggio percorsi tra {start} e {end}");
        bool[] visited = new bool[VerticesNumber];
        int pathCount = 0;
        DFS(start, end, visited, ref pathCount);
        Debug.Log($"Percorsi trovati: {pathCount}");
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
        Debug.Log($"Aggiunta di {count} collegamenti casuali");
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
            Debug.Log($"Collegamento aggiunto tra {Vertex1} e {Vertex2}");
        }
    }

    // Valida un nodo
    private bool isValidVertex(int Vertex)
    {
        return (Vertex >= 0 && Vertex < VerticesNumber);
    }

    // Popola la lista dei nodi collegati
    public void PopulateList()
    {
        Debug.Log("Popolamento lista nodi collegati");
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

            for (int k = count; k < nodo.nodes.Length; k++)
            {
                nodo.nodes[k] = null;
            }

            if (count > 0 && !ConnectedNodes.Contains(nodo))
            {
                ConnectedNodes.Add(nodo);
            }
        }
        Debug.Log($"Lista nodi collegati popolata con {ConnectedNodes.Count} nodi");
    }
}