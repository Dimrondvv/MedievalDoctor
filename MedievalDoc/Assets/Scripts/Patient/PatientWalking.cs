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
    [SerializeField] public Transform startPoint;
    [SerializeField] private float delay;
    [SerializeField] private float rotationDelay;
    [SerializeField] private Transform lookTowards;
    private NavMeshAgent meshAgent;
    private Vector3 endPointVector;
    private bool move;
    private bool rotated;
    public bool exit;
    private GameObject patient;
    private float tempX=9999;

    private void Start()
    {
        //PatientManager.OnPatientReleased.AddListener(HandleRelease);
        //callPatient = 
        PatientManager.ReleasePatient.AddListener(HandleRelease);
        PatientManager.RageQuitPatient.AddListener(HandleRelease);
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
        if (patient.GetComponent<Patient>().IsQuitting == true)
        {
            PatientManager.OnPatientReleased.Invoke(patient.GetComponent<Patient>());
            Destroy(patient);
            return;
        }
        else
        {
            StartCoroutine(RotatePatient());
        }
    }

    IEnumerator RotatePatient()
    {
        while (!rotated && !exit)
        {
            var rot = Rotation(patient.transform, lookTowards);
            float singleStep = 2f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(patient.transform.forward, rot, singleStep, 0.0f);
            if (newDirection.x != tempX)
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
        patient.GetComponent<Patient>().Immune = false;
        patient.GetComponent<Patient>().Tiltproof = false;
        patient.GetComponent<PatientStory>().StoryTime();
        patient.GetComponent<PatientAngry>().StartAnger();
        patient.GetComponent<NavMeshAgent>().enabled = false;
        //patient.GetComponent<PatientPickup>().ActivatePickup();
    }

    public void HandleRelease()
    {
        exit = true;
        patient = App.Instance.GameplayCore.PatientManager.patients[0].gameObject;
        patient.GetComponent<NavMeshAgent>().enabled = true;
        patient.GetComponent<Patient>().IsQuitting = true;
        patient.GetComponent<Patient>().Immune = true;
        patient.GetComponent<Patient>().Tiltproof = true;
        StartWalking(patient.gameObject, startPoint);
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
