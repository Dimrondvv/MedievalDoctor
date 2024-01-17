using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientLayDownPoint : MonoBehaviour
{
    private bool isOccupied;
    public bool IfOccupied {
        get { return isOccupied; }
        set { isOccupied = value; }
    }
}
