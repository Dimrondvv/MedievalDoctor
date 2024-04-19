using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookDataHandler : MonoBehaviour
{
    [SerializeField] string notebookDataFileName;
    [SerializeField] int interactionsRequired; //Field to see how many interactions are required with something to add it to discovered
    NotebookData data;
    SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        saveManager = App.Instance.GameplayCore.SaveManager;
        if(saveManager == null) //Wait for save manager to register if its null
        {
            App.Instance.GameplayCore.OnSaveManagerRegistered.AddListener(WaitForManagerRegister);
        }
        else
        {
            LoadNotebookData();
        }
        AddEventListeners();
        
    }
    private void OnDestroy()
    {
        SaveNotebookData();
    }
    private void WaitForManagerRegister(SaveManager mng)
    {
        saveManager = mng;
        LoadNotebookData();
    }
    private void LoadNotebookData()
    {
        data = saveManager.LoadGameData<NotebookData>(notebookDataFileName + ".json");
        if(!data.wasDataInitialized)
        {
            data = new NotebookData();
            data.InitializeData();
            saveManager.SaveGameData<NotebookData>(data, notebookDataFileName + ".json");

        }
    }

    private void AddEventListeners()
    {
        Patient.OnCureDisease.AddListener(AddCuredSicknessToDict);
    }

    private void SaveNotebookData()
    {
        saveManager.SaveGameData<NotebookData>(data, notebookDataFileName + ".json");
    }

    private void AddCuredSicknessToDict(Patient curedPatient)
    {
        string sicknessName = curedPatient.Sickness.sicknessName;
        if (data.discoveredSicknesses.ContainsKey(sicknessName))
            return;

        if (data.sicknessesDiscoveredDuringRun.ContainsKey(sicknessName))
        {
            if (data.sicknessesDiscoveredDuringRun[sicknessName] >= interactionsRequired)
            {
                data.discoveredSicknesses.Add(sicknessName, "abcdTODO");
                data.sicknessesDiscoveredDuringRun.Remove(sicknessName);
            }
            else
            {
                data.sicknessesDiscoveredDuringRun[sicknessName]++;
            }
        }
        else
        {
            data.sicknessesDiscoveredDuringRun.Add(sicknessName, 1);
        }
    }
}
