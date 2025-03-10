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
        // Ottieni il Collider2D del giocatore
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        if (playerCollider == null)
        {
            Debug.LogError("Il giocatore non ha un Collider2D!");
            return;
        }

        // Calcola l'altezza del giocatore
        float playerHeight = playerCollider.bounds.size.y;

        // Teletrasporta il giocatore alla posizione dello spawner, regolando per la base del giocatore
        Vector2 teleportPosition = teleportDestination.position;
        teleportPosition.y -= playerHeight / 2; // Sposta il giocatore in basso di metà della sua altezza

        // Applica la nuova posizione
        player.transform.position = teleportPosition;

        // Debug: stampa la posizione di destinazione
        Debug.Log("Player teleported to: " + teleportPosition);
    }
}