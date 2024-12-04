
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // For handling pointer events

public class HoverScaleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scaleFactor = 1.2f; // Scale factor when hovered.
    [SerializeField] private float scaleSpeed = 5f; // Speed of the scaling transition.

    private Vector3 originalScale; // The original scale of the button.
    private bool isHovered = false; // Whether the button is being hovered.

    private void Start()
    {
        // Store the original scale at the start.
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Smoothly scale the button based on hover status.
        Vector3 targetScale = isHovered ? originalScale * scaleFactor : originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    // Called when the mouse enters the button.
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    // Called when the mouse exits the button.
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}

