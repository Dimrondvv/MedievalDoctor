using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgressionBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    private PlayerInputActions playerInputActions;
    private float progress;
    private bool isPerformingAction;

    public void progressBar()
    {
        progress += 1.1f;
        barImage.fillAmount = (float)progress / (float)100;
    }
    void Start()
    {
        progress = 0f;
        barImage.fillAmount = 0f;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.InteractAnimation.started += OnInteractionStart;
        playerInputActions.Player.InteractAnimation.canceled += OnInteractionExit;
    }
    void FixedUpdate()
    {
        transform.eulerAngles = Vector3.zero;
        if (isPerformingAction)
        {
            
            if (barImage.fillAmount != 1)
            {
                gameObject.GetComponent<Canvas>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Canvas>().enabled = false;
            }
            
            progressBar();
        } else
        {
            gameObject.GetComponent<Canvas>().enabled = false;
            progress = 0;
            progressBar();
        }
    }

    private void OnInteractionStart(UnityEngine.InputSystem.InputAction.CallbackContext callback) {
        isPerformingAction = true;
    }

    private void OnInteractionExit(UnityEngine.InputSystem.InputAction.CallbackContext callback) {
        isPerformingAction = false;
    }
}
