using UnityEngine;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
    public List<GameObject> roomPrefabs;  // Lista di prefabs delle stanze
    public int numberOfRooms = 5;         // Numero di stanze da spawnare
    public float roomSpacing = 10;        // Spazio tra le stanze sull'asse X

    // Inizia lo spawn delle stanze
    void Start()
    {
        SpawnRooms();
    }

    // Funzione per spawnare le stanze
    void SpawnRooms()
    {
        if (roomPrefabs.Count == 0)
        {
            Debug.LogError("La lista roomPrefabs è vuota!");
            return;
        }

        Vector3 startPosition = transform.position;

        for (int i = 0; i < numberOfRooms; i++)
        {
            // Sceglie casualmente un prefab dalla lista
            GameObject randomRoomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];

            // Calcola la posizione per ogni stanza rispetto al punto di partenza
            Vector3 newPosition = startPosition + new Vector3(i * roomSpacing, 0, 0);

            // Istanzia la stanza scelta casualmente
            GameObject newRoom = Instantiate(randomRoomPrefab, newPosition, Quaternion.identity);
            newRoom.name = "Room_" + i;  // Rinomina la stanza per riconoscerla
        }
    }
}