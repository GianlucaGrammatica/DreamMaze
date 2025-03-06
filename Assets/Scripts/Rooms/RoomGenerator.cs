using UnityEngine;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
    // Riferimento al grafo che definisce la struttura delle stanze
    public Graph graph;

    // Array di prefabs per i diversi tipi di stanze
    public GameObject[] roomPrefabs;

    // Genitore delle stanze nella gerarchia di Unity
    public Transform roomParent;

    // Distanza tra le stanze lungo l'asse X
    public float roomOffset = 10f;

    // Array delle stanze create
    private Room[] rooms;

    void Start()
    {
        // Inizializza il grafo, genera la matrice di adiacenza e popola la lista delle connessioni
        graph.InitializeGraph(graph.VerticesNumber);
        graph.GenerateMatrix();
        graph.PopulateList();

        // Genera le stanze e le collega
        GenerateRooms();
        LinkRooms();
    }

    // Genera le stanze utilizzando i prefabs casuali e le posiziona in sequenza
    void GenerateRooms()
    {
        rooms = new Room[graph.VerticesNumber];
        Vector3 currentPosition = Vector3.zero;

        for (int i = 0; i < graph.VerticesNumber; i++)
        {
            // Sceglie un prefab casuale dall'array
            GameObject selectedPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];

            // Istanzia la stanza nella posizione corrente
            GameObject roomObj = Instantiate(selectedPrefab, currentPosition, Quaternion.identity, roomParent);

            Room room = roomObj.GetComponent<Room>();
            room.roomID = i;
            rooms[i] = room;

            // Aggiorna la posizione per la prossima stanza
            currentPosition += new Vector3(roomOffset, 0, 0);
        }

        Debug.Log("Stanze generate con successo.");
    }

    // Collega le stanze in base alla matrice di adiacenza del grafo
    void LinkRooms()
    {
        for (int i = 0; i < graph.VerticesNumber; i++)
        {
            Room currentRoom = rooms[i];

            // Ottiene le connessioni della stanza corrente dal grafo
            List<int> connections = graph.GetConnections(i);

            // Inizializza l'array delle stanze collegate
            currentRoom.connectedRooms = new Room[currentRoom.doors.Length];

            for (int j = 0; j < connections.Count; j++)
            {
                int connectedRoomID = connections[j];
                Room connectedRoom = rooms[connectedRoomID];

                // Assegna la stanza collegata se esiste una porta disponibile
                if (j < currentRoom.doors.Length)
                {
                    currentRoom.connectedRooms[j] = connectedRoom;
                }
            }

            // Inizializza le connessioni della stanza
            currentRoom.InitializeConnections(currentRoom.connectedRooms);
        }

        Debug.Log("Stanze collegate con successo.");
    }
}