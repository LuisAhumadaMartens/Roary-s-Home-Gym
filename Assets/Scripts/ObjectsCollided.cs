
using System.Collections;
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
    private GameObject fadeOutObject1; 
    
    [SerializeField]
    private GameObject fadeOutObject2; 

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

            if (fadeOutObject1 != null)
                enableDisableScript.DisableObject(fadeOutObject1);

            if (fadeOutObject2 != null)
                enableDisableScript.DisableObject(fadeOutObject2);
        }
    }
}
