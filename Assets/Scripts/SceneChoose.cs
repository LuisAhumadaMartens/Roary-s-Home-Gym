using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;  // For EventSystem
using UnityEngine.XR.Interaction.Toolkit;  // For XRInteractionManager

public class SceneChoose : MonoBehaviour
{
    [SerializeField]
    private int sceneNumber;

    public void ChangeScene()
    {
        // Ensure only the new scene is loaded
        SceneManager.LoadScene(sceneNumber, LoadSceneMode.Single);
        CleanupDuplicateManagers();
    }

    private void OnDisable()
    {
        // Cleanup managers to avoid duplicates
        CleanupDuplicateManagers();
    }

    private void CleanupDuplicateManagers()
    {
        // Destroy extra XR Interaction Managers
        XRInteractionManager[] managers = FindObjectsOfType<XRInteractionManager>();
        if (managers.Length > 1)
        {
            for (int i = 1; i < managers.Length; i++) // Skip the first manager
            {
                Destroy(managers[i].gameObject);
            }
        }

        // Destroy extra Event Systems
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();
        if (eventSystems.Length > 1)
        {
            for (int i = 1; i < eventSystems.Length; i++) // Skip the first Event System
            {
                Destroy(eventSystems[i].gameObject);
            }
        }
    }
}
