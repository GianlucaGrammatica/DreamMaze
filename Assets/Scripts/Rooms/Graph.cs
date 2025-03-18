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
    // Numero di nodi nel grafo
    public int VerticesNumber = 5;

    // Matrice di adiacenza
    private int[,] BaseMatrix;

    // Lista di tutti i nodi nel grafo
    public List<Node> Grafo = new List<Node>();

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
        this.GenerateMatrix();
        this.DebugGraph();
    }

    // Ottiene i collegamenti di un nodo specifico
    public List<int> GetConnections(int nodeID)
    {
        Debug.Log($"Ottengo connessioni per nodo {nodeID}");
        List<int> connections = new List<int> { -1, -1, -1, -1 };

        Node node = Grafo[nodeID];
        for (int i = 0; i < 4; i++)
        {
            if (node.nodes[i] != null)
            {
                connections[i] = node.nodes[i].ID;
            }
        }

        Debug.Log($"Nodo {nodeID} ha {connections.Count} connessioni");
        return connections;
    }

    // Genera la matrice di adiacenza con collegamenti casuali
    public void GenerateMatrix()
    {
        Debug.Log("Generazione matrice di adiacenza e connessioni direzionali");
        int[] connections = new int[VerticesNumber];

        // Collega i nodi in modo sequenziale per garantire un percorso minimo
        for (int i = 0; i < VerticesNumber - 1; i++)
        {
            ConnectNodes(i, i + 1); // Collega il nodo corrente al nodo successivo
        }

        // Aggiungi collegamenti casuali
        for (int i = 0; i < VerticesNumber; i++)
        {
            for (int j = 0; j < VerticesNumber; j++)
            {
                if (i == j) continue; // Evita di collegare un nodo a sé stesso

                // Collega i nodi con una probabilità del 30% (aumentata da 15%)
                if (connections[i] < 4 && connections[j] < 4 && random.NextDouble() < 0.30)
                {
                    // Verifica se la connessione esiste già
                    if (BaseMatrix[i, j] == 0)
                    {
                        Debug.Log("🎂Connetto nodi: " + i + " - " + j);
                        ConnectNodes(i, j);
                        connections[i]++;
                        connections[j]++;
                    }
                }
            }
        }

        // Verifica che ci siano almeno 2 percorsi tra il primo e l'ultimo nodo
        if (CountPaths(0, VerticesNumber - 1) < 2)
        {
            Debug.Log("Percorsi insufficienti, aggiunta di collegamenti casuali");
            AddRandomEdge(random.Next(1, 3));
        }
    }

    private void ConnectNodes(int node1ID, int node2ID)
    {
        Node node1 = Grafo[node1ID];
        Node node2 = Grafo[node2ID];

        // Trova una direzione libera per node1
        int direction1 = FindFreeDirection(node1);
        if (direction1 == -1)
        {
            Debug.LogWarning($"Nodo {node1ID} non ha direzioni libere.");
            return;
        }

        // Trova la direzione opposta per node2
        int direction2 = GetOppositeDirection(direction1);
        if (direction2 == -1 || node2.nodes[direction2] != null)
        {
            Debug.LogWarning($"Nodo {node2ID} non ha una direzione opposta libera.");
            return;
        }

        // Collega i nodi nelle direzioni appropriate
        node1.nodes[direction1] = node2; // Room0.nodes[0] = Room2
        node2.nodes[direction2] = node1; // Room2.nodes[2] = Room0

        // Aggiorna la BaseMatrix per riflettere la connessione bidirezionale
        BaseMatrix[node1ID, node2ID] = 1;
        BaseMatrix[node2ID, node1ID] = 1;

        Debug.Log($"💛💛Collegamento creato: Nodo {node1ID} (direzione {direction1}) -> Nodo {node2ID} (direzione {direction2})");
    }

    private int FindFreeDirection(Node node)
    {
        for (int i = 0; i < 4; i++)
        {
            if (node.nodes[i] == null)
            {
                return i;
            }
        }
        return -1; // Nessuna direzione libera
    }

    private int GetOppositeDirection(int direction)
    {
        switch (direction)
        {
            case 0: return 2; // Destra -> Sinistra
            case 1: return 3; // Sotto -> Sopra
            case 2: return 0; // Sinistra -> Destra
            case 3: return 1; // Sopra -> Sotto
            default: return -1;
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


    public void FindDebugNode(int nodeId)
    {
        Node toDebug = Grafo.Find(n => n.ID == nodeId);

        if (toDebug != null)
        {
            Debug.Log($"Nodo trovato: ID {toDebug.ID}");
        }
        else
        {
            Debug.Log($"Nodo con ID {nodeId} non trovato.");
        }
    }

    public void DebugGraph()
    {
        Debug.Log("===== DEBUG GRAFO =====");

        // Stampa la lista dei nodi e le loro connessioni
        foreach (Node nodo in Grafo)
        {
            string connessioni = "";
            foreach (Node connectedNode in nodo.nodes)
            {
                if (connectedNode != null)
                {
                    connessioni += connectedNode.ID + " ";
                }
            }
            Debug.Log($"Nodo {nodo.ID} → Connessioni: {connessioni}");
        }

        // Stampa la matrice di adiacenza
        Debug.Log("===== MATRICE DI ADIACENZA =====");
        for (int i = 0; i < VerticesNumber; i++)
        {
            string row = "";
            for (int j = 0; j < VerticesNumber; j++)
            {
                row += BaseMatrix[i, j] + " ";
            }
            Debug.Log(row);
        }

        Debug.Log("===== DEBUG COMPLETATO =====");
    }


}