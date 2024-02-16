using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientIconSymptom : MonoBehaviour
{
    [SerializeField] private List<Image> icons;
    [SerializeField] private Sprite plus;
    [SerializeField] private Sprite minus;
    private List<Symptom> symtoms;
    private bool state; // true = symtom added, false = symtom removed
    [SerializeField] private float timeToFade;
    private bool fadeout;
    private Vector3 stateDestination;
    private Vector3 symptomDestination;

    private Vector3 stateHomePosition;
    private Vector3 symptomHomePosition;

    // Start is called before the first frame update
    void Start()
    {
        symtoms = new List<Symptom>();
        InvokeRepeating("Display", 0, 1);
    }

    private void OnEnable()
    {
        Patient.OnAddSymptom.AddListener(AddedDisplay);
        Patient.OnRemoveSymptom.AddListener(RemovedDisplay);
    }

    private void AddedDisplay(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != GetComponent<Patient>())
        {
            return;
        }
        symtoms.Add(symptom);
        state = true;
    }

    private void RemovedDisplay(Symptom symptom, Patient patient, Tool tool)
    {
        if(patient != GetComponent<Patient>())
        {
            return;
        }
        symtoms.Add(symptom);
        state = false;
    }

    private void Display()
    {
        if (symtoms.Count == 0)
        {
            icons[0].enabled = false;
            icons[1].enabled = false;
            return;
        }

        icons[0].enabled = true;
        icons[1].enabled = true;

        if (state)
        {
            icons[0].sprite = plus;
        }
        else
        {
            icons[0].sprite = minus;
        }

        icons[0].color = Color.white;
        icons[1].color = Color.white;
        icons[1].sprite = symtoms[0].symptomIcon;

        icons[0].GetComponent<CanvasGroup>().alpha = 1;
        icons[1].GetComponent<CanvasGroup>().alpha = 1;

        symptomHomePosition = icons[4].transform.localPosition;
        stateHomePosition = icons[5].transform.localPosition;

        stateDestination = icons[2].transform.localPosition;
        symptomDestination = icons[3].transform.localPosition;

        icons[0].transform.localPosition = stateHomePosition;
        icons[1].transform.localPosition = symptomHomePosition;


        fadeout = true;
        symtoms.RemoveAt(0);
    }

    private void Update()
    {
        if (fadeout)
        {
            if (icons[0].GetComponent<CanvasGroup>().alpha >= 0)
            {
                icons[0].GetComponent<CanvasGroup>().alpha -= timeToFade* Time.deltaTime;
                icons[1].GetComponent<CanvasGroup>().alpha -= timeToFade* Time.deltaTime;

                icons[0].transform.localPosition = Vector3.MoveTowards(icons[0].transform.localPosition, stateDestination, 0.5f * Time.deltaTime);
                icons[1].transform.localPosition = Vector3.MoveTowards(icons[1].transform.localPosition, symptomDestination, 0.5f * Time.deltaTime);

                if (icons[0].GetComponent<CanvasGroup>().alpha == 0)
                {
                    fadeout = false;
                }
            }
        }
    }
}
