using UnityEngine;
using TMPro;


public class SetHeight : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI heightText; // Serialized field to display the height
    private Transform vrCameraTransform; // VR headset (eye height)

    void Start()
    {
        // Assign the VR camera (typically tagged as "MainCamera")
        vrCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Continuously read and display the VR headset height
        if (vrCameraTransform != null && heightText != null)
        {
            float currentHeight = vrCameraTransform.position.y;
            heightText.text = $"Height: {currentHeight:F2} meters";
        }
    }
}
