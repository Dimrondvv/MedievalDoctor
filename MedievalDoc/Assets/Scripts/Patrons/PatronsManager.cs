using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PatronsManager : MonoBehaviour
{
    [SerializeField] GameObject Patron;
    [SerializeField] GameObject questInfo;
    [SerializeField] private List<PatronScriptableObject> Patrons;
    [SerializeField] private List<Symptom> ListOfSymptoms;

    private Dictionary<Symptom, int> ListOfAddedSymptoms = new Dictionary<Symptom, int>();
    private Dictionary<Symptom, int> ListOfRemovedSymptoms = new Dictionary<Symptom, int>();

    // only one patron can be spawned so if one is spawned set it to false
    private bool spawnable;
    private Symptom key;


    private void Awake()
    {
        spawnable = true;
        // Add every symptom to a dictionary that counts them (Remove/Add)
        foreach(Symptom symptom in ListOfSymptoms)
        {
            ListOfAddedSymptoms.Add(symptom, 0);
            ListOfRemovedSymptoms.Add(symptom, 0);
        }
    }

    private void OnEnable()
    {
        // Add listeners to remove/add symptom
        Patient.OnAddSymptom.AddListener(AddedSymptom);
        Patient.OnRemoveSymptom.AddListener(RemovedSymptom);
    }

    private void AddedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        ListOfAddedSymptoms[symptom] += 1;
        CheckSpawn(symptom);
    }

    private void RemovedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        ListOfRemovedSymptoms[symptom] += 1;
        CheckSpawn(symptom);
    }

    private void CheckSpawn(Symptom symptom)
    {
        if (spawnable)
        {
            for (int i = 0; i < Patrons.Count; i++)
            {
                for (int x = 0; x < Patrons[i].requirementsToSpawn.Count; x++)
                {
                    // Check for Added symptoms requirements
                    if (Patrons[i].requirementsToSpawn[x].questAction == QuestAction.AddSymptom)
                    {
                        if (Patrons[i].requirementsToSpawn[x].requiredAmmount == ListOfAddedSymptoms[symptom] && Patrons[i].requirementsToSpawn[x].symptom == symptom)
                        {
                            Debug.Log("Requirements Met! Spawning Patron" + Patrons[i].patronName);
                            Patron.GetComponent<PatronCharacter>().PatronType = Patrons[i];
                            Patron.SetActive(true);
                            spawnable = false;
                        }
                    }
                    // check for removed symptoms requirements
                    else if((Patrons[i].requirementsToSpawn[x].questAction == QuestAction.RemoveSymptom))
                    {
                        if (Patrons[i].requirementsToSpawn[x].requiredAmmount == ListOfRemovedSymptoms[symptom] && Patrons[i].requirementsToSpawn[x].symptom == symptom)
                        {
                            Debug.Log("Requirements Met! Spawning Patron"+ Patrons[i].patronName);
                            Patron.GetComponent<PatronCharacter>().PatronType = Patrons[i];
                            Patron.SetActive(true);
                            spawnable = false;
                        }
                    }
                }
            }
        }

    }


}
