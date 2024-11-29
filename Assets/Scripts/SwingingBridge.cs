
using UnityEngine;

public class BridgeSwing : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 1f; 
    [SerializeField] private float maxAngle = 45f;
    [SerializeField] private AnimationCurve swingCurve; 
    [SerializeField] private bool isSwinging = true; 

    private float swingTime = 0f; // Variable to track the swing progression

    void Update()
    {
        if (isSwinging)
        {
            SwingBridge();
        }
    }

    void SwingBridge()
    {
        swingTime += Time.deltaTime * swingSpeed;
        float time = Mathf.PingPong(swingTime, 1f);
        float curveValue = swingCurve.Evaluate(time); 
        float angle = Mathf.Lerp(-maxAngle, maxAngle, curveValue); 
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void StartSwinging()
    {
        isSwinging = true;
    }

    public void StopSwinging()
    {
        isSwinging = false;
    }
}

