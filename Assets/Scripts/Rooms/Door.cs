using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform teleportDestination;  // 🟢 Porta a cui collegarsi
    public bool isLinked = false;          // 🟢 Controlla se è collegata a un'altra porta
    public float teleportCooldown = 1f;    // 🟢 Evita TP multipli istantanei

    private bool canTeleport = true;       // 🟢 Controlla se il TP è disponibile

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isLinked && canTeleport)
        {
            StartCoroutine(TeleportPlayer(other));
        }
    }

    private System.Collections.IEnumerator TeleportPlayer(Collider player)
    {
        canTeleport = false;  // 🟢 Disabilita il TP temporaneamente
        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
        {
            // 🟢 Disabilita e riabilita il CharacterController per evitare bug di collisione
            controller.enabled = false;
            player.transform.position = teleportDestination.position;
            controller.enabled = true;
        }
        else
        {
            player.transform.position = teleportDestination.position;
        }

        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;  // 🟢 Riabilita il TP dopo il cooldown
    }
}