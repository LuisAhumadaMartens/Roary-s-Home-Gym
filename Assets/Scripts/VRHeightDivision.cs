using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VRHeightDivision : MonoBehaviour
{
    // Serialize fields for UI buttons and TextMeshPro
    [SerializeField] private Button topButton; // Button for saving height A
    [SerializeField] private Button bottomButton; // Button for saving height B
    [SerializeField] private TextMeshProUGUI divisionText;

    private float heightA = 0f;
    private float heightB = 0f;
    private bool heightASet = false;
    private bool heightBSet = false;

    private Transform vrCameraTransform;  // Player's VR headset (eye height)

    void Start()
    {
        // Assign the VR camera (typically it's tagged as "MainCamera")
        vrCameraTransform = Camera.main.transform;

        // Attach the functions to the button click events
        topButton.onClick.AddListener(SetHeightA);
        bottomButton.onClick.AddListener(SetHeightB);
    }

    void Update()
    {
        if (heightASet && heightBSet)
        {
            float midpoint = (heightA + heightB) / 2;

            // Detect the VR player's head height
            float playerHeight = vrCameraTransform.position.y;

            // Check if player is above or below the midpoint and update TextMeshPro
            if (playerHeight > midpoint)
            {
                divisionText.text = "Above the midpoint!";
            }
            else
            {
                divisionText.text = "Below the midpoint!";
            }
        }
    }

    // Function to save height A
    public void SetHeightA()
    {
        heightA = vrCameraTransform.position.y;  // Save the eye height for point A
        heightASet = true;
        Debug.Log("Height A set to: " + heightA);
    }

    // Function to save height B
    public void SetHeightB()
    {
        heightB = vrCameraTransform.position.y;  // Save the eye height for point B
        heightBSet = true;
        Debug.Log("Height B set to: " + heightB);
    }
}
