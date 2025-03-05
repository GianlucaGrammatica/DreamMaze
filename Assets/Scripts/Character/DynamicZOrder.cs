using UnityEngine;
using Live2D.Cubism.Rendering; // Namespace di Cubism

public class SortingOrderDynamic : MonoBehaviour
{
    private CubismRenderController cubismRenderController; // CubismRenderController per gestire il sortingOrder
    private Vector3 lastPosition;

    public float feetOffset = 0.5f; // Offset per l'altezza dei piedi

    void Awake()
    {
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
            // Prende la posizione Y con l'offset dei piedi
            float feetY = transform.position.y - feetOffset;
            cubismRenderController.SortingOrder = -(Mathf.RoundToInt(feetY));
        }
    }
}
