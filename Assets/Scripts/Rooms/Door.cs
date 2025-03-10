using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public Transform teleportDestination;
    public bool isLinked = false;
    public float teleportOffset = 2f;
    private Canvas transitionCanvas;
    private Animator transitionAnimator;

    public void SetTransition(Canvas canvas)
    {
        transitionCanvas = canvas;
        if (transitionCanvas != null)
        {
            transitionAnimator = transitionCanvas.GetComponentInChildren<Animator>();
        }
    }


    private IEnumerator TeleportWithTransition(GameObject player)
    {
        if (transitionCanvas != null && transitionAnimator != null)
        {
            transitionCanvas.gameObject.SetActive(true);
            transitionAnimator.Play("Transition");
            yield return new WaitForSeconds(1.09f);
        }

        TeleportPlayer(player);

        if (transitionCanvas != null)
        {
            transitionCanvas.gameObject.SetActive(false);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        if (playerCollider == null)
        {
            Debug.LogError("Il giocatore non ha un Collider2D!");
            return;
        }

        float playerHeight = playerCollider.bounds.size.y;
        Vector2 teleportPosition = teleportDestination.position;
        teleportPosition.y -= playerHeight / 2;

        player.transform.position = teleportPosition;
        Debug.Log("Player teleported to: " + teleportPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isLinked)
        {
            StartCoroutine(TeleportWithTransition(collision.gameObject));
        }
    }
}