using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WindmillCalibrationManager : MonoBehaviour
{ // Deprecated
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private GameObject calibrationIndicatorPrefab;

    [SerializeField] private InputActionReference rightHandSelectAction;
    [SerializeField] private InputActionReference leftHandSelectAction;

    [SerializeField] private Button confirmButton; 

    private GameObject rightHandIndicator;
    private GameObject leftHandIndicator;

    public Vector3 RightHandCenter { get; private set; }
    public Vector3 LeftHandCenter { get; private set; }
    public bool IsCalibrated { get; private set; } = false;
    private bool calibrationStopped = false;

    private void Start()
    {
        if (confirmButton != null)
            confirmButton.interactable = false;
    }

    private void Update()
    {
        if (calibrationStopped) return;

        if (rightHandSelectAction.action.IsPressed())
        {
            RightHandCenter = rightHandTransform.position;
            UpdateIndicator(ref rightHandIndicator, RightHandCenter, "Right Tracking Point");
        }

        if (leftHandSelectAction.action.IsPressed())
        {
            LeftHandCenter = leftHandTransform.position;
            UpdateIndicator(ref leftHandIndicator, LeftHandCenter, "Left Tracking Point");
        }

        UpdateConfirmButtonState();
    }

    public void ConfirmCalibration()
    {
        if (rightHandIndicator != null && leftHandIndicator != null)
        {
            IsCalibrated = true;
            StopCalibration();
            Debug.Log("Calibration confirmed. Tracking can now start.");
        }
        else
        {
            Debug.LogWarning("Both hands must be calibrated before confirming.");
        }
    }

    public void StopCalibration()
    {
        calibrationStopped = true;
        if (rightHandIndicator != null) Destroy(rightHandIndicator);
        if (leftHandIndicator != null) Destroy(leftHandIndicator);
        Debug.Log("Calibration has been stopped. No further changes will occur.");
    }

    private void UpdateIndicator(ref GameObject indicator, Vector3 position, string name)
    {
        if (indicator != null)
        {
            Destroy(indicator); 
        }
        indicator = Instantiate(calibrationIndicatorPrefab, position, Quaternion.identity);
        indicator.name = name; 
    }

    private void UpdateConfirmButtonState()
    {
        if (confirmButton != null)
        {
            confirmButton.interactable = rightHandIndicator != null && leftHandIndicator != null;
        }
    }
}
