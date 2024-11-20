using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class HandCircleCounter : MonoBehaviour
{
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private TextMeshProUGUI rightHandCirclesText;
    [SerializeField] private TextMeshProUGUI leftHandCirclesText;

    private Vector3 rightHandCenter;
    private Vector3 leftHandCenter;
    private float rightHandAngleSum = 0f;
    private float leftHandAngleSum = 0f;
    private int rightHandCircles = 0;
    private int leftHandCircles = 0;
    private bool rightHandCalibrated = false;
    private bool leftHandCalibrated = false;
    private const float AngleThreshold = 5f;  // Minimum angle change to consider a movement
    private Vector3 lastRightHandPosition;
    private Vector3 lastLeftHandPosition;
    private const float MovementThreshold = 0.01f;

    // Input action references (assign these in the inspector)
    [SerializeField] private InputActionReference rightHandSelectAction;
    [SerializeField] private InputActionReference leftHandSelectAction;

    private const float DeadZoneRadius = 0.05f;  // Dead zone radius around the center point to ignore small movements

    void OnEnable()
    {
        rightHandSelectAction.action.performed += CalibrateRightHand;
        leftHandSelectAction.action.performed += CalibrateLeftHand;
    }

    void OnDisable()
    {
        rightHandSelectAction.action.performed -= CalibrateRightHand;
        leftHandSelectAction.action.performed -= CalibrateLeftHand;
    }

    void Update()
    {
        if (rightHandCalibrated)
            DetectCircles(rightHandTransform, ref rightHandCenter, ref rightHandAngleSum, ref rightHandCircles, rightHandCirclesText, "Right Hand", ref lastRightHandPosition);

        if (leftHandCalibrated)
            DetectCircles(leftHandTransform, ref leftHandCenter, ref leftHandAngleSum, ref leftHandCircles, leftHandCirclesText, "Left Hand", ref lastLeftHandPosition);
    }

    private void CalibrateRightHand(InputAction.CallbackContext context)
    {
        rightHandCenter = rightHandTransform.position;
        rightHandCalibrated = true;
        rightHandAngleSum = 0f;  // Reset angle sum for fresh calibration
        Debug.Log("Right Hand Center Calibrated");
    }

    private void CalibrateLeftHand(InputAction.CallbackContext context)
    {
        leftHandCenter = leftHandTransform.position;
        leftHandCalibrated = true;
        leftHandAngleSum = 0f;  // Reset angle sum for fresh calibration
        Debug.Log("Left Hand Center Calibrated");
    }

    private void DetectCircles(Transform handTransform, ref Vector3 center, ref float angleSum, ref int circleCount, TextMeshProUGUI counterText, string handName, ref Vector3 lastPosition)
    {
        // Calculate the distance from the hand to the calibrated center point
        float distanceFromCenter = Vector3.Distance(handTransform.position, center);

        // If the hand is within the dead zone radius, skip the detection for this frame
        if (distanceFromCenter < DeadZoneRadius)
        {
            return;  // Ignore movements within the dead zone
        }

        // Get the direction vector on the XZ plane relative to the center point
        Vector3 handDirection = new Vector3(handTransform.position.x - center.x, 0, handTransform.position.z - center.z).normalized;

        // If the movement is too small, skip the detection for this frame
        if (Vector3.Distance(handTransform.position, lastPosition) < MovementThreshold)
        {
            return;  // Skip small movements
        }

        // Calculate the angle on the XZ plane
        float angle = Vector3.SignedAngle(lastPosition - center, handDirection, Vector3.up);

        // Accumulate the angle only if it exceeds the threshold
        if (Mathf.Abs(angle) > AngleThreshold)
        {
            angleSum += angle;
            lastPosition = handTransform.position;  // Update last position

            // Log for debugging
            Debug.Log($"{handName} - Angle: {angle}, Accumulated Angle: {angleSum}");
        }

        // When a full rotation is completed
        if (Mathf.Abs(angleSum) >= 360f)
        {
            circleCount++;
            counterText.text = $"{handName} Circles: {circleCount}";
            Debug.Log($"{handName} completed a circle! Total Circles: {circleCount}");

            // Reset the accumulated angle for the next circle
            angleSum = 0f;
        }
    }
}
