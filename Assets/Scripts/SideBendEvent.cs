using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SideBendEvent : MonoBehaviour
{
    [Header("Serialized Fields")]
    [SerializeField] private SideBendTracker sideBendTracker;
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private TextMeshProUGUI correctCountText;
    [SerializeField] private TextMeshProUGUI targetCountText; 
    [SerializeField] private int targetCount = 10; 
    [SerializeField] private EnableDisableGameObject enableDisableManager; 
    [SerializeField] private GameObject nextGameObject;

    [Header("Debug Info")]
    private int correctCount = 0;
    private SideBendTracker.SideBendState lastState = SideBendTracker.SideBendState.None;
    private bool isWaitingForNone = false;

    public void ActivateGame()
    {
        correctCount = 0;
        UpdateUI();

        GenerateNewInstruction();
    }

    void Update()
    {
        if (sideBendTracker == null || instructionText == null) return;

        SideBendTracker.SideBendState currentState = sideBendTracker.DetectSideBend();

        if (isWaitingForNone && currentState == SideBendTracker.SideBendState.None)
        {
            isWaitingForNone = false; 
        }

        if (currentState != SideBendTracker.SideBendState.None && !isWaitingForNone)
        {
            if (instructionText.text == "Left" && currentState == SideBendTracker.SideBendState.Left ||
                instructionText.text == "Right" && currentState == SideBendTracker.SideBendState.Right)
            {
                if (lastState == SideBendTracker.SideBendState.None) 
                {
                    correctCount++;

                    if (correctCount >= targetCount)
                    {
                        HandleCompletion();
                        return; 
                    }

                    GenerateNewInstruction();
                    UpdateUI();
                    isWaitingForNone = true; 
                }
            }
            else if (lastState == SideBendTracker.SideBendState.None)
            {
                targetCount++;

                UpdateUI();
                isWaitingForNone = true; 
            }
        }

        lastState = currentState;
    }

    private void GenerateNewInstruction()
    {
        instructionText.text = Random.Range(0, 2) == 0 ? "Left" : "Right";
    }

    private void UpdateUI()
    {
        if (correctCountText != null)
        {
            correctCountText.text = $"{correctCount}";
        }

        if (targetCountText != null)
        {
            targetCountText.text = $"{targetCount}";
        }
    }

    private void HandleCompletion()
    {
        if (enableDisableManager != null && nextGameObject != null)
        {
            enableDisableManager.EnableObject(nextGameObject);
            enableDisableManager.DisableObject(gameObject);
        }
    }
}
