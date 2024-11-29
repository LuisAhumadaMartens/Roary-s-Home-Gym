
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectCovering : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject; // The GameObject to detect (with SphereCollider).

    [SerializeField]
    private GameObject boundaryObject; // The GameObject with MeshCollider.

    [SerializeField]
    private Image coveredBackground; // The warning background image.

    [SerializeField]
    private float fadeDuration = 1f; // Duration of the fade animation.

    private MeshCollider boundaryCollider; // MeshCollider reference.
    private SphereCollider playerCollider; // SphereCollider reference on the player.

    private bool isPlayerInside; // Track if the player is currently inside.
    private Coroutine backgroundFadeCoroutine;

    private void Start()
    {
        // Ensure the image is disabled at the start.
        coveredBackground?.gameObject.SetActive(false);

        boundaryCollider = boundaryObject?.GetComponent<MeshCollider>();
        playerCollider = playerObject?.GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (boundaryCollider == null || playerObject == null || playerCollider == null)
            return;

        // Calculate the effective position of the player's sphere collider.
        Vector3 playerPosition = playerObject.transform.position;
        float playerRadius = playerCollider.radius * playerObject.transform.lossyScale.x;

        // Check if the player's sphere is inside the mesh bounds.
        bool currentlyInside = IsSphereInsideMesh(playerPosition, playerRadius);

        if (currentlyInside != isPlayerInside)
        {
            isPlayerInside = currentlyInside;
            HandlePlayerBoundaryChange(currentlyInside);
        }
    }

    private bool IsSphereInsideMesh(Vector3 sphereCenter, float sphereRadius)
    {
        // Using the MeshCollider bounds to check if the sphere is inside the mesh.
        return boundaryCollider.bounds.Contains(sphereCenter + Vector3.up * sphereRadius) && 
               boundaryCollider.bounds.Contains(sphereCenter - Vector3.up * sphereRadius);
    }

    private void HandlePlayerBoundaryChange(bool inside)
    {
        if (inside)
        {
            // Fade in the background when the player is inside the boundary.
            StartFade(coveredBackground, true);
        }
        else
        {
            // Fade out the background when the player is outside the boundary.
            StartFade(coveredBackground, false);
        }
    }

    private void StartFade(Image image, bool fadeIn)
    {
        if (image == null) return;

        if (fadeIn)
        {
            image.gameObject.SetActive(true); // Enable the image before fading in.
        }

        if (backgroundFadeCoroutine != null)
            StopCoroutine(backgroundFadeCoroutine);

        Coroutine fadeCoroutine = StartCoroutine(FadeImage(image, fadeIn));
        backgroundFadeCoroutine = fadeCoroutine;
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
            image.gameObject.SetActive(false); // Disable the image after fading out.
    }
}
