using UnityEngine;

public class NextExerciseScene : MonoBehaviour
{
    [SerializeField] private GameObject challengeModeObject; 
    [SerializeField] private GameObject regularModeObject; 

    private const string ChallengeModeKey = "ChallengeMode"; 

    void Start()
    {
        if (challengeModeObject != null) challengeModeObject.SetActive(false);
        if (regularModeObject != null) regularModeObject.SetActive(false);

        bool isChallengeMode = PlayerPrefs.GetInt(ChallengeModeKey, 0) == 1;

        if (isChallengeMode)
        {
            if (challengeModeObject != null)
                challengeModeObject.SetActive(true);
        }
        else
        {
            if (regularModeObject != null)
                regularModeObject.SetActive(true);
        }

        Debug.Log($"ChallengeMode is {(isChallengeMode ? "enabled" : "disabled")}. Activated the appropriate object.");
    }
}
