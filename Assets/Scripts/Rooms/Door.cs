using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform teleportDestination;  // Porta a cui collegarsi
    public bool isLinked = false;          // Controlla se è collegata a un'altra porta
    public float teleportOffset = 2f;      // Offset per il teletrasporto

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se l'oggetto che entra nel trigger è il player e se la porta è collegata
        if (collision.gameObject.CompareTag("Player") && isLinked)
        {
            TeleportPlayer(collision.gameObject);  // Teletrasporta il player
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        // Calcola la posizione di teletrasporto con l'offset
        Vector2 offsetDirection = (teleportDestination.position - transform.position).normalized;
        Vector2 teleportPosition = (Vector2)teleportDestination.position + offsetDirection * teleportOffset;

        // Teletrasporta il player alla posizione calcolata
        player.transform.position = teleportPosition;
        Debug.Log("Player teleported to: " + teleportPosition);
    }
}