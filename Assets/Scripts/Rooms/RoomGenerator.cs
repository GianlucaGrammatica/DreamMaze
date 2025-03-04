using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject roomPrefab;  // Prefab della stanza
    public int numberOfRooms = 5;  // Numero di stanze da spawnare
    public float roomSpacing = 10; // Spazio tra le stanze sull'asse X

    // Inizia lo spawn delle stanze
    void Start()
    {
        SpawnRooms();
    }

    // Funzione per spawnare le stanze
    void SpawnRooms()
    {
        // Usa la posizione dell'oggetto come punto di partenza
        Vector3 startPosition = transform.position;

        for (int i = 0; i < numberOfRooms; i++)
        {
            // Calcola la posizione per ogni stanza rispetto al punto di partenza
            Vector3 newPosition = startPosition + new Vector3(i * roomSpacing, 0, 0);

            // Istanzia la stanza
            GameObject newRoom = Instantiate(roomPrefab, newPosition, Quaternion.identity);
            newRoom.name = "Room_" + i;  // Rinomina la stanza per riconoscerla
        }
    }
}