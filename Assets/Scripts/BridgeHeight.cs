
using UnityEngine;

public class BridgeHeight : MonoBehaviour
{
    [SerializeField] private GameObject bridge;
    [SerializeField] private GameObject bridgeOffset; 
    [SerializeField] private float percentage = 0.3f;
    [SerializeField] private float playerOffset = 12.5f;
    [SerializeField] private float offset = 1.1f;

    private const string PlayerHeightKey = "PlayerHeight"; 
    private float playerHeight; 

    void Start()
    {
        playerHeight = PlayerPrefs.GetFloat(PlayerHeightKey, 1f);

        float calculatedY = ((playerHeight + (playerOffset / 100)) * percentage) + Mathf.Abs(bridgeOffset.transform.position.z) + offset;

        if (bridge != null)
        {
            Vector3 bridgePosition = bridge.transform.position;
            bridgePosition.y = calculatedY;
            bridge.transform.position = bridgePosition;
        }
        else
        {
            Debug.LogWarning("Bridge GameObject is not assigned.");
        }
    }
}

