using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectCoveringBridge : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private GameObject boundaryObject; 

    [SerializeField]
    private Image coveredBackground;

    [SerializeField]
    private float fadeDuration = 1f;

    [Header("Counter Settings")]
    [SerializeField]
    private TextMeshProUGUI totalCountText;

    [SerializeField]
    private TextMeshProUGUI currentCountText;

    private MeshCollider boundaryCollider;
    private SphereCollider playerCollider;

    private bool isPlayerInside; 
    private Coroutine backgroundFadeCoroutine;
    
    [Header("Public Values")]
    public float totalCount = 20f; 
    private float currentCount = 0f;

    private float lastZRotation = 0f; 

    private void Start()
    {
        coveredBackground?.gameObject.SetActive(false);

        boundaryCollider = boundaryObject?.GetComponent<MeshCollider>();
        playerCollider = playerObject?.GetComponent<SphereCollider>();

        UpdateCounters();

        lastZRotation = NormalizeAngle(boundaryObject.transform.eulerAngles.z);
    }

    private void Update()
    {
        if (boundaryCollider == null || playerObject == null || playerCollider == null)
            return;

        Vector3 playerPosition = playerObject.transform.position;
        float playerRadius = playerCollider.radius * playerObject.transform.lossyScale.x;
        bool currentlyInside = IsSphereInsideMesh(playerPosition, playerRadius);

        if (currentlyInside != isPlayerInside)
        {
            isPlayerInside = currentlyInside;
            HandlePlayerBoundaryChange(currentlyInside);
        }

        CheckRotationCrossing();
    }

    private bool IsSphereInsideMesh(Vector3 sphereCenter, float sphereRadius)
    {
        return boundaryCollider.bounds.Contains(sphereCenter + Vector3.up * sphereRadius) &&
               boundaryCollider.bounds.Contains(sphereCenter - Vector3.up * sphereRadius);
    }

    private void HandlePlayerBoundaryChange(bool inside)
    {
        if (inside)
        {
            StartFade(coveredBackground, true);
        }
        else
        {
            StartFade(coveredBackground, false);
        }
    }

    private void StartFade(Image image, bool fadeIn)
    {
        if (image == null) return;

        if (fadeIn)
        {
            image.gameObject.SetActive(true);
        }

        if (backgroundFadeCoroutine != null)
            StopCoroutine(backgroundFadeCoroutine);

        backgroundFadeCoroutine = StartCoroutine(FadeImage(image, fadeIn));
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

    private void CheckRotationCrossing()
    {
        float currentZRotation = NormalizeAngle(boundaryObject.transform.eulerAngles.z);

        if (Mathf.Sign(currentZRotation) != Mathf.Sign(lastZRotation))
        {
            if (currentZRotation == 0f || Mathf.Abs(lastZRotation) > 179f)
            {
                return;
            }

            if (isPlayerInside)
            {
                totalCount++;
            }
            else
            {
                currentCount++;
            }

            UpdateCounters();
        }

        lastZRotation = currentZRotation;
    }

    private float NormalizeAngle(float angle)
    {
        angle = (angle + 180f) % 360f - 180f;
        return angle;
    }

    private void UpdateCounters()
    {
        if (totalCountText != null)
        {
            totalCountText.text = $"{totalCount}";
        }

        if (currentCountText != null)
        {
            currentCountText.text = $"{currentCount}";
        }
    }
}
