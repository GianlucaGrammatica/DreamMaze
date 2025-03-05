using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomID;            // ID della stanza, corrisponde al nodo nel grafo
    public Door[] doors;          // Array di porte della stanza
    public DoorCoverUp[] doorCoverUp;
    public Room[] connectedRooms; // Stanze collegate
    

    // Inizializza le porte con le stanze collegate
    public void InitializeConnections(Room[] connections)
    {
        connectedRooms = connections;
    }
}