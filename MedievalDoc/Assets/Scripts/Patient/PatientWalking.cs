using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class PatientWalking : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    private NavMeshAgent meshAgent;
    private Vector3 endPointVector;
    private bool move;
    private GameObject patient;

    public void StartWalking(GameObject patientObject)
    {
        patient = patientObject;
        meshAgent = patient.GetComponent<NavMeshAgent>();
        endPointVector = new Vector3(endPoint.position.x, endPoint.position.y, endPoint.position.z);
        Debug.Log(meshAgent.acceleration);
        move = true;
    }

    private void Update() // Przerobic to na korutyne + dodaæ obrócenie siê na koniec + pieczenie na nowo sceny co event ze zmian¹ sceny
    {
        if (move)
        {
            Debug.Log("hklhjkh");
            meshAgent.SetDestination(endPointVector);

            if (!meshAgent.hasPath)
            {
                Debug.Log("hljkhljkh");
                move = false;
            }
        }
    }

}
