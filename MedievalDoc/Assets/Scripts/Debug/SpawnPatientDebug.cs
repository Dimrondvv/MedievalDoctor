using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnPatientDebug : MonoBehaviour
{
    private SicknessScriptableObject selectedSickness;

    [SerializeField] TextMeshProUGUI sicknessName;
    [SerializeField] TextMeshProUGUI sicknessDescription;
    [SerializeField] Transform scrollViewContent;
    [SerializeField] GameObject textPrefab;
    [SerializeField] GameObject patientPrefab;
    [SerializeField] Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SelectSickness(SicknessScriptableObject sickness)
    {
        selectedSickness = sickness;
        ClearSelectedSicknessWindow();
        SetSelectedSicknessWindow(sickness);
    }
    private void ClearSelectedSicknessWindow()
    {
        sicknessName.text = "";
        sicknessDescription.text = "";
        foreach(Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }
    }
    public void SetSelectedSicknessWindow(SicknessScriptableObject sickness)
    {
        sicknessName.text = sickness.sicknessName;
        sicknessDescription.text = sickness.sicknessDescritpion;
        foreach(var smpt in sickness.symptomList)
        {
            var obj = Instantiate(textPrefab, scrollViewContent);
            obj.GetComponent<TextMeshProUGUI>().text = smpt.GetSymptomName();
        }

    }
    public void SpawnPatient()
    {
        PatientManager.OnPatientSpawnFinalized.AddListener(SetPatientSickness);
        var patient = Instantiate(patientPrefab, spawnPosition, Quaternion.identity);
    }

    private void SetPatientSickness(Patient patient)
    {
        InitializePatientStats initializer = new InitializePatientStats();
        initializer.SetPatientStats(patient.GetComponent<Patient>(), selectedSickness);
        PatientManager.OnPatientSpawnFinalized.RemoveListener(SetPatientSickness);
    }
}
