using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField] private bool isOccupied;
    public bool IsOccupied {
        get { return isOccupied; }
        set { isOccupied = value; }
    }

    private void Start()
    {
        Patient.OnReleasePatient.AddListener(ReleaseChair);
    }

    private void ReleaseChair(Patient patient)
    {
        if (!GetComponentInChildren<Patient>() && isOccupied == true)
        {
            isOccupied = false;
        }
    }

}
