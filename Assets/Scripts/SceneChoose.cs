
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private Object sceneAsset; // This will hold the scene asset

    private string sceneName; // The name of the scene to load

    private void Awake()
    {
#if UNITY_EDITOR
        // Ensure the field is assigned correctly
        if (sceneAsset != null)
        {
            sceneName = AssetDatabase.GetAssetPath(sceneAsset)
                .Replace("Assets/", "") // Remove "Assets/" from the path
                .Replace(".unity", ""); // Remove ".unity" extension
        }
#endif
    }

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is empty or not assigned!");
        }
    }
}

