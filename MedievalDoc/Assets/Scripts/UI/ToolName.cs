using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Data;

public class ToolName : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI nameDisplay;

    internal void SetDisplayText(Data.Tool toolData)
    {
        nameDisplay.text = toolData.toolName;
    }

    private void Update()
    {
        if (Interactor.InteractableCollider == GetComponent<Collider>())
        {

            nameDisplay.enabled = true;
        }
        else
        {
            nameDisplay.enabled = false;
        }
    }
}
