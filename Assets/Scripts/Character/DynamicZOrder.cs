using UnityEngine;
using Live2D.Cubism.Rendering; // Namespace di Cubism

public class SortingOrderDynamic : MonoBehaviour
{
    private CubismRenderController cubismRenderController; // CubismRenderController per gestire il sortingOrder
    private Vector3 lastPosition;

    void Awake()
    {
        // Cerca il CubismRenderController nell'oggetto figlio del modello
        cubismRenderController = GetComponentInChildren<CubismRenderController>();
        lastPosition = transform.position;
        UpdateSortingOrder();
    }

    void Update()
    {
        if (transform.position != lastPosition)
        {
            UpdateSortingOrder();
            lastPosition = transform.position;
        }
    }

    private void UpdateSortingOrder()
    {
        if (cubismRenderController != null)
        {
            // Cambia il sortingOrder in base alla posizione Y del player
            cubismRenderController.SortingOrder = -(Mathf.RoundToInt(transform.position.y));
        }
    }
}
