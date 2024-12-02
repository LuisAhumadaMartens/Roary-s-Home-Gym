using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindmillSettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text divisionFactorText; 
    [SerializeField] private float defaultWindmillDivisionFactor = 40f; 
    private float windmillDivisionFactor;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("WindmillDivisionFactor"))
        {
            windmillDivisionFactor = PlayerPrefs.GetFloat("WindmillDivisionFactor");
        }
        else
        {
            windmillDivisionFactor = defaultWindmillDivisionFactor;
            PlayerPrefs.SetFloat("WindmillDivisionFactor", windmillDivisionFactor);
            PlayerPrefs.Save();
        }

        UpdateDivisionFactorText();
    }

    public void AdjustDivisionFactor(int adjustment)
    {
        windmillDivisionFactor += adjustment;
        windmillDivisionFactor = Mathf.Clamp(windmillDivisionFactor, 1f, 100f); // Clamp to a reasonable range
        PlayerPrefs.SetFloat("WindmillDivisionFactor", windmillDivisionFactor);
        PlayerPrefs.Save();
        UpdateDivisionFactorText();
    }

    public void ResetToDefault()
    {
        windmillDivisionFactor = defaultWindmillDivisionFactor;
        PlayerPrefs.SetFloat("WindmillDivisionFactor", windmillDivisionFactor);
        PlayerPrefs.Save();
        UpdateDivisionFactorText();
    }

    private void UpdateDivisionFactorText()
    {
        if (divisionFactorText != null)
        {
            divisionFactorText.text = $"Division Factor: {windmillDivisionFactor:F1}";
        }
    }
}

