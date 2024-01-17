using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLayDownPoint : MonoBehaviour
{
    private bool isOccupied;
    public bool checkIfOccupied {
        get { return isOccupied; }
        set { isOccupied = value; }
    }
}
