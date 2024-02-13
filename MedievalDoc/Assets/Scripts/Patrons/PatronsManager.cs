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
    GameManager gameManager;
    // only one patron can be spawned so if one is spawned set it to false
    private bool spawnable;
    private Symptom key;


    private void Start()
    {
        spawnable = true;

    }

    private void OnEnable()
    {
        if (App.Instance.GameplayCore.GameManager != null)
        {
            gameManager = App.Instance.GameplayCore.GameManager;
            gameManager.SymptomAddedToDictionary.AddListener(CheckSpawn);
        }
        else
        {
            App.Instance.GameplayCore.OnGameManagerRegistered.AddListener(AddlistenerToGameManager);
        }
    }

    private void AddlistenerToGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
        Debug.Log("Add listener gamemanager");

        gameManager.SymptomAddedToDictionary.AddListener(CheckSpawn);
        Debug.Log(gameManager);
    }

    private void CheckSpawn(Symptom symptom)
    {
        if (spawnable)
        {
            for (int i = 0; i < Patrons.Count; i++)
            {
                for (int x = 0; x < Patrons[i].requirementsToSpawn.Count; x++)
                {
                    if (Patrons[i].CheckReq(symptom, gameManager) == true)
                    {
                        Debug.Log("Requirements Met! Spawning Patron" + Patrons[i].patronName);
                        Patron.SetActive(true);
                        spawnable = false;
                    }
                    // Check for Added symptoms requirements
                    //if (Patrons[i].requirementsToSpawn[x].questAction == QuestAction.AddSymptom)
                    //{
                    //    if (Patrons[i].requirementsToSpawn[x].requiredAmmount == gameManager.ListOfAddedSymptoms[symptom] && Patrons[i].requirementsToSpawn[x].symptom == symptom)
                    //    {
                    //        Debug.Log("Requirements Met! Spawning Patron" + Patrons[i].patronName);
                    //        Patron.GetComponent<PatronCharacter>().PatronType = Patrons[i];
                    //        Patron.SetActive(true);
                    //        spawnable = false;
                    //    }
                    //}
                    //// check for removed symptoms requirements
                    //else if((Patrons[i].requirementsToSpawn[x].questAction == QuestAction.RemoveSymptom))
                    //{
                    //    if (Patrons[i].requirementsToSpawn[x].requiredAmmount == gameManager.ListOfRemovedSymptoms[symptom] && Patrons[i].requirementsToSpawn[x].symptom == symptom)
                    //    {
                    //        Debug.Log("Requirements Met! Spawning Patron"+ Patrons[i].patronName);
                    //        Patron.GetComponent<PatronCharacter>().PatronType = Patrons[i];
                    //        Patron.SetActive(true);
                    //        spawnable = false;
                    //    }
                    //}
                }
            }
        }

    }


}
