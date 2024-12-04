using UnityEngine;

public class SideBendTracker : MonoBehaviour
{
    [Header("Input Objects")]
    [SerializeField] private GameObject visor; 
    [SerializeField] private GameObject leftController; 
    [SerializeField] private GameObject rightController; 

    [Header("Settings")]
    [SerializeField] private float tiltThreshold = 15f; 
    [SerializeField] private float handMoveThreshold = 0.2f;

    public enum SideBendState
    {
        None,
        Left,
        Right
    }

    private Vector3 initialLeftHandPosition;
    private Vector3 initialRightHandPosition;

    void Start()
    {
        if (leftController != null) initialLeftHandPosition = leftController.transform.position;
        if (rightController != null) initialRightHandPosition = rightController.transform.position;
    }

    void Update()
    {
        if (visor == null || leftController == null || rightController == null)
        {
            Debug.LogError("Assign all required fields in the Inspector.");
            return;
        }

        SideBendState currentState = DetectSideBend();
        Debug.Log("Current Side Bend State: " + currentState);
    }

    public SideBendState DetectSideBend()
    {
        Vector3 visorEulerAngles = visor.transform.eulerAngles;
        float tiltAngle = visorEulerAngles.z > 180 ? visorEulerAngles.z - 360 : visorEulerAngles.z;

        float leftHandHeight = leftController.transform.position.y;
        float rightHandHeight = rightController.transform.position.y;
        float handHeightDifference = Mathf.Abs(leftHandHeight - rightHandHeight);

        if (Mathf.Abs(tiltAngle) > tiltThreshold && handHeightDifference > handMoveThreshold)
        {
            return tiltAngle > 0 ? SideBendState.Left : SideBendState.Right;
        }

        return SideBendState.None;
    }
}
