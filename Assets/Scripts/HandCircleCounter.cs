using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandCircleCounter : MonoBehaviour
{
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private TextMeshProUGUI rightHandCounterText;
    [SerializeField] private TextMeshProUGUI leftHandCounterText;

    private int rightHandCircles = 0;
    private int leftHandCircles = 0;

    private Vector3 rightHandInitialDirection;
    private Vector3 leftHandInitialDirection;
    private float rightHandAngleSum = 0f;
    private float leftHandAngleSum = 0f;

    void Start()
    {
        rightHandInitialDirection = rightHandTransform.position - transform.position;
        leftHandInitialDirection = leftHandTransform.position - transform.position;
        rightHandCounterText.text = "1";
        leftHandCounterText.text = "2";
    }

    void Update()
    {
        TrackHandRotation(rightHandTransform, ref rightHandInitialDirection, ref rightHandAngleSum, ref rightHandCircles, rightHandCounterText);
        TrackHandRotation(leftHandTransform, ref leftHandInitialDirection, ref leftHandAngleSum, ref leftHandCircles, leftHandCounterText);
    }

    private void TrackHandRotation(Transform handTransform, ref Vector3 initialDirection, ref float angleSum, ref int circleCount, TextMeshProUGUI counterText)
    {
        Vector3 currentDirection = handTransform.position - transform.position;
        
        float angle = Vector3.SignedAngle(initialDirection, currentDirection, transform.forward);
        
        angleSum += angle;
        
        initialDirection = currentDirection;

        if (Mathf.Abs(angleSum) >= 360f)
        {
            circleCount++;
            counterText.text = $"Circles: {circleCount}";

            angleSum = 0f;
        }
    }
}
