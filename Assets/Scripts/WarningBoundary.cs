using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningBoundary : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject; 

    [SerializeField]
    private Image warningBackground;

    [SerializeField]
    private Image warningText;

    [SerializeField]
    private float fadeDuration = 0.25f;

    private BoxCollider boundaryCollider;
    private SphereCollider playerCollider;
    
    [Header("Output Values - DO NOT CHANGE")]
    public bool isPlayerOutside;
    private Coroutine backgroundFadeCoroutine;
    private Coroutine textFadeCoroutine;

    private void Start()
    {
        warningBackground?.gameObject.SetActive(false);
        warningText?.gameObject.SetActive(false);

        boundaryCollider = GetComponent<BoxCollider>();
        playerCollider = playerObject?.GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (boundaryCollider == null || playerObject == null || playerCollider == null)
            return;

        Vector3 playerPosition = playerObject.transform.position;
        float playerRadius = playerCollider.radius * playerObject.transform.lossyScale.x;

        bool currentlyOutside = !IsSphereInsideBox(playerPosition, playerRadius);

        if (currentlyOutside != isPlayerOutside)
        {
            isPlayerOutside = currentlyOutside;
            HandlePlayerBoundaryChange(currentlyOutside);
        }
    }

    private bool IsSphereInsideBox(Vector3 sphereCenter, float sphereRadius)
    {
        Bounds boxBounds = boundaryCollider.bounds;

        return sphereCenter.x + sphereRadius > boxBounds.min.x &&
               sphereCenter.x - sphereRadius < boxBounds.max.x &&
               sphereCenter.y + sphereRadius > boxBounds.min.y &&
               sphereCenter.y - sphereRadius < boxBounds.max.y &&
               sphereCenter.z + sphereRadius > boxBounds.min.z &&
               sphereCenter.z - sphereRadius < boxBounds.max.z;
    }

    private void HandlePlayerBoundaryChange(bool outside)
    {
        if (outside)
        {
            StartFade(warningBackground, true);
            StartFade(warningText, true);
        }
        else
        {
            StartFade(warningBackground, false);
            StartFade(warningText, false);
        }
    }

    private void StartFade(Image image, bool fadeIn)
    {
        if (image == null) return;

        if (fadeIn)
        {
            image.gameObject.SetActive(true); 
        }

        if (image == warningBackground && backgroundFadeCoroutine != null)
            StopCoroutine(backgroundFadeCoroutine);
        if (image == warningText && textFadeCoroutine != null)
            StopCoroutine(textFadeCoroutine);

        Coroutine fadeCoroutine = StartCoroutine(FadeImage(image, fadeIn));

        if (image == warningBackground)
            backgroundFadeCoroutine = fadeCoroutine;
        else if (image == warningText)
            textFadeCoroutine = fadeCoroutine;
    }

    private IEnumerator FadeImage(Image image, bool fadeIn)
    {
        float timer = 0f;
        Color imageColor = image.color;
        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = fadeIn ? 1f : 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            imageColor.a = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            image.color = imageColor;
            yield return null;
        }

        imageColor.a = endAlpha;
        image.color = imageColor;

        if (!fadeIn)
            image.gameObject.SetActive(false);
    }
}
