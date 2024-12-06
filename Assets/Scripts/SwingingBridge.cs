
using UnityEngine;

public class BridgeSwing : MonoBehaviour
{
    [Header("Swing Settings")]
    public float maxAngle = 65f; 
    public float minSpeed = 0.1f; 
    public float maxSpeed = 500f; 
    public float swingPeriod = 5f; 

    //[Header("Control Settings")]
    //[SerializeField]
    private bool isMoving = false; // If the Scene starts with this in a false, ONLY change it with the function SetMovement(true);

    private float currentAngle;
    private float elapsedTime;

    void Start()
    {
        currentAngle = maxAngle;
        ApplyRotation();

        if (isMoving)
        {
            elapsedTime = Mathf.Asin(currentAngle / maxAngle) / (Mathf.PI * 2) * swingPeriod;
        }
    }

    void Update()
    {
        if (!isMoving)
            return; 

        elapsedTime += Time.deltaTime;

        float normalizedTime = (elapsedTime % swingPeriod) / swingPeriod;

        float sineValue = Mathf.Sin(normalizedTime * Mathf.PI * 2);

        float speedModifier = Mathf.Abs(sineValue);
        float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, speedModifier);

        currentAngle = maxAngle * sineValue;

        ApplyRotation();
    }

    private void ApplyRotation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }

    public void SetMovement(bool move)
    {
        isMoving = move;

        if (move)
        {
            elapsedTime = Mathf.Asin(maxAngle / maxAngle) / (Mathf.PI * 2) * swingPeriod;
            currentAngle = maxAngle; 
            ApplyRotation();
        }
    }
}
