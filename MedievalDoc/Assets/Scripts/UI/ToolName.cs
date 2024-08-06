using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolName : MonoBehaviour
{

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private TextMeshProUGUI nameDisplay;

    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        nameDisplay.text = itemPrefab.GetComponent<InteractionTool>().toolData.toolName;
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
