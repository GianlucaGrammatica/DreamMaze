using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour
{
    [SerializeField] private Image victoryImage;  // Collega qui la tua immagine PNG
    [SerializeField] private string victoryRoomTag = "VictoryRoom";  // Tag della stanza della vittoria

    void Start()
    {
        victoryImage.enabled = false;  // Nasconde l'immagine all'inizio
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(victoryRoomTag))
        {
            ShowVictory();  // Mostra l'immagine di vittoria
        }
    }

    void ShowVictory()
    {
        victoryImage.enabled = true;  // Mostra l'immagine PNG
        Debug.Log("Hai vinto!");
    }
}