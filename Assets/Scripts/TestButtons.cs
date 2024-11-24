using UnityEngine;
using TMPro;

public class TestButtons : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    private int counter = 0;

    public void OnButtonPressed()
    {
        counter++; 
        UpdateCounterText(); 
    }

    private void UpdateCounterText()
    {
        if (counterText != null)
        {
            counterText.text = "" + counter;
        }
    }

    private void Start()
    {
        UpdateCounterText();
    }
}

