
using UnityEngine;

public class BridgeHeight : MonoBehaviour
{
    [SerializeField] private GameObject bridge; // GameObject to move
    [SerializeField] private GameObject bridgeOffset; // Object for getting the Z location offset
    [SerializeField] private float percentage = 0.3f; // Percentage of the player height to apply (e.g., 0.3 for 30%)
    [SerializeField] private float offset = 0f; // Additional offset (default 0)

    private const string PlayerHeightKey = "PlayerHeight"; // PlayerPrefs key for height
    private float playerHeight; // Player height from PlayerPrefs

    void Start()
    {
        // Get player height from PlayerPrefs
        playerHeight = PlayerPrefs.GetFloat(PlayerHeightKey, 1f); // Default to 1 if not found

        // Calculate the Y position for the bridge
        float calculatedY = (playerHeight * percentage) + Mathf.Abs(bridgeOffset.transform.position.z) + offset;

        // Apply the calculated Y position to the bridge's position
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

