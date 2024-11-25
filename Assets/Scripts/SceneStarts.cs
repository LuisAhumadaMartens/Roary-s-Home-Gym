using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneStarts : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage; 

    private void Start()
    {
        if (fadeImage != null)
        {
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        float fadeDuration = PlayerPrefs.GetFloat("FadeDuration", 1.0f);

        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsed / fadeDuration));
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }
}
