using UnityEngine;

public class WinCollision : MonoBehaviour
{
    public WinUI winUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            winUI.menuCanvas.SetActive(true);
            Debug.Log("Collided - Attivato menuCanvas");
        }
    }

}
