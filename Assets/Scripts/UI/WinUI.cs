using UnityEngine;

public class WinUI : MonoBehaviour
{
    public GameObject menuCanvas;
    public bool isShowing = false;

    private bool lastState = false;

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if (isShowing != lastState)
        {
            menuCanvas.SetActive(isShowing);
            lastState = isShowing;
        }
    }
}
