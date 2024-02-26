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
        gameManager.SymptomAddedToDictionary.AddListener(CheckSpawn);
    }

    private void CheckSpawn(Symptom symptom)
    {
        if (spawnable)
        {
            for (int i = 0; i < Patrons.Count; i++)
            {
                    if (Patrons[i].CheckReq(gameManager) == true)
                    {
                        Debug.Log("Requirements Met! Spawning Patron" + Patrons[i].patronName);
                        Patron.SetActive(true);
                        Patron.GetComponent<MeshFilter>().mesh = Patrons[i].prefab;
                        Patron.GetComponent<PatronCharacter>().PatronType = Patrons[i];
                        spawnable = false;
                    }
                
            }
        }
    }


}
