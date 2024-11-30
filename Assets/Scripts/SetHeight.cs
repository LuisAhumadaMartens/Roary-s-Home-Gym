using UnityEngine;
using TMPro;

public class SetHeight : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI heightText; 
    [SerializeField] private TextMeshProUGUI savedHeightText;
    private Transform vrCameraTransform;
    private float currentHeight;

    private const string PlayerHeightKey = "PlayerHeight";

    void Start()
    {
        vrCameraTransform = Camera.main.transform;

        if (PlayerPrefs.HasKey(PlayerHeightKey))
        {
            float savedHeight = PlayerPrefs.GetFloat(PlayerHeightKey);
            savedHeightText.text = $"{savedHeight:F2} m";
        }
        else
        {
            savedHeightText.text = "#.## m";
        }
    }

    void Update()
    {
        if (vrCameraTransform != null && heightText != null)
        {
            currentHeight = vrCameraTransform.position.y;
            heightText.text = $"{currentHeight:F2} m";
        }
    }

    public void SaveHeight()
    {
        if (vrCameraTransform != null)
        {
            PlayerPrefs.SetFloat(PlayerHeightKey, currentHeight);
            PlayerPrefs.Save();

            if (savedHeightText != null)
            {
                savedHeightText.text = $"{currentHeight:F2} m";
            }
        }
    }
}
