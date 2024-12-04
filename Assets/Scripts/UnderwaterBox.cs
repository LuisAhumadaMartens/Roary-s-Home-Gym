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

    [Header("Public Value")]
    public bool isUnderwater = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == mainCamera)
        {
            EnableEffects(true);
            fogSettings.SwitchFog(false);
            isUnderwater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == mainCamera)
        {
            EnableEffects(false);
            fogSettings.SwitchFog(true); 
            isUnderwater = false;
        }
    }

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
