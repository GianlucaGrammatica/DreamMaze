using UnityEngine;

public class WinCollision : MonoBehaviour
{
    public WinUI winUI;

    // Chiamato quando un oggetto entra in collisione con il trigger
    void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto che ha colpito è il player
        if (other.CompareTag("Player"))
        {
            // Imposta la visibilità di WinUI a true
            winUI.isShowing = true;
        }
    }
}
