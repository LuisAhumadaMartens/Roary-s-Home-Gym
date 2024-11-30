using UnityEngine;
using System.Collections;

public class EnableDisableGameObject : MonoBehaviour
{
    public float fadeDuration = 1f;

    public void EnableObject(GameObject obj)
    {
        if (obj != null)
        {
            if (obj.activeSelf)
            {
                return;
            }

            obj.SetActive(true);
            StartCoroutine(FadeIn(obj));
        }
        else
        {
            Debug.LogWarning("GameObject is null.");
        }
    }

    public void DisableObject(GameObject obj)
    {
        if (obj != null)
        {
            StartCoroutine(FadeOut(obj));
        }
        else
        {
            Debug.LogWarning("GameObject is null.");
        }
    }

    private IEnumerator FadeIn(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        obj.SetActive(false);
    }
}

