using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChoose : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage; 

    [SerializeField]
    private bool enableFade = true;

    private int sceneNumber; 

    public void ChangeScene(int sceneNumber)
    {
        this.sceneNumber = sceneNumber;

        if (enableFade && fadeImage != null)
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
        else
        {
            SceneManager.LoadSceneAsync(sceneNumber);
        }
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        float fadeDuration = PlayerPrefs.GetFloat("FadeDuration", 1.0f);

        fadeImage.gameObject.SetActive(true);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadSceneAsync(sceneNumber);
    }
}
