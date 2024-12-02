using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class HandCircleCounter : MonoBehaviour
{
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private WarningBoundary warningSpace; 

    public int rightHandCircles = 0;
    public int leftHandCircles = 0;

    private float rightHandAngleSum = 0f;
    private float leftHandAngleSum = 0f;
    private Vector3 lastRightHandPosition;
    private Vector3 lastLeftHandPosition;
    private const float DeadZoneRadius = 0.05f;
    private const float AngleThreshold = 5f;
    private const float MovementThreshold = 0.01f;

    private void Update()
    {
        if(!warningSpace.isPlayerOutside)
        {
        DetectCircles(rightHandTransform, ref rightHandAngleSum, ref rightHandCircles, "Right Hand", ref lastRightHandPosition);
        DetectCircles(leftHandTransform, ref leftHandAngleSum, ref leftHandCircles, "Left Hand", ref lastLeftHandPosition);
        }
    }

    private void DetectCircles(Transform handTransform, ref float angleSum, ref int circleCount, string handName, ref Vector3 lastPosition)
    {
        float distanceFromLast = Vector3.Distance(handTransform.position, lastPosition);

        if (distanceFromLast < DeadZoneRadius)
            return;

        Vector3 handDirection = new Vector3(handTransform.position.x - lastPosition.x, 0, handTransform.position.z - lastPosition.z).normalized;

        if (distanceFromLast < MovementThreshold)
            return;

        float angle = Vector3.SignedAngle(lastPosition - handTransform.position, handDirection, Vector3.up);

        if (Mathf.Abs(angle) > AngleThreshold)
        {
            angleSum += angle;
            lastPosition = handTransform.position;

            Debug.Log($"{handName} - Angle: {angle}, Accumulated Angle: {angleSum}");
        }

        if (Mathf.Abs(angleSum) >= 360f)
        {
            circleCount++;
            Debug.Log($"{handName} completed a circle! Total Circles: {circleCount}");

            angleSum = 0f;
        }
    }
}
