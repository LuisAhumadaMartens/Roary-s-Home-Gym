using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private EnableDisableGameObject enableDisableScript;
    [SerializeField] private List<GameObject> backgrounds;
    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private List<GameObject> buttonsInfinite;
    [SerializeField] private GameObject thisButton;
    [SerializeField] private GameObject thisBackground;
    [SerializeField] private GameObject buttonInfinite;
    [SerializeField] private TextMeshProUGUI titleGameObject;
    [SerializeField] private TextMeshProUGUI descriptionGameObject;
    [SerializeField] private string title;
    [SerializeField] private string description;

    public void UpdateUI()
    {
        if (titleGameObject != null)
        {
            titleGameObject.text = title;
        }

        if (descriptionGameObject != null)
        {
            descriptionGameObject.text = description;
        }

        foreach (GameObject background in backgrounds)
        {
            if (background != null)
            {
                enableDisableScript.DisableObject(background);
            }
        }

        foreach (GameObject button in buttons)
        {
            if (button != null)
            {
                enableDisableScript.DisableObject(button);
            }
        }

        foreach (GameObject button in buttonsInfinite)
        {
            if (button != null)
            {
                enableDisableScript.DisableObject(button);
            }
        }

        if (thisButton != null)
        {
            enableDisableScript.EnableObject(thisButton);
        }

        if (thisBackground != null)
        {
            enableDisableScript.EnableObject(thisBackground);
        }

        if (buttonInfinite != null)
        {
            enableDisableScript.EnableObject(buttonInfinite);
        }
    }
}
