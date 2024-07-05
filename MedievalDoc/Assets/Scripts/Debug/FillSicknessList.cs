using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FillSicknessList : MonoBehaviour
{
    List<SicknessScriptableObject> sicknessList;
    [SerializeField] SpawnPatientDebug patientDebug;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform content;
    // Start is called before the first frame update
    void Start()
    {
        if(App.Instance.GameplayCore.PatientManager == null)
            App.Instance.GameplayCore.OnPatientManagerRegistered.AddListener(SetSicknessList);
        else
        {
            sicknessList = App.Instance.GameplayCore.PatientManager.sicknessPool;
            foreach (var sick in sicknessList)
            {
                AddSicknessButton(sick);
            }
        }
    }

    private void SetSicknessList(PatientManager manager)
    {
        sicknessList = manager.sicknessPool;
        foreach (var sick in sicknessList)
        {
            AddSicknessButton(sick);
        }
    }

    private void AddSicknessButton(SicknessScriptableObject sickness)
    {
        var button = Instantiate(buttonPrefab, content);
        button.GetComponentInChildren<TextMeshProUGUI>().text = sickness.sicknessName;
        button.GetComponent<Button>().onClick.AddListener(delegate { patientDebug.SelectSickness(sickness); });
    }
}
