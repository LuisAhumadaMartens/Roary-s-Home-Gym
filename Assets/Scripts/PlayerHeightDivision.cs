using UnityEngine;

public class PlayerHeightDivision : MonoBehaviour
{
    private const string PlayerHeightKey = "PlayerHeight";
    
    [Tooltip("Player's calf distance in centimeters")]
    public float PlayerCalfDistanceInCm = 5f;

    public Transform vrCameraTransform;

    [Tooltip("Public Value")]
    public bool isAboveMidpoint;   

    private bool heightASet = false;
    private bool heightBSet = false;

    private float heightA;
    private float heightB;

    void Start()
    {
        if (PlayerPrefs.HasKey(PlayerHeightKey))
        {
            heightA = PlayerPrefs.GetFloat(PlayerHeightKey);
            float playerCalfDistanceInMeters = PlayerCalfDistanceInCm / 100f; 
            heightB = heightA + playerCalfDistanceInMeters;

            heightASet = true;
            heightBSet = true;
        }
        else
        {
            Debug.LogWarning($"PlayerHeight not found in PlayerPrefs. Please set it first.");
        }
    }

    void Update()
    {
        if (heightASet && heightBSet)
        {
            float midpoint = (heightA + heightB) / 2;

            float playerHeight = vrCameraTransform.position.y;

            isAboveMidpoint = playerHeight > midpoint;
        }
    }
}
