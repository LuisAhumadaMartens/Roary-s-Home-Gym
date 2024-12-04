using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CalfRaisesEvent : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerHeightDivision playerHeightDivision;
    [SerializeField] private UnderwaterBox underwaterBox; 
    [SerializeField] private GameObject movableObject;
    [SerializeField] private Image radialFillImage;
    [SerializeField] private EnableDisableGameObject enableDisableScript;

    [Header("Configuration")]
    [SerializeField] private float waterOffset = 0.1f;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float rateToFill = 0.05f;
    [SerializeField] private GameObject objectToEnable;

    private const string PlayerHeightKey = "PlayerHeight";
    private float playerHeight;
    private Vector3 originalPosition;
    private Vector3 aboveMidpointPosition;
    private Vector3 waterTargetPosition;
    private Vector3 waterOriginPosition;
    private float fillAmount = 0f;

    private bool isDisabled = false;
    private bool isActivated = false; 

    void Update()
    {
        if (isDisabled || !isActivated) return; 

        bool isAboveMidpoint = playerHeightDivision.isAboveMidpoint;
        bool isUnderwater = underwaterBox.isUnderwater;

        if (isAboveMidpoint)
        {
            Vector3 targetPosition = waterTargetPosition;
            movableObject.transform.position = Vector3.MoveTowards(
                movableObject.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (!isUnderwater)
            {
                fillAmount += rateToFill * Time.deltaTime;
                fillAmount = Mathf.Clamp01(fillAmount);
                radialFillImage.fillAmount = fillAmount;

                if (fillAmount >= 1f)
                {
                    CompleteEvent();
                }
            }
        }
        else
        {
            Vector3 targetPosition = new Vector3(
                movableObject.transform.position.x,
                playerHeight + waterOffset, 
                movableObject.transform.position.z
            );

            movableObject.transform.position = Vector3.MoveTowards(
                movableObject.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }

    
    public void Activate()
    {
        playerHeight = PlayerPrefs.GetFloat(PlayerHeightKey, 1f);

        Vector3 targetPosition = new Vector3(
            movableObject.transform.position.x,
            playerHeight + waterOffset,
            movableObject.transform.position.z 
        );

        StartCoroutine(MoveToTargetSmoothly(targetPosition));

        originalPosition = movableObject.transform.position;
        aboveMidpointPosition = new Vector3(
            originalPosition.x,
            playerHeight + waterOffset,
            originalPosition.z
        );

        waterOriginPosition = new Vector3(originalPosition.x, 0f, originalPosition.z);

        waterTargetPosition = new Vector3(originalPosition.x, waterOffset, originalPosition.z);

        isActivated = true;
    }

    private IEnumerator MoveToTargetSmoothly(Vector3 targetPosition)
    {
        while (movableObject.transform.position != targetPosition)
        {
            movableObject.transform.position = Vector3.MoveTowards(
                movableObject.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null; 
        }

        movableObject.transform.position = targetPosition;
    }

    
    private void CompleteEvent()
    {
        StartCoroutine(MoveToTargetSmoothlyAndComplete(originalPosition));
    }

    private IEnumerator MoveToTargetSmoothlyAndComplete(Vector3 targetPosition)
    {
        while (movableObject.transform.position != targetPosition)
        {
            movableObject.transform.position = Vector3.MoveTowards(
                movableObject.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        movableObject.transform.position = targetPosition;

        enableDisableScript.EnableObject(objectToEnable);
        isDisabled = true;
        this.enabled = false;
    }
}
