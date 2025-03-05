using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomID;            // ID della stanza, corrisponde al nodo nel grafo
    public Door[] doors;          // Array di porte della stanza
    public DoorCoverUp[] doorCoverUps; // Array di coperture delle porte
    public GameObject[] Spawners; // Array di spawner delle porte
    public Room[] connectedRooms; // Stanze collegate

    // Inizializza le porte con le stanze collegate
    public void InitializeConnections(Room[] connections)
    {
        connectedRooms = connections;
        UpdateDoorStates();
        AssignTeleportDestinations();
    }

    // Aggiorna lo stato delle porte e dei DoorCoverUp in base ai collegamenti
    public void UpdateDoorStates()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            if (connectedRooms[i] != null)
            {
                // Se c'è una stanza collegata, la porta è attiva e il muro è nascosto
                doors[i].isLinked = true;
                doorCoverUps[i].SetVisibility(false);
            }
            else
            {
                // Se non c'è una stanza collegata, la porta è disattivata e il muro è visibile
                doors[i].isLinked = false;
                doorCoverUps[i].SetVisibility(true);
            }
        }
    }

    // Assegna i teleportDestination delle porte in base agli spawner delle stanze collegate
    public void AssignTeleportDestinations()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            if (connectedRooms[i] != null)
            {
                // Trova l'indice della porta opposta
                int oppositeDoorIndex = (i + 2) % 4;

                // Assegna il teleportDestination della porta corrente allo spawner della porta opposta della stanza collegata
                doors[i].teleportDestination = connectedRooms[i].Spawners[oppositeDoorIndex].transform;
            }
        }
    }
}