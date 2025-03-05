using UnityEngine;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
    public Graph graph;                            // Riferimento al grafo
    public GameObject[] roomPrefabs;                // 🟢 Array di prefabs per 4 tipi di stanze
    public Transform roomParent;                    // Genitore delle stanze
    public float roomOffset = 10f;                  // Distanza tra le stanze

    private Room[] rooms;                           // Array delle stanze create

    void Start()
    {
        //graph = GetComponent<Graph>();        
        graph.InitializeGraph(5);  // Crea il grafo con i nodi
        graph.GenerateMatrix();                // Genera la matrice di collegamenti
        graph.PopulateList();                  // Popola la lista dei nodi collegati             // Popola la lista dei nodi collegati
        
        GenerateRooms();
        LinkRooms();
    }

    // 🟢 Genera stanze con prefab casuali
    void GenerateRooms()
    {
        rooms = new Room[graph.VerticesNumber];
        Vector3 currentPosition = Vector3.zero;  // 🟢 Punto di partenza

        for (int i = 0; i < graph.VerticesNumber; i++)
        {
            // 🟢 Scegli un prefab casuale
            GameObject selectedPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];

            // 🟢 Posiziona progressivamente le stanze
            GameObject roomObj = Instantiate(selectedPrefab, currentPosition, Quaternion.identity, roomParent);


        
            Room room = roomObj.GetComponent<Room>();
            room.roomID = i;
            rooms[i] = room;
            
            

            // 🟢 Aggiorna la posizione per la prossima stanza
            currentPosition += new Vector3(roomOffset, 0, 0);  // Sposta di 10 unità sull'asse X
        }
    }

    // 🟢 Collega le stanze basandosi sulla matrice di adiacenza
    void LinkRooms()
    {
        for (int i = 0; i < graph.VerticesNumber; i++)
        {
            Room currentRoom = rooms[i];
            List<int> connections = graph.GetConnections(0);
            Debug.Log("Connctions: "+ connections);

            for (int j = 0; j < connections.Count; j++)
            {
                int connectedRoomID = connections[j];
                Room connectedRoom = rooms[connectedRoomID];

                // 🟢 Collega le porte solo se non sono già collegate
                if (currentRoom.doors.Length > j && connectedRoom.doors.Length > (j + 2) % 4)
                {
                    Door doorA = currentRoom.doors[j];
                    Door doorB = connectedRoom.doors[(j + 2) % 4];

                    doorA.teleportDestination = doorB.transform;
                    doorA.isLinked = true;

                    doorB.teleportDestination = doorA.transform;
                    doorB.isLinked = true;
                }

            }
        }
    }
}
