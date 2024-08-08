using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string itemName;
    public string ItemName { get { return itemName; } }

    private void Start()
    {
        
    }
}
