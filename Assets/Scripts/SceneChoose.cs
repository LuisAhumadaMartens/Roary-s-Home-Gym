
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChoose : MonoBehaviour
{
    [SerializeField]
    private int sceneNumber; 

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(sceneNumber);

    }
}

