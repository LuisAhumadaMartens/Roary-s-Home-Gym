using UnityEngine;

public class FogSwitch : MonoBehaviour
{
    [Header("Fog Setting 1 (Default)")]
    [SerializeField] private Color fogColor1 = new Color(0.5f, 0.5f, 0.5f); 
    [SerializeField] private float fogDensity1 = 0.05f; 
    [SerializeField] private float fogStartDistance1 = 10f; 
    [SerializeField] private float fogEndDistance1 = 50f;

    [Header("Fog Setting 2 (Alternative)")]
    [SerializeField] private Color fogColor2 = new Color(0.1f, 0.1f, 0.1f); 
    [SerializeField] private float fogDensity2 = 0.02f; 
    [SerializeField] private float fogStartDistance2 = 5f;
    [SerializeField] private float fogEndDistance2 = 30f;

    private bool isFogSetting1 = true; 

    void Start()
    {
        ApplyFogSettings(isFogSetting1);
    }

    public void SwitchFog(bool isDefault)
    {
        isFogSetting1 = isDefault; 
        ApplyFogSettings(isFogSetting1);
    }

    private void ApplyFogSettings(bool isDefault)
    {
        RenderSettings.fog = true;

        if (isDefault)
        {
            SetFogSettings(fogColor1, fogDensity1, fogStartDistance1, fogEndDistance1);
        }
        else
        {
            SetFogSettings(fogColor2, fogDensity2, fogStartDistance2, fogEndDistance2);
        }
    }

    private void SetFogSettings(Color color, float density, float startDistance, float endDistance)
    {
        RenderSettings.fogColor = color;
        RenderSettings.fogDensity = density;
        RenderSettings.fogStartDistance = startDistance;
        RenderSettings.fogEndDistance = endDistance;
    }
}
