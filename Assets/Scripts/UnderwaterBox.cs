using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnderwaterBox : MonoBehaviour
{
    [Header("Depth Parameters")]
    [SerializeField] private Transform mainCamera;
    
    [Header("Post Processing Volume")]
    [SerializeField] private Volume postProcessingVolume;
    
    [Header("Post Processing Profiles")]
    [SerializeField] private VolumeProfile surfacePostProcessing;
    [SerializeField] private VolumeProfile underwaterPostProcessing;

    [Header("External Game Objects")]
    [SerializeField] private FogSwitch fogSettings;

    private bool isUnderwater = false; // Track whether the camera is underwater

    // Called when the camera enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the camera
        if (other.transform == mainCamera)
        {
            EnableEffects(true);
            fogSettings.SwitchFog(false); // Set underwater fog settings
            isUnderwater = true;
        }
    }

    // Called when the camera exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the camera
        if (other.transform == mainCamera)
        {
            EnableEffects(false);
            fogSettings.SwitchFog(true); // Set default fog settings
            isUnderwater = false;
        }
    }

    // Enable or disable the underwater post-processing effects
    private void EnableEffects(bool active)
    {
        if (active)
        {
            postProcessingVolume.profile = underwaterPostProcessing;
        }
        else
        {
            postProcessingVolume.profile = surfacePostProcessing;
        }
    }
}
