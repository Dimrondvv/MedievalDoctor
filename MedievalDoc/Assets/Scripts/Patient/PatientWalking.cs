using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;


public class PatientWalking : MonoBehaviour
{
    [SerializeField] public Transform endPoint;
    [SerializeField] private float delay;
    [SerializeField] private float rotationDelay;
    [SerializeField] private Transform lookTowards;
    private NavMeshAgent meshAgent;
    private Vector3 endPointVector;
    private bool move;
    private bool rotated;
    private bool exit;
    private GameObject patient;
    private float tempX=9999;
    private CallPatient callPatient;

    private void Start()
    {
        PatientManager.OnPatientReleased.AddListener(HandleRelease);
        callPatient = GetComponent<CallPatient>();
    }

    public void StartWalking(GameObject patientObject, Transform destination)
    {
        patient = patientObject;
        meshAgent = patient.GetComponent<NavMeshAgent>();
        endPointVector = new Vector3(destination.position.x, destination.position.y, destination.position.z);
        move = true;
        rotated = false;
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
        if (exit == true)
        {
            Destroy(patient);
        }
        else
        {
            StartCoroutine(RotatePatient());
        }
    }

    IEnumerator RotatePatient()
    {
        while (!rotated)
        {
            var rot = Rotation(patient.transform, lookTowards);
            float singleStep = 2f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(patient.transform.forward, rot, singleStep, 0.0f);
            if(newDirection.x != tempX)
            {
                tempX = newDirection.x;
            }
            else
            {
                ActivatePatient();
                rotated = true;
            }
            patient.transform.rotation = Quaternion.LookRotation(newDirection);
            yield return new WaitForSeconds(rotationDelay);
        }
    }

    private void ActivatePatient()
    {
        patient.GetComponent<PatientStory>().StoryTime();
        patient.GetComponent<PatientAngry>().StartAnger();
        patient.GetComponent<Patient>().Immune = false;
        patient.GetComponent<NavMeshAgent>().enabled = false;
        //patient.GetComponent<PatientPickup>().ActivatePickup();
    }

    private void HandleRelease(Patient patient)
    {
        patient.GetComponent<NavMeshAgent>().enabled = true;
        exit = true;
        StartWalking(patient.gameObject, callPatient.tempSpawnPosition);
    }


    private Vector3 Rotation(Transform rotatingObject, Transform facingObject)
    {
        var obj1 = rotatingObject.position;
        obj1.y = 0f;
        var obj2 = facingObject.position;
        obj2.y = 0f;
        var direction = obj2 - obj1;
        return direction;
    }

    //pieczenie na nowo sceny co event ze zmian¹ sceny


}
