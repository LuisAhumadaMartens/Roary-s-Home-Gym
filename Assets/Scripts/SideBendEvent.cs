using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SideBendEvent : MonoBehaviour
{
    [Header("Serialized Fields")]
    [SerializeField] private SideBendTracker sideBendTracker; // Reference to the SideBendTracker script
    [SerializeField] private TextMeshProUGUI instructionText; // Single TextMeshPro for instructions
    [SerializeField] private TextMeshProUGUI correctCountText; // TextMeshPro to display the correct count
    [SerializeField] private TextMeshProUGUI targetCountText; // TextMeshPro to display the target count
    [SerializeField] private int targetCount = 10; // Total reps needed to complete the set
    [SerializeField] private EnableDisableGameObject enableDisableManager; // Reference to the enable/disable script
    [SerializeField] private GameObject nextGameObject; // The GameObject to enable after completion

    [Header("Debug Info")]
    private int correctCount = 0; // Track correct repetitions
    private SideBendTracker.SideBendState lastState = SideBendTracker.SideBendState.None;
    private bool isWaitingForNone = false; // Ensure player returns to "None" before the next bend

    public void ActivateGame()
    {
        // Initialize the game
        correctCount = 0;
        UpdateUI();

        // Generate the first instruction
        GenerateNewInstruction();
    }

    void Update()
    {
        if (sideBendTracker == null || instructionText == null) return;

        // Detect the current bend state
        SideBendTracker.SideBendState currentState = sideBendTracker.DetectSideBend();

        // Ensure the player has returned to "None" before the next side bend
        if (isWaitingForNone && currentState == SideBendTracker.SideBendState.None)
        {
            isWaitingForNone = false; // Reset waiting flag
        }

        // Only proceed if the player is not in the "None" state
        if (currentState != SideBendTracker.SideBendState.None && !isWaitingForNone)
        {
            // Check if the instruction matches the player's action
            if (instructionText.text == "Left" && currentState == SideBendTracker.SideBendState.Left ||
                instructionText.text == "Right" && currentState == SideBendTracker.SideBendState.Right)
            {
                if (lastState == SideBendTracker.SideBendState.None) // Only valid if starting from "None"
                {
                    correctCount++;

                    // Check if the player has completed the target
                    if (correctCount >= targetCount)
                    {
                        HandleCompletion();
                        return; // Stop further processing
                    }

                    // Generate a new instruction and update UI
                    GenerateNewInstruction();
                    UpdateUI();
                    isWaitingForNone = true; // Wait for "None" before the next move
                }
            }
            else if (lastState == SideBendTracker.SideBendState.None) // Handle incorrect action only once per attempt
            {
                targetCount++;

                // Update UI to reflect the updated target count
                UpdateUI();
                isWaitingForNone = true; // Wait for "None" before allowing further actions
            }
        }

        // Update last state
        lastState = currentState;
    }

    private void GenerateNewInstruction()
    {
        // Randomly generate "Left" or "Right"
        instructionText.text = Random.Range(0, 2) == 0 ? "Left" : "Right";
    }

    private void UpdateUI()
    {
        // Update correct count in UI
        if (correctCountText != null)
        {
            correctCountText.text = $"Correct: {correctCount}";
        }

        // Update target count in UI
        if (targetCountText != null)
        {
            targetCountText.text = $"Target: {targetCount}";
        }
    }

    private void HandleCompletion()
    {
        // Use the EnableDisableGameObject script to enable the next object and disable this one
        if (enableDisableManager != null && nextGameObject != null)
        {
            enableDisableManager.EnableObject(nextGameObject);
            enableDisableManager.DisableObject(gameObject);
        }
    }
}
