using UnityEngine;

public class Door : MonoBehaviour
{
    // Porta a cui collegarsi per il teletrasporto
    public Transform teleportDestination;

    // Controlla se la porta è collegata a un'altra porta
    public bool isLinked = false;

    // Offset per il teletrasporto, per evitare sovrapposizioni
    public float teleportOffset = 2f;

    // Metodo chiamato quando un oggetto entra nel trigger della porta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se l'oggetto che entra nel trigger è il player e se la porta è collegata
        if (collision.gameObject.CompareTag("Player") && isLinked)
        {
            TeleportPlayer(collision.gameObject);
        }
    }

    // Teletrasporta il giocatore alla destinazione con l'offset applicato
    private void TeleportPlayer(GameObject player)
    {
        // Calcola la direzione dell'offset per il teletrasporto
        Vector2 offsetDirection = (teleportDestination.position - transform.position).normalized;

        // Calcola la posizione di teletrasporto con l'offset applicato
        Vector2 teleportPosition = (Vector2)teleportDestination.position + offsetDirection * teleportOffset;

        // Teletrasporta il giocatore alla posizione calcolata
        player.transform.position = teleportPosition;

        // Debug: stampa la posizione di destinazione e la posizione di teletrasporto
        Debug.Log("Destination position: " + teleportDestination.position);
        Debug.Log("Player teleported to: " + teleportPosition);
    }
}