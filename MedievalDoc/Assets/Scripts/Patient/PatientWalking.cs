using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;


public class PatientWalking : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private float delay;
    [SerializeField] private float rotationDelay;
    [SerializeField] private Transform lookTowards;
    private NavMeshAgent meshAgent;
    private Vector3 endPointVector;
    private bool move;
    private bool rotated;
    private GameObject patient;
    private float tempX=9999;

    public void StartWalking(GameObject patientObject)
    {
        patient = patientObject;
        meshAgent = patient.GetComponent<NavMeshAgent>();
        endPointVector = new Vector3(endPoint.position.x, endPoint.position.y, endPoint.position.z);
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
        Debug.Log("stop walking");
        StartCoroutine(RotatePatient());
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
        patient.GetComponent<PatientPickup>().ActivatePickup();
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
