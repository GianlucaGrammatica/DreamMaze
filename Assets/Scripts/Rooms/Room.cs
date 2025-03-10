using UnityEngine;

public class Room : MonoBehaviour
{
    // ID della stanza, corrisponde al nodo nel grafo
    public int roomID;

    // Array di porte della stanza
    public Door[] doors;

    // Array di coperture delle porte
    public DoorCoverUp[] doorCoverUps;

    // Array di spawner delle porte
    public GameObject[] Spawners;

    // Stanze collegate a questa stanza
    public Room[] connectedRooms;

    // Inizializza le connessioni della stanza con altre stanze
    // Aggiorna lo stato delle porte e assegna le destinazioni di teletrasporto
    public void InitializeConnections(Room[] connections)
    {
        connectedRooms = connections;
        UpdateDoorStates();
        AssignTeleportDestinations();
    }

    // Aggiorna lo stato delle porte e delle coperture delle porte in base ai collegamenti
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

        Debug.Log("Stato delle porte aggiornato per la stanza: " + roomID);
    }

    // Assegna le destinazioni di teletrasporto delle porte in base agli spawner delle stanze collegate
    public void AssignTeleportDestinations()
    {
        for (int i = 0; i < 4; i++)
        {
            if (connectedRooms[i] != null)
            {
                // Trova l'indice della porta opposta
                int oppositeDoorIndex = 0;

                switch (i)
                {
                    case 0:
                        oppositeDoorIndex = 2; // Destra -> Sinistra
                        break;
                    case 2:
                        oppositeDoorIndex = 0; // Sinistra -> Destra
                        break;
                    case 1:
                        oppositeDoorIndex = 3; // Sotto -> Sopra
                        break;
                    case 3:
                        oppositeDoorIndex = 1; // Sopra -> Sotto
                        break;
                }

                // Assegna il teleportDestination della porta corrente allo spawner della porta opposta della stanza collegata
                doors[i].teleportDestination = connectedRooms[i].Spawners[oppositeDoorIndex].transform;
            }
        }

        Debug.Log("Destinazioni di teletrasporto assegnate per la stanza: " + roomID);
    }
}