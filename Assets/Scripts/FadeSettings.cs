using UnityEngine;

public class FadeSettings : MonoBehaviour
{
    [SerializeField]
    private float defaultFadeDuration = 1.0f; 

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("FadeDuration"))
        {
            PlayerPrefs.SetFloat("FadeDuration", defaultFadeDuration);
            PlayerPrefs.Save();
        }
    }
}

