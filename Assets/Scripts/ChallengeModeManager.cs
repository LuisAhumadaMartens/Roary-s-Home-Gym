using UnityEngine;

public class ChallengeModeManager : MonoBehaviour
{
    [SerializeField] private SceneChoose sceneChooser; 
    [SerializeField] private int totalScenes;

    private const string ChallengeModeKey = "ChallengeMode";
    private const string CountKey = "ExerciseCount";
    private const string TotalScenesKey = "TotalScenes";

    void Start()
    {
        PlayerPrefs.SetInt(TotalScenesKey, totalScenes);
        PlayerPrefs.Save();
    }

    public void SetChallengeMode(bool isEnabled)
    {
        PlayerPrefs.SetInt(ChallengeModeKey, isEnabled ? 1 : 0);

        if (isEnabled)
        {
            PlayerPrefs.SetInt(CountKey, 0);
        }
        PlayerPrefs.Save();

        Debug.Log($"ChallengeMode set to: {isEnabled}");
    }

    public bool GetChallengeMode()
    {
        return PlayerPrefs.GetInt(ChallengeModeKey, 0) == 1; 
    }

    public void NextExercise()
    {
        int count = PlayerPrefs.GetInt(CountKey, 0);
        int totalScenes = PlayerPrefs.GetInt(TotalScenesKey, 1);

        count++; 

        if (count > totalScenes)
        {
            count = 0; 
        }

        PlayerPrefs.SetInt(CountKey, count); 
        PlayerPrefs.Save();

        Debug.Log($"Next Exercise: Count set to {count}");

        if (sceneChooser != null)
        {
            sceneChooser.ChangeScene(count);
        }
        else
        {
            Debug.LogWarning("SceneChoose script not assigned.");
        }
    }
}
