using System.Collections.Generic;
using UnityEngine;

public class ObjectsCollided : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToCollide;

    [SerializeField]
    private EnableDisableGameObject enableDisableScript; 

    [SerializeField]
    private GameObject targetObjectToFadeIn;

    [SerializeField]
    private List<GameObject> fadeOutObjects;

    private bool hasTriggeredAction = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name);

        if (!hasTriggeredAction && other.gameObject == objectToCollide)
        {
            Debug.Log("Collided with the specified object: " + objectToCollide.name);

            PerformFadeActions();

            hasTriggeredAction = true;

            Destroy(this);
        }
    }

    private void PerformFadeActions()
    {
        if (enableDisableScript != null && targetObjectToFadeIn != null)
        {
            enableDisableScript.EnableObject(targetObjectToFadeIn);

            foreach (GameObject fadeOutObject in fadeOutObjects)
            {
                if (fadeOutObject != null)
                {
                    enableDisableScript.DisableObject(fadeOutObject);
                }
            }
        }
    }
}
