using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GraphRoomSpawner : MonoBehaviour
{
    public GameObject roomPrefab;
    public int roomSpacing = 12; // Distanza tra le stanze

    private Dictionary<int, Vector3> roomPositions = new Dictionary<int, Vector3>();

    private Graph graph = new Graph(10);

    public void GenerateRooms(Graph graph)
    {
        List<(int, int)> edges = graph.GetEdges();
        roomPositions.Clear();

        // Posizioniamo la prima stanza al centro
        roomPositions[0] = Vector3.zero;
        InstantiateRoom(0, Vector3.zero);

        foreach (var edge in edges)
        {
            int roomA = edge.Item1;
            int roomB = edge.Item2;

            if (!roomPositions.ContainsKey(roomB))
            {
                Vector3 newPosition = roomPositions[roomA] + new Vector3(roomSpacing, 0, 0);
                roomPositions[roomB] = newPosition;
                InstantiateRoom(roomB, newPosition);
            }
        }
    }

    private void InstantiateRoom(int id, Vector3 position)
    {
        GameObject newRoom = Instantiate(roomPrefab, position, Quaternion.identity);
        newRoom.name = "Room_" + id;
    }

    public void Start()
    {
        GenerateRooms(graph);
    }
}
