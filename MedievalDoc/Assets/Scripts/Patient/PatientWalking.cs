using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class PatientWalking : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private float delay;
    private NavMeshAgent meshAgent;
    private Vector3 endPointVector;
    private bool move;
    private GameObject patient;

    public void StartWalking(GameObject patientObject)
    {
        patient = patientObject;
        meshAgent = patient.GetComponent<NavMeshAgent>();
        endPointVector = new Vector3(endPoint.position.x, endPoint.position.y, endPoint.position.z);
        move = true;
        //meshAgent.destination = endPointVector;
       // meshAgent.SetDestination(endPointVector);
        StartCoroutine(Walking());
    }

    IEnumerator Walking()
    {
        while (move)
        {
            meshAgent.SetDestination(endPointVector);
            if (!meshAgent.pathPending)
            {
                if (meshAgent.remainingDistance <= meshAgent.stoppingDistance)
                {
                    if (!meshAgent.hasPath || meshAgent.velocity.sqrMagnitude == 0f)
                    {
                        move = false;
                        StopWalking();
                    }
                }
            }
            yield return new WaitForSeconds(delay);
        }
    }

    private void StopWalking()
    {
        StopCoroutine(Walking());
        // rotate pacient
    }

    //dodaæ obrócenie siê na koniec + pieczenie na nowo sceny co event ze zmian¹ sceny


}
