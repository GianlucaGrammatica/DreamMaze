using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomID;            // ID della stanza, corrisponde al nodo nel grafo
    public Door[] doors;          // Array di porte della stanza
    public DoorCoverUp[] doorCoverUps; // Array di coperture delle porte
    public GameObject[] Spawners; // Array di coperture delle porte
    public Room[] connectedRooms; // Stanze collegate

    // Inizializza le porte con le stanze collegate
    public void InitializeConnections(Room[] connections)
    {
        connectedRooms = connections;
        UpdateDoorStates();
    }

    // Aggiorna lo stato delle porte e dei DoorCoverUp in base ai collegamenti
    public void UpdateDoorStates()
    {
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Length: " + connectedRooms.Length);
            if (connectedRooms[i] != null)
            {
                // Se c'è una stanza collegata, la porta è attiva e il muro è nascosto
                Debug.Log("I: " + i + " Door di i: "+ doorCoverUps.ToString());
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
}