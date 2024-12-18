using UnityEngine;

public class CounterChecker : MonoBehaviour
{
    [SerializeField]
    private ObjectCoveringBridge objectCoveringScript; 

    [SerializeField]
    private EnableDisableGameObject enableDisableScript;

    [SerializeField]
    private GameObject targetObjectToFadeIn;

    [SerializeField]
    private GameObject fadeOutObject1; 
    
    [SerializeField]
    private GameObject fadeOutObject2; 

    [SerializeField] private Animator roary;

    private void Update()
    {
        if (objectCoveringScript != null && enableDisableScript != null && targetObjectToFadeIn != null)
        {
            if (objectCoveringScript.currentCount >= objectCoveringScript.totalCount)
            {
                enableDisableScript.EnableObject(targetObjectToFadeIn);
                roary.SetBool("roarySquat", false); 
                
                if (fadeOutObject1 != null)
                    enableDisableScript.DisableObject(fadeOutObject1);
                
                if (fadeOutObject2 != null)
                    enableDisableScript.DisableObject(fadeOutObject2);
            }
        }
    }
}
