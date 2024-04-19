using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookDataHandler : MonoBehaviour
{
    [SerializeField] string notebookDataFileName;
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
    private void SaveNotebookData()
    {
        saveManager.SaveGameData<NotebookData>(data, notebookDataFileName + ".json");
    }


}
