using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Data;
public class SpawnPatientDebug : MonoBehaviour
{
    private Sickness selectedSickness;

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


    public void SelectSickness(Sickness sickness)
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
    public void SetSelectedSicknessWindow(Sickness sickness)
    {
        sicknessName.text = sickness.sicknessName;
        sicknessDescription.text = sickness.sicknessDescription;
        foreach(var smpt in sickness.symptomsContainerList)
        {
            var obj = Instantiate(textPrefab, scrollViewContent);
            obj.GetComponent<TextMeshProUGUI>().text = HelperFunctions.SymptomLookup(smpt).symptomName;
        }

    }
    public void SpawnPatient()
    {
        //PatientManager.OnPatientSpawnFinalized.AddListener(SetPatientSickness);
        var patient = Instantiate(patientPrefab, spawnPosition, Quaternion.identity);
        SetPatientSickness(patient.GetComponent<Patient>());
    }

    private void SetPatientSickness(Patient patient)
    {
        InitializePatientStats initializer = new InitializePatientStats();
        initializer.SetPatientStats(patient.GetComponent<Patient>(), selectedSickness);
        PatientManager.OnPatientSpawnFinalized.RemoveListener(SetPatientSickness);
    }
}
