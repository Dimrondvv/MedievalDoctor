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
        Debug.Log(itemPrefab.GetComponent<Tool>().ToolName);
        nameDisplay.text = itemPrefab.GetComponent<Tool>().ToolName;
    }
    private void Update()
    {
        if (SharedOverlapBox.HighestCollider == GetComponent<Collider>())
        {

            nameDisplay.enabled = true;
        }
        else
        {
            nameDisplay.enabled = false;
        }
    }
}
