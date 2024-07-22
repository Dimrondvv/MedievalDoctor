using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Data;
public class FillSicknessList : MonoBehaviour
{
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
            foreach (var sick in ImportJsonData.sicknessConfig)
            {
                AddSicknessButton(sick);
            }
        }
    }

    private void SetSicknessList(PatientManager manager)
    {
        foreach (var sick in ImportJsonData.sicknessConfig)
        {
            AddSicknessButton(sick);
        }
    }

    private void AddSicknessButton(Sickness sickness)
    {
        var button = Instantiate(buttonPrefab, content);
        button.GetComponentInChildren<TextMeshProUGUI>().text = sickness.sicknessName;
        button.GetComponent<Button>().onClick.AddListener(delegate { patientDebug.SelectSickness(sickness); });
    }
}
