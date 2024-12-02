using UnityEngine;
using TMPro;
using System.Reflection;

public class IntegerToTextMeshPro : MonoBehaviour
{
    [SerializeField] private Object targetScript; 
    [SerializeField] private string integerFieldName;
    [SerializeField] private TextMeshProUGUI targetText; 

    private FieldInfo fieldInfo;

    private void Start()
    {
        if (targetScript != null && !string.IsNullOrEmpty(integerFieldName))
        {
            fieldInfo = targetScript.GetType().GetField(integerFieldName);
            if (fieldInfo == null || fieldInfo.FieldType != typeof(int))
            {
                Debug.LogError("Field not found or incorrect field type. Please ensure it's a public integer.");
                enabled = false; 
                return;
            }
        }
        else
        {
            Debug.LogError("Target script or integer field name is not set.");
            enabled = false;
        }
    }

    private void Update()
    {
        if (fieldInfo != null)
        {
            int currentValue = (int)fieldInfo.GetValue(targetScript);

            targetText.text = $"{integerFieldName}: {currentValue}";
        }
    }
}
