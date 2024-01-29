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


    private void ReleaseBed(Patient patient)
    {
        if(GetComponentInChildren<Patient>() != patient)
        {
            return;
        }

        isOccupied = false;
    }
}
