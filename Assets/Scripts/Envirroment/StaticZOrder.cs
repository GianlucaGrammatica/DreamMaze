using UnityEngine;

public class SortingOrderStatic : MonoBehaviour
{
    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = -(Mathf.RoundToInt(transform.position.y));
    }
}
