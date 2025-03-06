using UnityEngine;

public class HideParts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is creato
    void Start()
    {
        // Imposta l'opacità dell'oggetto e dei suoi figli a 0
        SetOpacityToZero(transform);
    }

    // Funzione ricorsiva per impostare l'opacità a 0 per l'oggetto e i suoi figli
    void SetOpacityToZero(Transform parent)
    {
        // Controlla se il componente Renderer esiste sull'oggetto
        Renderer renderer = parent.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Ottieni il materiale e modifica l'alpha (opacità)
            Color color = renderer.material.color;
            color.a = 0f;  // Imposta l'opacità a 0
            renderer.material.color = color;
        }

        // Ripeti la stessa operazione per ogni figlio
        foreach (Transform child in parent)
        {
            SetOpacityToZero(child);  // Chiamata ricorsiva per i figli
        }
    }
}
