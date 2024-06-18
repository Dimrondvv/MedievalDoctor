using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FillSicknessList : MonoBehaviour
{
    [SerializeField] List<SicknessScriptableObject> sicknessList;
    [SerializeField] SpawnPatientDebug patientDebug;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform content;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var sick in sicknessList)
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
