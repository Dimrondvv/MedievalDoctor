using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PatientNotebook : MonoBehaviour
{
    [SerializeField] private List<Button> bookmarks;
    private PlayerInputActions playerInputActions;
    private int currentBookmarkIndex = 0;
    

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Bookmarks.performed += ChangeBookMark;

    }

    

    public void ChangeBookMark(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        currentBookmarkIndex += (int)playerInputActions.Player.Bookmarks.ReadValue<float>();
        if (currentBookmarkIndex >= bookmarks.Count)
        {
            currentBookmarkIndex = 0;
        }
        else if (currentBookmarkIndex < 0)
        {
            currentBookmarkIndex = bookmarks.Count - 1;
        }
        bookmarks[currentBookmarkIndex].onClick.Invoke();
    }
}
