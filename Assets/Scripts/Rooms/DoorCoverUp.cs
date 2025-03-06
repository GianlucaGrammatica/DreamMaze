using UnityEngine;

public class DoorCoverUp : MonoBehaviour
{
    // Controlla se il muro è nascosto
    public bool isHidden = false; 

    // Imposta la visibilità del muro e disattiva/attiva l'oggetto stesso
    public void SetVisibility(bool isVisible)
    {
        isHidden = !isVisible;

        // Disattiva o attiva l'oggetto stesso (e tutti i suoi figli, incluse le Tilemap)
        gameObject.SetActive(isVisible);
    }
}