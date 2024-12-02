using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWindmill : MonoBehaviour
{
    [SerializeField] private HandCircleCounter handCircleCounter;
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private float divisionFactor = 40f;
    [SerializeField] private float smoothTime = 0.2f;

    private float currentZPosition;

    private void Update()
    {
        int totalCircles = handCircleCounter.rightHandCircles + handCircleCounter.leftHandCircles;

        float targetZPosition = totalCircles / divisionFactor;

        currentZPosition = Mathf.Lerp(currentZPosition, -targetZPosition, Time.deltaTime / smoothTime);

        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, objectToMove.transform.position.y, currentZPosition);
    }
}
