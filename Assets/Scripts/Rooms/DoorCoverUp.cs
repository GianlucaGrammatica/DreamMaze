using UnityEngine;

public class DoorCoverUp : MonoBehaviour
{
    public bool isHidden = false; // Controlla se il muro è nascosto

    // Imposta la visibilità del muro e disattiva/attiva l'oggetto stesso
    public void SetVisibility(bool isVisible)
    {
        isHidden = !isVisible;

        // Disattiva o attiva l'oggetto stesso (e tutti i suoi figli, incluse le Tilemap)
        gameObject.SetActive(isVisible);
    }
}