using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SideBendEvent : MonoBehaviour
{
    [Header("Serialized Fields")]
    [SerializeField] private SideBendTracker sideBendTracker; // Reference to the SideBendTracker script
    [SerializeField] private GameObject textPrefab; // Prefab of the TextMeshPro object to instantiate
    [SerializeField] private int numberOfTextItems = 5; // Number of TextMeshPro items to generate
    [SerializeField] private TextMeshProUGUI correctCountText; // TextMeshPro to display the correct count
    [SerializeField] private TextMeshProUGUI targetCountText; // TextMeshPro to display the target count
    [SerializeField] private int targetCount = 10; // The number of correct repetitions needed
    [SerializeField] private GameObject spawnParent; // GameObject where the TextMeshPro prefabs will be instantiated

    [Header("Debug Info")]
    [SerializeField] private int totalIncorrect = 0; // Total incorrect actions
    [SerializeField] private TextMeshProUGUI currentSideText; // TextMeshPro for debugging (current side)
    private List<GameObject> textMeshProObjects = new List<GameObject>();
    private List<string> currentInstructions = new List<string>();
    private SideBendTracker.SideBendState lastState = SideBendTracker.SideBendState.None;

    private int correctCount = 0; // Track correct repetitions
    private int currentIndex = 0;

    public void ActivateGame()
    {
        // Clear any previous state
        foreach (var textObj in textMeshProObjects)
        {
            Destroy(textObj); // Destroy all previous TextMeshPro objects
        }
        textMeshProObjects.Clear();

        totalIncorrect = 0;
        correctCount = 0;
        currentIndex = 0;

        // Instantiate the TextMeshPro objects as children of the spawnParent
        for (int i = 0; i < numberOfTextItems; i++)
        {
            GameObject newText = Instantiate(textPrefab, spawnParent.transform); // Instantiate and parent to spawnParent
            textMeshProObjects.Add(newText); // Add the new object to the list
        }

        // Generate initial instruction buffer
        GenerateInstructions();
        UpdateUI();
    }

    void Update()
    {
        if (sideBendTracker == null) return;

        // Get the current bend state
        SideBendTracker.SideBendState currentState = sideBendTracker.DetectSideBend();

        // Display the current side (for debugging)
        if (currentSideText != null)
        {
            currentSideText.text = $"Current Side: {currentState}"; // Show the current side detected (None, Left, or Right)
        }

        // Only proceed if the player is in "None" state before they can do left/right
        if (currentState == SideBendTracker.SideBendState.None)
        {
            return;
        }

        // Get the expected side (left or right) from the current instruction
        string expected = currentInstructions[currentIndex];

        // Check if the current state matches the expected instruction
        if ((expected == "Left" && currentState == SideBendTracker.SideBendState.Left) ||
            (expected == "Right" && currentState == SideBendTracker.SideBendState.Right))
        {
            if (lastState == SideBendTracker.SideBendState.None)
            {
                // Correct move, update the index
                correctCount++;

                // Destroy the top-most TextMeshPro object (index 0)
                Destroy(textMeshProObjects[0]);

                // Instantiate a new random instruction and add it to the top of the list
                string newInstruction = Random.Range(0, 2) == 0 ? "Left" : "Right";
                GameObject newText = Instantiate(textPrefab, spawnParent.transform);
                textMeshProObjects.Insert(0, newText); // Insert at the top of the list

                // Update the text of the new top object
                newText.GetComponent<TextMeshProUGUI>().text = newInstruction;
                currentInstructions.Insert(0, newInstruction); // Insert the new instruction at the top

                // Update the UI
                UpdateUI();

                // If the player has completed the required repetitions
                if (correctCount >= targetCount)
                {
                    Debug.Log("Goal achieved!");
                }
            }
        }
        else
        {
            // Incorrect move: increment the incorrect total
            totalIncorrect++;
            ShiftAndRegenerate();
            UpdateUI();
        }

        // Update last state for the next frame
        lastState = currentState;
    }

    private void GenerateInstructions()
    {
        // Clear the current instruction list
        currentInstructions.Clear();

        // Generate random "Left" or "Right" instructions for the specified number of items
        for (int i = 0; i < numberOfTextItems; i++)
        {
            currentInstructions.Add(Random.Range(0, 2) == 0 ? "Left" : "Right");
        }

        // Update the UI for the new instructions
        for (int i = 0; i < textMeshProObjects.Count; i++)
        {
            if (i < currentInstructions.Count)
            {
                textMeshProObjects[i].GetComponent<TextMeshProUGUI>().text = currentInstructions[i];
            }
            else
            {
                textMeshProObjects[i].GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void ShiftAndRegenerate()
    {
        // Shift the instructions list left (remove the first instruction) and add a new random instruction
        currentInstructions.RemoveAt(0);
        currentInstructions.Add(Random.Range(0, 2) == 0 ? "Left" : "Right");

        // Regenerate the TextMeshPro objects to reflect the updated instructions
        for (int i = 0; i < textMeshProObjects.Count; i++)
        {
            textMeshProObjects[i].GetComponent<TextMeshProUGUI>().text = currentInstructions[i];
        }
    }

    private void UpdateUI()
    {
        // Update correct count display
        if (correctCountText != null)
        {
            correctCountText.text = $"Correct: {correctCount}/{targetCount}";
        }

        // Update target count display
        if (targetCountText != null)
        {
            targetCountText.text = $"Target: {targetCount}";
        }

        // Update total incorrect count display
        if (currentSideText != null)
        {
            currentSideText.text += $"\nIncorrect: {totalIncorrect}";
        }
    }
}
